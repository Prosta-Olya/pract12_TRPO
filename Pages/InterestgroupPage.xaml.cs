using pract12_TRPO.Services;
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
    /// Логика взаимодействия для InterestgroupPage.xaml
    /// </summary>
    public partial class InterestgroupPage : Page
    {
        public InterestGroupService service { get; set; } = new();
        public InterestGroup? current { get; set; } = null;
        public InterestgroupPage()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddInterestGroup(null, service));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (current != null)
            {
                if (MessageBox.Show("Вы действительно хотите удалить курс?",
                "Удалить курс?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    service.Remove(current);
                }
            }
            else
            {
                MessageBox.Show("Выберите курс для удаления", "Выберите курс",
                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (current != null)
                NavigationService.Navigate(new AddInterestGroup(current, service));
            else
                MessageBox.Show("Выберите курс");
        }
    }
}
