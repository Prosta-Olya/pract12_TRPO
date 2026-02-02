using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            // Проверка пароля
            bool upper = false;
            bool lower = false;
            bool numbers = false;
            bool symbols = false;
            if (string.IsNullOrEmpty(student.Password) || student.Password.Length < 8)
            {
                return message = "Пароль не может содержать менее 8 символов";
            }
            foreach (char c in student.Password)
            {
                if (char.IsUpper(c))
                    upper = true;
                else if (char.IsLower(c))
                    lower = true;
                else if (char.IsDigit(c))
                    numbers = true;
                else if (!char.IsWhiteSpace(c))
                    symbols = true;
            }
            if (!upper)
                return message = "Пароль обязательно должен содержать буквы в верхнем регистре";
            if (!lower)
                return message = "Пароль обязательно должен содержать буквы в нижнем регистре";
            if (!numbers)
                return message = "Пароль обязательно должен содержать цифры";
            if (!symbols)
                return message = "Пароль обязательно должен содержать специальные символы";


            // Проверка логина
            if (student.Login.Length < 5)
            {
                return message = "Логин не может содержать менее 5 символов";
            }
            for(int i = 0; i < Students.Count; i++)
            {
                if (Students[i].Login.ToLower() == student.Login.ToLower())
                {
                    return message = "Логин должен быть уникальным";
                }
            }

            // Проверка почты
            bool email = false;
            for(int i = 0; i<student.Email.Length; i++)
            {
                if (student.Email[i] == '@')
                {
                    email = true;
                }
            }
            if (!email)
            {
                return message = "Неккоректный адрес электронной почты";
            }
            for (int i = 0; i < Students.Count; i++)
            {
                if (Students[i].Email == student.Email)
                {
                    return message = "Почта должна быть уникальной";
                }
            }

            return message;
        }
        public void Add(Student student)
        {
            var _student = new Student
            {
                Login = student.Login,
                Name = student.Name,
                Email = student.Email,
                Password = student.Password,
                CreatedAt = DateTime.Now,
            };
            string message = Validation(student);
            if (message == "")
            {
                _db.Add<Student>(_student);
                Commit();
                Students.Add(_student);
            }
            else
            {
                MessageBox.Show("Ошибка сохранения: "+ message);
            }

        }
        public int Commit() => _db.SaveChanges();
        public void GetAll()
        {
            var students = _db.Students.ToList();
            Students.Clear();
            foreach (var student in students)
            {
                Students.Add(student);
            }
        }
        public void Remove(Student student)
        {
            _db.Remove<Student>(student);
            if (Commit() > 0)
                if (Students.Contains(student))
                    Students.Remove(student);
        }
    }
}
