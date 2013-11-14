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
using Windows.Devices.Geolocation;

namespace EHealthCareDesktopApp
{
    /// <summary>
    /// Interaction logic for DoctorsLookup.xaml
    /// </summary>
    public partial class DoctorsLookup : MetroWindow
    {
        private DialogWindow dialogWindow;

        private Geolocator geolocator = null;

        private List<string> regionsList = new List<string>{
            "",
             "CA",
            "NY",
            "FL",
            "TX",
            "PA",
            "IL",
            "OH",
            "MA",
            "MI",
            "WA",
            "NC",
            "NJ",
            "GA",
            "MD",
            "VA",
            "WI",
            "CO",
            "MN",
            "TN",
            "AZ",
            "MO",
            "OR",
            "IN",
            "CT",
            "OK",
            "KY",
            "SC",
            "LA",
            "AR",
            "AL",
            "NM",
            "KS",
            "UT",
            "NV",
            "IA",
            "DC",
            "ME",
            "MS",
            "NE",
            "NH",
            "WV",
            "HI",
            "RI",
            "ID",
            "DE",
            "AK",
            "MT",
            "PR",
            "ND",
            "SD"
        };

        private List<string> insurancesList = new List<string>{
            "",
            "CIGNA",
            "AETNA",
            "UNITEDHEALTHCARE",
            "HUMANA",
            "BLUE CROSS AND BLUE SHIELD",
            "MULTIPLAN",
            "BLUE CROSS BLUE SHIELD",
            "COVENTRY HEALTH CARE",
            "MEDICAID",
            "MEDICARE",
            "PHCS",
            "GREAT WEST HEALTHCARE",
            "WELLPOINT",
            "FIRST HEALTH NETWORK",
            "HEALTHNET",
            "GUARDIAN",
            "DELTA DENTAL",
            "MET-LIFE",
            "UNITED CONCORDIA",
            "AMERITAS",
            "UNICARE",
            "UNITEDHEALTHCARE DENTAL",
            "GHI",
            "OXFORD HEALTH PLANS",
            "ASSURANT HEALTH",
            "DENTEMAX DENTAL",
            "ANTHEM",
            "PACIFICARE",
            "RAILROAD MEDICARE",
            "COVENTRY HEALTHCARE",
            "MEDICAL MUTUAL OF OHIO",
            "WELLCARE",
            "PRINCIPAL FINANCIAL",
            "GREAT-WEST HEALTHCARE",
            "AMERIGROUP",
            "WORKERS' COMPENSATION",
            "BLUE CROSS - CALIFORNIA",
            "KAISER PERMANENTE",
            "WELLCARE HEALTH PLANS",
            "BCBS",
            "OXFORD",
            "PPOM",
            "MOLINA",
            "THE GUARDIAN LIFE INSURANCE COMPANY - AMERICA",
            "PACIFICSOURCE",
            "SAGAMORE HEALTH NETWORK",
            "CENTENE",
            "FIRST CHOICE HEALTH",
            "PROVIDENCE HEALTH PLANS",
            "EVERCARE"
        };

        private List<string> localitiesList = new List<string>{
            "",
            "NEW YORK",
            "BROOKLYN",
            "CHICAGO",
            "LOS ANGELES",
            "HOUSTON",
            "PHILADELPHIA",
            "PORTLAND",
            "WASHINGTON",
            "BOSTON",
            "SAN FRANCISCO",
            "SAN DIEGO",
            "SEATTLE",
            "ROCHESTER",
            "DALLAS",
            "BRONX",
            "BALTIMORE",
            "SAINT LOUIS",
            "COLUMBUS",
            "PHOENIX",
            "MINNEAPOLIS",
            "PITTSBURGH",
            "CLEVELAND",
            "SAN ANTONIO",
            "MIAMI",
            "ATLANTA",
            "INDIANAPOLIS",
            "LAS VEGAS",
            "SPRINGFIELD",
            "SACRAMENTO",
            "DENVER",
            "MILWAUKEE",
            "CINCINNATI",
            "ALBUQUERQUE",
            "OKLAHOMA CITY",
            "LOUISVILLE",
            "TAMPA",
            "JACKSONVILLE",
            "AUSTIN",
            "SALT LAKE CITY",
            "NASHVILLE",
            "COLUMBIA",
            "TUCSON",
            "BUFFALO",
            "DETROIT",
            "CHARLOTTE",
            "RICHMOND",
            "MADISON",
            "KANSAS CITY",
            "ORLANDO",
            "OAKLAND"
       };

        public DoctorsLookup()
        {
            InitializeComponent();
            geolocator = new Geolocator();
            webBrowserControl.LoadCompleted += webBrowserControl_LoadCompleted;
            cmbRegions.ItemsSource = regionsList;
            cmbInsurances.ItemsSource = insurancesList;
            cmbLocalities.ItemsSource = localitiesList;
        }

        async private void btnGetLocation_Click(object sender, RoutedEventArgs e)
        {
            Geoposition pos = await geolocator.GetGeopositionAsync();
            txtLatitude.Text = pos.Coordinate.Latitude.ToString();
            txtLongitude.Text = pos.Coordinate.Longitude.ToString();       
        }

        private void btnSearchProviders(object sender, RoutedEventArgs e)
        {
            RefreshWebBrowserControl();

            dialogWindow = new DialogWindow();
            dialogWindow.Message = "Please Wait!";
            dialogWindow.Show();
        }
        
        void webBrowserControl_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            if(dialogWindow != null)
                dialogWindow.Close();
        }

        private void RefreshWebBrowserControl()
        {
            string queryStringParms = "";

            if (txtLatitude.Text != "" && double.Parse(txtLatitude.Text) != 0.0)
            {
                queryStringParms = "Latitude=" + txtLatitude.Text;
                if (txtLongitude.Text != "" && double.Parse(txtLongitude.Text) != 0.0)
                    queryStringParms = queryStringParms + "&Longitude=" + txtLongitude.Text;
            }

            if (queryStringParms.Length > 0 && cmbLocalities.SelectedIndex > 0)
                queryStringParms = queryStringParms + "&Locality=" + cmbLocalities.SelectedItem.ToString().Replace("\"", "");
            else if (cmbLocalities.SelectedIndex > 0)
                queryStringParms = "Locality=" + cmbLocalities.SelectedItem.ToString();

            if (queryStringParms.Length > 0 && cmbInsurances.SelectedIndex > 0)
                queryStringParms = queryStringParms + "&Insurances=" + cmbInsurances.SelectedItem.ToString().Replace("\"", "");
            else if (cmbInsurances.SelectedIndex > 0)
                queryStringParms = "Insurances=" + cmbInsurances.SelectedItem.ToString();

            if (queryStringParms.Length > 0 && cmbRegions.SelectedIndex > 0)
                queryStringParms = queryStringParms + "&Region=" + cmbRegions.SelectedItem.ToString().Replace("\"", "");
            else if (cmbRegions.SelectedIndex > 0)
                queryStringParms = "Region=" + cmbRegions.SelectedItem.ToString();

            queryStringParms = queryStringParms + "&Meters="+txtMeters.Text.Trim();

            webBrowserControl.Navigate(string.Format("http://ehealthcare.azurewebsites.net/healthcareprovider/getallproviders?{0}", queryStringParms));
        }
    }
}
