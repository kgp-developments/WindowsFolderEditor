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
        Canvas ChosenCanvas;
        static string saved_path = @"..\..\saved\";

        public LoadTreeWindow()
        {

            InitializeComponent();
            //AppMW.FolderSearchTB.Text = files[0];
            DisplayAll();
        }
        private void DisplayAll()
        {
            string[] files = Directory.GetFiles(saved_path);
            foreach (string file in files)
            {
                string actualName = file.Substring(saved_path.Length);
                DisplayStructure(actualName, file);
            }
        }
        public void DisplayStructure(string name, string path) //wyswietla strukture w liscie string name to nazwa struktury, mozesz modyfikowac
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

            MainLayer.Tag = path;
            MainLayer.Children.Add(Clicker);
            
            SavedStructuresList.Children.Add(MainLayer);


        }

        private void SetChosenLTW_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Button chosen = (Button)e.Parameter;
            Canvas chosensCanvas = (Canvas)chosen.Parent;

            if (ChosenCanvas != null)
            {
                ChosenCanvas.Background = null;
            }

            chosensCanvas.Background = Brushes.LightBlue;
            ChosenCanvas = chosensCanvas;
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
            if (ChosenCanvas != null)
            {
                AppMW.Seed = SaveAndReadElementInBinaryFile.GetDefaultInstance().ReadFromBinaryFile<MainDir>((string)ChosenCanvas.Tag);
            }
            AppMW.sorteritno.ResetTree(AppMW.ResTree, AppMW.ResetHighlight, AppMW.Seed, AppMW.drzewo, "MW");
            AppMW.thisStructureName = (string)ChosenCanvas.Tag.ToString().Substring(saved_path.Length);
            AppMW.CurrentlyChosenDir = null;
            this.Close(); //ostatnia linijka, zamyka okno

        }
        //ma sprawdzac czy cos jest wybrane ; w funkcje SetChosenLTW mozesz dac zmienna czy cos ktora to orzeka
        private void LoadLTW_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            //File.Exists(DirPath)
            if (ChosenCanvas != null)
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
