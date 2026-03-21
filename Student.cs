using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace pract12_TRPO
{
    public class Student : ObservableObject
    {
        private int _id;
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }
        private DateTime? _createdAt;
        public DateTime? CreatedAt
        {
            get => _createdAt;
            set => SetProperty(ref _createdAt, value);
        }

        private UserProfile? _userProfile;
        public UserProfile? UserProfile
        {
            get => _userProfile;
            set => SetProperty(ref _userProfile, value);
        }

        private int _roleId;
        public int RoleId
        {
            get => _roleId;
            set => SetProperty(ref _roleId, value);
        }
        private Role? _role;
        public Role? Role
        {
            get => _role;
            set => SetProperty(ref _role, value);
        }
        private string? _login;
        public string? Login
        {
            get => _login;
            set => SetProperty(ref _login, value);
        }

        private string? _Name;
        public string? Name
        {
            get => _Name;
            set => SetProperty(ref _Name, value);
        }

        private string? _email;
        public string? Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private string? _password;
        public string? Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }
        private ObservableCollection<UserInterestGroup>? _userInterestGroups;
        public ObservableCollection<UserInterestGroup>? UserInterestGroups
        {
            get => _userInterestGroups;
            set => SetProperty(ref _userInterestGroups, value);
        }

        public string GroupsPreview
        {
            get
            {
                var db = BaseDbService.Instance.Context;
                var groups = db.UserInterestGroups
                    .Where(uig => uig.UserId == this.Id)
                    .Select(uig => uig.InterestGroup.Title)
                    .Take(3)
                    .ToList();

                return groups.Any()
                    ? string.Join(", ", groups) + (groups.Count < db.UserInterestGroups.Count(uig => uig.UserId == this.Id) ? "..." : "")
                    : "-";
            }
        }
    }
}
