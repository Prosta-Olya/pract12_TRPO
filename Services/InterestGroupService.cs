using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
                throw new InvalidOperationException("Группа с таким названием уже существует");

            _db.InterestGroups.Add(interestGroup);
            Commit();
            InterestGroups.Add(interestGroup);
        }

        public void GetAll()
        {
            var interestGroups = _db.InterestGroups.ToList();
            InterestGroups.Clear();
            foreach (var interestGroup in interestGroups)
            {
                InterestGroups.Add(interestGroup);
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

        public void LoadRelation(InterestGroup interestGroup, string relation)
        {
            var entry = _db.Entry(interestGroup);
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
        public void LoadMembers(InterestGroup interestGroup)
        {
            _db.Entry(interestGroup)
                .Collection(g => g.UserInterestGroups)
                .Load();
        }
    }
}