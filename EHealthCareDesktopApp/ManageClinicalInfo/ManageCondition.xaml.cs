using MahApps.Metro.Controls;
using EHealthCareDataAccess;
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
using System.Windows.Shapes;

namespace EHealthCareDesktopApp.ManageClinicalInfo
{
    /// <summary>
    /// Interaction logic for ManageCondition.xaml
    /// </summary>
    public partial class ManageCondition : MetroWindow
    {
        public delegate void ConditionAddedDelegate(string message);
        public event ConditionAddedDelegate ConditionAddedEvent;

        public ManageCondition()
        {
            InitializeComponent();
        }

        private void SaveCondition(object sender, RoutedEventArgs e)
        {
            try
            {
                var conditionDataRepository = new ConditionDataRepository(EHealthCareDesktopApp.Properties.Settings.Default.UniqueIdentifier);
                var condition = new EHealthCareDataAccess.Condition();
                condition.Name = txtConditionName.Text.Trim();
                condition.PatientId = EHealthCareDesktopApp.Properties.Settings.Default.PatientID;
                condition.Recovered = txtRecovered.Text.Trim();
                condition.StartDate = dtPickerStartDate.SelectedDate.Value;
                condition.EndDate = dtPickerEndDate.SelectedDate.Value;
                condition.Status = txtStatus.Text.Trim();
                condition.Notes = txtNotes.Text.Trim();
                conditionDataRepository.SaveCondition(condition);

                if (ConditionAddedEvent != null)
                    ConditionAddedEvent("Success");
            }
            catch(Exception ex)
            {
                if (ConditionAddedEvent != null)
                    ConditionAddedEvent(string.Format("Problem in adding Condition: {0}", ex.Message));
            }
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            this.Title = "Manage Condition for: " + EHealthCareDesktopApp.Properties.Settings.Default.PatientName;
        }
    }
}
