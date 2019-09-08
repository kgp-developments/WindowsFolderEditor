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
            public float GetTextFolderWidth(string testedName)
        {
            float width = 0;
            foreach (char c in testedName)
            {
                if (width < 60)
                {
                    if (char.IsWhiteSpace(c))
                    {
                        width += 3f;
                    }
                    else
                    {
                        width+=5f;
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

                SwitchedGrid.Margin = new Thickness(20);
                SwitchedGrid.Background = Brushes.LightGray;
                ResetButton = new Button();
                ResetButton.Opacity = 0;
                ResetButton.Command = KGPcommands.ResetHighlight;
                SwitchedGrid.Children.Add(ResetButton);
                TreeSV.Content = SwitchedGrid;

                Sorteritno Temporary = new Sorteritno();
                Temporary.Create(SwitchedGrid, 30, 0, Seed, window);
                Temporary.Sort(Seed, SwitchedGrid, 0, 30, window);

        }

            public void Pionowa(int start, int end, Grid TheGrid, int parX)
            {
                Canvas Line = new Canvas();
                Line.Background = Brushes.Red;
                Line.VerticalAlignment = VerticalAlignment.Top;
                Line.HorizontalAlignment = HorizontalAlignment.Center;
                Line.Width = 2;
                Line.Height = end;
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
                Line.Margin = marg;
                TheGrid.Children.Add(Line);
            }
            public void Pozioma(int start, int end, int parY, Grid TheGrid)
            {
                Canvas Line = new Canvas();
                Line.Background = Brushes.Red;
                Line.VerticalAlignment = VerticalAlignment.Top;
                Line.HorizontalAlignment = HorizontalAlignment.Center;
                int difference = end - start;
                Line.Width = Math.Abs(difference / 2);
                Line.Height = 2;
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
                Line.Margin = marg;
                TheGrid.Children.Add(Line);
            }

            public void Create(Grid SwitchedGrid, int up, int parX, IEditableDirWithChildren Folder, string window)
            {
                                // Ustawienia obrazka
                BitmapImage carBitmap = new BitmapImage(new Uri(@"icons8-folder-48.png", UriKind.Relative));
                Image Icon = new Image();
                Icon.Source = carBitmap;
                Thickness obrazek = new Thickness();
                obrazek.Top = 1;
                obrazek.Bottom = 2;
                obrazek.Left = 3;
                obrazek.Right = 3;
                Icon.Margin = obrazek;
                                // Ustawienia glownej warstwy (podswietlenia)
                Canvas MainLayer = new Canvas();
                MainLayer.HorizontalAlignment = HorizontalAlignment.Center;
                MainLayer.VerticalAlignment = VerticalAlignment.Top;
                MainLayer.Width = 36;
                MainLayer.Height = 36;

            Thickness marg = MainLayer.Margin;
                marg.Top = up;
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
                VCbutton.Height = 12;
                VCbutton.Width = 12;
                Thickness vmarg = new Thickness();
                vmarg.Left = -10;
                VCbutton.HorizontalAlignment = HorizontalAlignment.Left;
                VCbutton.VerticalAlignment = VerticalAlignment.Top;
                VCbutton.Margin = vmarg;
                VCbutton.Command = KGPcommands.ViewContent;
                VCbutton.CommandParameter = VCbutton;
                MainLayer.Children.Add(VCbutton);
                                     // ustawienia przycisku do resetu highlightu
                Button HLRbutton = new Button();
                HLRbutton.Opacity = 0;
                HLRbutton.Height = 30;
                HLRbutton.Width = 30;
                HLRbutton.Margin = obrazek;
                HLRbutton.CommandParameter = HLRbutton;
                HLRbutton.Command = KGPcommands.HighlightChosen;
                MainLayer.Children.Add(HLRbutton);
                                 // Ustawienia wyswietlania nazwy folderu
                TextBlock FolderText = new TextBlock();
                FolderText.Text = Folder.Description.Name;
                FolderText.FontSize = 10;
                FolderText.MaxWidth = 60;
                Canvas.SetTop(FolderText ,25);
                Canvas.SetLeft(FolderText, 18 - GetTextFolderWidth(Folder.Description.Name)/2);
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

            public int GetSize(IEditableDirWithChildren Folder) //szerokosc galezi 
            {
                int size = 0;

                if (Folder.Children.Count != 0)
                {
                    foreach (IEditableDirWithChildren Child in Folder.Children)
                    {
                        size += GetSize(Child);
                    }
                }
                else
                {
                    size += 90;
                }
                return size;
            }


            public int GetSideSize(IEditableDirWithChildren Folder, int parameter) //szerokosc stron 1- lewej, 0 - prawej
            {
                int size = 0;
                int mid = Folder.Children.Count / 2;
                if (Folder.Children.Count == 0)
                {
                    size += 90;
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

            public void Sort(IEditableDirWithChildren IEditableDirWithChildren, Grid Switched, int parX, int parY, string window)
            {
                if (IEditableDirWithChildren.Children.Count != 0)
                {
                    Pionowa(parY + 38, 8, Switched, parX);
                    int mid; //indeks srodkowego
                    int k;   //krok  ;; faktyczny odstep
                    int x;   //parametr do obliczen
                    if (IEditableDirWithChildren.Children.Count % 2 == 0 && IEditableDirWithChildren.ShowContent == true) //parzyste
                    {
                        mid = IEditableDirWithChildren.Children.Count / 2 - 1; //indeks o 1 wiekszy! <
                        k = parX;
                        for (int i = mid; i >= 0; i--)  //od srodkowego w lewo
                        {
                            x = GetSideSize(IEditableDirWithChildren.Children[i], 0);
                            Create(Switched, parY + 60, x + k, IEditableDirWithChildren.Children[i], window);
                            Pionowa(parY + 45, 15, Switched, x + k);
                            Pozioma(parX, x + k, parY + 45, Switched);

                            Sort(IEditableDirWithChildren.Children[i], Switched, x + k, parY + 60, window);
                            k += 2 * GetSize(IEditableDirWithChildren.Children[i]);


                        }
                        k = parX;
                        for (int i = mid + 1; i < IEditableDirWithChildren.Children.Count; i++)
                        {
                            x = -1 * GetSideSize(IEditableDirWithChildren.Children[i], 1);
                            Create(Switched, parY + 60, x + k, IEditableDirWithChildren.Children[i], window);
                            Pionowa(parY + 45, 15, Switched, x + k);
                            Pozioma(parX, x + k, parY + 45, Switched);

                            Sort(IEditableDirWithChildren.Children[i], Switched, x + k, parY + 60, window);
                            k -= 2 * GetSize(IEditableDirWithChildren.Children[i]);

                        }
                    }
                    else if (IEditableDirWithChildren.Children.Count % 2 != 0 && IEditableDirWithChildren.ShowContent == true) //nieparzyste
                    {
                        mid = IEditableDirWithChildren.Children.Count / 2;
                        k = parX + GetSideSize(IEditableDirWithChildren.Children[mid], 1);
                        Pionowa(parY + 45, 15, Switched, parX);
                        Create(Switched, parY + 60, parX, IEditableDirWithChildren.Children[mid], window); //srodkowy
                        Sort(IEditableDirWithChildren.Children[mid], Switched, parX, parY + 60, window);
                        for (int i = mid - 1; i >= 0; i--)
                        {

                            x = GetSideSize(IEditableDirWithChildren.Children[i], 0);
                            Pionowa(parY + 45, 15, Switched, x + k);
                            Create(Switched, parY + 60, x + k, IEditableDirWithChildren.Children[i], window);
                            Pozioma(parX, x + k, parY + 45, Switched);

                            Sort(IEditableDirWithChildren.Children[i], Switched, x + k, parY + 60, window);
                            k += 2 * GetSize(IEditableDirWithChildren.Children[i]);
                        }//dla drugiej strony podobnie, ale w druga strone ;
                        k = parX - GetSideSize(IEditableDirWithChildren.Children[mid], 0);

                        for (int i = mid + 1; i < IEditableDirWithChildren.Children.Count; i++)
                        {
                            x = -1 * GetSideSize(IEditableDirWithChildren.Children[i], 1);
                            Create(Switched, parY + 60, x + k, IEditableDirWithChildren.Children[i], window);
                            Pionowa(parY + 45, 15, Switched, x + k);
                            Pozioma(parX, x + k, parY + 45, Switched);
                            Sort(IEditableDirWithChildren.Children[i], Switched, x + k, parY + 60, window);
                            k -= 2 * GetSize(IEditableDirWithChildren.Children[i]);
                        }
                    }
                }

            }
        }
    
}
