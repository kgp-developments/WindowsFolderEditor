using ReFolder.Dir;
using System;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ReFolder.Memento;

namespace main_1._0
{
    public class Sorteritno
    {
            static MainWindow AppMW = (MainWindow)Application.Current.MainWindow;
        public float oyFolderDistance;
        public float first;
        public float scnd;
        public float fontsize;
        public float scale = 1f;

            public float GetTextFolderWidth(string testedName)
        {
            float width = 0;
            foreach (char c in testedName)
            {
                if (width < 60)
                {
                    if (char.IsWhiteSpace(c))
                    {
                        width += 0.6f*fontsize/2;
                    }
                    else
                    {
                        width+=fontsize/2f;
                    }
                }
                else
                {
                    break;
                }

            }
            return width;
        }
            
            public void ResetTree(Grid SwitchedGrid, Button ResetButton, IEditableDirWithChildren Seed, ScrollViewer TreeSV, string window)
            {


            TreeSV.Content = null;
                SwitchedGrid = new Grid();

                SwitchedGrid.Margin = new Thickness(40);
                SwitchedGrid.Background = Brushes.LightGray;
                ResetButton = new Button();
                ResetButton.Opacity = 0;
                ResetButton.Command = KGPcommands.ResetHighlight;
                SwitchedGrid.Children.Add(ResetButton);
                TreeSV.Content = SwitchedGrid;

                Create(SwitchedGrid, 30, 0, Seed, window);
                Sort(Seed, SwitchedGrid, 0, 30, window);

        }

            public void Pionowa(float start, float end, Grid TheGrid, float parX)
            {
                Canvas Line = new Canvas();
                Line.Background = Brushes.Red;
                Line.VerticalAlignment = VerticalAlignment.Top;
                Line.HorizontalAlignment = HorizontalAlignment.Center;
                Line.Width = 2 * scale;
                Line.Height = end * scale;
                Thickness marg = new Thickness();
                marg.Top = start * scale;
                if (parX < 0)
                {
                    marg.Left = -1 * parX;
                }
                else
                {
                    marg.Right = parX;
                }
                Line.Margin = marg;
                TheGrid.Children.Add(Line);
            }
            public void Pozioma(float start, float end, float parY, Grid TheGrid)
            {
                Canvas Line = new Canvas();
                Line.Background = Brushes.Red;
                Line.VerticalAlignment = VerticalAlignment.Top;
                Line.HorizontalAlignment = HorizontalAlignment.Center;
                float difference = (end  - start);
                Line.Width = Math.Abs(difference / 2 );
                Line.Height = 2 * scale;
                Thickness marg = new Thickness();
                marg.Top = parY * scale;
                if (start < 0)
                {
                    marg.Left = (-1) * start + (-1) * difference / 2;
                }
                else
                {
                    marg.Right = start + difference / 2;

                }
                Line.Margin = marg;
                TheGrid.Children.Add(Line);
            }

