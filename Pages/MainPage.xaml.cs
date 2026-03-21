using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using pract12_TRPO.Services;

namespace pract12_TRPO.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public StudentsService service { get; set; } = new();
        public Student? student { get; set; } = null;
        public InterestGroupService service2 { get; set; } = new();
        public MainPage()
        {
            InitializeComponent();
            DataContext = this;
        }
        public void go_form(object sender, EventArgs e)
        {
            NavigationService.Navigate(new StudentFormPage());
        }
        public void Edit(object sender, EventArgs e)
        {
            if (student == null)
            {
                MessageBox.Show("Выберите элемент из списка!");
                return;
            }
            NavigationService.Navigate(new StudentFormPage(student));
        }
        private void remove(object sender, RoutedEventArgs e)
        {
            if (student == null)
            {
                MessageBox.Show("Выберите запись!");
                return;
            }
            if (MessageBox.Show("Вы действительно хотите удалить запись?", "Удалить?",
            MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                service.Remove(student);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            service.GetAll();
            var listView = FindName("List") as ListView;
            if (listView != null)
            {
                var items = listView.Items.Cast<object>().ToList();
                listView.ItemsSource = null;
                listView.ItemsSource = items;
            }
        }

        private void roles(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RoleList());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (student == null)
            {
                MessageBox.Show("Выберите студента в списке!");
                return;
            }

            NavigationService.Navigate(new InterestGroupUsersPage(student));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddInterestGroup(null, service2));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GroupList());
        }
    }
}
