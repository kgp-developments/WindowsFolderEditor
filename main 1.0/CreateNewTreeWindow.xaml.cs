using ReFolder.Dir;
using ReFolder.Management;
using System;
using System.Windows;
using System.Windows.Input;


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
                AppMW.sorteritno.ResetTree(AppMW.ResTree, AppMW.ResetHighlight, AppMW.Seed, AppMW.drzewo, "MW");
            }
            string filePath = @"..\..\saved\" + StructureName.Text;
            SaveAndReadElementInBinaryFile.GetDefaultInstance().WriteToBinaryFile<IEditableDirWithChildren>(filePath, AppMW.Seed);
            AppMW.thisStructureName = StructureName.Text;
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
            System.Windows.Forms.FolderBrowserDialog x = new System.Windows.Forms.FolderBrowserDialog();
            x.ShowDialog();
            SeedLocation.Text = x.SelectedPath;
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
            { }
            if (flag && StructureName.Text.Length > 0)
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