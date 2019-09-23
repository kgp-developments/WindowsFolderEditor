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
using ReFolder.Dir.Description;
using ReFolder.Management;
using ReFolder.Memento;
using ReFolder.TxtFileWriter;

namespace main_1._0
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    
        
    public partial class MainWindow : Window
    {
        // folder główny z którego rozpoczyna się dziedziczenie 
        List<StackPanel> Panels = new List<StackPanel>(); // lista paneli z gornego paska, np. plik, widok
        bool rightHandedView = true; // zmienna okreslajaca widok (leworeczny/praworeczny)
        public static Canvas CurrentlyChosen = null;
        public IEditableDirWithChildren CurrentlyChosenDir = null;
        public IEditableDirWithChildren Seed;
        List<IEditableDirWithChildrenAndParent> CopyOfChildren;
        public Sorteritno sorteritno = new Sorteritno();
        public ComplexAdditionWindow CAW;
        NameEditionWindow NEW;
        NoteEditWindow NtEW;
        IconSwitchWindow ISW;
        public ViewWindow VW;
        public Settings settings;
        public CreateNewTreeWindow CNTW;
        public LoadTreeWindow LTW;
        public string thisStructureName;
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


            IEditableDirWithChildren Assistant = DirManagement.GetDefaultInstance().GetFolderAsNewMainDir(@"C:\Users\lenovo\Desktop\gui testowe\AppTest");
            Seed = Assistant;

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
        private void FolderSearchTB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (FolderSearchTB.Text.Length == 0)
            {
                FoundFoldersSV.Visibility = Visibility.Collapsed;
            }
            else
            {
                FoundFoldersSV.Visibility = Visibility.Visible;
                SearchForFolder(FolderSearchTB.Text);
            }
        }
        private void SearchForFolder(string searchedName)
        {
            FoundFoldersSV.Content = null;
            StackPanel FoundFoldersSP = new StackPanel();
            List<IEditableDirWithChildren> FoldersFound = new List<IEditableDirWithChildren>();
            FindAllFolders(Seed, FoldersFound, searchedName);

            foreach (IEditableDirWithChildren x in FoldersFound)
            {
                FoundFoldersSP.Children.Add(AddReferenceOfFound(x));
            }
            FoundFoldersSV.Content = FoundFoldersSP;
        }
        public Canvas AddReferenceOfFound(IEditableDirWithChildren folderFound)
        {
            Canvas MainLayer = new Canvas();
            MainLayer.Height = 30;

            TextBlock DisplayedName = new TextBlock();
            DisplayedName.Text = folderFound.Description.Name;
            DisplayedName.FontSize = 20;
            MainLayer.Children.Add(DisplayedName);

            Button Clicker = new Button();
            Thickness thickness = new Thickness();
            thickness.Right = -500;
            Clicker.Margin = thickness;
            Clicker.Opacity = 0.2f;
            Clicker.Width = MainLayer.Width;
            Clicker.Height = 30;
            Clicker.Command = KGPcommands.HighlightFoundAndChosen;
            Clicker.CommandParameter = Clicker;
            MainLayer.Children.Add(Clicker);

            MainLayer.Tag = (object)folderFound;
            return MainLayer;
        }

        private void FindAllFolders(IEditableDirWithChildren CheckedDir, List<IEditableDirWithChildren> FList, string searched)
        {
            if (CheckedDir.Description.Name.ToUpper().Contains(searched.ToUpper()))
            {
                FList.Add(CheckedDir);
            }
            if (CheckedDir.Children.Count != 0)
            {
                foreach (IEditableDirWithChildren child in CheckedDir.Children)
                {
                    FindAllFolders(child, FList, searched);
                }
            }
        }

        #region funkcje komend _Executed
        private void Saving_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Button Clicked = (Button)e.Parameter;
            if (Clicked == SaveAsNewBtn)
            {
                string temporary = thisStructureName;
                NEW = new NameEditionWindow();
                NEW.IsItBeingSaved = true;
                NEW.ShowDialog();
                if (thisStructureName != temporary)
                {
                    if (CurrentlyChosenDir != null)
                        {
                            CurrentlyChosenDir.IsMarked = false;
                        }
                    string filePath = @"..\..\saved\" + thisStructureName;
                    SaveAndReadElementInBinaryFile.GetDefaultInstance().WriteToBinaryFile<IEditableDirWithChildren>(filePath, Seed);
                }
            }
            else if (Clicked == SaveBtn)
            {
                if (CurrentlyChosenDir != null)
                {
                    CurrentlyChosenDir.IsMarked = false;
                }
                string filePath = @"..\..\saved\" + thisStructureName;
                SaveAndReadElementInBinaryFile.GetDefaultInstance().WriteToBinaryFile<IEditableDirWithChildren>(filePath, Seed);
            }
            if (CurrentlyChosenDir != null)
            {
                CurrentlyChosenDir.IsMarked = true;
            }

        }
        private void NEWshow_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            NtEW = new NoteEditWindow();
            NtEW.DirsNote.Text = CurrentlyChosenDir.Description.Note;
            NtEW.ShowDialog();
        }
        private void ISWshow_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ISW = new IconSwitchWindow();
            ISW.ShowDialog();
        }

        private void HighlightFoundAndChosen_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Button Clicked = (Button)e.Parameter;
            Canvas Layer = (Canvas)Clicked.Parent;
            IEditableDirWithChildren Searched = (IEditableDirWithChildren)Layer.Tag;

            if (CurrentlyChosen != null)
            {

                CurrentlyChosen.Background = Brushes.LightGray;
                CurrentlyChosenDir = (IEditableDirWithChildren)CurrentlyChosen.Tag;
                CurrentlyChosenDir.IsMarked = false;

            }

            CurrentlyChosenDir = (IEditableDirWithChildren)Layer.Tag;
            CurrentlyChosenDir.IsMarked = true;
            //sorteritno.ResetTree(ResTree, ResetHighlight, Seed, drzewo, "MW");
            HideAllPanels();
        }

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
            NEW.IsItBeingSaved = false;
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
            TxtFileWriter writer = TxtFileWriter.GetDefaultInstance();
            List<string> editedFullNames= writer.TxtFileEditor.GetMainDirChildrenNamesAndAddStringWithNote(Seed);
            writer.WriteListToFile(Seed.Description.FullName, editedFullNames);
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
        private void Saving_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (thisStructureName != null)
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }
        }
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
            if(CurrentlyChosenDir == null)
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
            if (ZoomSlider != null && FolderSearchTB != null && thisStructureName != null)
            {
                FolderSearchTB.Text = ZoomSlider.Value.ToString();
               
                sorteritno.scale = (float)ZoomSlider.Value;
                sorteritno.ResetTree(ResTree, ResetHighlight, Seed, drzewo, "MW");
            }
        }
    }



}
