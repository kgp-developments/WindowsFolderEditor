using ReFolder.Dir;
using ReFolder.Management;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for LoadTreeWindow.xaml
    /// </summary>
    public partial class LoadTreeWindow : Window
    {
        static MainWindow AppMW = (MainWindow)Application.Current.MainWindow; //odwolanie do MainWindow.xaml.cs


        //zainicjuj
        string DirPath;

        public LoadTreeWindow()
        {
            InitializeComponent();

        }
        public void DisplayStructure(string name) //wyswietla strukture w liscie string name to nazwa struktury, mozesz modyfikowac
        {
            Canvas MainLayer = new Canvas();
            MainLayer.Height = 20;
            TextBlock StructName = new TextBlock();
            StructName.FontSize = 16;
            StructName.Text = name;
            MainLayer.Children.Add(StructName);
            Button Clicker = new Button();
            Clicker.Height = 20;
            Clicker.Width = 330;
            Clicker.Opacity = 0.2f;
            Clicker.Command = KGPcommands.SetChosenLTW;
            Clicker.CommandParameter = Clicker;

            MainLayer.Children.Add(Clicker);
            
            SavedStructuresList.Children.Add(MainLayer);


        }

        private void SetChosenLTW_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Button chosen = (Button)e.Parameter;
            Canvas chosenCanvas = (Canvas)chosen.Parent;
            chosenCanvas.Background = Brushes.LightBlue;

            // to inicjuj
            //DirPath = chosenCanvas.Children[0].;

            //jakas zmienna ktora oznajmia co jest wybrane musisz sobie wymysic, ja nie zdazylem
        }
        private void CanceLTW_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }
        private void LoadLTW_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //AppMW.Seed = SaveAndReadElementInBinaryFile.GetDefaultInstance().ReadFromBinaryFile<MainDir>(____Ścieżka_________________);
            this.Close(); //ostatnia linijka, zamyka okno

        }
        //ma sprawdzac czy cos jest wybrane ; w funkcje SetChosenLTW mozesz dac zmienna czy cos ktora to orzeka
        private void LoadLTW_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (File.Exists(DirPath))
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }
        }

        private void AlwaysTrueForCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

    }
}
