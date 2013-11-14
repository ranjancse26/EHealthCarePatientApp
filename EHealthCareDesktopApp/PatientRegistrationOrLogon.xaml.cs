using MahApps.Metro.Controls;
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
    /// Interaction logic for PatientRegistrationOrLogon.xaml
    /// </summary>
    public partial class PatientRegistrationOrLogon : MetroWindow
    {
        public PatientRegistrationOrLogon()
        {
            InitializeComponent();
        }

        private void PatientRegistrationButtonClick(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var patientRegistration = new PatientRegistration();
            var status = patientRegistration.ShowDialog();
            this.Close();
        }

        private void LoginButtonClick(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var mainWindow = new MainWindow();
            var status = mainWindow.ShowDialog();
            this.Close();
        }
    }
}
