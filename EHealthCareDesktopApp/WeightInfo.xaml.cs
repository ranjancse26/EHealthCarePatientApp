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
    public partial class WeightInfo : MetroWindow
    {
        private DialogWindow dialogWindow;

        public WeightInfo()
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
            webBrowserControl.Navigate(string.Format("http://ehealthcare.azurewebsites.net/home/weight?{0}", queryStringParm));
        }

        void webBrowserControl_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            dialogWindow.Close();
        }

        private void AddWeightClick(object sender, RoutedEventArgs e)
        {
            var manageWeight = new ManageWeight();
            manageWeight.WeightAddedEvent += manageWeight_WeightAddedEvent;
            manageWeight.ShowDialog();
        }

        void manageWeight_WeightAddedEvent(string message)
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
