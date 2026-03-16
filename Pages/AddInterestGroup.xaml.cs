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
    /// Логика взаимодействия для AddInterestGroup.xaml
    /// </summary>
    public partial class AddInterestGroup : Page
    {
        InterestGroup _interestGroup = new();
        InterestGroupService service = new();
        bool IsEdit = false;
        public AddInterestGroup(InterestGroup? interestGroup, InterestGroupService sharedService)
        {
            InitializeComponent();
            service = sharedService;
            if (interestGroup != null)
            {
                service.LoadRelation(interestGroup, "Students");
                _interestGroup = interestGroup;
                IsEdit = true;
            }
            DataContext = _interestGroup;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (IsEdit)
                service.Commit();
            else
                service.Add(_interestGroup);
            NavigationService.GoBack();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
