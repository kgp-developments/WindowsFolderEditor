using ReFolder.Dir;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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
        public int dir_counter = 0;
        public int caw_dir_counter = 0;
        public int dir_count_max = 100;


        #region składowe funkcji Create (cele optymalizacyjne)
        public float icon_and_hlrbtn_size;
        public float mainLayer_size;
        public float vcbutton_size;
        public float vcbutton_marg_left;
        public float fontsize_size;
        public float folder_text_maxwidth_size;
        public float canvas_settop_size;
        public float canvas_setleft_half_size;
        public float line_size;
        public float folder_text_half__size;
        public float fontsize_halved_size;
        public int length_checker;
        public float icon_margin_size;
        public float folder_width_size;
        public float folder_doubled_width_size;
        public void Recalibration()
        {
            icon_and_hlrbtn_size = 30 * scale;
            mainLayer_size = 36 * scale;
            vcbutton_size = 12 * scale;
            vcbutton_marg_left = -15 * scale;
            fontsize_size = fontsize * scale;
            folder_text_maxwidth_size = 80 * scale;
            canvas_settop_size = 25 * scale;
            canvas_setleft_half_size = mainLayer_size / 2f;
            line_size = 2 * scale;
            folder_text_half__size = folder_text_maxwidth_size / 2f;
            fontsize_halved_size = fontsize / 2f;
            length_checker = (int)(folder_text_maxwidth_size / fontsize_halved_size);
            icon_margin_size = 3 * scale;
            folder_width_size = 90 * scale;
            folder_doubled_width_size = folder_width_size * 2;
        }
        #endregion

        #region składowe do kontroli focusu

        public double sv_width;
        public void SetTreeSVSize(string window)
        {
            if (window == "MW")
            {
                sv_width = ((MainWindow)Application.Current.MainWindow).Width - 200f;
                //sv.Width = ((MainWindow)Application.Current.MainWindow).Width - 200f;
                //SwitchedGrid.Width = sv.Width - 80f;
                //((MainWindow)Application.Current.MainWindow).FolderSearchTB.Text = sv.Width.ToString() + "   " + SwitchedGrid.Width.ToString();
            }
            else
            {

            }
        }
        #endregion

        public int GetDirCount(IEditableDirWithChildren dir)
        {
            int total = 0;
            if (dir.Children.Count > 0)
            {
                foreach (ChildDir childdir in dir.Children)
                {
                    GetDirCount(childdir);
                    total++;
                }
            }
            return total;
        }

        public float GetTextFolderWidth(string testedName)
        {
            float width = 0;
            if (testedName.Length >= length_checker)
            {
                width = 80;
            }
            else
            {
                foreach (char c in testedName)
                {

                    if (!char.IsWhiteSpace(c))
                    {
                        width += fontsize_halved_size;
                    }
                    else
                    {
                        width += 0.6f * fontsize_halved_size;
                    }

                }

            }
            return width;
        }

        public void ResetTree(Grid SwitchedGrid, Button ResetButton, IEditableDirWithChildren Seed, ScrollViewer TreeSV, string window)
        {
            Recalibration();
            if (window == "MW")
            {
                dir_counter = 0;
            }
            //TreeSV.Content;
            SwitchedGrid.Children.Clear();

            SwitchedGrid.Margin = new Thickness(40);
            SwitchedGrid.Background = Brushes.LightGray;
            ResetButton = new Button();
            ResetButton.Opacity = 0;
            ResetButton.Command = KGPcommands.ResetHighlight;
            SwitchedGrid.Children.Add(ResetButton);
            //TreeSV.Content = SwitchedGrid;

            Create(SwitchedGrid, 30, 0, Seed, window);
            Sort(Seed, SwitchedGrid, 0, 30, window);
            //AppMW.drzewo.ScrollToHorizontalOffset(0);
        }

        public void Pionowa(float start, float end, Grid TheGrid, float parX)
        {
            Canvas Line = new Canvas();
            Line.Background = Brushes.Red;
            Line.VerticalAlignment = VerticalAlignment.Top;
            Line.HorizontalAlignment = HorizontalAlignment.Center;
            Line.Width = line_size;
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
            float difference = (end - start);
            Line.Width = Math.Abs(difference / 2);
            Line.Height = line_size;
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
            Image Icon = new Image();
            Icon.Source = new BitmapImage(new Uri(System.IO.Path.GetFullPath(Folder.Description.IconAddress), UriKind.Absolute));
            Icon.Margin = new Thickness(icon_margin_size);
            Icon.Width = icon_and_hlrbtn_size;
            Icon.Height = icon_and_hlrbtn_size;
            // Ustawienia glownej warstwy (podswietlenia)
            Canvas MainLayer = new Canvas();
            MainLayer.HorizontalAlignment = HorizontalAlignment.Center;
            MainLayer.VerticalAlignment = VerticalAlignment.Top;
            MainLayer.Width = mainLayer_size;
            MainLayer.Height = mainLayer_size;

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
            VCbutton.Height = vcbutton_size;
            VCbutton.Width = vcbutton_size;
            marg = new Thickness();
            marg.Left = vcbutton_marg_left;

            VCbutton.HorizontalAlignment = HorizontalAlignment.Left;
            VCbutton.VerticalAlignment = VerticalAlignment.Top;
            VCbutton.Margin = marg;
            VCbutton.Command = KGPcommands.ViewContent;
            VCbutton.CommandParameter = VCbutton;
            MainLayer.Children.Add(VCbutton);
            // ustawienia przycisku do highlightu
            Button HLRbutton = new Button();
            HLRbutton.Opacity = 0;
            HLRbutton.Height = icon_and_hlrbtn_size;
            HLRbutton.Width = icon_and_hlrbtn_size;
            HLRbutton.CommandParameter = HLRbutton;
            HLRbutton.Command = KGPcommands.HighlightChosen;
            MainLayer.Children.Add(HLRbutton);
            // Ustawienia wyswietlania nazwy folderu
            TextBlock FolderText = new TextBlock();
            FolderText.Text = Folder.Description.Name;
            FolderText.FontSize = fontsize_size;
            FolderText.MaxWidth = folder_text_maxwidth_size;
            Canvas.SetTop(FolderText, canvas_settop_size);
            Canvas.SetLeft(FolderText, (canvas_setleft_half_size - GetTextFolderWidth(Folder.Description.Name) / 2f * scale));
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
            if (window == "MW")
            {
                dir_counter++;
            }
            else if (window == "CAW")
            {
                caw_dir_counter++;
            }
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
                size += folder_width_size;
            }
            return size;
        }

        public float GetSideSize(IEditableDirWithChildren Folder, int parameter) //szerokosc stron 1- lewej, 0 - prawej
        {
            float size = 0;
            if (Folder.Children.Count == 0)
            {
                size += folder_width_size;
            }
            else if (Folder.Children.Count == 1)
            {
                size += GetSideSize(Folder.Children[0], parameter);
            }
            else if (Folder.Children.Count != 0)
            {
                int mid = Folder.Children.Count / 2;

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
                Pionowa(parY + 30 + fontsize, scnd - fontsize / 2, Switched, parX);
                int mid; //indeks srodkowego
                float k;   //krok  ;; faktyczny odstep
                float x;   //parametr do obliczen
                float first_posX;
                float last_posX;
                //float first_dir_posX;
                if (IEditableDirWithChildren.Children.Count % 2 == 0 && IEditableDirWithChildren.ShowContent == true) //parzyste
                {
                    mid = IEditableDirWithChildren.Children.Count / 2 - 1; //indeks o 1 wiekszy! <
                    k = parX;
                    for (int i = mid; i > 0; i--)  //od srodkowego w lewo
                    {
                        x = GetSideSize(IEditableDirWithChildren.Children[i], 0);
                        Create(Switched, parY + oyFolderDistance + fontsize / 2, x + k, IEditableDirWithChildren.Children[i], window);
                        Pionowa(parY + first + fontsize / 2, scnd, Switched, x + k);
                        // Pozioma(parX, x + k, parY + first + fontsize / 2, Switched);
                        if (IEditableDirWithChildren.Children[i].Children.Count != 0)
                        {
                            Sort(IEditableDirWithChildren.Children[i], Switched, x + k, parY + oyFolderDistance + fontsize / 2, window);
                            k += 2 * GetSize(IEditableDirWithChildren.Children[i]);
                        }
                        else
                        {
                            k += folder_doubled_width_size;
                        }

                    }
                    x = GetSideSize(IEditableDirWithChildren.Children[0], 0);
                    Create(Switched, parY + oyFolderDistance + fontsize / 2, x + k, IEditableDirWithChildren.Children[0], window);
                    Pionowa(parY + first + fontsize / 2, scnd, Switched, x + k);
                    //Pozioma(parX, x + k, parY + first + fontsize / 2, Switched);
                    if (IEditableDirWithChildren.Children[0].Children.Count != 0)
                    {
                        Sort(IEditableDirWithChildren.Children[0], Switched, x + k, parY + oyFolderDistance + fontsize / 2, window);
                    }
                    first_posX = x + k;

                    k = parX;
                    for (int i = mid + 1; i < IEditableDirWithChildren.Children.Count - 1; i++)
                    {
                        x = -1 * GetSideSize(IEditableDirWithChildren.Children[i], 1);
                        Create(Switched, parY + oyFolderDistance + fontsize / 2, x + k, IEditableDirWithChildren.Children[i], window);
                        Pionowa(parY + first + fontsize / 2, scnd, Switched, x + k);
                        // Pozioma(parX, x + k, parY + first + fontsize / 2, Switched);
                        if (IEditableDirWithChildren.Children[i].Children.Count != 0)
                        {
                            Sort(IEditableDirWithChildren.Children[i], Switched, x + k, parY + oyFolderDistance + fontsize / 2, window);
                            k -= 2 * GetSize(IEditableDirWithChildren.Children[i]);
                        }
                        else
                        {
                            k -= folder_doubled_width_size;
                        }
                    }
                    x = -1 * GetSideSize(IEditableDirWithChildren.Children[IEditableDirWithChildren.Children.Count - 1], 1);
                    Create(Switched, parY + oyFolderDistance + fontsize / 2, x + k, IEditableDirWithChildren.Children[IEditableDirWithChildren.Children.Count - 1], window);
                    Pionowa(parY + first + fontsize / 2, scnd, Switched, x + k);
                    //Pozioma(parX, x + k, parY + first + fontsize / 2, Switched);
                    if (IEditableDirWithChildren.Children[IEditableDirWithChildren.Children.Count - 1].Children.Count != 0)
                    {
                        Sort(IEditableDirWithChildren.Children[IEditableDirWithChildren.Children.Count - 1], Switched, x + k, parY + oyFolderDistance + fontsize / 2, window);
                    }
                    last_posX = x + k;
                    Pozioma(first_posX, last_posX, parY + first + fontsize / 2, Switched);
                }
                else if (IEditableDirWithChildren.Children.Count % 2 != 0 && IEditableDirWithChildren.ShowContent == true) //nieparzyste
                {
                    mid = IEditableDirWithChildren.Children.Count / 2;
                    Pionowa(parY + first + fontsize / 2, scnd, Switched, parX);
                    Create(Switched, parY + oyFolderDistance + fontsize / 2, parX, IEditableDirWithChildren.Children[mid], window); //srodkowy
                    if (IEditableDirWithChildren.Children[mid].Children.Count != 0)
                    {
                        Sort(IEditableDirWithChildren.Children[mid], Switched, parX, parY + oyFolderDistance + fontsize / 2, window);
                        k = parX + GetSideSize(IEditableDirWithChildren.Children[mid], 1);
                    }
                    else
                    {
                        k = parX + folder_width_size;
                    }
                    if (mid != 0)
                    {
                        for (int i = mid - 1; i > 0; i--)
                        {
                            x = GetSideSize(IEditableDirWithChildren.Children[i], 0);
                            Pionowa(parY + first + fontsize / 2, scnd, Switched, x + k);
                            Create(Switched, parY + oyFolderDistance + fontsize / 2, x + k, IEditableDirWithChildren.Children[i], window);
                            //Pozioma(parX, x + k, parY + first + fontsize / 2, Switched);
                            if (IEditableDirWithChildren.Children[i].Children.Count != 0)
                            {
                                Sort(IEditableDirWithChildren.Children[i], Switched, x + k, parY + oyFolderDistance + fontsize / 2, window);
                                k += 2 * GetSize(IEditableDirWithChildren.Children[i]);
                            }
                            else
                            {
                                k += folder_doubled_width_size;
                            }
                        }
                        x = GetSideSize(IEditableDirWithChildren.Children[0], 0);
                        Pionowa(parY + first + fontsize / 2, scnd, Switched, x + k);
                        Create(Switched, parY + oyFolderDistance + fontsize / 2, x + k, IEditableDirWithChildren.Children[0], window);
                        //Pozioma(parX, x + k, parY + first + fontsize / 2, Switched);
                        if (IEditableDirWithChildren.Children[0].Children.Count != 0)
                        {
                            Sort(IEditableDirWithChildren.Children[0], Switched, x + k, parY + oyFolderDistance + fontsize / 2, window);
                        }
                        first_posX = x + k;
                        //dla drugiej strony podobnie, ale w druga strone ;
                        k = parX - GetSideSize(IEditableDirWithChildren.Children[mid], 0);

                        for (int i = mid + 1; i < IEditableDirWithChildren.Children.Count - 1; i++)
                        {
                            x = -1 * GetSideSize(IEditableDirWithChildren.Children[i], 1);
                            Create(Switched, parY + oyFolderDistance + fontsize / 2, x + k, IEditableDirWithChildren.Children[i], window);
                            Pionowa(parY + first + fontsize / 2, scnd, Switched, x + k);
                            //Pozioma(parX, x + k, parY + first + fontsize / 2, Switched);
                            if (IEditableDirWithChildren.Children[i].Children.Count != 0)
                            {
                                Sort(IEditableDirWithChildren.Children[i], Switched, x + k, parY + oyFolderDistance + fontsize / 2, window);
                                k -= 2 * GetSize(IEditableDirWithChildren.Children[i]);
                            }
                            else
                            {
                                k -= folder_doubled_width_size;
                            }
                        }
                        x = -1 * GetSideSize(IEditableDirWithChildren.Children[IEditableDirWithChildren.Children.Count - 1], 1);
                        Create(Switched, parY + oyFolderDistance + fontsize / 2, x + k, IEditableDirWithChildren.Children[IEditableDirWithChildren.Children.Count - 1], window);
                        Pionowa(parY + first + fontsize / 2, scnd, Switched, x + k);
                        //Pozioma(parX, x + k, parY + first + fontsize / 2, Switched);
                        if (IEditableDirWithChildren.Children[IEditableDirWithChildren.Children.Count - 1].Children.Count != 0)
                        {
                            Sort(IEditableDirWithChildren.Children[IEditableDirWithChildren.Children.Count - 1], Switched, x + k, parY + oyFolderDistance + fontsize / 2, window);
                        }
                        last_posX = x + k;
                        Pozioma(first_posX, last_posX, parY + first + fontsize / 2, Switched);
                    }

                }
            }
        }
    }
}

