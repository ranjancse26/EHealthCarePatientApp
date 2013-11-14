using MahApps.Metro.Controls;
using EHealthCareDesktopApp.ManageClinicalInfo;
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
using EHealthCareDesktopApp.Helpers;
using EHealthCareDataAccess;

namespace EHealthCareDesktopApp
{
    /// <summary>
    /// Interaction logic for MedicationInfo.xaml
    /// </summary>
    public partial class MedicationInfo : MetroWindow
    {
        private DialogWindow dialogWindow;
        
        public MedicationInfo()
        {
            InitializeComponent();
        }

        private void AddMedicationsButtonClick(object sender, RoutedEventArgs e)
        {
            var manageMedication = new ManageMedication();
            manageMedication.MedicationAddedEvent += manageMedication_MedicationAddedEvent;
            manageMedication.ShowDialog();
        }

        private void LoadMedications()
        {
            var appointmentDataHelper = new MedicationDataHelper(EHealthCareDesktopApp.Properties.Settings.Default.UniqueIdentifier.ToString(),
                 EHealthCareDesktopApp.Properties.Settings.Default.PatientID);

            listViewMedications.ItemsSource = appointmentDataHelper.GetViewModel();
        }

        void manageMedication_MedicationAddedEvent(string message)
        {
            if (message.Equals("Success"))
                LoadMedications();
            else
            {
                MessageBox.Show(message);
            }
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
              LoadMedications();
        }

        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var id = int.Parse(((Button)sender).CommandParameter.ToString());
                var medicationDataRepository = new MedicationDataRepository(EHealthCareDesktopApp.Properties.Settings.Default.UniqueIdentifier);
                medicationDataRepository.DeleteMedication(EHealthCareDesktopApp.Properties.Settings.Default.PatientID, id);

                LoadMedications();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
