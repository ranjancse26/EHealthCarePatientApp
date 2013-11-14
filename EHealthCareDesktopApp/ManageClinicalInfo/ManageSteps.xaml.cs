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
    public partial class ManageSteps : MetroWindow
    {
        public delegate void StepsAddedDelegate(string message);
        public event StepsAddedDelegate StepsAddedEvent;

        public ManageSteps()
        {
            InitializeComponent();
        }

        private void SaveSteps(object sender, RoutedEventArgs e)
        {
            try
            {
                var stepsDataRepository = new StepsDataRepository(EHealthCareDesktopApp.Properties.Settings.Default.UniqueIdentifier);
                stepsDataRepository.SaveSteps(new Step
                {
                    PatientId = EHealthCareDesktopApp.Properties.Settings.Default.PatientID,
                    Date = dtPicker.SelectedDate.Value,
                    Steps = int.Parse(txtSteps.Text.Trim()),
                    UniqueIdentifier = EHealthCareDesktopApp.Properties.Settings.Default.UniqueIdentifier
                });

                if (StepsAddedEvent != null)
                    StepsAddedEvent("Success");
            }
            catch(Exception ex)
            {
                if (StepsAddedEvent != null)
                    StepsAddedEvent(string.Format("Problem in adding Steps: {0}", ex.Message));
            }
        }

        private void StepTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsNumeric(e.Text);
        }

        private static bool IsNumeric(string text)
        {
            Regex regex = new Regex("[^0-9.]+"); //regex that matches disallowed text
            return regex.IsMatch(text);
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            this.Title = "Manage Steps for Patient: " + EHealthCareDesktopApp.Properties.Settings.Default.PatientName;
            this.dtPicker.SelectedDate = DateTime.Now.Date;
        }
    }
}
