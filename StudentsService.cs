using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;

namespace pract12_TRPO
{
    public class StudentsService
    {
        private readonly AppDbContext _db = BaseDbService.Instance.Context;
        public ObservableCollection<Student> Students { get; set; } = new();

        public StudentsService()
        {
            GetAll();
        }

        public string Validation(Student student)
        {
            string message = "";

            if (string.IsNullOrEmpty(student.Password) || student.Password.Length < 8)
                return "Пароль не может содержать менее 8 символов";

            bool hasUpper = false, hasLower = false, hasDigit = false, hasSymbol = false;
            foreach (char c in student.Password)
            {
                if (char.IsUpper(c)) hasUpper = true;
                else if (char.IsLower(c)) hasLower = true;
                else if (char.IsDigit(c)) hasDigit = true;
                else if (!char.IsWhiteSpace(c)) hasSymbol = true;
            }

            if (!hasUpper) return "Пароль должен содержать заглавные буквы";
            if (!hasLower) return "Пароль должен содержать строчные буквы";
            if (!hasDigit) return "Пароль должен содержать цифры";
            if (!hasSymbol) return "Пароль должен содержать специальные символы";

            if (student.Login.Length < 5)
                return "Логин не может содержать менее 5 символов";

            if (Students.Any(s => s.Login.ToLower() == student.Login.ToLower() && s.Id != student.Id))
                return "Логин должен быть уникальным";

            if (!Regex.IsMatch(student.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                return "Некорректный формат email";

            return message;
        }

        public void Add(Student student)
        {
            string message = Validation(student);
            if (string.IsNullOrEmpty(message))
            {
                var _student = new Student
                {
                    Login = student.Login,
                    Name = student.Name,
                    Email = student.Email,
                    Password = student.Password,
                    CreatedAt = DateTime.Now,
                    RoleId = student.RoleId
                };
                _db.Students.Add(_student);
                Commit();
                Students.Add(_student);
            }
            else
            {
                MessageBox.Show("Ошибка валидации: " + message);
            }
        }

        public void Update(Student student)
        {
            string message = Validation(student);
            if (string.IsNullOrEmpty(message))
            {
                var existing = _db.Students.Find(student.Id);
                if (existing != null)
                {
                    existing.Login = student.Login;
                    existing.Name = student.Name;
                    existing.Email = student.Email;
                    existing.Password = student.Password;
                    existing.RoleId = student.RoleId;
                    Commit();
                }
            }
            else
            {
                MessageBox.Show("Ошибка валидации: " + message);
            }
        }

        public int Commit() => _db.SaveChanges();

        public void GetAll()
        {
            var students = _db.Students
                .Include(s => s.UserProfile)
                .Include(s => s.Role)
                .Include(s => s.UserInterestGroups)
                    .ThenInclude(uig => uig.InterestGroup)
                .ToList();

            Students.Clear();
            foreach (var student in students)
            {
                Students.Add(student);
            }
        }

        public void Remove(Student student)
        {
            _db.Students.Remove(student);
            if (Commit() > 0)
                if (Students.Contains(student))
                    Students.Remove(student);
        }

        public ObservableCollection<InterestGroup> GetStudentGroups(Student student)
        {
            var groups = _db.UserInterestGroups
                .Where(uig => uig.UserId == student.Id)
                .Include(uig => uig.InterestGroup)
                .Select(uig => uig.InterestGroup)
                .ToList();

            return new ObservableCollection<InterestGroup>(groups);
        }
    }
}
