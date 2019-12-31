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

namespace main_1._0
{
    /// <summary>
    /// Interaction logic for ViewWindow.xaml
    /// </summary>
    public partial class ViewWindow : Window
    {
        static MainWindow AppMW = (MainWindow)Application.Current.MainWindow;
        public Settings settings;
        public string rightHandedView;

        public ViewWindow()
        {
            InitializeComponent();
            settings = AppMW.settings;
            SetShownSettings();

        }

        private void SetShownSettings()
        {
            BrushConverter bc = new BrushConverter();
            LanguageCB.SelectedItem = FindName(settings.language);
            FontSizeCB.SelectedItem = FindName(settings.fontSize);
            OYdisCB.SelectedItem = FindName(settings.OYdis);
            colorCB.SelectedItem = FindName(settings.color);
            if (bool.Parse(settings.RHV))
            {
                righthanded.Background = Brushes.LightGray;
                lefthanded.Background = (Brush)bc.ConvertFrom("#FF7A7878");
                rightHandedView = "true";
            }
            else
            {
                lefthanded.Background = Brushes.LightGray;
                righthanded.Background = (Brush)bc.ConvertFrom("#FF7A7878");
                rightHandedView = "false";
            }

        }

       private  void RestoreDefault_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            righthanded.Background = Brushes.LightGray;
            lefthanded.Background = (Brush)bc.ConvertFrom("#FF7A7878");
            rightHandedView = "true";
            FontSizeCB.SelectedItem = fourteen;
            OYdisCB.SelectedItem = mid;
            colorCB.SelectedItem = dark;
        }

        private void ApplyChanges_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Button Clicked = (Button)e.Parameter;
            string[] newSettings = new string[5];
            newSettings[0] = "language = " + ((ComboBoxItem)LanguageCB.SelectedItem).Name.ToString();
            newSettings[1] = "fontsize = " + ((ComboBoxItem)FontSizeCB.SelectedItem).Name.ToString();
            newSettings[2] = "color = " + ((ComboBoxItem)colorCB.SelectedItem).Name.ToString();
            newSettings[3] = "oydis = " + ((ComboBoxItem)OYdisCB.SelectedItem).Name.ToString();
            newSettings[4] = "rhv = " + rightHandedView;

            settings.ChangeStyleSettings(newSettings);
            settings.GetStyleSettings();
            settings.ApplyStyleMW();

            AppMW.RestoreMainLayout();
            if(Clicked.Name == "OkBtn")
            {
                this.Close();
            }
        }

        private void CancelBtn_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }
        private void RHVbtnSwitch_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Button Clicked = (Button)e.Parameter;
            if(Clicked.Name == "righthanded")
            {
                righthanded.Background = Brushes.LightGray;
                lefthanded.Background = (Brush)bc.ConvertFrom("#FF7A7878");
                rightHandedView = "true";
            }
            else if (Clicked.Name == "lefthanded")
            {
                lefthanded.Background = Brushes.LightGray;
                righthanded.Background = (Brush)bc.ConvertFrom("#FF7A7878");
                rightHandedView = "false";
            }
        }

        private void AlwaysTrueForCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

    }



}
