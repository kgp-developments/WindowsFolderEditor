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
        string chosenIcon;
        Canvas ChosenCanvas;
        public IconSwitchWindow()
        {
            InitializeComponent();
            chosenIcon = AppMW.CurrentlyChosenDir.Description.IconAddress;
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
                if (file == chosenIcon)
                {
                    MainLayer.Background = Brushes.Blue;
                    ChosenCanvas = MainLayer;
                }
                //MainLayer.Background = Brushes.Brown;

                Image Icon = new Image();
                Icon.Source = new BitmapImage(new Uri(file, UriKind.Relative));
                Icon.Height = 80;
                Icon.Width = 80;
                marg.Top = 5;
                marg.Left = 5;
                Icon.Margin = marg;

                Button Highlight = new Button();
                Highlight.Height = 90;
                Highlight.Width = 90;
                Highlight.Opacity = 0;
                Highlight.Command = KGPcommands.HighlightChosen;
                Highlight.CommandParameter = Highlight;

                MainLayer.Children.Add(Icon);
                MainLayer.Children.Add(Highlight);
                AddedTo.Children.Add(MainLayer);


                count++;
            }
        }

        private void ApplyChanges_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AppMW.CurrentlyChosenDir.Description.IconAddress = chosenIcon;
            AppMW.sorteritno.ResetTree(AppMW.ResTree, AppMW.ResetHighlight, AppMW.Seed, AppMW.drzewo, "MW");
            this.Close();
        }

        private void Cancel_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        private void HighlightChosen_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ChosenCanvas.Background = null;

            Button Clicked = (Button)e.Parameter;
            Canvas MainLayer = (Canvas)Clicked.Parent;
            MainLayer.Background = Brushes.Blue;
            ChosenCanvas = MainLayer;

            chosenIcon = (string)MainLayer.Tag;
        }


        private void AlwaysTrueForCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

    }
}
