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
    /// Interaction logic for ManageMedication.xaml
    /// </summary>
    public partial class ManageMedication : MetroWindow
    {
        public delegate void MedicationAddedDelegate(string message);
        public event MedicationAddedDelegate MedicationAddedEvent;

        public ManageMedication()
        {
            InitializeComponent();
        }

        private void SaveMedicationClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var medicationDataRepository = new MedicationDataRepository(EHealthCareDesktopApp.Properties.Settings.Default.UniqueIdentifier);
                var medication = new Medication()
                {
                    PatientId = EHealthCareDesktopApp.Properties.Settings.Default.PatientID,
                    DoseText = cmbDose.SelectedValue.ToString(),
                    DoseUnit = int.Parse(txtDose.Text.Trim()),
                    HowTaken = ((MedicationTaken)cmbHowTaken.SelectedItem).Name,
                    MedicationName = txtMedicationName.Text.Trim(),
                    Notes = txtNotes.Text.Trim(),
                    StartDate = dtPickerStartDate.SelectedDate.Value,
                    StrengthText = cmbStrength.SelectedValue.ToString(),
                    StrengthUnit = int.Parse(txtStrength.Text),
                    ReasonForTaking = txtReasonForTaking.Text.Trim()
                };

                if (dtPickerEndDate.SelectedDate != null)
                    medication.EndDate = dtPickerEndDate.SelectedDate.Value;

                medicationDataRepository.SaveMedication(medication);

                if (MedicationAddedEvent != null)
                    MedicationAddedEvent("Success");
            }
            catch (Exception ex)
            {
                if (MedicationAddedEvent != null)
                    MedicationAddedEvent(string.Format("Problem in adding Medications: {0}", ex.Message));
            }
        }

        private void MedicationNameTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsAlpha(e.Text);
        }

        private void ReasonForTakingTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsAlpha(e.Text);
        }

        private void StrengthTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsNumeric(e.Text);
        }

        private void DoseTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsNumeric(e.Text);
        }

        private static bool IsAlpha(string text)
        {
            Regex regex = new Regex("[^a-zA-Z]+"); //regex that matches disallowed text
            return regex.IsMatch(text);
        }

        private static bool IsNumeric(string text)
        {
            Regex regex = new Regex("[^0-9.]+"); //regex that matches disallowed text
            return regex.IsMatch(text);
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                this.dtPickerStartDate.SelectedDate = DateTime.Now.Date;

                var medicationDataRepository = new MedicationDataRepository(EHealthCareDesktopApp.Properties.Settings.Default.UniqueIdentifier);

                // Get all medication stregths
                var medicationStrengths = medicationDataRepository.GetAllMedicationStrengths();
                cmbStrength.ItemsSource = medicationStrengths;
                cmbStrength.DisplayMemberPath = "Unit";
                cmbStrength.SelectedValuePath = "Value";

                // Get all medication doses
                var medicationDoses = medicationDataRepository.GetAllMedicationDoses();
                cmbDose.ItemsSource = medicationDoses;
                cmbDose.DisplayMemberPath = "Name";
                cmbDose.SelectedValuePath = "Value";

                // Get all medication takens
                var medicationTakens = medicationDataRepository.GetAllMedicationTakens();
                cmbHowTaken.ItemsSource = medicationTakens;
                cmbHowTaken.DisplayMemberPath = "Name";
                cmbHowTaken.SelectedValuePath = "Value";

                this.Title = "Manage Medication for: " + EHealthCareDesktopApp.Properties.Settings.Default.PatientName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
