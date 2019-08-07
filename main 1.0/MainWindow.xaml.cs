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

namespace main_1._0
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    
        
    public partial class MainWindow : Window
    {
        List<StackPanel> Panele = new List<StackPanel>(); // lista paneli z gornego paska, np. plik, widok
        bool rightHandedView = true; // zmienna okreslajaca widok (leworeczny/praworeczny)
        Canvas CurrentlyChosen = null;
        Folder seed;
        public MainWindow()
        {
            InitializeComponent();

            Panele.Add(PanelPliku);
            Panele.Add(PanelWidoku);
            HideAllPanels();



            Folder mainn = new Folder("mainn", null);
            seed = mainn;
            
            Folder f1 = new Folder("f1", null);
            mainn.Dzieci.Add(f1);
            Folder f2 = new Folder("f2", null);
            mainn.Dzieci.Add(f2);
            Folder f3 = new Folder("f3", null);
            mainn.Dzieci.Add(f3);

            Folder f31 = new Folder("f31", null);
            f3.Dzieci.Add(f31);

            Folder f11 = new Folder("f11", null);
            f1.Dzieci.Add(f11);
            Folder f12 = new Folder("f12", null);
            f1.Dzieci.Add(f12);
            Folder f13 = new Folder("f13", null);
            f1.Dzieci.Add(f13);
            Folder f14 = new Folder("f13", null);
            f1.Dzieci.Add(f14);


            Folder f21 = new Folder("mainn", null);
            f2.Dzieci.Add(f21);
            Folder f22 = new Folder("mainn", null);
            f2.Dzieci.Add(f22);

            Folder f211 = new Folder("mainn", null);
            f21.Dzieci.Add(f211);
            Folder f212 = new Folder("mainn", null);
            f21.Dzieci.Add(f212);

            Folder f131 = new Folder("mainn", null);
            f13.Dzieci.Add(f131);
            Folder f132 = new Folder("mainn", null);
            f13.Dzieci.Add(f132);
            Folder f133 = new Folder("mainn", null);
            f13.Dzieci.Add(f133);

           Folder f1331 = new Folder("mainn", null);
            f133.Dzieci.Add(f1331);
            Folder f1332 = new Folder("mainn", null);
            f133.Dzieci.Add(f1332);

            Folder f2111 = new Folder("mainn", null);
            f211.Dzieci.Add(f2111);
            Folder f2112 = new Folder("mainn", null);
            f211.Dzieci.Add(f2112);
            Folder f2113 = new Folder("mainn", null);
            f211.Dzieci.Add(f2113);

            Folder f13321 = new Folder("mainn", null);
            f1332.Dzieci.Add(f13321);
            Folder f13322 = new Folder("mainn", null);
            f1332.Dzieci.Add(f13322);

            Folder f21111 = new Folder("mainn", null);
            f2111.Dzieci.Add(f21111);
            Folder f21112 = new Folder("mainn", null);
            f2111.Dzieci.Add(f21112);
            Folder f21113 = new Folder("mainn", null);
            f2111.Dzieci.Add(f21113);
            Folder f21114 = new Folder("mainn", null);
            f2111.Dzieci.Add(f21114); 






            Sorteritno x = new Sorteritno();
            x.Create(can, 30, 0, seed);
            //yas.Text = f13.name;

            x.Sort(seed, can, 0, 30);

        }
                                                //Rozdział z komendami
                                                //Rozdział z komendami      
                                                //Rozdział z komendami
        // funkcje komend
        // ukryj/pokaz panel
        private void ShowHide(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void ViewContent_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            Button xyz = (Button)e.Parameter;
            Canvas zyx = (Canvas)xyz.Parent;
            Folder fff = (Folder)zyx.Tag;
            if (fff.Dzieci.Count == 0)
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


        //do usuniecia
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
            Folder yhyh = (Folder)CurrentlyChosen.Tag;
            yas.Text = yhyh.name;
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
            Folder fff = (Folder)zyx.Tag;
            if (fff.showContent)
            {
                fff.showContent = false;
            }
            else
            {
                fff.showContent = true;
            }
            Sorteritno x = new Sorteritno();
            x.ResetTree(can, ResetHighlight, ResTree, seed);
        }

    }

    // "deklaracje" komend
    public static class KGPcommands
    {
        public static readonly RoutedUICommand HorizontalStyleSwitch = new RoutedUICommand
           (
            "HorizontalStyleSwitch",
            "HorizontalStyleSwitch",
            typeof(KGPcommands)
           );
        public static readonly RoutedUICommand Jakas = new RoutedUICommand
        (
            "Jakas",
            "Jakas",
            typeof(KGPcommands)
        );

        public static readonly RoutedUICommand WidokShowHide = new RoutedUICommand
        (
            "WidokShowHide",
            "WidokShowHide",
            typeof(KGPcommands)
        );
        public static readonly RoutedUICommand HighlightChosen = new RoutedUICommand
        (
            "HighlightChosen",
            "HighlightChosen",
            typeof(KGPcommands)
        );
        public static readonly RoutedUICommand ResetHighlight = new RoutedUICommand
        (
        "ResetHighlight",
        "ResetHighlight",
        typeof(KGPcommands)
        );

        public static readonly RoutedUICommand ViewContent = new RoutedUICommand
        (
        "ViewContent",
        "ViewContent",
        typeof(KGPcommands)
        );

    }

                                    /// <summary>
                                    /// Na potrzeby
                                    /// </summary>
    public class Folder
    {
        public string name;
        public List<Folder> Dzieci = new List<Folder>();
        public List<Folder> Pliki = new List<Folder>();
        public Folder parent;
        public bool showContent = true;
        public Folder(string name, Folder parent)
        {
            this.name = name;
            this.parent = parent;
        }

    }

    public class Sorteritno
    {
        public void ResetTree(Grid lol, Button res, Grid lol2, Folder seed)
        {
            lol2.Children.Remove(lol);
            lol = new Grid();
            lol.Margin = new Thickness(10);
            lol.Background = Brushes.LightGray;
            res = new Button();
            res.Opacity = 0;
            res.Command = KGPcommands.ResetHighlight;
            lol.Children.Add(res);
            lol2.Children.Add(lol);
            
            Sorteritno x = new Sorteritno();
            x.Create(lol, 30, 0, seed);
            x.Sort(seed, lol, 0, 30);
        }

        public void Pionowa(int start, int end, Grid lol, int parX)
        {
            Canvas n = new Canvas();
            n.Background = Brushes.Red;
            n.VerticalAlignment = VerticalAlignment.Top;
            n.HorizontalAlignment = HorizontalAlignment.Center;
            n.Width = 2;
            n.Height = end;
            Thickness marg = new Thickness();
            marg.Top = start;
            if (parX < 0)
            {
                marg.Left = -1 * parX;
            }
            else
            {
                marg.Right = parX;
            }
            n.Margin = marg;
            lol.Children.Add(n);
        }
        public void Pozioma(int start, int end, int parY, Grid lol)
        {
            Canvas n = new Canvas();
            n.Background = Brushes.Red;
            n.VerticalAlignment = VerticalAlignment.Top;
            n.HorizontalAlignment = HorizontalAlignment.Center;
            int difference = end - start;
            n.Width = Math.Abs(difference/2);
            n.Height = 2;
            Thickness marg = new Thickness();
            marg.Top = parY;
            if (start < 0)
            {
                marg.Left = (-1) * start + (-1) * difference / 2;
            }
            else
            {
                marg.Right = start + difference / 2;

            }
            n.Margin = marg;
            lol.Children.Add(n);
        }

        public void Create(Grid lol, int up, int parX, Folder f)
        {
            BitmapImage carBitmap = new BitmapImage(new Uri("icons8-folder-48.png", UriKind.Relative));
            Image m = new Image();
            m.Source = carBitmap;
            Thickness obrazek = m.Margin;
            obrazek.Top = 2;
            obrazek.Bottom = 2;
            obrazek.Left = 2;
            obrazek.Right = 2;
            m.Margin = obrazek;

            Canvas n = new Canvas();
            n.HorizontalAlignment = HorizontalAlignment.Center;
            n.VerticalAlignment = VerticalAlignment.Top;
            n.Width = 32;
            n.Height = 32;
            n.Background = Brushes.LightGray;

            Thickness marg = n.Margin;
            marg.Top = up;
            if (parX < 0)
            {
                marg.Left = -1 * parX;
            }
            else
            {
                marg.Right = parX;
            }
            n.Children.Add(m);
            n.Margin = marg;

            Button v = new Button();
            v.Height = 12;
            v.Width = 12;
            Thickness vmarg = new Thickness();
            vmarg.Left = -10;
            v.HorizontalAlignment = HorizontalAlignment.Left;
            v.VerticalAlignment = VerticalAlignment.Top;
            v.Margin = vmarg;
            v.Command = KGPcommands.ViewContent;
            v.CommandParameter = v;
            n.Children.Add(v);

            Button o = new Button();
            o.Opacity = 0;
            o.Height = 30;
            o.Width = 30;
            o.Margin = obrazek;
            o.CommandParameter = o;
            o.Command = KGPcommands.HighlightChosen;
            n.Tag = f;
            n.Children.Add(o);

            lol.Children.Add(n);
        }

        public int GetSize(Folder f) //szerokosc galezi 
        {
            int size = 0;

            if (f.Dzieci.Count != 0)
            {
                foreach (Folder g in f.Dzieci)
                {
                    size += GetSize(g);
                }
            }
            else
            {
                size += 90;
            }
            return size;
        }


        public int GetSideSize(Folder f, int s) //szerokosc stron 1- lewej, 0 - prawej
        {
            int size = 0;
            int mid = f.Dzieci.Count / 2;
            if (f.Dzieci.Count == 0)
            {
                size += 90;
            }
            else if (f.Dzieci.Count != 0)
            {
                if (s == 1) //lewa strona
                {
                    for (int i = mid - 1; i >= 0; i--)
                    {
                        size += 2 * GetSize(f.Dzieci[i]);
                    }
                    if (f.Dzieci.Count % 2 != 0)
                    {
                        size += GetSideSize(f.Dzieci[mid], s) - 45;
                    }
                }
                else if (s == 0) //prawa strona
                {
                    if (f.Dzieci.Count % 2 == 0)
                    {
                        for (int i = mid; i < f.Dzieci.Count; i++)
                        {
                            size += 2 * GetSize(f.Dzieci[i]);
                        }
                    }
                    else
                    {
                        for (int i = mid + 1; i < f.Dzieci.Count; i++)
                        {
                            size += 2 * GetSize(f.Dzieci[i]);
                        }
                        size += GetSideSize(f.Dzieci[mid], s) - 45;
                    }
                }
            }

            return size;
        }

        public void Sort(Folder folder, Grid lol, int parX, int parY)
        {
            if (folder.Dzieci.Count != 0)
            {
                Pionowa(parY+30, 15, lol, parX);
                int mid; //indeks srodkowego
                int k;   //krok  ;; faktyczny odstep
                int x;   //parametr do obliczen
                if (folder.Dzieci.Count % 2 == 0 && folder.showContent == true) //parzyste
                {
                    mid = folder.Dzieci.Count / 2 - 1; //indeks o 1 wiekszy! <
                    k = parX;
                    for (int i = mid; i >= 0; i--)  //od srodkowego w lewo
                    {
                        x = GetSideSize(folder.Dzieci[i], 0);
                        Create(lol, parY + 60, x + k, folder.Dzieci[i]);
                        Pionowa(parY + 45, 15, lol, x + k);
                        Pozioma(parX,x+k, parY+45, lol);

                            Sort(folder.Dzieci[i], lol, x + k, parY + 60);
                            k += 2 * GetSize(folder.Dzieci[i]);
                        

                    }
                    k = parX;
                    for (int i = mid + 1; i < folder.Dzieci.Count; i++)
                    {
                        x = -1 * GetSideSize(folder.Dzieci[i], 1);
                        Create(lol, parY + 60, x + k, folder.Dzieci[i]);
                        Pionowa(parY + 45, 15, lol, x + k);
                        Pozioma(parX, x + k, parY + 45, lol);

                            Sort(folder.Dzieci[i], lol, x + k, parY + 60);
                            k -= 2 * GetSize(folder.Dzieci[i]);

                    }
                }
                else if (folder.Dzieci.Count%2 != 0 && folder.showContent == true) //nieparzyste
                {
                    mid = folder.Dzieci.Count / 2;
                    k = parX + GetSideSize(folder.Dzieci[mid], 1);
                    Pionowa(parY + 45, 15, lol, parX);
                    Create(lol, parY + 60, parX, folder.Dzieci[mid]); //srodkowy
                        Sort(folder.Dzieci[mid], lol, parX, parY + 60);
                    for (int i = mid - 1; i >= 0; i--)
                    {

                        x = GetSideSize(folder.Dzieci[i], 0);
                        Pionowa(parY + 45, 15, lol, x + k);
                        Create(lol, parY + 60, x + k, folder.Dzieci[i]);
                        Pozioma(parX, x + k, parY + 45, lol);

                            Sort(folder.Dzieci[i], lol, x + k, parY + 60);
                            k += 2 * GetSize(folder.Dzieci[i]);
                    }//dla drugiej strony podobnie, ale w druga strone ;
                    k = parX - GetSideSize(folder.Dzieci[mid], 0);

                    for (int i = mid + 1; i < folder.Dzieci.Count; i++)
                    {
                        x = -1 * GetSideSize(folder.Dzieci[i], 1);
                        Create(lol, parY + 60, x + k, folder.Dzieci[i]);
                        Pionowa(parY + 45, 15, lol, x + k);
                        Pozioma(parX, x + k, parY + 45, lol);
                            Sort(folder.Dzieci[i], lol, x + k, parY + 60);
                            k -= 2 * GetSize(folder.Dzieci[i]);
                    }
                }
            }

        }
    }


}
