﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace main_1._0
{
    /// <summary>
    /// Interaction logic for NameEditionWindow.xaml
    /// </summary>
    public partial class NameEditionWindow : Window
    {
        static MainWindow AppMW = (MainWindow)Application.Current.MainWindow;
        string NewText;
        public bool IsItBeingSaved = false;
        public NameEditionWindow()
        {
            InitializeComponent();
            if (IsItBeingSaved)
            {
                NewNameTB.Text = AppMW.thisStructureName;
            }
            else if (IsItBeingSaved == false)
            {
                if (AppMW.CurrentlyChosenDir != null)
                {
                    NewNameTB.Text = AppMW.CurrentlyChosenDir.Description.Name;
                }
            }
        }

        private void NEWchanger()
        {
            int counter = 0;
            foreach (char c in NewText)
            {
                if (char.IsWhiteSpace(c))
                {
                    counter++;
                }
                else
                {
                    break;
                }
            }
            NewText = NewText.Remove(0, counter);
            counter = 0;
            for (int i = NewText.Length - 1; i > 0; i--)
            {
                if (char.IsWhiteSpace(NewText[i]))
                {
                    counter++;
                }
                else
                {
                    break;
                }
            }
            NewText = NewText.Remove(NewText.Length - counter, counter);

        }
        private bool NEWhelper()
        {
            int counter = 0;
            foreach (char sign in NewNameTB.Text)
            {
                if (!char.IsWhiteSpace(sign))
                {
                    counter++;
                    break;
                }
            }
            if (counter != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void NewNameCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Button Clicked = (Button)e.Parameter;

            if (Clicked.Name == "OK")
            {
                if (NewNameTB.Text.Length != 0)
                {
                    if (NEWhelper())
                    {
                        NewText = NewNameTB.Text;
                        NEWchanger();
                        if (IsItBeingSaved)
                        {
                            AppMW.thisStructureName = NewText;
                        }
                        else
                        {
                            AppMW.CurrentlyChosenDir.Description.Name = NewText;
                            AppMW.sorteritno.ResetTree(AppMW.ResTree, AppMW.ResetHighlight, AppMW.Seed, AppMW.drzewo, "MW");

                            AddMemento();
                        }
                    }
                }
            }

            this.Close();
        }
        private void AlwaysTrueForCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void AddMemento()
        {
            AppMW.careTakerGlobal.AddMemento(AppMW.orginatorGlobal.Save(AppMW.Seed));

        }

    }
}
