using MahApps.Metro.Controls;
using EHealthCareDataAccess;
using EHealthCareDesktopApp.Helpers;
using System;
using System.Collections.Generic;
using System.Windows;

namespace EHealthCareDesktopApp
{
    /// <summary>
    /// Interaction logic for ManageAppointments.xaml
    /// </summary>
    public partial class ManageAppointments : MetroWindow
    {
        public delegate void AppointmentAddedDelegate(string message);
        public event AppointmentAddedDelegate AppointmentAddedEvent;

        public ManageAppointments()
        {
            InitializeComponent();
        }

        private void SaveAppointment(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                var appointment = new Appointment();
                var appointmentDataRepository = new AppointmentDataRepository(EHealthCareDesktopApp.Properties.Settings.Default.UniqueIdentifier);
                appointment.ProviderId = Convert.ToInt32(cmbProvider.SelectedValue.ToString());
                appointment.Notes = txtNotes.Text.Trim();
                appointment.PatientId = EHealthCareDesktopApp.Properties.Settings.Default.PatientID;
                appointment.Type = cmbType.SelectedItem.ToString();
                appointment.Status = "InProgress";
                appointment.StartDate = DateTime.Parse(string.Format("{0} {1}", dtPickerStartDate.SelectedDate.Value.ToShortDateString(), startTime.Text));
                appointment.SpecialtyId = Convert.ToInt32(cmbSpecialty.SelectedValue.ToString());
                appointment.EndDate = DateTime.Parse(string.Format("{0} {1}", dtPickerStartDate.SelectedDate.Value.ToShortDateString(), endTime.Text));
                appointment.Purpose = txtPurpose.Text.Trim();
                appointmentDataRepository.SaveAppointment(appointment);

                if (AppointmentAddedEvent != null)
                    AppointmentAddedEvent("Success");                
            }
            catch (Exception ex)
            {
                if (AppointmentAddedEvent != null)
                    AppointmentAddedEvent(string.Format("Problem in adding Appointment: {0}", ex.Message));
            }
        }

        public List<string> GetAppointmentTypes()
        {
            return new List<string>
            {
                "Day Surgery",
                "Emergency",
                "Inpatient ",
                "Outpatient",
                "Recurring"
            };
        }

        private void WindowLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this.dtPickerEndDate.SelectedDate = DateTime.Now.Date;
            this.dtPickerStartDate.SelectedDate = DateTime.Now.Date;
            this.cmbType.ItemsSource = GetAppointmentTypes();

            try
            {
                var providerDataRepository = new ProviderDataRepository();
                var providers = providerDataRepository.GetAllProviders();
                List<NameValuePairs> nameValuePairs = new List<NameValuePairs>();

                foreach(Provider provider in providers){
                    nameValuePairs.Add(new NameValuePairs(string.Format("{0} {1}", provider.FirstName, provider.LastName), provider.Id.ToString()));
                }

                cmbProvider.ItemsSource = nameValuePairs;
                cmbProvider.DisplayMemberPath = "Name";
                cmbProvider.SelectedValuePath = "Value";
 
                var appointmentDataRepository = new AppointmentDataRepository(EHealthCareDesktopApp.Properties.Settings.Default.UniqueIdentifier);
                var specialtiesList = appointmentDataRepository.GetAllSpecialties();
                cmbSpecialty.ItemsSource = specialtiesList;
                cmbSpecialty.DisplayMemberPath = "SpecialityName";
                cmbSpecialty.SelectedValuePath = "Id";

                cmbSpecialty.SelectedIndex = 0;
                if (cmbProvider.Items.Count > 0)
                    cmbProvider.SelectedIndex = 0;

                if(cmbType.Items.Count > 0)
                    cmbType.SelectedIndex = 3;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Problem in loading specialties: '{0}'" , ex.ToString()));
            }
        }
    }
}
