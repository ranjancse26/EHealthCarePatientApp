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
    /// Interaction logic for WeightInfo.xaml
    /// </summary>
    public partial class DrugSearch : MetroWindow
    {
        private DialogWindow dialogWindow;

        public DrugSearch()
        {
            InitializeComponent();
            webBrowserControl.LoadCompleted += webBrowserControl_LoadCompleted;
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            rdDrugByName.IsChecked = true;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string url = "";

            dialogWindow = new DialogWindow();
            dialogWindow.Message = "Please Wait!";
            dialogWindow.Show();

            if (rdDrugByName.IsChecked != null && (bool)rdDrugByName.IsChecked)
                url = string.Format("http://ehealthcare.azurewebsites.net/drug/drugsearchbyname/{0}", txtDrugSearch.Text.Trim());
            if (rdDrugByNDA.IsChecked != null && (bool)rdDrugByNDA.IsChecked)
                url = string.Format("http://ehealthcare.azurewebsites.net/drug/drugsearchbynda/{0}", txtDrugSearch.Text.Trim());
            if (rdDrugByNDC.IsChecked != null && (bool)rdDrugByNDC.IsChecked)
                url = string.Format("http://ehealthcare.azurewebsites.net/drug/drugsearchbyndc/{0}", txtDrugSearch.Text.Trim());

            if(txtDrugSearch.Text.Trim() != string.Empty)
                webBrowserControl.Navigate(url);
        }

        void webBrowserControl_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            dialogWindow.Close();
        }
    }
}
