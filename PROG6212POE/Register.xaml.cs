using DataHandeling;
using DataHandeling.Models;
using System.Windows;
using System.Windows.Controls;
using System.Configuration;

namespace PROG6212POE
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Page
    {
        public Register()
        {
            InitializeComponent();
        }

        public string connectionString = ConfigurationManager.ConnectionStrings["MyDbContext"].ConnectionString;

        private async void RegisterBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInput.NotNull(UsernameTxtBox.Text))
            {
                UsernameErrorLabel.Content = "* Username cannot be empty!";
                UsernameTxtBox.Clear();
                return;
            }
            UsernameErrorLabel.Content = "";

            bool isUsernameUnique = await UserAuthentication.IsUsernameUniqueAsync(UsernameTxtBox.Text);

            if (!isUsernameUnique)
            {
                UsernameErrorLabel.Content = "* Username already taken!";
                UsernameTxtBox.Clear();
                return;
            }
            UsernameErrorLabel.Content = "";

            if (!ValidateInput.IsPasswordValid(PasswordBox.Password).Equals(""))
            {
                PasswordErrorLabel.Content = ValidateInput.IsPasswordValid(PasswordBox.Password);
                PasswordBox.Clear();
                ConfirmPasswordBox.Clear();
            }
            PasswordErrorLabel.Content = "";

            if (!ValidateInput.NotNull(ConfirmPasswordBox.Password))
            {
                ConfirmPasswordErrorLabel.Content = "* Confirm password cannot be empty!";
                ConfirmPasswordBox.Clear();
                return;
            }
            ConfirmPasswordErrorLabel.Content = "";

            if (!PasswordBox.Password.Equals(ConfirmPasswordBox.Password))
            {
                ConfirmPasswordErrorLabel.Content = "* Passwords do not match!";
                PasswordBox.Clear();
                ConfirmPasswordBox.Clear();
                return;
            }
            ConfirmPasswordErrorLabel.Content = "";

            using (var db = new MyDbContext())
            {
                string username = UsernameTxtBox.Text;
                string password = PasswordBox.Password;

                // Hash the password (using a secure hashing library, not this simple example)
                string hashedPassword = PasswordHasher.HashPassword(password);

                var user = new User { Username = username, PasswordHash = hashedPassword };
                db.Users.Add(user);
                await db.SaveChangesAsync(); // Use async SaveChanges
            }

            NavigationService.Navigate(new Login());
        }


        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new HomePage());
        }
    }
}
