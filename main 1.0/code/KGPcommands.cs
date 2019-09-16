using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
//CopyDir
namespace main_1._0 { 
  public static class KGPcommands
   {
    public static readonly RoutedUICommand HorizontalStyleSwitch = new RoutedUICommand
       (
        "HorizontalStyleSwitch",
        "HorizontalStyleSwitch",
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
    public static readonly RoutedUICommand FoldSwitch = new RoutedUICommand
    (
        "FoldSwitch",
        "FoldSwitch",
        typeof(KGPcommands)
    );
    public static readonly RoutedUICommand ShowHideTypeOfSeries = new RoutedUICommand
    (
        "ShowHideTypeOfSeries",
        "ShowHideTypeOfSeries",
        typeof(KGPcommands)
    );
    public static readonly RoutedUICommand ShowHideAutoNames = new RoutedUICommand
    (
        "ShowHideAutoNames",
        "ShowHideAutoNames",
        typeof(KGPcommands)
    );
    public static readonly RoutedUICommand SetChosenType = new RoutedUICommand
    (
        "SetChosenType",
        "SetChosenType",
        typeof(KGPcommands)
    );
    public static readonly RoutedUICommand SetChosenAutoName = new RoutedUICommand
    (
        "SetChosenAutoName",
        "SetChosenAutoName",
        typeof(KGPcommands)
    );
    public static readonly RoutedUICommand SetComplexAddition = new RoutedUICommand
    (
        "SetComplexAddition",
        "SetComplexAddition",
        typeof(KGPcommands)
    );
    public static readonly RoutedUICommand ComplexAdditionWindowShow = new RoutedUICommand
    (
        "ComplexAdditionWindowShow",
        "ComplexAdditionWindowShow",
        typeof(KGPcommands)
    );
        public static readonly RoutedUICommand ApplyChangesCAW = new RoutedUICommand
        (
            "ApplyChangesCAW",
            "ApplyChangesCAW",
            typeof(KGPcommands)
        );
        public static readonly RoutedUICommand PasteDir = new RoutedUICommand
        (
        "PasteDir",
        "PasteDir",
        typeof(KGPcommands)
        );
        public static readonly RoutedUICommand CopyDir = new RoutedUICommand
       (
       "CopyDir",
       "CopyDir",
       typeof(KGPcommands)
       );
        public static readonly RoutedUICommand CancelCAW = new RoutedUICommand
(
"CancelCAW",
"CancelCAW",
typeof(KGPcommands)
);
        public static readonly RoutedUICommand NewNameCommand = new RoutedUICommand
(
"NewNameCommand",
"NewNameCommand",
typeof(KGPcommands)
);
        public static readonly RoutedUICommand NameEditionWindowShow = new RoutedUICommand
        (
        "NameEditionWindowShow",
        "NameEditionWindowShow",
        typeof(KGPcommands)
        );
        public static readonly RoutedUICommand DeleteDir = new RoutedUICommand
(
"DeleteDir",
"DeleteDir",
typeof(KGPcommands)
);
        public static readonly RoutedUICommand UndoChanges = new RoutedUICommand
(
"UndoChanges",
"UndoChanges",
typeof(KGPcommands)
);
        public static readonly RoutedUICommand RedoChanges = new RoutedUICommand
(
"RedoChanges",
"RedoChanges",
typeof(KGPcommands)
);
        public static readonly RoutedUICommand ViewWindowShow = new RoutedUICommand
(
"ViewWindowShow",
"ViewWindowShow",
typeof(KGPcommands)
);
        public static readonly RoutedUICommand ApplyChangesVW = new RoutedUICommand
(
    "ApplyChangesVW",
    "ApplyChangesVW",
    typeof(KGPcommands)
);
        public static readonly RoutedUICommand CancelVW = new RoutedUICommand
(
    "CancelVW",
    "CancelVW",
    typeof(KGPcommands)
);
        public static readonly RoutedUICommand ApplyStyleChanges = new RoutedUICommand
(
"ApplyStyleChanges",
"ApplyStyleChanges",
typeof(KGPcommands)
);
        public static readonly RoutedUICommand RHVBtnSwitch = new RoutedUICommand
(
"RHVBtnSwitch",
"RHVBtnSwitch",
typeof(KGPcommands)
);
        public static readonly RoutedUICommand CancelCNTW = new RoutedUICommand
(
"CancelCNTW",
"CancelCNTW",
typeof(KGPcommands)
);
        public static readonly RoutedUICommand CreateCNTW = new RoutedUICommand
(
"CreateCNTW",
"CreateCNTW",
typeof(KGPcommands)
);
        public static readonly RoutedUICommand BrowseCNTW = new RoutedUICommand
(
"BrowseCNTW",
"BrowseCNTW",
typeof(KGPcommands)
);
        public static readonly RoutedUICommand CNTWshow = new RoutedUICommand
(
"CNTWshow",
"CNTWshow",
typeof(KGPcommands)
         );
                    public static readonly RoutedUICommand LTWshow = new RoutedUICommand
(
"LTWshow",
"LTWshow",
typeof(KGPcommands)
);
        public static readonly RoutedUICommand CancelLTW = new RoutedUICommand
(
"CancelLTW",
"CancelLTW",
typeof(KGPcommands)
);
        public static readonly RoutedUICommand LoadLTW = new RoutedUICommand
(
"LoadLTW",
"LoadLTW",
typeof(KGPcommands)
);
        public static readonly RoutedUICommand SetChosenLTW = new RoutedUICommand
(
"SetChosenLTW",
"SetChosenLTW",
typeof(KGPcommands)
);

    }



}