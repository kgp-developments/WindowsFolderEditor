using ReFolder.Dir;
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
            AppMW.Seed = DirManagement.GetDefaultInstance().GetFolderAsNewMainDir(SeedLocation.Text);
            if (AppMW.Seed != null) //warunek zbey nie wypierdalalo bledu, ponizej rysowanie
            {

                AppMW.sorteritno.ResetTree(AppMW.ResTree, AppMW.ResetHighlight,AppMW.Seed, AppMW.drzewo, "MW" );
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

        }
        //warunek sprawdzajacy czy podana sciezka istnieje/ jest poprawna    
        public void CreateCNTW_CanExecute(object sender, CanExecuteRoutedEventArgs e) 
        {
            bool flag = false;
            try
            {
                flag = DirValidate.GetDefaultInstance().IsfolderExisting(SeedLocation.Text);

            }
            catch (Exception)
            {  }
            if (flag)
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }
        }
        //olej
        public void AlwaysTrueForCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}
/*            if (DirValidate.GetDefaultInstance().IsfolderExisting(SeedLocation.Text))
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }*/