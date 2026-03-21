using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Data.SqlTypes;
using System.Windows;

namespace pract12_TRPO.Services
{
    public class InterestGroupService
    {
        private readonly AppDbContext _db = BaseDbService.Instance.Context;
        public ObservableCollection<InterestGroup> InterestGroups { get; set; } = new();

        public int Commit() => _db.SaveChanges();

        public void Add(InterestGroup interestGroup)
        {
            if (_db.InterestGroups.Any(g => g.Title == interestGroup.Title))
            {
                MessageBox.Show("Группа с таким названием уже существует");
                return;
            }   

            _db.InterestGroups.Add(interestGroup);
            Commit();
            InterestGroups.Add(interestGroup);
        }

        public void Update(InterestGroup interestGroup)
        {
            var existing = _db.InterestGroups.Find(interestGroup.Id);
            if (existing == null) return;

            if (_db.InterestGroups.Any(g => g.Title == interestGroup.Title && g.Id != interestGroup.Id))
                throw new InvalidOperationException("Группа с таким названием уже существует");

            existing.Title = interestGroup.Title;
            existing.Description = interestGroup.Description;
            Commit();
        }

        public void GetAll()
        {
            try
            {
                var interestGroups = _db.InterestGroups
                    .Include(g => g.UserInterestGroups)
                    .ThenInclude(uig => uig.Student)
                    .ToList();

                InterestGroups.Clear();
                foreach (var group in interestGroups)
                {
                    InterestGroups.Add(group);
                }
            }
            catch (SqlNullValueException ex)
            {
                var fieldName = ex.Data.Contains("ColumnName") ? ex.Data["ColumnName"] : "Неизвестно";
                MessageBox.Show($"Ошибка NULL в поле: {fieldName}\n\n{ex.Message}");
                throw;
            }
        }

        public InterestGroupService()
        {
            GetAll();
        }

        public void Remove(InterestGroup interestGroup)
        {
            _db.InterestGroups.Remove(interestGroup);
            if (Commit() > 0)
                if (InterestGroups.Contains(interestGroup))
                    InterestGroups.Remove(interestGroup);
        }

        public void LoadMembers(InterestGroup interestGroup)
        {
            _db.Entry(interestGroup)
                .Collection(g => g.UserInterestGroups)
                .Query()
                .Include(uig => uig.Student)
                .ThenInclude(s => s.UserProfile)
                .Load();
        }

        public void LoadRelation(InterestGroup role, string relation)
        {
            var entry = _db.Entry(role);
            var navigation = entry.Metadata.FindNavigation(relation)
            ?? throw new InvalidOperationException($"Navigation '{relation}' not found");
            if (navigation.IsCollection)
            {
                entry.Collection(relation).Load();
            }
            else
            {
                entry.Reference(relation).Load();
            }
        }
    }
}