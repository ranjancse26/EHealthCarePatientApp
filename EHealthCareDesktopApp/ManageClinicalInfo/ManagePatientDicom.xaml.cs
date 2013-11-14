using EHealthCareDataAccess;
using MahApps.Metro.Controls;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace EHealthCareDesktopApp.ManageClinicalInfo
{
    /// <summary>
    /// Interaction logic for ManagePatientDicom.xaml
    /// </summary>
    public partial class ManagePatientDicom : MetroWindow
    {
        public ManagePatientDicom()
        {
            InitializeComponent();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            LoadDicom();
        }

        private void LoadDicom()
        {
            try
            {
                var dicomRepository = new DicomDataRepository(EHealthCareDesktopApp.Properties.Settings.Default.UniqueIdentifier);
                var patientDicom = dicomRepository.GetAllDicom(EHealthCareDesktopApp.Properties.Settings.Default.PatientID);
                listView.ItemsSource = patientDicom;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
               
        private void DeleteButtonClick(object sender, RoutedEventArgs e)
        {
            var id = int.Parse(((Button)sender).CommandParameter.ToString());
            var dicomRepository = new DicomDataRepository(EHealthCareDesktopApp.Properties.Settings.Default.UniqueIdentifier);
            dicomRepository.DeleteDicom(EHealthCareDesktopApp.Properties.Settings.Default.PatientID, id);
            LoadDicom();
        }

        public System.Drawing.Image ByteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
            return returnImage;
        }

        private void ViewDicomButtonClick(object sender, RoutedEventArgs e)
        {
            var id = int.Parse(((Button)sender).CommandParameter.ToString());
            var dicomRepository = new DicomDataRepository(EHealthCareDesktopApp.Properties.Settings.Default.UniqueIdentifier);

            try
            {
                var dicom = dicomRepository.GetDicomById(EHealthCareDesktopApp.Properties.Settings.Default.PatientID, id);
                var displayDicomImage = new DisplayDicomImage(ByteArrayToImage(dicom.Dicom1));
                displayDicomImage.ShowDialog();
            }
            catch(Exception ex){
                MessageBox.Show(ex.ToString());
                return;
            }
        }
    }
}
