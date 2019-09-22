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
using ReFolder.Dir;
using ReFolder.Dir.Description;
using ReFolder.Management;
using ReFolder.Memento;

namespace main_1._0
{
    /// <summary>
    /// Interaction logic for ComplexAdditionWindow.xaml
    /// </summary>
    public partial class ComplexAdditionWindow : Window
    {
        static MainWindow AppMW = (MainWindow)Application.Current.MainWindow;
        IEditableDirWithChildren NewSeed;
        public static Canvas CurrentlyChosenCAW = null;
        public IEditableDirWithChildren CurrentlyChosenDir = null;

        public ComplexAdditionWindow()
        {
            InitializeComponent();
            //NewSeed = (ChildDir)AppMW.CurrentlyChosenDir;
            if (AppMW.Seed != null)
            {
                NewSeed = new MainDir(AppMW.CurrentlyChosenDir.Description);
                CurrentlyChosenDir = NewSeed;
                NewSeed.IsMarked = true;
                //ChildDir sa = new ChildDir("test", NewSeed);
                //NewSeed.Children.Add(sa);
                //ChildDir sas = new ChildDir("test", NewSeed);
                //NewSeed.Children.Add(sas);
                ZoomSlider.Value = 1;

                AppMW.sorteritno.Create(DisplayedBranchGrid, 30, 0, NewSeed, "CAW");
                AppMW.sorteritno.Sort(NewSeed, DisplayedBranchGrid, 0, 30, "CAW");
                ChosenNameSeries.SelectedItem = Default;
                //AppMW.sorteritno.ResetTree(DisplayedBranchGrid, null, NewSeed, drzewo);
            }
            //AppMW.settings.ApplyStyleCAW();
        }

        #region Inne
        private void SaveSize()
        {
            string[] size = new string[2];
            size[0] = this.Height.ToString();
            size[1] = this.Width.ToString();
            if (size[0] != AppMW.settings.CAWheight && size[1] != AppMW.settings.CAWwidth)
            {
                AppMW.settings.SaveLatestCAWSize(size);
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Filter out non-digit text input
            foreach (char c in e.Text)
                if (!Char.IsDigit(c))
                {
                    e.Handled = true;
                    break;
                }
        }
        private void SetComplexHelper(int steps, IEditableDirWithChildren Targeted)
        {
            if (steps < int.Parse(AmountOfSerialTB.Text))
            {
                //Target = AppMW.CurrentlyChosenDir;
                for (int j = 0; j < int.Parse(AmountOfParallelTB.Text); j++)
                {
                    ChildDir NewDir = new ChildDir(SetGeneratedName(ChosenNameSeries.SelectedItem, Targeted), Targeted);
                    Targeted.Children.Add(NewDir);
                    SetComplexHelper(steps + 1, NewDir);
                }
            }
        }

        string SetGeneratedName(object check, IEditableDirWithChildren Parent)
        {
            List<string> namesInSystem = new List<string>();
            // names to ignore
            string[] namesToIgnore = new string[AppMW.CurrentlyChosenDir.Children.Count + namesInSystem.Count];




            //selecting names to ignore
            for (int j = 0; j < AppMW.CurrentlyChosenDir.Children.Count; j++)
            {
                namesToIgnore[j] = AppMW.CurrentlyChosenDir.Children[j].Description.Name;

                if (j == AppMW.CurrentlyChosenDir.Children.Count - 1 && DirValidate.GetDefaultInstance().IsfolderExisting(AppMW.CurrentlyChosenDir.Description.FullName))
                {
                    namesInSystem.CopyTo(namesToIgnore, ++j);

                }
            }


            if (check == Default)
            {
                return DirManagement.GetDefaultInstance().GeneratetName_Default(Parent, namesToIgnore: namesToIgnore);
            }
            else if (check == Numbers)
            {
                return DirManagement.GetDefaultInstance().GenerateName_Number(Parent, 1, namesToIgnore: namesToIgnore);
            }
            else if (check == NumSiPar)
            {
                return DirManagement.GetDefaultInstance().GenerateName_Number_Text_ParentName(Parent, SignTB.Text, namesToIgnore: namesToIgnore);
            }
            else if (check == ParSiNum)
            {
                return DirManagement.GetDefaultInstance().GenerateName_ParentName_Text_Number(Parent, SignTB.Text, namesToIgnore: namesToIgnore);
            }
            return "If you see this message - report us an error";
        }

        #endregion

        #region _Executed
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
            AppMW.sorteritno.ResetTree(DisplayedBranchGrid, ResetHighlight, NewSeed, drzewo, "CAW");
        }
        private void HighlightChosen_Executed(object sender, ExecutedRoutedEventArgs e)
        {


            Button HiddenButton = (Button)e.Parameter;
            Canvas MainLayer = (Canvas)HiddenButton.Parent;
            if (CurrentlyChosenCAW != null)
            {

                CurrentlyChosenCAW.Background = Brushes.LightGray;
                CurrentlyChosenDir = (IEditableDirWithChildren)CurrentlyChosenCAW.Tag;
                CurrentlyChosenDir.IsMarked = false;

            }
            MainLayer.Background = Brushes.Blue;
            CurrentlyChosenCAW = MainLayer;

            CurrentlyChosenDir = (IEditableDirWithChildren)CurrentlyChosenCAW.Tag;
            CurrentlyChosenDir.IsMarked = true;
        }
        private void ResetHighlight_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (CurrentlyChosenCAW != null)
            {
                CurrentlyChosenCAW.Background = Brushes.LightGray;
                CurrentlyChosenDir = (IEditableDirWithChildren)CurrentlyChosenCAW.Tag;
                CurrentlyChosenDir.IsMarked = false;
                CurrentlyChosenCAW = null;
                CurrentlyChosenDir = null;
            }
        }

