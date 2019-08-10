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
        IEditableDirWithChildren mainn;
        List<StackPanel> Panele = new List<StackPanel>(); // lista paneli z gornego paska, np. plik, widok
        bool rightHandedView = true; // zmienna okreslajaca widok (leworeczny/praworeczny)
        Canvas CurrentlyChosen = null;
        IEditableDirWithChildren seed;

        // zawiera inicjalizację Dirów do testu
        public MainWindow()
        {
            InitializeComponent();

            Panele.Add(PanelPliku);
            Panele.Add(PanelWidoku);
            HideAllPanels();

            #region inicjalizacja  ChildDirów do testu

            mainn = new MainDir(new DirDescription(@"C:\Users\Klakier\Desktop\kociFolderek","kociFolderek"));
            seed = mainn;

            ChildDir f1 = new ChildDir("f1", mainn);
            mainn.Children.Add(f1);
            ChildDir f2 = new ChildDir("f2", mainn);
            mainn.Children.Add(f2);
            ChildDir f3 = new ChildDir("f3", mainn);
            mainn.Children.Add(f3);

            ChildDir f31 = new ChildDir("f31", f3);
            f3.Children.Add(f31);

            ChildDir f11 = new ChildDir("f11", f1);
            f1.Children.Add(f11);
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

            ChildDir f1331 = new ChildDir("f888", f133);
            f133.Children.Add(f1331);
            ChildDir f1332 = new ChildDir("f999", f133);
            f133.Children.Add(f1332);

            ChildDir f2111 = new ChildDir("f10000", f211);
            f211.Children.Add(f2111);
          
            #endregion


            Sorteritno x = new Sorteritno();
            x.Create(can, 30, 0, seed);
            //yas.Text = f13.name;

            x.Sort(seed, can, 0, 30);

        }


        // funkcje komend
        #region funkcje komend

        // ukryj/pokaz panel
        private void ShowHide(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void GenerateDirs_Executed(object sender,ExecutedRoutedEventHandler e)
        {
            DirManagement management = DirManagement.GetDefaultInstance();
            DirManagement.MemoryDirs memoryDirs = DirManagement.MemoryDirs.GetInstance();
            memoryDirs.InitializeAllChildren(mainn);
            management.GenerateAllChildrenDirsAsFolders();
        }
        private void ViewContent_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            Button xyz = (Button)e.Parameter;
            Canvas zyx = (Canvas)xyz.Parent;
            IEditableDirWithChildren fff = (IEditableDirWithChildren)zyx.Tag;
            if (fff.Children.Count == 0)
            {
                e.CanExecute = false;
            }
            else
            {
                e.CanExecute = true;
            }
        }

        private void HideAllPanels()
        {
            foreach(StackPanel element in Panele)
            {
                element.Visibility = Visibility.Collapsed;
            }
        }

        private void ShowHide_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            StackPanel kurwa = (StackPanel)e.Parameter;
            if (kurwa.IsVisible)
            {
                kurwa.Visibility = Visibility.Collapsed;
            }
            else
            {
                kurwa.Visibility = Visibility.Visible;
                foreach(StackPanel element in Panele)
                {
                    if (element.Name != kurwa.Name)
                    {
                        element.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }


        //do usuniecia // po usunięciu wywala błąd przy kompilacji
        private void testkol(object sender, ExecutedRoutedEventArgs e)
        {
            drzewo.Background = Brushes.PaleVioletRed;
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
            Button xyz = (Button)e.Parameter;
            Canvas zyx = (Canvas)xyz.Parent;
            if(CurrentlyChosen != null)
            {
                CurrentlyChosen.Background = Brushes.LightGray;
            }
            
            zyx.Background = Brushes.Blue;
            CurrentlyChosen = zyx;
        }

        private void ResetHighlight_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if(CurrentlyChosen != null)
            {
                CurrentlyChosen.Background = Brushes.LightGray;
                CurrentlyChosen = null;
            }
        }
        private void ViewContent_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Button xyz = (Button)e.Parameter;
            Canvas zyx = (Canvas)xyz.Parent;
            IEditableDirWithChildren fff = (IEditableDirWithChildren)zyx.Tag;
            if (fff.ShowContent)
            {
                fff.ShowContent = false;
            }
            else
            {
                fff.ShowContent = true;
            }
            Sorteritno x = new Sorteritno();
            x.ResetTree(can, ResetHighlight, ResTree, seed);
        }
        #endregion
    }



}
