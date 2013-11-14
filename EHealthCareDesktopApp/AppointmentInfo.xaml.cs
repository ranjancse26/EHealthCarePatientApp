using MahApps.Metro.Controls;
using EHealthCareDesktopApp.ManageClinicalInfo;
using System.Windows;
using System;
using EHealthCareDesktopApp.Helpers;
using EHealthCareDataAccess;
using System.Windows.Controls;

namespace EHealthCareDesktopApp
{
    /// <summary>
    /// Interaction logic for WeightInfo.xaml
    /// </summary>
    public partial class AppointmentInfo : MetroWindow
    {
        public AppointmentInfo()
        {
            InitializeComponent();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadAppointments();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void LoadAppointments()
        {
            var appointmentDataHelper = new AppointmentDataHelper(EHealthCareDesktopApp.Properties.Settings.Default.UniqueIdentifier.ToString(),
                 EHealthCareDesktopApp.Properties.Settings.Default.PatientID);

            listViewAppointments.ItemsSource = appointmentDataHelper.GetViewModel();
        }

        private void AddAppointmentClick(object sender, RoutedEventArgs e)
        {
            var manageAppointment = new ManageAppointments();
            manageAppointment.AppointmentAddedEvent += manageAppointment_AppointmentAddedEvent;
            manageAppointment.ShowDialog();
        }

        void manageAppointment_AppointmentAddedEvent(string message)
        {
            if (message.Equals("Success"))
                 LoadAppointments();
            else
            {
                MessageBox.Show(message);
            }
        }

        private void UploadAudioButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var id = int.Parse(((Button)sender).CommandParameter.ToString());
                var uploadAudio = new UploadAudio(id);
                uploadAudio.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var id = int.Parse(((Button)sender).CommandParameter.ToString());
                var appointmentDataRepository = new AppointmentDataRepository(EHealthCareDesktopApp.Properties.Settings.Default.UniqueIdentifier);
                appointmentDataRepository.DeleteAppointment(EHealthCareDesktopApp.Properties.Settings.Default.PatientID, id);
                
                LoadAppointments();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
