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
using ReFolder.Dir;
using ReFolder.Dir.Description;
using ReFolder.Management;

namespace main_1._0
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    
        
    public partial class MainWindow : Window
    {
        // folder główny z którego rozpoczyna się dziedziczenie 
        IEditableDirWithChildren Main;
        List<StackPanel> Panels = new List<StackPanel>(); // lista paneli z gornego paska, np. plik, widok
        bool rightHandedView = true; // zmienna okreslajaca widok (leworeczny/praworeczny)
        public static Canvas CurrentlyChosen = null;
        public IEditableDirWithChildren CurrentlyChosenDir = null;
        IEditableDirWithChildren Seed;
        List<IEditableDirWithChildrenAndParrent> CopyOfChildren;

        // zawiera inicjalizację Dirów do testu
        public MainWindow()
        {
            InitializeComponent();

            Panels.Add(PanelPliku);
            Panels.Add(PanelWidoku);
            HideAllPanels();

            #region inicjalizacja  ChildDirów do testu

            Main = new MainDir(new DirDescription(@"C:\Users\Klakier\Desktop\kociFolderek","kociFolderek"));
            Seed = Main;

            ChildDir f1 = new ChildDir("f1", Main);
            Main.Children.Add(f1);
            ChildDir f2 = new ChildDir("f2", Main);
            Main.Children.Add(f2);
            ChildDir f3 = new ChildDir("f3", Main);
            Main.Children.Add(f3);

            ChildDir f11 = new ChildDir("f11", f1);
            f1.Children.Add(f11);

            ChildDir f31 = new ChildDir("f31", f3);
            f3.Children.Add(f31);


            ChildDir f12 = new ChildDir("f12", f1);
            f1.Children.Add(f12);
            ChildDir f13 = new ChildDir("f13", f1);
            f1.Children.Add(f13);
            ChildDir f14 = new ChildDir("f13", f1);
            f1.Children.Add(f14);


            ChildDir f21 = new ChildDir("f111", f2);
            f2.Children.Add(f21);
            ChildDir f22 = new ChildDir("f222", f2);
            f2.Children.Add(f22);

            ChildDir f211 = new ChildDir("f333", f21);
            f21.Children.Add(f211);
            ChildDir f212 = new ChildDir("f444", f21);
            f21.Children.Add(f212);

            ChildDir f131 = new ChildDir("f555", f13);
            f13.Children.Add(f131);
            ChildDir f132 = new ChildDir("f666", f13);
            f13.Children.Add(f132);
            ChildDir f133 = new ChildDir("f777", f13);
            f13.Children.Add(f133);

            ChildDir f1331 = new ChildDir("f8880", f133);
            f133.Children.Add(f1331);
            ChildDir f1332 = new ChildDir("f999", f133);
            f133.Children.Add(f1332);

            ChildDir f2111 = new ChildDir("r10001", f211);
            f211.Children.Add(f2111);

            #endregion


            Sorteritno sorteritno = new Sorteritno();
            sorteritno.Create(ResTree, 30, 0, Seed);

            sorteritno.Sort(Seed, ResTree, 0, 30);



        }

        #region funkcje składowe komend
        private void HideAllPanels()
        {
            foreach (StackPanel element in Panels)
            {
                element.Visibility = Visibility.Collapsed;
            }
        }
        #endregion

        // funkcje komend
        #region funkcje komend _Executed
        private void GenerateDirs_Executed(object sender,ExecutedRoutedEventArgs e)
        {
            DirManagement management = DirManagement.GetDefaultInstance();
            MemoryDirs memoryDirs = MemoryDirs.GetDefaultInstance();
            memoryDirs.InitializeAllChildren(Main);
            DirWrite.GetDefaultInstance().GenerateAllChildrenDirsAsFolders();
        }
        private void CopyChildrenDirs_Executed(object sender, ExecutedRoutedEventArgs e)
        {

            SaveAndReadElementInBinaryFile.GetDefaultInstance()
                .WriteToBinaryFile<IEditableDirWithChildren>(@"..\..\..\TemporaryFiles\tempFile~Copy", CurrentlyChosenDir);
        }
        private void PasteChildrenDirs_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            List<IEditableDirWithChildrenAndParrent> CopyOfChildren =
                SaveAndReadElementInBinaryFile.GetDefaultInstance()
                .ReadFromBinaryFile<IEditableDirWithChildren>(@"C:..\..\..\TemporaryFiles\tempFile~Copy")
                .Children;

            var validate= DirValidate.GetDefaultInstance();
            foreach (IEditableDirWithChildrenAndParrent child in CopyOfChildren)
            {

                if (validate.IsDirExistingAsFolderAndChild(CurrentlyChosenDir, child.Description.Name))
                {
                    child.Description.Name = DirManagement.GetDefaultInstance().GeneratetName_Default(CurrentlyChosenDir,1,child.Description.Name);
                }

                child.ParrentDir = CurrentlyChosenDir;
            }
            CurrentlyChosenDir.AddChildrenToChildrenList(CopyOfChildren);
            MainDir.AutoGenerateChildrenFullName(CurrentlyChosenDir);
            Sorteritno ToSort = new Sorteritno();
            ToSort.ResetTree(ResTree, ResetHighlight, Seed, drzewo);
        }
        private void ShowHide_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            StackPanel PanelGiven = (StackPanel)e.Parameter;
            if (PanelGiven.IsVisible)
            {
                PanelGiven.Visibility = Visibility.Collapsed;
            }
            else
            {
                PanelGiven.Visibility = Visibility.Visible;
                foreach(StackPanel element in Panels)
                {
                    if (element.Name != PanelGiven.Name)
                    {
                        element.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }
        private void HorizontalStyleSwitch_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (rightHandedView)
            {
                RamkaDrzewa.Margin = new Thickness(190, 15, 0, 61);
                EdycjaDrzewa.HorizontalAlignment = HorizontalAlignment.Left;
                EdycjaFolderu.HorizontalAlignment = HorizontalAlignment.Right;
                KomendyUzytkownika.Margin = new Thickness(190, 0, 420, 0);
                rightHandedView = false;
            }
            else
            {
                RamkaDrzewa.Margin = new Thickness(0, 15, 190, 61);
                EdycjaDrzewa.HorizontalAlignment = HorizontalAlignment.Right;
                EdycjaFolderu.HorizontalAlignment = HorizontalAlignment.Left;
                KomendyUzytkownika.Margin = new Thickness(420, 0, 190, 0);
                rightHandedView = true;
            }
        }
        private void HighlightChosen_Executed(object sender, ExecutedRoutedEventArgs e)
        {

                
            Button HiddenButton = (Button)e.Parameter;
            Canvas MainLayer = (Canvas)HiddenButton.Parent;
            if(CurrentlyChosen != null )
            {

                CurrentlyChosen.Background = Brushes.LightGray;
                CurrentlyChosenDir = (IEditableDirWithChildren)CurrentlyChosen.Tag;
                CurrentlyChosenDir.IsMarked = false;

            }
            MainLayer.Background = Brushes.Blue;
            CurrentlyChosen = MainLayer;

                CurrentlyChosenDir = (IEditableDirWithChildren)CurrentlyChosen.Tag;
                CurrentlyChosenDir.IsMarked = true;
        }
        private void ResetHighlight_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if(CurrentlyChosen != null)
            {
                CurrentlyChosen.Background = Brushes.LightGray;
                CurrentlyChosenDir = (IEditableDirWithChildren)CurrentlyChosen.Tag;
                CurrentlyChosenDir.IsMarked = false;
                CurrentlyChosen = null;
                CurrentlyChosenDir = null;
            }
            HideAllPanels();
        }
        private void ViewContent_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Button ViewContentButton = (Button)e.Parameter;
            Canvas MainLayer = (Canvas)ViewContentButton.Parent;
            IEditableDirWithChildren FolderContained = (IEditableDirWithChildren)MainLayer.Tag;
            if (FolderContained.ShowContent)
            {
                FolderContained.ShowContent = false;
            }
            else
            {
                FolderContained.ShowContent = true;
            }
            Sorteritno Temporary = new Sorteritno();
            Temporary.ResetTree(ResTree, ResetHighlight, Seed, drzewo);
        }
        private void DefaultAddition_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            string name= DirManagement.GetDefaultInstance().GeneratetName_Default(CurrentlyChosenDir);
            IEditableDirWithChildrenAndParrent NewDir = new ChildDir(name, CurrentlyChosenDir);
            CurrentlyChosenDir.AddChildToChildrenList(NewDir);
            Sorteritno ToSort = new Sorteritno();
            ToSort.ResetTree(ResTree, ResetHighlight, Seed, drzewo);
        }
        #endregion

        #region funkcje komend _CanExecute
        private void ChosenNotNullDepended(object sender, CanExecuteRoutedEventArgs e)
        {
            if(CurrentlyChosen == null)
            {
                e.CanExecute = false;
            }
            else
            {
                e.CanExecute = true;
            }
        }
        private void ViewContent_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            Button ViewContentButton = (Button)e.Parameter;
            Canvas MainLayer = (Canvas)ViewContentButton.Parent;
            IEditableDirWithChildren FolderContained = (IEditableDirWithChildren)MainLayer.Tag;
            if (FolderContained.Children.Count == 0)
            {
                e.CanExecute = false;
            }
            else
            {
                e.CanExecute = true;
            }
        }
        private void CopyChildrenDirs_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            if (CurrentlyChosen == null) e.CanExecute = false;
            else
            {
                Console.WriteLine("no i pyk dane do kieszeni");
                e.CanExecute = true;
            }

        }
        private void PasteChildrenDirs_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (CurrentlyChosenDir == null && (CopyOfChildren == null || CopyOfChildren.Count == 0)) e.CanExecute = false;
            else e.CanExecute = true;
        }
        private void AlwaysTrueForExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        #endregion

    }



}
