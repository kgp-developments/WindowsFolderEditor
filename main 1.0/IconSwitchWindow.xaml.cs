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
using System.IO;

namespace main_1._0
{
    /// <summary>
    /// Interaction logic for IconSwitchWindow.xaml
    /// </summary>
    public partial class IconSwitchWindow : Window
    {
        static MainWindow AppMW = (MainWindow)Application.Current.MainWindow;

        public IconSwitchWindow()
        {
            InitializeComponent();
            DisplayOptionalIcons();

        }



        private void DisplayOptionalIcons()
        {
            int count = 0;
            int posY = -90;
            WrapPanel AddedTo = new WrapPanel();
            string[] files = Directory.GetFiles(@"..\..\images\");
            Thickness marg = new Thickness();

            foreach (string file in files)
            {
                if (count%4 == 0)
                {
                    //AppMW.EdycjaDrzewa.Background = Brushes.Red;
                    AddedTo = new WrapPanel();
                    AddedTo.Height = 90;
                    posY += 90;
                    marg.Top = posY;
                    AddedTo.Margin = marg;
                    AddedTo.VerticalAlignment = VerticalAlignment.Top;
                    DisplayedGrid.Children.Add(AddedTo);

                }
                Canvas MainLayer = new Canvas();
                MainLayer.Height = 90;
                MainLayer.Width = 90;
                MainLayer.Tag = file;
                MainLayer.Background = Brushes.Brown;

                Image Icon = new Image();
                Icon.Source = new BitmapImage(new Uri(file, UriKind.Relative));
                Icon.Height = 85;
                Icon.Width = 85;
                marg.Top = 5;
                marg.Left = 5;
                Icon.Margin = marg;
                MainLayer.Children.Add(Icon);
                AddedTo.Children.Add(MainLayer);
                count++;
            }
        }

        private void ApplyChanges_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void Cancel_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }


        private void AlwaysTrueForCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

    }
}