            public void Create(Grid SwitchedGrid, float up, float parX, IEditableDirWithChildren Folder, string window)
            {
                                // Ustawienia obrazka
                BitmapImage carBitmap = new BitmapImage(new Uri(@"icons8-folder-48.png", UriKind.Relative));
                Image Icon = new Image();
                Icon.Source = carBitmap;
                Thickness obrazek = new Thickness();
                obrazek.Top = 1 * scale;
                obrazek.Left = 3 * scale;
                Icon.Margin = obrazek;
            Icon.Width = 30 * scale;
            Icon.Height = 30 * scale;
                                // Ustawienia glownej warstwy (podswietlenia)
                Canvas MainLayer = new Canvas();
                MainLayer.HorizontalAlignment = HorizontalAlignment.Center;
                MainLayer.VerticalAlignment = VerticalAlignment.Top;
                MainLayer.Width = 36 * scale;
                MainLayer.Height = 36 * scale;

            Thickness marg = MainLayer.Margin;
                marg.Top = up * scale;
                if (parX < 0)
                {
                    marg.Left = -1 * parX;
                }
                else
                {
                    marg.Right = parX;
                }
                MainLayer.Children.Add(Icon);
                MainLayer.Margin = marg;
                                //Ustawienia przycisku view content
                Button VCbutton = new Button();  //view content button
                VCbutton.Height = 12 * scale;
                VCbutton.Width = 12 * scale;
                Thickness vmarg = new Thickness();
                vmarg.Left = -10 * scale;
                VCbutton.HorizontalAlignment = HorizontalAlignment.Left;
                VCbutton.VerticalAlignment = VerticalAlignment.Top;
                VCbutton.Margin = vmarg;
                VCbutton.Command = KGPcommands.ViewContent;
                VCbutton.CommandParameter = VCbutton;
                MainLayer.Children.Add(VCbutton);
                                     // ustawienia przycisku do resetu highlightu
                Button HLRbutton = new Button();
                HLRbutton.Opacity = 0;
                HLRbutton.Height = 30 * scale;
                HLRbutton.Width = 30 * scale;
                HLRbutton.Margin = obrazek;
                HLRbutton.CommandParameter = HLRbutton;
                HLRbutton.Command = KGPcommands.HighlightChosen;
                MainLayer.Children.Add(HLRbutton);
                                 // Ustawienia wyswietlania nazwy folderu
                TextBlock FolderText = new TextBlock();
                FolderText.Text = Folder.Description.Name;
                    FolderText.FontSize = fontsize * scale;
                FolderText.MaxWidth = 80 * scale;
                Canvas.SetTop(FolderText ,25 * scale);
                Canvas.SetLeft(FolderText, (18 - GetTextFolderWidth(Folder.Description.Name)/2) * scale);
                MainLayer.Children.Add(FolderText);
                if (Folder.IsMarked)
                {
                    MainLayer.Background = Brushes.Blue;
                    if (window == "MW")
                    {
                        MainWindow.CurrentlyChosen = MainLayer;
                    }
                    else if (window == "CAW")
                    {
                        ComplexAdditionWindow.CurrentlyChosenCAW = MainLayer;
                    }
                }
                else
                {
                    MainLayer.Background = Brushes.LightGray;
                }
                SwitchedGrid.Children.Add(MainLayer);
                MainLayer.Tag = Folder;
        }

            public float GetSize(IEditableDirWithChildren Folder) //szerokosc galezi 
            {
                float size = 0;

                if (Folder.Children.Count != 0)
                {
                    foreach (IEditableDirWithChildren Child in Folder.Children)
                    {
                        size += GetSize(Child);
                    }
                }
                else
                {
                    size += 90 * scale;
                }
                return size;
            }


            public float GetSideSize(IEditableDirWithChildren Folder, int parameter) //szerokosc stron 1- lewej, 0 - prawej
            {
            float size = 0;
                int mid = Folder.Children.Count / 2;
                if (Folder.Children.Count == 0)
                {
                    size += 90 * scale;
                }
                else if (Folder.Children.Count == 1)
                {
                    size += GetSideSize(Folder.Children[0], parameter);
                }
                else if (Folder.Children.Count != 0)
                {
                    if (parameter == 1) //lewa strona
                    {
                        for (int i = mid - 1; i >= 0; i--)
                        {
                            size += 2 * GetSize(Folder.Children[i]);
                        }
                        if (Folder.Children.Count % 2 != 0)
                        {
                            size += GetSideSize(Folder.Children[mid], parameter);
                        }
                    }
                    else if (parameter == 0) //prawa strona
                    {
                        if (Folder.Children.Count % 2 == 0)
                        {
                            for (int i = mid; i < Folder.Children.Count; i++)
                            {
                                size += 2 * GetSize(Folder.Children[i]);
                            }
                        }
                        else
                        {
                            for (int i = mid + 1; i < Folder.Children.Count; i++)
                            {
                                size += 2 * GetSize(Folder.Children[i]);
                            }
                            size += GetSideSize(Folder.Children[mid], parameter);
                        }
                    }
                }

                return size;
            }

