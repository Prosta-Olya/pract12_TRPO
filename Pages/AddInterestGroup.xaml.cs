using pract12_TRPO.Services;
using System;
using System.Windows;
using System.Windows.Controls;

namespace pract12_TRPO.Pages
{
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
                _interestGroup = interestGroup;
                IsEdit = true;
            }

            DataContext = _interestGroup;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (IsEdit)
                service.Update(_interestGroup);
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