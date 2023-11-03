// Code Attribution
// Troelsen, A. and Japikse, P. (2021). Pro C# 9 with .NET 5 :
// foundational principles and practices in programming.
// 10th ed. Berkeley, Ca: Apress L. P., . Copyright.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Configuration;
using System.Runtime.Remoting.Contexts;
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
using DataHandeling.Models;
using System.Data.Entity;
using System.Configuration;

namespace PROG6212POE
{
    /// <summary>
    /// Interaction logic for AddStudyHours.xaml
    /// </summary>
    public partial class AddStudyHours : Page
    {
        // SelectedModule represents the module for which study hours are being added
        Module SelectedModule;

        public static string connectionString = ConfigurationManager.ConnectionStrings["MyDbContext"].ConnectionString;
        // Constructor for the AddStudyHours page
        public AddStudyHours()
        {
            InitializeComponent();

            // Initialize SelectedModule and update UI elements with module details
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                // Find the WindowButton in the MainWindow
                ListView moduleListView = mainWindow.FindName("ModuleListView") as ListView;
                if (moduleListView != null)
                {
                    SelectedModule = moduleListView.SelectedItem as Module;
                }
            }

            ModuleNameLabel.Content = SelectedModule.ModuleName;

        }

        // Event handler for the "Update" button click
        private async void Update_Click(object sender, RoutedEventArgs e)
        {
            // Validate and process user input

            // Check and validate the study hours input
            if (!ValidateInput.NotNull(StudyHoursTxtBox.Text))
            {
                StudyHoursErrorLabel.Content = "* Please enter a valid number of study hours!";
                StudyHoursTxtBox.Clear();
                return;
            }
            StudyHoursErrorLabel.Content = "";

            // Check and validate the selected study date
            if (!ValidateInput.ValidateStartDate(StudyDate_DatePicker.SelectedDate.ToString()))
            {
                StudyDateErrorLabel.Content = "* Please select a valid study date!";
                StudyDate_DatePicker.SelectedDate = null;
                return;
            }
            StudyDateErrorLabel.Content = "";

            if (!ValidateInput.ValidateHours(StudyHoursTxtBox.Text.ToString()))
            {
                StudyHoursErrorLabel.Content = "* Please enter a numeric value";
                StudyHoursTxtBox.Clear();
                return;
            }
            StudyHoursErrorLabel.Content = "";

            // Retrieve study hours and date from user input
            double studyHours = double.Parse(StudyHoursTxtBox.Text);
            DateTime studyDate = StudyDate_DatePicker.SelectedDate.Value;

            // Record study hours for the module and handle errors


            string error = SelectedModule.RecordStudyHours(studyDate, studyHours, SelectedModule);

            

            if (error == "")
            {
                // Calculate the week number for the study date and display it
                int weekNumber = SelectedModule.CalculateWeekNumber(studyDate, SelectedModule);
                WeekNumberLabel.Content = weekNumber.ToString();

                // Display the remaining study hours for the current week

                var studyHoursList = await UserAuthentication.GetStudyHoursForModuleAsync(SelectedModule);
                double hoursRemaining = studyHoursList
                    .Where(record => record.WeekNumber == weekNumber)
                    .Select(record => record.RemainingHours)
                    .FirstOrDefault();



                Console.WriteLine(hoursRemaining);

                /*double hoursRemaining = SelectedModule.StudyHoursByWeek[weekNumber];*/
                HoursRemainingLabel.Content = hoursRemaining;
            }
            else
            {
                // Display the error message if there is an issue with recording hours
                StudyDateErrorLabel.Content = error;
                return;
            }
        }

        // Event handler for the StudyDate_DatePicker's SelectedDateChanged event
        private async void StudyDate_DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            // Retrieve the selected study date and calculate the week number
            DateTime studyDate = StudyDate_DatePicker.SelectedDate.Value;
            int weekNumber = SelectedModule.CalculateWeekNumber(studyDate, SelectedModule);
            string hoursRemaining;

            WeekNumberLabel.Content = weekNumber.ToString();

            // Check if study hours are recorded for the current week and update UI accordingly
            using (var context = new MyDbContext())
            {
                var studyHoursList = await UserAuthentication.GetStudyHoursForModuleAsync(SelectedModule);

                var matchingRecord = studyHoursList
                    .Where(record => record.WeekNumber == weekNumber)
                    .Select(record => record.RemainingHours)
                    .FirstOrDefault();

                hoursRemaining = matchingRecord != 0 ? matchingRecord.ToString() : "N/A";




                if (!hoursRemaining.Equals("N/A"))
                {
                    HoursRemainingLabel.Content = hoursRemaining.ToString();
                }
                else
                {
                    var studyHours = new StudyHour
                    {
                        WeekNumber = weekNumber,
                        RemainingHours = SelectedModule.CalculateSelfStudyHours(SelectedModule),
                        ModuleID = SelectedModule.ModuleID
                    };

                    HoursRemainingLabel.Content = studyHours.RemainingHours.ToString();

                    context.StudyHours.Add(studyHours);
                    context.SaveChanges();
                }
            }
        }
    }

}
