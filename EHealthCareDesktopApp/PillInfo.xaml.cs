using MahApps.Metro.Controls;
using System.Collections.Generic;
using System.Windows;
using Windows.UI.Xaml.Controls;

namespace EHealthCareDesktopApp
{
    /// <summary>
    /// Interaction logic for PillInfo.xaml
    /// </summary>
    public partial class PillInfo : MetroWindow
    {
        private Dictionary<string, string> ColorDictionary = new Dictionary<string, string>();
        private Dictionary<string, string> ShapeDictionary = new Dictionary<string, string>();
        private DialogWindow dialogWindow;

        public PillInfo()
        {
            InitializeComponent();
            LoadColorDictionary();
            LoadShapeDictionary();
            webBrowserControl.LoadCompleted += webBrowserControl_LoadCompleted;
        }

        private void LoadColorDictionary()
        {
            ColorDictionary.Add("BLACK", "C48323");
            ColorDictionary.Add("BLUE", "C48333");
            ColorDictionary.Add("BROWN", "C48332");
            ColorDictionary.Add("GRAY", "C48324");
            ColorDictionary.Add("GREEN", "C48329");
            ColorDictionary.Add("ORANGE", "C48331");
            ColorDictionary.Add("PINK", "C48328");
            ColorDictionary.Add("PURPLE", "C48327");
            ColorDictionary.Add("RED", "C48326");
            ColorDictionary.Add("TURQUOISE", "C48334");
            ColorDictionary.Add("WHITE", "C48325");
            ColorDictionary.Add("YELLOW", "C48330");
        }

        private void LoadShapeDictionary()
        {
            ShapeDictionary.Add("BULLET", "C48335");
            ShapeDictionary.Add("CAPSULE", "C48336");
            ShapeDictionary.Add("CLOVER", "C48337");
            ShapeDictionary.Add("DIAMOND", "C48338");
            ShapeDictionary.Add("DOUBLE CIRCLE", "C48339");
            ShapeDictionary.Add("FREEFORM", "C48340");
            ShapeDictionary.Add("GEAR", "C48341");
            ShapeDictionary.Add("HEPTAGON", "C48342");
            ShapeDictionary.Add("HEXAGON", "C48343");
            ShapeDictionary.Add("OCTAGON", "C48344");
            ShapeDictionary.Add("OVAL", "C48345");
            ShapeDictionary.Add("PENTAGON", "C48346");
            ShapeDictionary.Add("RECTANGLE", "C48347");
            ShapeDictionary.Add("ROUND", "C48348");
            ShapeDictionary.Add("SEMICIRCLE", "C48349");
            ShapeDictionary.Add("SQUARE", "C48350");
            ShapeDictionary.Add("TEAR", "C48351");
            ShapeDictionary.Add("TRAPEZOID", "C48352");
            ShapeDictionary.Add("TRIANGLE", "C48353");
        }

        private void LoadColor()
        {
            cmbColor.ItemsSource = new PillColor().GetPillColor();
            cmbColor.SelectedValuePath = "Value";
            cmbColor.DisplayMemberPath = "Name";
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            LoadColor();          
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            string queryStringParameters = "";

            var item = cmbShape.SelectedValue as System.Windows.Controls.ComboBoxItem;
            if (item != null)
            {
                var stackpanel = item.Content as System.Windows.Controls.StackPanel;
                var selectedContent = (stackpanel.Children[0] as System.Windows.Controls.ContentPresenter).Content;
                queryStringParameters = string.Format("shape={0}", ShapeDictionary[selectedContent.ToString().ToUpper()]);
            }

            if (cmbColor.SelectedIndex > -1)
            {
                queryStringParameters += string.Format("&color={0}", cmbColor.SelectedValue.ToString());
            }

            if(txtIngredient.Text != "")
                queryStringParameters += string.Format("&ingredient={0}", txtIngredient.Text);

            dialogWindow = new DialogWindow();
            dialogWindow.Message = "Please Wait!";
            dialogWindow.Show();

            webBrowserControl.Navigate("http://ehealthcare.azurewebsites.net/PillIdentifier?" + queryStringParameters);
        }

        void webBrowserControl_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            dialogWindow.Close();
        }
    }

    public class PillColor{

        List<PillColor>  pillColorList = new List<PillColor>();
        public string Name { get; set; }
        public string Value { get; set; }

        public PillColor() { }

        public PillColor(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        public List<PillColor> GetPillColor()
        {
            pillColorList.Add(new PillColor("BLACK", "C48323"));
            pillColorList.Add(new PillColor("BLUE", "C48333"));
            pillColorList.Add(new PillColor("BROWN", "C48332"));
            pillColorList.Add(new PillColor("GRAY", "C48324"));
            pillColorList.Add(new PillColor("GREEN", "C48329"));
            pillColorList.Add(new PillColor("ORANGE", "C48331"));
            pillColorList.Add(new PillColor("PINK", "C48328"));
            pillColorList.Add(new PillColor("PURPLE", "C48327"));
            pillColorList.Add(new PillColor("RED", "C48326"));
            pillColorList.Add(new PillColor("TURQUOISE", "C48334"));
            pillColorList.Add(new PillColor("WHITE", "C48325"));
            pillColorList.Add(new PillColor("YELLOW", "C48330"));
            return pillColorList;
        }
    }
}
