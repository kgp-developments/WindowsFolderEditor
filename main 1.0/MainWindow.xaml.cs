﻿using System;
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
using ReFolder.Memento;

namespace main_1._0
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    
        
    public partial class MainWindow : Window
    {
        // folder główny z którego rozpoczyna się dziedziczenie 
        readonly IEditableDirWithChildren  Main;
        List<StackPanel> Panels = new List<StackPanel>(); // lista paneli z gornego paska, np. plik, widok
        bool rightHandedView = true; // zmienna okreslajaca widok (leworeczny/praworeczny)
        public static Canvas CurrentlyChosen = null;
        public IEditableDirWithChildren CurrentlyChosenDir = null;
        public IEditableDirWithChildren Seed;
        List<IEditableDirWithChildrenAndParent> CopyOfChildren;
        public Sorteritno sorteritno = new Sorteritno();
        public ComplexAdditionWindow CAW;
        NameEditionWindow NEW;
        public ViewWindow VW;
        public Settings settings;
        public CreateNewTreeWindow CNTW;
        public LoadTreeWindow LTW;
        // zawiera inicjalizację Dirów do testu
        public MainWindow()
        {
            InitializeComponent();

            Panels.Add(PanelPliku);
            Panels.Add(PanelWidoku);
            Panels.Add(PanelEdycji);
            //HideAllPanels();
            settings = new Settings();
            settings.GetStyleSettings();
            settings.GetMWSize();
            settings.ApplyStyleMW();


            MainDir Main = DirManagement.GetDefaultInstance().GetFolderAsNewMainDir(@"C:\Users\Klakier\Desktop\kociFolderek");
            Seed = Main;


            sorteritno.Create(ResTree, 30, 0, Seed, "MW");

            sorteritno.Sort(Seed, ResTree, 0, 30, "MW");
            ZoomSlider.Value = 1;

            AddMemento();
        }


        #region funkcje składowe komend
        private void SaveSize()
        {
            string[] size = new string[2];
            size[0] = this.Height.ToString();
            size[1] = this.Width.ToString();
            if (size[0] != settings.MWheight && size[1] != settings.MWwidth)
            {
                settings.SaveLatestMWSize(size);
            }
        }

        private void HideAllPanels()
        {
            foreach (StackPanel element in Panels)
            {
                element.Visibility = Visibility.Collapsed;
            }
        }
        private void AddMemento()
        {
            Orginator.State = Seed;
            Caretaker.AddMemento(Orginator.Save());

        }
        public void HorizontalStyleSwitch(bool check)
        {
            if (!check)
            {
                RamkaDrzewa.Margin = new Thickness(190, 20, 0, 61);
                EdycjaDrzewa.HorizontalAlignment = HorizontalAlignment.Left;
                EdycjaFolderu.HorizontalAlignment = HorizontalAlignment.Right;
                KomendyUzytkownika.Margin = new Thickness(190, 0, 420, 0);
            }
            else
            {
                RamkaDrzewa.Margin = new Thickness(0, 20, 190, 61);
                EdycjaDrzewa.HorizontalAlignment = HorizontalAlignment.Right;
                EdycjaFolderu.HorizontalAlignment = HorizontalAlignment.Left;
                KomendyUzytkownika.Margin = new Thickness(420, 0, 190, 0);
            }
        }
        public void RestoreMainLayout()
        {
            sorteritno.ResetTree(ResTree, ResetHighlight, Seed, drzewo, "MW");
        }


        #endregion

        #region funkcje komend _Executed
        private void LTWshow_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            LTW = new LoadTreeWindow();
            LTW.ShowDialog();
        }

        private void ViewWindowShow_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            VW = new ViewWindow();
            VW.ShowDialog();
        }
        private void UndoChanges_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Seed = Orginator.Restore(Caretaker.GetMemento(Caretaker.CurrentMemento - 1));
            sorteritno.ResetTree(ResTree, ResetHighlight, Seed, drzewo, "MW");
        }
        private void RedoChanges_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Seed = Orginator.Restore(Caretaker.GetMemento(Caretaker.CurrentMemento + 1));
            sorteritno.ResetTree(ResTree, ResetHighlight, Seed, drzewo, "MW");
        }
        private void CNTWshow_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            CNTW = new CreateNewTreeWindow();
            CNTW.ShowDialog();
        }
        private void DeleteDir_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Button Clicked = (Button)e.Parameter;
            IEditableDirWithChildrenAndParent Target = (IEditableDirWithChildrenAndParent)CurrentlyChosenDir;

            if (Clicked.Name == "DeleteMiddlebtn")
            {
                List<IEditableDirWithChildrenAndParent> TemporaryList = new List<IEditableDirWithChildrenAndParent>();
                for (int i = Target.ParentDir.Children.Count - 1; i > Target.ParentDir.Children.IndexOf(Target); i--)
                {
                    IEditableDirWithChildrenAndParent child = Target.ParentDir.Children[i];
                    if (Target.ParentDir.Children.IndexOf(child) > Target.ParentDir.Children.IndexOf(Target))
                    {
                        TemporaryList.Insert(TemporaryList.Count, child);
                        Target.ParentDir.DeleteChildDirFromList(child);
                    }
                }
                foreach (IEditableDirWithChildrenAndParent child in Target.Children)
                {
                    child.ParentDir = Target.ParentDir;
                    Target.ParentDir.AddChildToChildrenList(child);
                }
                Target.ParentDir.AddChildrenToChildrenList(TemporaryList);
            }
            Target.ParentDir.DeleteChildDirFromList(Target);

            CurrentlyChosen = null;
            CurrentlyChosenDir = null;
            AddMemento();
            sorteritno.ResetTree(ResTree, ResetHighlight, Seed, drzewo, "MW");
        }
        private void NameEditionWindow_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            NEW = new NameEditionWindow();
            NEW.ShowDialog();
        }

        private void ComplexAdditionWindowShow_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            CAW = new ComplexAdditionWindow();
            settings.GetCAWsize();
            settings.ApplyStyleCAW();
            CAW.ShowDialog();
        }
        private void GenerateDirs_Executed(object sender,ExecutedRoutedEventArgs e)
        {
            
            DirManagement management = DirManagement.GetDefaultInstance();
            MemoryDirs memoryDirs = MemoryDirs.GetDefaultInstance();
            memoryDirs.InitializeAllChildren(Seed);
            DirWrite.GetDefaultInstance().GenerateAllChildrenDirsAsFolders();
        }
        private void CopyChildrenDirs_Executed(object sender, ExecutedRoutedEventArgs e)
        {

            SaveAndReadElementInBinaryFile.GetDefaultInstance()
                .WriteToBinaryFile<IEditableDirWithChildren>(@"..\..\..\TemporaryFiles\tempFile~Copy", CurrentlyChosenDir);
            Pastebtn.Command = KGPcommands.PasteChildrenDirs;
        }

        private void PasteChildrenDirs_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            List<IEditableDirWithChildrenAndParent> CopyOfChildren =
                SaveAndReadElementInBinaryFile.GetDefaultInstance()
                .ReadFromBinaryFile<IEditableDirWithChildren>(@"C:..\..\..\TemporaryFiles\tempFile~Copy")
                .Children;

            var validate= DirValidate.GetDefaultInstance();
            foreach (IEditableDirWithChildrenAndParent child in CopyOfChildren)
            {

                if (validate.IsDirExistingAsFolderAndChild(CurrentlyChosenDir, child.Description.Name))
                {
                    child.Description.Name = DirManagement.GetDefaultInstance().GeneratetName_Default(CurrentlyChosenDir,1,child.Description.Name);
                }

                child.ParentDir = CurrentlyChosenDir;
            }
            CurrentlyChosenDir.AddChildrenToChildrenList(CopyOfChildren);
            MainDir.AutoGenerateChildrenFullName(CurrentlyChosenDir);
            AddMemento();
            sorteritno.ResetTree(ResTree, ResetHighlight, Seed, drzewo, "MW");
        }
        private void PasteDir_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            IEditableDirWithChildrenAndParent childDir =
               SaveAndReadElementInBinaryFile.GetDefaultInstance()
               .ReadFromBinaryFile<IEditableDirWithChildrenAndParent>(@"C:..\..\..\TemporaryFiles\tempFile~Copy");

            var validate= DirValidate.GetDefaultInstance();


                if (validate.IsDirExistingAsFolderAndChild(CurrentlyChosenDir, childDir.Description.Name))
                {
                    childDir.Description.Name = DirManagement.GetDefaultInstance().GeneratetName_Default(CurrentlyChosenDir,1, childDir.Description.Name);
                }

                childDir.ParentDir = CurrentlyChosenDir;
            
            CurrentlyChosenDir.AddChildToChildrenList(childDir);
            MainDir.AutoGenerateChildrenFullName(CurrentlyChosenDir);
            AddMemento();
            sorteritno.ResetTree(ResTree, ResetHighlight, Seed, drzewo, "MW");

        }

        private void CopyDir_Executed(object sender, ExecutedRoutedEventArgs e)
        {

            IEditableDirWithChildrenAndParent child = new ChildDir(CurrentlyChosenDir.Description, CurrentlyChosenDir.Children);

            SaveAndReadElementInBinaryFile.GetDefaultInstance()
                 .WriteToBinaryFile<IEditableDirWithChildrenAndParent>(@"..\..\..\TemporaryFiles\tempFile~Copy", child);
            Pastebtn.Command = KGPcommands.PasteDir;

        }
        private void ShowHide_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            StackPanel PanelGiven = (StackPanel)e.Parameter;
            if (PanelGiven.IsVisible)
            {
                PanelGiven.Visibility = Visibility.Collapsed;
            }
            else
            {
                PanelGiven.Visibility = Visibility.Visible;
                foreach(StackPanel element in Panels)
                {
                    if (element.Name != PanelGiven.Name)
                    {
                        element.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }
        private void HighlightChosen_Executed(object sender, ExecutedRoutedEventArgs e)
        {

                
            Button HiddenButton = (Button)e.Parameter;
            Canvas MainLayer = (Canvas)HiddenButton.Parent;
            if(CurrentlyChosen != null )
            {

                CurrentlyChosen.Background = Brushes.LightGray;
                CurrentlyChosenDir = (IEditableDirWithChildren)CurrentlyChosen.Tag;
                CurrentlyChosenDir.IsMarked = false;

            }
            MainLayer.Background = Brushes.Blue;
            CurrentlyChosen = MainLayer;

                CurrentlyChosenDir = (IEditableDirWithChildren)CurrentlyChosen.Tag;
                CurrentlyChosenDir.IsMarked = true;

        }
        private void ResetHighlight_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if(CurrentlyChosen != null)
            {
                CurrentlyChosen.Background = Brushes.LightGray;
                CurrentlyChosenDir = (IEditableDirWithChildren)CurrentlyChosen.Tag;
                CurrentlyChosenDir.IsMarked = false;
                CurrentlyChosen = null;
                CurrentlyChosenDir = null;
            }
            HideAllPanels();

        }
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
            AddMemento();
            sorteritno.ResetTree(ResTree, ResetHighlight, Seed, drzewo, "MW");
        }
        private void DefaultAddition_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            string name= DirManagement.GetDefaultInstance().GeneratetName_Default(CurrentlyChosenDir);
            IEditableDirWithChildrenAndParent NewDir = new ChildDir(name, CurrentlyChosenDir);
            CurrentlyChosenDir.AddChildToChildrenList(NewDir);
            AddMemento();
            sorteritno.ResetTree(ResTree, ResetHighlight, Seed, drzewo, "MW");
        }
        #endregion

        #region funkcje komend _CanExecute
        private void UndoChanges_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (Caretaker.CurrentMemento > 0)
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }
        }
        private void RedoChanges_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if(Caretaker.CurrentMemento +1< Caretaker.CountMemento())
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }

        }
        private void ChosenNotNullMainNeither(object sender, CanExecuteRoutedEventArgs e)
        {
            if (CurrentlyChosen == null || CurrentlyChosenDir == Seed)
            {
                e.CanExecute = false;
            }
            else
            {
                e.CanExecute = true;
            }
        }

        private void ChosenNotNullDepended(object sender, CanExecuteRoutedEventArgs e)
        {
            if(CurrentlyChosen == null)
            {
                e.CanExecute = false;
            }
            else
            {
                e.CanExecute = true;
            }
        }
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
        private void CopyChildrenDirs_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            if (CurrentlyChosen == null) e.CanExecute = false;
            else
            {
                e.CanExecute = true;
            }

        }
        
        private void PasteChildrenDirs_CanExecute(object sender, CanExecuteRoutedEventArgs e)
            {
            if (CurrentlyChosenDir == null && (CopyOfChildren == null || CopyOfChildren.Count == 0)) e.CanExecute = false;
            else e.CanExecute= true;
        }
        private void AlwaysTrueForExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        #endregion

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveSize();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (ZoomSlider != null && userCommand != null)
            {
                userCommand.Text = ZoomSlider.Value.ToString();
                sorteritno.scale = (float)ZoomSlider.Value;
                sorteritno.ResetTree(ResTree, ResetHighlight, Seed, drzewo, "MW");
            }
        }
    }



}
