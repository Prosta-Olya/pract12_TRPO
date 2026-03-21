using pract12_TRPO.Services;
using System;
using System.Collections.ObjectModel;
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

            Students = new ObservableCollection<Student>(_studentService.Students);
            AllGroups = new ObservableCollection<InterestGroup>(_groupService.InterestGroups);

            if (_preselectedStudent != null)
            {
                var student = Students.FirstOrDefault(s => s.Id == _preselectedStudent.Id);
                if (student != null)
                {
                    SelectedStudent = student;
                    UpdateAvailableGroups();

                    if (FindName("StudentsList") is ListBox studentsListBox)
                    {
                        studentsListBox.SelectedItem = student;
                        studentsListBox.ScrollIntoView(student);
                    }
                }
            }
            else
            {
                UpdateAvailableGroups();
            }
        }

        public ObservableCollection<Student> Students { get; private set; } = new();

        private Student? _selectedStudent;
        public Student? SelectedStudent
        {
            get => _selectedStudent;
            set
            {
                if (_selectedStudent != value)
                {
                    _selectedStudent = value;
                    UpdateAvailableGroups();

                    if (FindName("StudentInfoBlock") is TextBlock infoBlock && value != null)
                    {
                        infoBlock.Text = $"{value.Name}\n{value.Email}\n{value.Login}";
                    }
                    else if (FindName("StudentInfoBlock") is TextBlock emptyBlock)
                    {
                        emptyBlock.Text = "";
                    }
                }
            }
        }

        public ObservableCollection<InterestGroup> AllGroups { get; private set; } = new();

        private ObservableCollection<InterestGroup> _availableGroups = new();
        public ObservableCollection<InterestGroup> AvailableGroups
        {
            get => _availableGroups;
            set
            {
                _availableGroups = value;
                OnPropertyChanged(nameof(Cources));
            }
        }

        public ObservableCollection<InterestGroup> Cources => AvailableGroups;

        private InterestGroup? _currentGroup;
        public InterestGroup? CurrentGroup
        {
            get => _currentGroup;
            set => _currentGroup = value;
        }

        public InterestGroup? current
        {
            get => CurrentGroup;
            set => CurrentGroup = value;
        }

        private void UpdateAvailableGroups()
        {
            if (SelectedStudent == null)
            {
                AvailableGroups = new ObservableCollection<InterestGroup>(AllGroups);
                return;
            }

            var existingGroupIds = _db.UserInterestGroups
                .Where(uig => uig.UserId == SelectedStudent.Id)
                .Select(uig => uig.InterestGroupId)
                .ToList();

            var available = AllGroups
                .Where(g => !existingGroupIds.Contains(g.Id))
                .ToList();

            AvailableGroups = new ObservableCollection<InterestGroup>(available);

            if (FindName("CourcesList") is ListView groupsList)
            {
                groupsList.ItemsSource = null;
                groupsList.ItemsSource = AvailableGroups;
            }
        }

        private void OnPropertyChanged(string propertyName)
        {

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
            var student = SelectedStudent;
            if (student == null && FindName("StudentsList") is ListView studentsList)
                student = studentsList.SelectedItem as Student;

            var group = CurrentGroup;
            if (group == null && FindName("CourcesList") is ListView groupsList)
                group = groupsList.SelectedItem as InterestGroup;

            var date = StartDate;
            if (!date.HasValue && FindName("StartDatePicker") is DatePicker datePicker && datePicker.SelectedDate.HasValue)
                date = DateOnly.FromDateTime(datePicker.SelectedDate.Value);

            var role = SelectedRole;
            if (string.IsNullOrEmpty(role) && FindName("RoleComboBox") is ComboBox roleBox && roleBox.SelectedItem is ComboBoxItem roleItem)
                role = roleItem.Content?.ToString();

            if (student == null)
            {
                MessageBox.Show("Выберите студента!", "Ошибка", MessageBoxButton.OK);
                return;
            }
            if (group == null)
            {
                MessageBox.Show("Выберите группу!", "Ошибка", MessageBoxButton.OK);
                return;
            }
            if (!date.HasValue)
            {
                MessageBox.Show("Выберите дату вступления!", "Ошибка", MessageBoxButton.OK);
                return;
            }

            if (_db.UserInterestGroups.Any(x =>
                x.UserId == student.Id && x.InterestGroupId == group.Id))
            {
                MessageBox.Show("Студент уже состоит в этой группе!", "Внимание",
                    MessageBoxButton.OK);
                return;
            }

            var record = new UserInterestGroup
            {
                UserId = student.Id,
                InterestGroupId = group.Id,
                JoinedAt = date.Value,
                IsModerator = role == "Модератор"
            };

            _db.UserInterestGroups.Add(record);
            _db.SaveChanges();

            MessageBox.Show($"Студент \"{student.Name}\" добавлен в группу \"{group.Title}\"!",
                "Добавлен", MessageBoxButton.OK);

            UpdateAvailableGroups();

            if (NavigationService?.CanGoBack == true)
                NavigationService.GoBack();
        }
    }
}