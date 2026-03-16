using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace pract12_TRPO.Services
{
    public class RolesService
    {
        private readonly AppDbContext _db = BaseDbService.Instance.Context;
        public static ObservableCollection<Role> Roles { get; set; } = new();
        public void GetAll()
        {
            var roles = _db.Roles.ToList();
            Roles.Clear();
            foreach (var role in roles)
                Roles.Add(role);
        }
        public int Commit() => _db.SaveChanges();
        public RolesService()
        {
            GetAll();
        }
        public void Add(Role role)
        {
            var _role = new Role
            {
                Title = role.Title,
            };
            _db.Add(_role);
            Commit();
            Roles.Add(_role);
        }
        public void Remove(Role role)
        {
            _db.Remove(role);
            if (Commit() > 0)
                if (Roles.Contains(role))
                    Roles.Remove(role);
        }

        public void LoadRelation(Role role, string relation)
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

        public ObservableCollection<Student> GetStudentsByRoleId(int roleId)
        {
            var students = _db.Students
                .Include(s => s.UserProfile)
                .Include(s => s.Role)
                .Where(s => s.RoleId == roleId)
                .ToList();

            return new ObservableCollection<Student>(students);
        }
    }
}
