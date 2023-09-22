// Code Attribution
// Troelsen, A. and Japikse, P. (2021). Pro C# 9 with .NET 5 :
// foundational principles and practices in programming.
// 10th ed. Berkeley, Ca: Apress L. P., . Copyright.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    /// Interaction logic for AddModule.xaml
    /// </summary>
    public partial class AddModule : Page
    {
        // Collection to store modules
        /*public static ObservableCollection<Module> modules = new ObservableCollection<Module>();*/

        // Constructor for the AddModule page
        public AddModule()
        {
            InitializeComponent();
        }

        // Event handler for the Save button click
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            // Validate and process user input

            // Check and validate the module code
            if (!ValidateInput.NotNull(ModuleCodeTxtBox.Text) || !ValidateInput.ValidateModule(ModuleCodeTxtBox.Text))
            {
                ModuleCodeErrorLabel.Content = "* Please enter a valid module code!";
                ModuleCodeTxtBox.Clear();
                return;
            }
            ModuleCodeErrorLabel.Content = "";

            // Check and validate the module name
            if (!ValidateInput.NotNull(ModuleNameTxtBox.Text))
            {
                ModuleNameErrorLabel.Content = "* Module name cannot be null!";
                ModuleNameTxtBox.Clear();
                return;
            }
            ModuleNameErrorLabel.Content = "";

            // Check and validate the number of credits
            if (!ValidateInput.NotNull(NumberOfCreditsTxtBox.Text) || !ValidateInput.ValidateCredits(NumberOfCreditsTxtBox.Text))
            {
                NumberOfCreditsErrorLabel.Content = "* Please enter a valid number of credits!";
                NumberOfCreditsTxtBox.Clear();
                return;
            }
            NumberOfCreditsErrorLabel.Content = "";

            // Check and validate the number of class hours per week
            if (!ValidateInput.NotNull(ClassHoursTxtBox.Text) || !ValidateInput.ValidateHours(ClassHoursTxtBox.Text))
            {
                ClassHoursPerWeekErrorLabel.Content = "* Please enter a valid number of class hours!";
                ClassHoursTxtBox.Clear();
                return;
            }
            ClassHoursPerWeekErrorLabel.Content = "";

            // Check and validate the number of weeks
            if (!ValidateInput.NotNull(NumberOfWeeksTxtBox.Text) || !ValidateInput.ValidateWeeks(NumberOfWeeksTxtBox.Text))
            {
                NumberOfWeeksErrorLabel.Content = "* Please enter a valid number of weeks!";
                NumberOfWeeksTxtBox.Clear();
                return;
            }
            NumberOfWeeksErrorLabel.Content = "";

            // Check and validate the selected start date
            if (!ValidateInput.NotNull(StartDate_DatePicker.SelectedDate.ToString()))
            {
                StartDateErrorLabel.Content = "* Please select a valid start date!";
                return;
            }
            StartDateErrorLabel.Content = "";

            // Retrieve user input and create a new module
            string moduleCode = ModuleCodeTxtBox.Text;
            string moduleName = ModuleNameTxtBox.Text;
            int numberOfCredits = int.Parse(NumberOfCreditsTxtBox.Text);
            double classHours = double.Parse(ClassHoursTxtBox.Text);
            int numberOfWeeks = int.Parse(NumberOfWeeksTxtBox.Text);
            DateTime startDate = StartDate_DatePicker.SelectedDate.Value;

            Module module = new Module(moduleCode, moduleName, numberOfCredits, classHours, numberOfWeeks, startDate);

            // Clear input fields
            ModuleCodeTxtBox.Clear();
            ModuleNameTxtBox.Clear();
            NumberOfCreditsTxtBox.Clear();
            ClassHoursTxtBox.Clear();
            NumberOfWeeksTxtBox.Clear();
            StartDate_DatePicker.SelectedDate = null;

            // Navigate to the AddStudyHours page with the created module
            NavigationService.Navigate(new AddStudyHours(module));
        }

        // Event handler for the Back button click
        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            // Navigate back to the previous page
            NavigationService.GoBack();
        }
    }

}
