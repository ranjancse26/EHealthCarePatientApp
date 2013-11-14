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
    /// Interaction logic for ManageBloodPressure.xaml
    /// </summary>
    public partial class ManageBloodPressure : MetroWindow
    {
        public delegate void BloodPressureAddedDelegate(string message);
        public event BloodPressureAddedDelegate BloodPressureAddedEvent;

        public ManageBloodPressure()
        {
            InitializeComponent();
        }

        private void SaveBloodPressure(object sender, RoutedEventArgs e)
        {
            try
            {
                var bloodPressureDataRepository = new BloodPressureDataRepository(EHealthCareDesktopApp.Properties.Settings.Default.UniqueIdentifier);
                var bloodPressure = new BloodPressure
                {
                    PatientId = EHealthCareDesktopApp.Properties.Settings.Default.PatientID,
                    When = dtPicker.SelectedDate.Value,
                    Systolic = int.Parse(txtSystolic.Text.Trim()),
                    Diastolic = int.Parse(txtDiastolic.Text.Trim()),
                    Pulse = int.Parse(txtPulse.Text.Trim()),
                };

                if (chkIrregularHeartBeat.IsChecked != null && (bool)chkIrregularHeartBeat.IsChecked)
                    bloodPressure.IrregularHeartbeat = true;
                else
                    bloodPressure.IrregularHeartbeat = false;

               bloodPressureDataRepository.SaveBloodPressure(bloodPressure);

               if (BloodPressureAddedEvent != null)
                   BloodPressureAddedEvent("Success");
            }
            catch(Exception ex)
            {
                if (BloodPressureAddedEvent != null)
                    BloodPressureAddedEvent(string.Format("Problem in adding BloodPressure Readings: {0}", ex.ToString()));
            }
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            this.Title = "Manage Blood Pressure for: " + EHealthCareDesktopApp.Properties.Settings.Default.PatientName;
            this.dtPicker.SelectedDate = DateTime.Now.Date;
        }

        private void SystolicPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsNumeric(e.Text);           
        }

        private static bool IsNumeric(string text)
        {
            Regex regex = new Regex("[^0-9.]+"); //regex that matches disallowed text
            return regex.IsMatch(text);
        }

        private void DiastolicPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsNumeric(e.Text); 
        }
    }
}
