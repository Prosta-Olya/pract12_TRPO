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

namespace pract12_TRPO.Pages
{
    /// <summary>
    /// Логика взаимодействия для GroupList.xaml
    /// </summary>
    public partial class GroupList : Page
    {
        public Services.InterestGroupService service { get; set; } = new();
        public InterestGroup? current { get; set; } = null;
        public GroupList()
        {
            InitializeComponent();
        }
        private void back(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        private void add(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GroupForm());
        }
        private void edit(object sender, RoutedEventArgs e)
        {
            if (current != null)
            {
                NavigationService.Navigate(new GroupForm(current));
            }
            else
            {
                MessageBox.Show("Выберите группу");
            }
        }
        private void remove(object sender, RoutedEventArgs e)
        {
            if (current != null)
            {
                if (MessageBox.Show("Вы действительно хотите удалить группу?",
                "Удалить группу?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    service.Remove(current);
                }
            }
            else
            {
                MessageBox.Show("Выберите группу для удаления", "Выберите группу",
                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ViewUsers_Click(object sender, RoutedEventArgs e)
        {
            if (current != null)
            {
                NavigationService.Navigate(new RoleUsersPage(current.Id, current.Title));
            }
            else
            {
                MessageBox.Show("Выберите группу для просмотра пользователей",
                               "Внимание",
                               MessageBoxButton.OK,
                               MessageBoxImage.Information);
            }
        }
    }
}
