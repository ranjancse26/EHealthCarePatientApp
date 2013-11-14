using EHealthCareDesktopApp.ManageClinicalInfo;
using MahApps.Metro.Controls;
using SmartLoginOverlayDemo2.ViewModels;
using SoftArcs.WPFSmartLibrary.MVVMCore;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EHealthCareDesktopApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public LoginViewModel ViewModel;
        
        public MainWindow()
        {
            InitializeComponent();

            this.ViewModel = new LoginViewModel();
            this.DataContext = this.ViewModel;
        }

        private void WeightClick(object sender, RoutedEventArgs e)
        {
            var weightInfo = new WeightInfo();
            weightInfo.ShowDialog();
        }

        private void StepsClick(object sender, RoutedEventArgs e)
        {
            var stepsInfo = new StepsInfo();
            stepsInfo.ShowDialog();
        }

        private void BloodPressureClick(object sender, RoutedEventArgs e)
        {
            var bloodPressureInfo = new BloodPressureInfo();
            bloodPressureInfo.ShowDialog();
        }

        private void RxClick(object sender, RoutedEventArgs e)
        {
            var drugSearch = new DrugSearch();
            drugSearch.ShowDialog();
        }

        private void PharmacyClick(object sender, RoutedEventArgs e)
        {
            var pharmacyLookup = new PharmacyLookup();
            pharmacyLookup.ShowDialog();
        }

        private void SearchDoctorsClick(object sender, RoutedEventArgs e)
        {
            var doctorLookup = new DoctorsLookup();
            doctorLookup.ShowDialog();
        }

        private void MedicationClick(object sender, RoutedEventArgs e)
        {
            var medicationInfo = new MedicationInfo();
            medicationInfo.ShowDialog();
        }

        private void NotesClick(object sender, RoutedEventArgs e)
        {
            var patientNotes = new PatientNotes();
            patientNotes.ShowDialog();
        }

        private void PillInfoClick(object sender, RoutedEventArgs e)
        {
            var pillInfo = new PillInfo();
            pillInfo.ShowDialog();
        }

        private void AppointmentClick(object sender, RoutedEventArgs e)
        {
            var appointmentInfo = new AppointmentInfo();
            appointmentInfo.ShowDialog();
        }

        private void ViewDicomImagesClick(object sender, RoutedEventArgs e)
        {
            var dicom = new ManagePatientDicom();
            dicom.ShowDialog();
        }
    }
}
