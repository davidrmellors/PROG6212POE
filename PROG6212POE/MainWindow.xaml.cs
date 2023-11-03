// Code Attribution
// Troelsen, A. and Japikse, P. (2021). Pro C# 9 with .NET 5 :
// foundational principles and practices in programming.
// 10th ed. Berkeley, Ca: Apress L. P., . Copyright.

using System;
using System.Collections.Generic;
using System.Configuration.Provider;
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
using DataHandeling;

namespace PROG6212POE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Constructor for the MainWindow
        public MainWindow()
        {
            // Initialize the main window
            InitializeComponent();   
            // Navigate to the HomePage when the application starts
            Main.NavigationService.Navigate(new HomePage());

            // Clear and set the item source for the ModuleListView
            ModuleListView.Items.Clear();
           

            using (var context = new MyDbContext())
            {
                var data = context.Modules.ToList(); // Replace YourDbSet with the name of your entity DbSet
                foreach (var item in data)
                {
                    // Process and display data...
                    Console.WriteLine(item);
                }
            }

            


        }

        // Event handler for the "Add Module" button click
        private void AddModuleBtn_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the AddModule page
            Main.Content = new AddModule();
            Main.NavigationService.Navigate(new AddModule());
        }

        // Event handler for the "Home" button click
        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the HomePage
            Main.Content = new HomePage();
        }

        // Event handler for the "Exit" button click
        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            // Close the application
            this.Close();
        }

        // Event handler for the ModuleListView item click
        private void ModuleListView_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // Retrieve the selected module from the list view
            Module selectedModule = ModuleListView.SelectedItem as Module;

            // Navigate to the AddStudyHours page with the selected module
            Main.NavigationService.Navigate(new AddStudyHours());
        }

        // Event handler for the ModuleCodeSearchBox key down event
        private void ModuleCodeSearchBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // Clear any previous search error message and set the error label color
                SearchBoxErrorLabel.Content = "";
                SearchBoxErrorLabel.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#b05656"));

                // Get the search text from the ModuleCodeSearchBox
                string searchText = ModuleCodeSearchBox.Text.Trim().ToLower();

                if (string.IsNullOrWhiteSpace(searchText))
                {
                    // Display an error if the search box is empty
                    SearchBoxErrorLabel.Content = "* Search box cannot be empty!";
                    return;
                }

                // Find the first module that matches the search text
                var matchingModule = ModuleListView.Items.Cast<Module>()
                    .FirstOrDefault(module => module.ModuleCode.ToString().ToLower().Contains(searchText));

                if (matchingModule != null)
                {
                    // Set the selected module, change the error label color, and navigate to the AddStudyHours page
                    ModuleListView.SelectedItem = matchingModule;
                    SearchBoxErrorLabel.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#56b077"));
                    SearchBoxErrorLabel.Content = "* Module found!";
                    Main.NavigationService.Navigate(new AddStudyHours());
                }
                else
                {
                    // Display an error if no matching module is found
                    SearchBoxErrorLabel.Content = "* Module code not found!";
                }
            }
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            Main.NavigationService.Navigate(new Login());
        }

        
    }

}
