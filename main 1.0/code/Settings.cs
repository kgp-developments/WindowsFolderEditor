using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ReFolder.Dir;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace main_1._0
{
    
    public class Settings
    {
        static MainWindow AppMW = (MainWindow)Application.Current.MainWindow;
        readonly string path_style = @"C:..\..\set\stylesettings.txt";
        readonly string path_mwsize = @"C:..\..\set\mwsize.txt";
        readonly string path_cawsize = @"..\..\set\cawsize.txt";

        public string language;
        //public string format;
        public string fontSize;
        public string color;
        public string OYdis;
        public string RHV;
        public string MWheight;
        public string MWwidth;
        public string CAWheight;
        public string CAWwidth;


        public void GetStyleSettings()
        {
            System.IO.StreamReader settings = File.OpenText(path_style);
            string x = settings.ReadLine();
            language = x.Substring(11, x.Length - 11);
            x = settings.ReadLine();
            fontSize = x.Substring(11, x.Length - 11);
            if (fontSize == "ten")
            {
                AppMW.sorteritno.fontsize = 10;
            }
            else if (fontSize == "twelve")
            {
                AppMW.sorteritno.fontsize = 12;

            }
            else if(fontSize == "fourteen")
            {
                AppMW.sorteritno.fontsize = 14;

            }
            else if (fontSize == "sixteen")
            {
                AppMW.sorteritno.fontsize = 16;

            }
            else if (fontSize == "eighteen")
            {
                AppMW.sorteritno.fontsize = 18;

            }
            x = settings.ReadLine();
            color = x.Substring(8, x.Length - 8);
            x = settings.ReadLine();
            OYdis = x.Substring(8, x.Length - 8);
            if (OYdis == "close")
            {
                AppMW.sorteritno.oyFolderDistance = 60;
                AppMW.sorteritno.first = 45;
                AppMW.sorteritno.scnd = 15;

            }
            else if (OYdis == "mid")
            {
                AppMW.sorteritno.oyFolderDistance = 120;
                AppMW.sorteritno.first = 75;
                AppMW.sorteritno.scnd = 45;
            }
            else if(OYdis == "far")
            {
                AppMW.sorteritno.oyFolderDistance = 180;
                AppMW.sorteritno.first = 105;
                AppMW.sorteritno.scnd = 75;
            }
            x = settings.ReadLine();
            RHV = x.Substring(6, x.Length - 6);
            x = settings.ReadLine();
            settings.Close();
        }
        public void GetMWSize()
        {
            System.IO.StreamReader settings = File.OpenText(path_mwsize);
            MWheight = settings.ReadLine();
            MWwidth = settings.ReadLine();
        }
        public void GetCAWsize()
        {
            System.IO.StreamReader settings = File.OpenText(path_cawsize);
            CAWheight = settings.ReadLine();
            CAWwidth = settings.ReadLine();
        }
        public void ApplyStyleMW()
        {
                AppMW.HorizontalStyleSwitch(bool.Parse(RHV));
            BrushConverter bc = new BrushConverter();
            if (color == "dark")
            {
                AppMW.RamkaDrzewa.Background = (Brush)bc.ConvertFrom("#FF0F1487");
                LinearGradientBrush lgd = new LinearGradientBrush();
                lgd.GradientStops.Add(new GradientStop(Colors.White, 1.9));
                lgd.GradientStops.Add(new GradientStop(Colors.Black, 0));
                AppMW.EdycjaDrzewa.Background = lgd;
                AppMW.EdycjaDrzewaTextBlock.Foreground = Brushes.AliceBlue;
                AppMW.EdycjaFolderuTextblock.Foreground = Brushes.AliceBlue;
                lgd = new LinearGradientBrush();
                lgd.GradientStops.Add(new GradientStop(Colors.White, 4));
                lgd.GradientStops.Add(new GradientStop(Colors.Black, -0.5));
                AppMW.EdycjaFolderu.Background = lgd;
                AppMW.KomendyUzytkownika.Background = (Brush)bc.ConvertFrom("#FF373837");
                lgd = new LinearGradientBrush();
                lgd.StartPoint = new Point(0.5, 0);
                lgd.EndPoint = new Point(0.5, 1);
                lgd.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#FF080C6E"), 0.9));
                lgd.GradientStops.Add(new GradientStop(Colors.Black, 0));
                AppMW.MenuGlowne.Background = lgd;
                AppMW.Height = double.Parse(MWheight);
                AppMW.Width = double.Parse(MWwidth);
            }
        }
        public void ApplyStyleCAW()
        {
            AppMW.CAW.Width = double.Parse(CAWwidth);
            AppMW.CAW.Height = double.Parse(CAWheight);
            if (color == "dark")
            {
                LinearGradientBrush lgd = new LinearGradientBrush();
                lgd.EndPoint = new Point(0.5, 1);
                lgd.StartPoint = new Point(0.5, 0);
                lgd.GradientStops.Add(new GradientStop(Colors.Black, 0));
                lgd.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#FF7E7E7E"), 0.762));
                lgd.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#FF4B4B4B"), 0.486));
                lgd.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#FF070B5B"), 1));
                if (AppMW.CAW != null)
                {
                    AppMW.CAW.ComplexBranchAdditionGrid.Background = lgd;
                    AppMW.CAW.ExistingBranchAdditionGrid.Background = lgd;
                    AppMW.CAW.bindingTextColorHelper.Background = Brushes.AliceBlue;
                    AppMW.CAW.bindingInactiveColorHelper.Background = Brushes.DarkGray;
                    AppMW.CAW.bindingActiveColorHelper.Background = Brushes.Black;
                    lgd = new LinearGradientBrush();
                    lgd.EndPoint = new Point(0.5, 1);
                    lgd.StartPoint = new Point(0.5, 0);
                    lgd.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#FF7E7E7E"), 0.203));
                    lgd.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#FF0F1487"), 1));
                    AppMW.CAW.CAWbackGrid.Background = lgd;
                }
                
            }
        }

        public void SaveLatestMWSize(string[] newSettings)
        {
            File.WriteAllLines(path_mwsize, newSettings);
        }
        public void SaveLatestCAWSize(string[] newSettings)
        {
            File.WriteAllLines(path_cawsize, newSettings);
        }
        public void ChangeStyleSettings(string[] newSettings)
        {
           File.WriteAllLines(path_style, newSettings);
        }
    }
}
