using ReFolder.Dir;
using System;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace main_1._0
{
    public class Sorteritno
    {
            public void ResetTree(Grid lol, Button res, Grid lol2, IEditableDirWithChildren seed)
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
                n.Width = Math.Abs(difference / 2);
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

            public void Create(Grid lol, int up, int parX, IEditableDirWithChildren f)
            {
                BitmapImage carBitmap = new BitmapImage(new Uri(@"icons8-folder-48.png", UriKind.Relative));
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

            public int GetSize(IEditableDirWithChildren f) //szerokosc galezi 
            {
                int size = 0;

                if (f.Children.Count != 0)
                {
                    foreach (IEditableDirWithChildren g in f.Children)
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


            public int GetSideSize(IEditableDirWithChildren f, int s) //szerokosc stron 1- lewej, 0 - prawej
            {
                int size = 0;
                int mid = f.Children.Count / 2;
                if (f.Children.Count == 0)
                {
                    size += 90;
                }
                else if (f.Children.Count != 0)
                {
                    if (s == 1) //lewa strona
                    {
                        for (int i = mid - 1; i >= 0; i--)
                        {
                            size += 2 * GetSize(f.Children[i]);
                        }
                        if (f.Children.Count % 2 != 0)
                        {
                            size += GetSideSize(f.Children[mid], s) - 45;
                        }
                    }
                    else if (s == 0) //prawa strona
                    {
                        if (f.Children.Count % 2 == 0)
                        {
                            for (int i = mid; i < f.Children.Count; i++)
                            {
                                size += 2 * GetSize(f.Children[i]);
                            }
                        }
                        else
                        {
                            for (int i = mid + 1; i < f.Children.Count; i++)
                            {
                                size += 2 * GetSize(f.Children[i]);
                            }
                            size += GetSideSize(f.Children[mid], s) - 45;
                        }
                    }
                }

                return size;
            }

            public void Sort(IEditableDirWithChildren IEditableDirWithChildren, Grid lol, int parX, int parY)
            {
                if (IEditableDirWithChildren.Children.Count != 0)
                {
                    Pionowa(parY + 30, 15, lol, parX);
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
                            Create(lol, parY + 60, x + k, IEditableDirWithChildren.Children[i]);
                            Pionowa(parY + 45, 15, lol, x + k);
                            Pozioma(parX, x + k, parY + 45, lol);

                            Sort(IEditableDirWithChildren.Children[i], lol, x + k, parY + 60);
                            k += 2 * GetSize(IEditableDirWithChildren.Children[i]);


                        }
                        k = parX;
                        for (int i = mid + 1; i < IEditableDirWithChildren.Children.Count; i++)
                        {
                            x = -1 * GetSideSize(IEditableDirWithChildren.Children[i], 1);
                            Create(lol, parY + 60, x + k, IEditableDirWithChildren.Children[i]);
                            Pionowa(parY + 45, 15, lol, x + k);
                            Pozioma(parX, x + k, parY + 45, lol);

                            Sort(IEditableDirWithChildren.Children[i], lol, x + k, parY + 60);
                            k -= 2 * GetSize(IEditableDirWithChildren.Children[i]);

                        }
                    }
                    else if (IEditableDirWithChildren.Children.Count % 2 != 0 && IEditableDirWithChildren.ShowContent == true) //nieparzyste
                    {
                        mid = IEditableDirWithChildren.Children.Count / 2;
                        k = parX + GetSideSize(IEditableDirWithChildren.Children[mid], 1);
                        Pionowa(parY + 45, 15, lol, parX);
                        Create(lol, parY + 60, parX, IEditableDirWithChildren.Children[mid]); //srodkowy
                        Sort(IEditableDirWithChildren.Children[mid], lol, parX, parY + 60);
                        for (int i = mid - 1; i >= 0; i--)
                        {

                            x = GetSideSize(IEditableDirWithChildren.Children[i], 0);
                            Pionowa(parY + 45, 15, lol, x + k);
                            Create(lol, parY + 60, x + k, IEditableDirWithChildren.Children[i]);
                            Pozioma(parX, x + k, parY + 45, lol);

                            Sort(IEditableDirWithChildren.Children[i], lol, x + k, parY + 60);
                            k += 2 * GetSize(IEditableDirWithChildren.Children[i]);
                        }//dla drugiej strony podobnie, ale w druga strone ;
                        k = parX - GetSideSize(IEditableDirWithChildren.Children[mid], 0);

                        for (int i = mid + 1; i < IEditableDirWithChildren.Children.Count; i++)
                        {
                            x = -1 * GetSideSize(IEditableDirWithChildren.Children[i], 1);
                            Create(lol, parY + 60, x + k, IEditableDirWithChildren.Children[i]);
                            Pionowa(parY + 45, 15, lol, x + k);
                            Pozioma(parX, x + k, parY + 45, lol);
                            Sort(IEditableDirWithChildren.Children[i], lol, x + k, parY + 60);
                            k -= 2 * GetSize(IEditableDirWithChildren.Children[i]);
                        }
                    }
                }

            }
        }
    
}
