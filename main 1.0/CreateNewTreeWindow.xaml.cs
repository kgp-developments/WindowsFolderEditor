using ReFolder.Management;
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
    /// Interaction logic for CreateNewTreeWindow.xaml
    /// </summary>
    public partial class CreateNewTreeWindow : Window
    {
        static MainWindow AppMW = (MainWindow)Application.Current.MainWindow;//odwolanie do MainWindow.xaml.cs

        public CreateNewTreeWindow()
        {
            InitializeComponent();
        }

        //funkcja podpieta do przycisku Utworz
        public void CreateCNTW_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (AppMW.Seed != null) //warunek zbey nie wypierdalalo bledu, ponizej rysowanie
            {
                AppMW.sorteritno.Create(AppMW.ResTree, 30, 0, AppMW.Seed, "MW");
                AppMW.sorteritno.Sort(AppMW.Seed, AppMW.ResTree, 0, 30, "MW");
            }
            this.Close(); //ostatnia linijka - zamyka okno 
        }
        //olej
        public void CancelCNTW_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close(); //ostatnia linijka - zamyka okno 
        }
        //funkcja do popdieta do przycisku wywolania szukania folderu w systemie
        public void BrowseCNTW_Executed(object sender, ExecutedRoutedEventArgs e)
        {
        //    SaveAndReadElementInBinaryFile.GetDefaultInstance().WriteToBinaryFile<MainDir>(_______ŚCIEŻKA_______, AppMW.Seed);
        }
        //warunek sprawdzajacy czy podana sciezka istnieje/ jest poprawna    
        public void CreateCNTW_CanExecute(object sender, CanExecuteRoutedEventArgs e) //warunek sprawdzajacy czy podana sciezka istnieje/ jest poprawna
        {
/*            if (AppMW.Seed.Children.Count > 0 && DirValidate.GetDefaultInstance().IsfolderExisting(_____ŚCIEŻKA______))
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }*/
            //e.canexecute = true - mozna kliknac, false - nie

        }
        //olej
        public void AlwaysTrueForCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}
