// Code Attribution
// Troelsen, A. and Japikse, P. (2021). Pro C# 9 with .NET 5 :
// foundational principles and practices in programming.
// 10th ed. Berkeley, Ca: Apress L. P., . Copyright.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
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
    /// Interaction logic for AddStudyHours.xaml
    /// </summary>
    public partial class AddStudyHours : Page
    {
        // SelectedModule represents the module for which study hours are being added
        Module SelectedModule;

        // Constructor for the AddStudyHours page
        public AddStudyHours(Module module)
        {
            InitializeComponent();

            // Initialize SelectedModule and update UI elements with module details
            SelectedModule = module;
            ModuleNameLabel.Content = SelectedModule.ModuleName;
            HoursRequiredLabel.Content = SelectedModule.CalculateSelfStudyHours(SelectedModule);
        }

        // Event handler for the "Update" button click
        private void Update_Click(object sender, RoutedEventArgs e)
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

            // Retrieve study hours and date from user input
            double studyHours = double.Parse(StudyHoursTxtBox.Text);
            DateTime studyDate = StudyDate_DatePicker.SelectedDate.Value;

            // Record study hours for the module and handle errors
            string error = SelectedModule.RecordHours(studyDate, studyHours, SelectedModule);

            if (error == "")
            {
                // Calculate the week number for the study date and display it
                int weekNumber = SelectedModule.CalculateWeekNumber(studyDate, SelectedModule);
                WeekNumberLabel.Content = weekNumber.ToString();

                // Display the remaining study hours for the current week
                double hoursRemaining = SelectedModule.StudyHoursByWeek[weekNumber];
                HoursRemainingLabel.Content = hoursRemaining.ToString();
            }
            else
            {
                // Display the error message if there is an issue with recording hours
                StudyDateErrorLabel.Content = error;
                return;
            }
        }

        // Event handler for the StudyDate_DatePicker's SelectedDateChanged event
        private void StudyDate_DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            // Retrieve the selected study date and calculate the week number
            DateTime studyDate = StudyDate_DatePicker.SelectedDate.Value;
            int weekNumber = SelectedModule.CalculateWeekNumber(studyDate, SelectedModule);

            // Check if study hours are recorded for the current week and update UI accordingly
            if (SelectedModule.StudyHoursByWeek.ContainsKey(weekNumber))
            {
                WeekNumberLabel.Content = weekNumber.ToString();
                double hoursRemaining = SelectedModule.StudyHoursByWeek[weekNumber];
                HoursRemainingLabel.Content = hoursRemaining.ToString();
            }
            else
            {
                // Clear the week number and remaining hours if no data is available
                WeekNumberLabel.Content = "";
                HoursRemainingLabel.Content = "";
            }
        }
    }

}