        private void FoldSwitch_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Button Trigger = (Button)e.Parameter;
            if (Trigger.Name == "ComplexBranchAddidionFoldButton" && ExistingBranchAdditionGrid.Visibility == Visibility.Visible)
            {
                ExistingBranchAdditionGrid.Visibility = Visibility.Collapsed;
                ExistingBranchAdditionFold.Background = bindingInactiveColorHelper.Background; ;
                ComplexBranchAddidionFold.Background = bindingActiveColorHelper.Background;
            }
            else if (Trigger.Name == "ExistingBranchAdditionFoldButton" && ExistingBranchAdditionGrid.Visibility == Visibility.Collapsed)
            {
                ExistingBranchAdditionGrid.Visibility = Visibility.Visible;
                ExistingBranchAdditionFold.Background = bindingActiveColorHelper.Background;
                ComplexBranchAddidionFold.Background = bindingInactiveColorHelper.Background;
            }

        }

        private void SetComplexAddition_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            IEditableDirWithChildren Target = CurrentlyChosenDir;



            int i;
            //string generateName;
            if (TypeOfSeries.SelectedItem == Serial)
            {
                for (i = 0; i < int.Parse(AmountInSeriesTB.Text); i++)
                {
                    ChildDir NewDir = new ChildDir(SetGeneratedName(ChosenNameSeries.SelectedItem, Target), Target);
                    Target.Children.Add(NewDir);
                    Target = NewDir;
                }
            }
            else if (TypeOfSeries.SelectedItem == Parallel)
            {
                for (i = 0; i < int.Parse(AmountInSeriesTB.Text); i++)
                {
                    ChildDir NewDir = new ChildDir(SetGeneratedName(ChosenNameSeries.SelectedItem, Target), Target);
                    Target.Children.Add(NewDir);
                }
            }
            else if (TypeOfSeries.SelectedItem == Complex)
            {
                for (i = 0; i < int.Parse(AmountInSeriesTB.Text); i++)
                {
                    int k = 0; //steps to match with AmountOfSerialTB
                    SetComplexHelper(k, Target);
                }
            }
            AppMW.sorteritno.ResetTree(DisplayedBranchGrid, ResetHighlight, NewSeed, drzewo, "CAW");
        }
        private void ApplyChangesCAW_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Button ThisButton = (Button)e.Parameter;
            SaveAndReadElementInBinaryFile.GetDefaultInstance()
    .WriteToBinaryFile<IEditableDirWithChildren>(@"..\..\..\TemporaryFiles\tempFile~CopyCAW",CurrentlyChosenDir);
            List<IEditableDirWithChildrenAndParent> children =
    SaveAndReadElementInBinaryFile.GetDefaultInstance()
    .ReadFromBinaryFile<IEditableDirWithChildren>(@"C:..\..\..\TemporaryFiles\tempFile~CopyCAW")
    .Children;


            AppMW.CurrentlyChosenDir.AddChildrenToChildrenList(children);
            MainDir.AutoGenerateChildrenFullName(AppMW.CurrentlyChosenDir);

            AppMW.sorteritno.scale = (float)AppMW.ZoomSlider.Value;
            AppMW.sorteritno.ResetTree(AppMW.ResTree, AppMW.ResetHighlight, AppMW.Seed, AppMW.drzewo, "MW");
            if(ThisButton.Name == "OkBtn")
            {
                this.Close();
            }
            AddMemento();
        }
        #endregion
        private void CancelCAW_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }
        #region _CanExecute
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
        private void SetComplexAddition_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (TypeOfSeries != null && TypeOfSeries.SelectedItem != null && CurrentlyChosenDir != null)
            {
                if (AmountInSeriesTB.Text.Length > 0)
                {
                    if (TypeOfSeries.SelectedItem == Complex)
                    {
                        if (AmountOfParallelTB.Text.Length > 0 && AmountOfSerialTB.Text.Length > 0)
                        {
                            if (ChosenNameSeries.SelectedItem == NumSiPar || ChosenNameSeries.SelectedItem ==  ParSiNum)
                            {
                                if (SignTB.Text.Length > 0)
                                {
                                    e.CanExecute = true;
                                }
                                else
                                {
                                    e.CanExecute = false;
                                }
                            }
                            else
                            {
                                e.CanExecute = true;
                            }
                        }
                        else
                        {
                            e.CanExecute = false;
                        }
                    }
                    else
                    {
                        if (ChosenNameSeries.SelectedItem == NumSiPar || ChosenNameSeries.SelectedItem == ParSiNum)
                        {
                            if (SignTB.Text.Length > 0)
                            {
                                e.CanExecute = true;
                            }
                            else
                            {
                                e.CanExecute = false;
                            }
                        }
                        else
                        {
                            e.CanExecute = true;
                        }
                    }
                }
            }
            else
            {
                e.CanExecute = false;
            }
        }
        private void AlwaysTrueForCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void NewSeedNotEmpty(object sender, CanExecuteRoutedEventArgs e)
        {
            if (NewSeed != null)
            {
                if (NewSeed.Children.Count != 0)
                {
                    e.CanExecute = true;
                }
                else
                {
                    e.CanExecute = false;
                }
            }
            else
            {
                e.CanExecute = false;
            }
        }
        #endregion

        private void AddMemento()
        {
            Orginator.State = AppMW.Seed;
            Caretaker.AddMemento(Orginator.Save());

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveSize();
            AppMW.sorteritno.scale = (float)AppMW.ZoomSlider.Value;
        }

        private void ZoomSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (NewSeed != null)
            {
                AppMW.sorteritno.scale = (float)ZoomSlider.Value;
                AppMW.sorteritno.ResetTree(DisplayedBranchGrid, ResetHighlight, NewSeed, drzewo, "CAW");
            }
        }

        private void TypeOfSeries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AmountInSeries.Visibility = Visibility.Visible;
            if (TypeOfSeries.SelectedItem == Complex)
            {
                AmountOfElements.Visibility = Visibility.Visible;
                AmountOfSteps.Visibility = Visibility.Visible;
                AmountInSeries.Visibility = Visibility.Visible;
            }
            else
            {
                AmountOfElements.Visibility = Visibility.Collapsed;
                AmountOfSteps.Visibility = Visibility.Collapsed;
            }
        }

        private void ChosenNameSeries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ChosenNameSeries.SelectedItem == NumSiPar || ChosenNameSeries.SelectedItem == ParSiNum)
            {
                ChosenSign.Visibility = Visibility.Visible;
            }
            else
            {
                ChosenSign.Visibility = Visibility.Collapsed;
            }
        }

    }
}
