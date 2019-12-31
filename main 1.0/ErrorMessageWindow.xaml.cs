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
    /// Interaction logic for ErrorMessageWindow.xaml
    /// </summary>
    public partial class ErrorMessageWindow : Window
    {

        public ErrorMessageWindow()
        {
            InitializeComponent();
        }

        public void SetMessage(int errorDependance)
        {
            if (errorDependance == 1)
            {
                ErrorShowingText.Text = "Przekroczono dopuszczalny limit folderów obsługiwanych przez aplikacje.";
            }
            else if (errorDependance == 2)
            {
                ErrorShowingText.Text = "Wstawiana ilość folderów przekroczy dopuszczalną ilość folderów obsługiwanych przez aplikacje, spróbuj skopiować mniejszą ich ilość.";
            }
            else if (errorDependance == 3)
            {
                ErrorShowingText.Text = "Docelowy folder przekroczył dopuszczalną długość ścieżki.";
            }
            else if (errorDependance == 4)
            {
                ErrorShowingText.Text = "Wstawiana gałąź przekroczyła dopuszczalne długości ścieżek. Fragment gałęzi został pominięty.";
            }
        }

        private void EMWconfirm_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }
        private void EMWconfirm_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}
