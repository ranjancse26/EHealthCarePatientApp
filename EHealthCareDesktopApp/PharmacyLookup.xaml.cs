using HtmlAgilityPack;
using MahApps.Metro.Controls;
using mshtml;
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
    /// Interaction logic for PharmacyLookup.xaml
    /// </summary>
    public partial class PharmacyLookup : MetroWindow
    {
        public PharmacyLookup()
        {
            InitializeComponent();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            webBrowserControl.Navigate("http://pharmacy.unarxcard.com/");
            webBrowserControl.LoadCompleted += BrowserOnLoadCompleted;
        }

        private void BrowserOnLoadCompleted(object sender, NavigationEventArgs navigationEventArgs)
        {
            var doc = (HTMLDocument)webBrowserControl.Document;
            HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.LoadHtml(doc.documentElement.outerHTML);
            HtmlNode node = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='navbar navbar-fixed-top']");
            node.ParentNode.RemoveChild(node);
            //webBrowserControl.NavigateToString(htmlDoc.DocumentNode.OuterHtml);
        }
    }
}
