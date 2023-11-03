using DataHandeling;
using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace PROG6212POE
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }

        private async void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInput.NotNull(UsernameTxtBox.Text))
            {
                UsernameErrorLabel.Content = "* Username cannot be empty!";
                UsernameTxtBox.Clear();
                return;
            }
            UsernameErrorLabel.Content = "";

            if (!ValidateInput.IsPasswordValid(PasswordBox.Password).Equals(""))
            {
                PasswordErrorLabel.Content = ValidateInput.IsPasswordValid(PasswordBox.Password);
                PasswordBox.Clear();
                return;
            }
            PasswordErrorLabel.Content = "";

            string username = UsernameTxtBox.Text.ToString();
            string password = PasswordBox.Password.ToString();

            bool isAuthenticated = await UserAuthentication.VerifyUserLoginAsync(username, password);

            if (!isAuthenticated)
            {
                PasswordErrorLabel.Content = "* Username or password is incorrect!";
                UsernameTxtBox.Clear();
                PasswordBox.Clear();
                return;
            }
            PasswordErrorLabel.Content = "";

            UserAuthentication.LoggedInUser = username;

            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                Button windowButton = mainWindow.FindName("AddModuleBtn") as Button;
                ListView moduleListView = mainWindow.FindName("ModuleListView") as ListView;

                if (windowButton != null)
                {
                    windowButton.Visibility = Visibility.Visible;
                }

                if (moduleListView != null)
                {
                    moduleListView.ItemsSource = await UserAuthentication.GetModulesForUsername();
                }
            }

            NavigationService.Navigate(new AddModule());
        }


        private void RegisterBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Register());
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
