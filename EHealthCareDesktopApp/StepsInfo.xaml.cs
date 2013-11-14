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
    public partial class StepsInfo : MetroWindow
    {
        private DialogWindow dialogWindow;

        public StepsInfo()
        {
            InitializeComponent();
            webBrowserControl.LoadCompleted += webBrowserControl_LoadCompleted;
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
            webBrowserControl.Navigate(string.Format("http://ehealthcare.azurewebsites.net/home/steps?{0}", queryStringParm));
        }
        
        void webBrowserControl_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            dialogWindow.Close();
        }

        private void AddStepsClick(object sender, RoutedEventArgs e)
        {
            var manageSteps = new ManageSteps();
            manageSteps.StepsAddedEvent += manageSteps_StepsAddedEvent;
            manageSteps.ShowDialog();
        }

        void manageSteps_StepsAddedEvent(string message)
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
