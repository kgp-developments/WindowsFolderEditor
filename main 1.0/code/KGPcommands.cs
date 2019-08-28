using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace main_1._0
{
    // "deklaracje" komend
    public static class KGPcommands
    {
        public static readonly RoutedUICommand HorizontalStyleSwitch = new RoutedUICommand
           (
            "HorizontalStyleSwitch",
            "HorizontalStyleSwitch",
            typeof(KGPcommands)
           );
        public static readonly RoutedUICommand Jakas = new RoutedUICommand
        (
            "Jakas",
            "Jakas",
            typeof(KGPcommands)
        );

        public static readonly RoutedUICommand WidokShowHide = new RoutedUICommand
        (
            "WidokShowHide",
            "WidokShowHide",
            typeof(KGPcommands)
        );
        public static readonly RoutedUICommand HighlightChosen = new RoutedUICommand
        (
            "HighlightChosen",
            "HighlightChosen",
            typeof(KGPcommands)
        );
        public static readonly RoutedUICommand GenerateDirs = new RoutedUICommand
       (
           "GenerateDirs",
           "GenerateDirs",
           typeof(KGPcommands)
       );

       public static readonly RoutedUICommand CopyChildrenDirs = new RoutedUICommand
        (
            "CopyChildrenDirs",
            "CopyChildrenDirs",
            typeof(KGPcommands)
        );
        public static readonly RoutedUICommand PasteChildrenDirs = new RoutedUICommand
        (
            "PasteChildrenDirs",
            "PasteChildrenDirs",
            typeof(KGPcommands)
        );



        public static readonly RoutedUICommand ResetHighlight = new RoutedUICommand
        (
        "ResetHighlight",
        "ResetHighlight",
        typeof(KGPcommands)
        );

        public static readonly RoutedUICommand ViewContent = new RoutedUICommand
        (
        "ViewContent",
        "ViewContent",
        typeof(KGPcommands)
        );

        public static readonly RoutedUICommand DefaultAddition = new RoutedUICommand
        (
        "DefaultAddition",
        "DefaultAddition",
        typeof(KGPcommands)
        );

    }

}