            public void Sort(IEditableDirWithChildren IEditableDirWithChildren, Grid Switched, float parX, float parY, string window)
            {
                if (IEditableDirWithChildren.Children.Count != 0)
                {
                    Pionowa(parY + 30 + fontsize, scnd - fontsize/2, Switched, parX);
                    int mid; //indeks srodkowego
                float k;   //krok  ;; faktyczny odstep
                float x;   //parametr do obliczen
                    if (IEditableDirWithChildren.Children.Count % 2 == 0 && IEditableDirWithChildren.ShowContent == true) //parzyste
                    {
                        mid = IEditableDirWithChildren.Children.Count / 2 - 1; //indeks o 1 wiekszy! <
                        k = parX;
                        for (int i = mid; i >= 0; i--)  //od srodkowego w lewo
                        {
                            x = GetSideSize(IEditableDirWithChildren.Children[i], 0);
                            Create(Switched, parY + oyFolderDistance + fontsize / 2, x + k, IEditableDirWithChildren.Children[i], window);
                            Pionowa(parY + first + fontsize / 2, scnd, Switched, x + k);
                            Pozioma(parX, x + k, parY + first + fontsize/2, Switched);

                            Sort(IEditableDirWithChildren.Children[i], Switched, x + k, parY + oyFolderDistance + fontsize / 2, window);
                            k += 2 * GetSize(IEditableDirWithChildren.Children[i]);


                        }
                        k = parX;
                        for (int i = mid + 1; i < IEditableDirWithChildren.Children.Count; i++)
                        {
                            x = -1 * GetSideSize(IEditableDirWithChildren.Children[i], 1);
                            Create(Switched, parY + oyFolderDistance + fontsize / 2, x + k, IEditableDirWithChildren.Children[i], window);
                            Pionowa(parY + first + fontsize / 2, scnd, Switched, x + k);
                            Pozioma(parX, x + k, parY + first + fontsize / 2, Switched);

                            Sort(IEditableDirWithChildren.Children[i], Switched, x + k, parY + oyFolderDistance + fontsize / 2, window);
                            k -= 2 * GetSize(IEditableDirWithChildren.Children[i]);

                        }
                    }
                    else if (IEditableDirWithChildren.Children.Count % 2 != 0 && IEditableDirWithChildren.ShowContent == true) //nieparzyste
                    {
                        mid = IEditableDirWithChildren.Children.Count / 2;
                        k = parX + GetSideSize(IEditableDirWithChildren.Children[mid], 1);
                        Pionowa(parY + first + fontsize / 2, scnd, Switched, parX);
                        Create(Switched, parY + oyFolderDistance + fontsize / 2, parX, IEditableDirWithChildren.Children[mid], window); //srodkowy
                        Sort(IEditableDirWithChildren.Children[mid], Switched, parX, parY + oyFolderDistance + fontsize / 2, window);
                        for (int i = mid - 1; i >= 0; i--)
                        {

                            x = GetSideSize(IEditableDirWithChildren.Children[i], 0);
                            Pionowa(parY + first + fontsize / 2, scnd, Switched, x + k);
                            Create(Switched, parY + oyFolderDistance + fontsize / 2, x + k, IEditableDirWithChildren.Children[i], window);
                            Pozioma(parX, x + k, parY + first + fontsize / 2, Switched);

                            Sort(IEditableDirWithChildren.Children[i], Switched, x + k, parY + oyFolderDistance + fontsize / 2, window);
                            k += 2 * GetSize(IEditableDirWithChildren.Children[i]);
                        }//dla drugiej strony podobnie, ale w druga strone ;
                        k = parX - GetSideSize(IEditableDirWithChildren.Children[mid], 0);

                        for (int i = mid + 1; i < IEditableDirWithChildren.Children.Count; i++)
                        {
                            x = -1 * GetSideSize(IEditableDirWithChildren.Children[i], 1);
                            Create(Switched, parY + oyFolderDistance + fontsize / 2, x + k, IEditableDirWithChildren.Children[i], window);
                            Pionowa(parY + first + fontsize / 2, scnd, Switched, x + k);
                            Pozioma(parX, x + k, parY + first + fontsize / 2, Switched);
                            Sort(IEditableDirWithChildren.Children[i], Switched, x + k, parY + oyFolderDistance + fontsize / 2, window);
                            k -= 2 * GetSize(IEditableDirWithChildren.Children[i]);
                        }
                    }
                }

            }
        }
    
}
