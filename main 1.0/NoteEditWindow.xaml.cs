using System.Windows;
using System.Windows.Input;

namespace main_1._0
{
    /// <summary>
    /// Interaction logic for NoteEditWindow.xaml
    /// </summary>

    public partial class NoteEditWindow : Window
    {
        static MainWindow AppMW = (MainWindow)Application.Current.MainWindow;

        public NoteEditWindow()
        {
            InitializeComponent();
        }

        private void ApplyChanges_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AppMW.CurrentlyChosenDir.Description.Note = DirsNote.Text;
            this.Close();
        }

        private void Cancel_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }


        private void AlwaysTrueForCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

    }
}
