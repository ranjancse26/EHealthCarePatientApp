using MahApps.Metro.Controls;
using EHealthCareDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EHealthCareDesktopApp.ManageClinicalInfo
{
    /// <summary>
    /// Interaction logic for ManageWeight.xaml
    /// </summary>
    public partial class ManageWeight : MetroWindow
    {
        public delegate void WeightAddedDelegate(string message);
        public event WeightAddedDelegate WeightAddedEvent;

        public ManageWeight()
        {
            InitializeComponent();
        }

        private void SaveWeight(object sender, RoutedEventArgs e)
        {
            try
            {
                var weightDataRepository = new WeightDataRepository(EHealthCareDesktopApp.Properties.Settings.Default.UniqueIdentifier);
                weightDataRepository.SaveWeight(new Weight
                {
                    PatientId = EHealthCareDesktopApp.Properties.Settings.Default.PatientID,
                    Date = dtPicker.SelectedDate.Value,
                    Weight1 = double.Parse(txtWeight.Text.Trim()),
                    WeightGoal = double.Parse(txtWeightGoal.Text.Trim()),
                    UniqueIdentifier = EHealthCareDesktopApp.Properties.Settings.Default.UniqueIdentifier
                });

                if(WeightAddedEvent != null)
                    WeightAddedEvent("Success");
            }
            catch(Exception ex)
            {
                if (WeightAddedEvent != null)
                    WeightAddedEvent(string.Format("Problem in adding Weight: {0}",ex.Message));
            }
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            this.Title = "Manage Weight for Patient: " + EHealthCareDesktopApp.Properties.Settings.Default.PatientName;
            this.dtPicker.SelectedDate = DateTime.Now.Date;
        }

        private void WeightTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsNumeric(e.Text);
        }

        private void WeightGoalTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsNumeric(e.Text);
        }

        private static bool IsNumeric(string text)
        {
            Regex regex = new Regex("[^0-9.]+"); //regex that matches disallowed text
            return regex.IsMatch(text);
        }
    }
}
