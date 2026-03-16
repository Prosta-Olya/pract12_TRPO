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
    public partial class RoleUsersPage : Page
    {
        private readonly Services.RolesService _rolesService = new();
        private readonly int _roleId;

        public RoleUsersPage(int roleId, string roleTitle)
        {
            InitializeComponent();
            _roleId = roleId;

            RoleTitleText.Text = $"Пользователи роли: {roleTitle}";

            LoadUsers();
        }

        private void LoadUsers()
        {
            var users = _rolesService.GetStudentsByRoleId(_roleId);
            UsersDataGrid.ItemsSource = users;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService?.CanGoBack == true)
            {
                NavigationService.GoBack();
            }
        }
    }
}
