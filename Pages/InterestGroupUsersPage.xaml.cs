using pract12_TRPO.Services;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace pract12_TRPO.Pages
{
    public partial class InterestGroupUsersPage : Page
    {
        private readonly AppDbContext _db = BaseDbService.Instance.Context;
        private readonly StudentsService _studentService = new();
        private readonly InterestGroupService _groupService = new();
        private readonly Student? _preselectedStudent;

        public InterestGroupUsersPage(Student? preselectedStudent = null)
        {
            InitializeComponent();
            _preselectedStudent = preselectedStudent;
            DataContext = this;
            Loaded += InterestGroupUsersPage_Loaded;
        }

        private void InterestGroupUsersPage_Loaded(object sender, RoutedEventArgs e)
        {
            _studentService.GetAll();
            _groupService.GetAll();

            if (_preselectedStudent != null)
            {
                var student = Students.FirstOrDefault(s => s.Id == _preselectedStudent.Id);
                if (student != null)
                {
                    SelectedStudent = student;
                    StudentsList.ScrollIntoView(student);
                }
            }
        }

        public System.Collections.ObjectModel.ObservableCollection<Student> Students => _studentService.Students;

        private Student? _selectedStudent;
        public Student? SelectedStudent
        {
            get => _selectedStudent;
            set => _selectedStudent = value;
        }

        public System.Collections.ObjectModel.ObservableCollection<InterestGroup> Groups => _groupService.InterestGroups;

        private InterestGroup? _currentGroup;
        public InterestGroup? CurrentGroup
        {
            get => _currentGroup;
            set => _currentGroup = value;
        }

        private DateOnly? _startDate;
        public DateOnly? StartDate
        {
            get => _startDate;
            set => _startDate = value;
        }

        private string _selectedRole = "Обычный участник";
        public string SelectedRole
        {
            get => _selectedRole;
            set => _selectedRole = value;
        }

        private void back(object sender, RoutedEventArgs e)
        {
            if (NavigationService?.CanGoBack == true)
                NavigationService.GoBack();
        }

        private void enter(object sender, RoutedEventArgs e)
        {
            var student = SelectedStudent ?? StudentsList.SelectedItem as Student;
            var group = CurrentGroup ?? (FindName("GroupsList") as ListBox)?.SelectedItem as InterestGroup;

            var date = StartDate ?? (StartDatePicker.SelectedDate.HasValue
                ? DateOnly.FromDateTime(StartDatePicker.SelectedDate.Value)
                : (DateOnly?)null);

            var roleItem = RoleComboBox.SelectedItem as ComboBoxItem;
            var isModerator = roleItem?.Content?.ToString() == "Модератор";

            if (student == null)
            {
                MessageBox.Show("Выберите студента!");
                return;
            }
            if (group == null)
            {
                MessageBox.Show("Выберите группу!");
                return;
            }
            if (!date.HasValue)
            {
                MessageBox.Show("Выберите дату вступления!");
                return;
            }

            if (_db.UserInterestGroups.Any(x =>
                x.UserId == student.Id && x.InterestGroupId == group.Id))
            {
                MessageBox.Show("Студент уже в этой группе");
                return;
            }

            var record = new UserInterestGroup
            {
                UserId = student.Id,
                InterestGroupId = group.Id,
                JoinedAt = date.Value,
                IsModerator = isModerator
            };

            _db.UserInterestGroups.Add(record);
            _db.SaveChanges();

            MessageBox.Show("Готово!");
            if (NavigationService?.CanGoBack == true)
                NavigationService.GoBack();
        }
    }
}