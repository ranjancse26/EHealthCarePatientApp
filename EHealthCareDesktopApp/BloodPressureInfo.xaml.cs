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

namespace EHealthCareDesktopApp
{
    /// <summary>
    /// Interaction logic for WeightInfo.xaml
    /// </summary>
    public partial class BloodPressureInfo : MetroWindow
    {
        private DialogWindow dialogWindow;

        public BloodPressureInfo()
        {
            InitializeComponent();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            dialogWindow = new DialogWindow();
            dialogWindow.Message = "Please Wait!";
            dialogWindow.Show();
      
            RefreshWebBrowserControl();
        }

        private void RefreshWebBrowserControl()
        {
            string queryStringParm = string.Format("uniqueGuid={0}&patientId={1}", EHealthCareDesktopApp.Properties.Settings.Default.UniqueIdentifier,
                EHealthCareDesktopApp.Properties.Settings.Default.PatientID);
            webBrowserControl.LoadCompleted += webBrowserControl_LoadCompleted;
            webBrowserControl.Navigate(string.Format("http://ehealthcare.azurewebsites.net/home/BloodPressure?{0}", queryStringParm));
        }

        void webBrowserControl_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            dialogWindow.Close();
        }

        private void AddBloodPressureClick(object sender, RoutedEventArgs e)
        {
            var manageBloodPressure = new ManageBloodPressure();
            manageBloodPressure.BloodPressureAddedEvent += manageBloodPressure_BloodPressureAddedEvent;
            manageBloodPressure.ShowDialog();
        }

        void manageBloodPressure_BloodPressureAddedEvent(string message)
        {
            if (message.Equals("Success"))
                RefreshWebBrowserControl();
            else
            {
                MessageBox.Show(message);
            }
        }
    }
}
