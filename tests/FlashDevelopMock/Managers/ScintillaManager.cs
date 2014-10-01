using FlashDevelop.Helpers;
using PluginCore;
using PluginCore.Controls;
using PluginCore.Helpers;
using PluginCore.Localization;
using PluginCore.Managers;
using PluginCore.Utilities;
using ScintillaNet;
using ScintillaNet.Configuration;
using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;

namespace FlashDevelop.Mock.Managers
{
    class ScintillaManager
    {
        public static Scintilla SciConfig;
        public static ConfigurationUtility SciConfigUtil;
        public static string XpmBookmark;

        static ScintillaManager()
        {
            Bitmap bookmark = new Bitmap(ResourceHelper.GetStream("BookmarkIcon.bmp"));
            XpmBookmark = ScintillaNet.XPM.ConvertToXPM(bookmark, "#00FF00");
            LoadConfiguration();
        }

        /// <summary>
        /// Loads the syntax and refreshes scintilla settings.
        /// </summary>
        public static void LoadConfiguration()
        {
            SciConfigUtil = new ConfigurationUtility(Assembly.GetExecutingAssembly());
            string[] configFiles = Directory.GetFiles(Path.Combine(PathHelper.SettingDir, "Languages"), "*.xml");
            SciConfig = (Scintilla)SciConfigUtil.LoadConfiguration(configFiles);
            ScintillaControl.Configuration = SciConfig;
            MainForm.Instance.ApplyAllSettings();
        }

        /// <summary>
        /// Update the manually syncable properties if needed.
        /// </summary>
        public static void UpdateSyncProps(ScintillaControl e1, ScintillaControl e2)
        {
            if (e2.SaveBOM != e1.SaveBOM) e2.SaveBOM = e1.SaveBOM;
            if (e2.Encoding != e1.Encoding) e2.Encoding = e1.Encoding;
            if (e2.FileName != e1.FileName) e2.FileName = e1.FileName;
            if (e2.SmartIndentType != e1.SmartIndentType) e2.SmartIndentType = e1.SmartIndentType;
            if (e2.UseHighlightGuides != e1.UseHighlightGuides) e2.UseHighlightGuides = e1.UseHighlightGuides;
            if (e2.ConfigurationLanguage != e1.ConfigurationLanguage) e2.ConfigurationLanguage = e1.ConfigurationLanguage;
            if (e2.IsHiliteSelected != e1.IsHiliteSelected) e2.IsHiliteSelected = e1.IsHiliteSelected;
            if (e2.IsBraceMatching != e1.IsBraceMatching) e2.IsBraceMatching = e1.IsBraceMatching;
        }

        /// <summary>
        /// Updates editor Globals.Settings to the specified ScintillaControl
        /// </summary>
        public static void ApplySciSettings(ScintillaControl sci)
        {
            ApplySciSettings(sci, false);
        }
        public static void ApplySciSettings(ScintillaControl sci, bool hardUpdate)
        {
            Settings.SettingObject Settings = MainForm.Instance.AppSettings;
            sci.CaretPeriod = Settings.CaretPeriod;
            sci.CaretWidth = Settings.CaretWidth;
            sci.EOLMode = LineEndDetector.DetectNewLineMarker(sci.Text, (int)Settings.EOLMode);
            sci.IsBraceMatching = Settings.BraceMatchingEnabled;
            sci.UseHighlightGuides = !Settings.HighlightGuide;
            sci.Indent = Settings.IndentSize;
            sci.SmartIndentType = Settings.SmartIndentType;
            sci.IsBackSpaceUnIndents = Settings.BackSpaceUnIndents;
            sci.IsCaretLineVisible = Settings.CaretLineVisible;
            sci.IsIndentationGuides = Settings.ViewIndentationGuides;
            sci.IndentView = Settings.IndentView;
            sci.IsTabIndents = Settings.TabIndents;
            sci.IsUseTabs = Settings.UseTabs;
            sci.IsViewEOL = Settings.ViewEOL;
            sci.ScrollWidth = Settings.ScrollWidth;
            sci.TabWidth = Settings.TabWidth;
            sci.ViewWS = Convert.ToInt32(Settings.ViewWhitespace);
            sci.WrapMode = Convert.ToInt32(Settings.WrapText);
            sci.SetProperty("fold", Convert.ToInt32(Settings.UseFolding).ToString());
            sci.SetProperty("fold.comment", Convert.ToInt32(Settings.FoldComment).ToString());
            sci.SetProperty("fold.compact", Convert.ToInt32(Settings.FoldCompact).ToString());
            sci.SetProperty("fold.preprocessor", Convert.ToInt32(Settings.FoldPreprocessor).ToString());
            sci.SetProperty("fold.at.else", Convert.ToInt32(Settings.FoldAtElse).ToString());
            sci.SetProperty("fold.html", Convert.ToInt32(Settings.FoldHtml).ToString());
            sci.SetProperty("lexer.cpp.track.preprocessor", "0");
            sci.SetVirtualSpaceOptions((int)Settings.VirtualSpaceMode);
            sci.SetFoldFlags((int)Settings.FoldFlags);
            /** 
            * Set correct line number margin width
            */
            bool viewLineNumbers = Settings.ViewLineNumbers;
            if (viewLineNumbers) sci.SetMarginWidthN(1, ScaleHelper.Scale(31));
            else sci.SetMarginWidthN(1, 0);
            /**
            * Set correct bookmark margin width
            */
            bool viewBookmarks = Settings.ViewBookmarks;
            if (viewBookmarks) sci.SetMarginWidthN(0, ScaleHelper.Scale(14));
            else sci.SetMarginWidthN(0, 0);
            /**
            * Set correct folding margin width
            */
            bool useFolding = Settings.UseFolding;
            if (!useFolding && !viewBookmarks && !viewLineNumbers) sci.SetMarginWidthN(2, 0);
            else if (useFolding) sci.SetMarginWidthN(2, ScaleHelper.Scale(15));
            else sci.SetMarginWidthN(2, ScaleHelper.Scale(2));
            /**
            * Adjust the print margin
            */
            sci.EdgeColumn = Settings.PrintMarginColumn;
            if (sci.EdgeColumn > 0) sci.EdgeMode = 1;
            else sci.EdgeMode = 0;
            /**
            * Add missing ignored keys
            */
            //foreach (Keys keys in ShortcutManager.AllShortcuts)
            //{
            //    if (keys != Keys.None && !sci.ContainsIgnoredKeys(keys))
            //    {
            //        sci.AddIgnoredKeys(keys);
            //    }
            //}
            if (hardUpdate)
            {
                string lang = sci.ConfigurationLanguage;
                sci.ConfigurationLanguage = lang;
            }
            sci.Colourise(0, -1);
            sci.Refresh();
        }

        /// <summary>
        /// Creates a new editor control for the document 
        /// </summary>
        public static ScintillaControl CreateControl(string file, string text, int codepage)
        {
            Settings.SettingObject Settings = MainForm.Instance.AppSettings;
            ScintillaControl sci = new ScintillaControl(@"d:\projects\flashdevelop-fork\FlashDevelop\Bin\Debug\SciLexer.dll");
            sci.AutoCSeparator = 32;
            sci.AutoCTypeSeparator = 63;
            sci.IsAutoCGetAutoHide = true;
            sci.IsAutoCGetCancelAtStart = false;
            sci.IsAutoCGetChooseSingle = false;
            sci.IsAutoCGetDropRestOfWord = false;
            sci.IsAutoCGetIgnoreCase = false;
            sci.ControlCharSymbol = 0;
            sci.CurrentPos = 0;
            sci.CursorType = -1;
            sci.Dock = System.Windows.Forms.DockStyle.Fill;
            sci.EndAtLastLine = 1;
            sci.EdgeColumn = 0;
            sci.EdgeMode = 0;
            sci.IsHScrollBar = true;
            sci.IsMouseDownCaptures = true;
            sci.IsBufferedDraw = true;
            sci.IsOvertype = false;
            sci.IsReadOnly = false;
            sci.IsUndoCollection = true;
            sci.IsVScrollBar = true;
            sci.IsUsePalette = true;
            sci.IsTwoPhaseDraw = true;
            sci.LayoutCache = 1;
            sci.Lexer = 3;
            sci.Location = new System.Drawing.Point(0, 0);
            sci.MarginLeft = 5;
            sci.MarginRight = 5;
            sci.ModEventMask = (int)ScintillaNet.Enums.ModificationFlags.InsertText | (int)ScintillaNet.Enums.ModificationFlags.DeleteText | (Int32)ScintillaNet.Enums.ModificationFlags.RedoPerformed | (Int32)ScintillaNet.Enums.ModificationFlags.UndoPerformed;
            sci.MouseDwellTime = ScintillaControl.MAXDWELLTIME;
            sci.Name = "sci";
            sci.PasteConvertEndings = false;
            sci.PrintColourMode = (int)ScintillaNet.Enums.PrintOption.Normal;
            sci.PrintWrapMode = (int)ScintillaNet.Enums.Wrap.Word;
            sci.PrintMagnification = 0;
            sci.SearchFlags = 0;
            sci.SelectionEnd = 0;
            sci.SelectionMode = 0;
            sci.SelectionStart = 0;
            sci.SmartIndentType = ScintillaNet.Enums.SmartIndent.CPP;
            sci.Status = 0;
            sci.StyleBits = 7;
            sci.TabIndex = 0;
            sci.TargetEnd = 0;
            sci.TargetStart = 0;
            sci.WrapStartIndent = Settings.IndentSize;
            sci.WrapVisualFlagsLocation = (int)ScintillaNet.Enums.WrapVisualLocation.EndByText;
            sci.WrapVisualFlags = (int)ScintillaNet.Enums.WrapVisualFlag.End;
            sci.XOffset = 0;
            sci.ZoomLevel = 0;
            sci.UsePopUp(false);
            sci.SetMarginTypeN(0, 0);
            sci.SetMarginWidthN(0, ScaleHelper.Scale(14));
            sci.SetMarginTypeN(1, 1);
            sci.SetMarginMaskN(1, 0);
            sci.SetMarginTypeN(2, 0);
            sci.SetMarginMaskN(2, -33554432 | 1 << 2);
            sci.SetMultiSelectionTyping(true);
            sci.MarginSensitiveN(2, true);
            sci.MarkerDefinePixmap(0, XpmBookmark);
            sci.SetMarginMaskN(0, 1 << 0);// FlashDevelop.Managers.MarkerManager.MARKERS
            sci.MarkerDefine(2, ScintillaNet.Enums.MarkerSymbol.Fullrect);
            sci.MarkerDefine((int)ScintillaNet.Enums.MarkerOutline.Folder, ScintillaNet.Enums.MarkerSymbol.BoxPlus);
            sci.MarkerDefine((int)ScintillaNet.Enums.MarkerOutline.FolderOpen, ScintillaNet.Enums.MarkerSymbol.BoxMinus);
            sci.MarkerDefine((int)ScintillaNet.Enums.MarkerOutline.FolderSub, ScintillaNet.Enums.MarkerSymbol.VLine);
            sci.MarkerDefine((int)ScintillaNet.Enums.MarkerOutline.FolderTail, ScintillaNet.Enums.MarkerSymbol.LCorner);
            sci.MarkerDefine((int)ScintillaNet.Enums.MarkerOutline.FolderEnd, ScintillaNet.Enums.MarkerSymbol.BoxPlusConnected);
            sci.MarkerDefine((int)ScintillaNet.Enums.MarkerOutline.FolderOpenMid, ScintillaNet.Enums.MarkerSymbol.BoxMinusConnected);
            sci.MarkerDefine((int)ScintillaNet.Enums.MarkerOutline.FolderMidTail, ScintillaNet.Enums.MarkerSymbol.TCorner);
            sci.SetXCaretPolicy((int)(ScintillaNet.Enums.CaretPolicy.Jumps | ScintillaNet.Enums.CaretPolicy.Even), 30);
            sci.SetYCaretPolicy((int)(ScintillaNet.Enums.CaretPolicy.Jumps | ScintillaNet.Enums.CaretPolicy.Even), 2);
            sci.ScrollWidthTracking = Settings.ScrollWidth == 3000;
            sci.CodePage = SelectCodePage(codepage);
            sci.Encoding = Encoding.GetEncoding(codepage);
            sci.SaveBOM = (sci.CodePage == 65001) && Settings.SaveUnicodeWithBOM;
            sci.Text = text; sci.FileName = file; // Set text and save file name
            //sci.Modified += new ModifiedHandler(Globals.MainForm.OnScintillaControlModified);
            //sci.MarginClick += new MarginClickHandler(Globals.MainForm.OnScintillaControlMarginClick);
            //sci.UpdateUI += new UpdateUIHandler(Globals.MainForm.OnScintillaControlUpdateControl);
            //sci.URIDropped += new URIDroppedHandler(Globals.MainForm.OnScintillaControlDropFiles);
            //sci.ModifyAttemptRO += new ModifyAttemptROHandler(Globals.MainForm.OnScintillaControlModifyRO);
            string untitledFileStart = TextHelper.GetString("Info.UntitledFileStart");
            if (!file.StartsWith(untitledFileStart)) sci.IsReadOnly = FileHelper.FileIsReadOnly(file);
            sci.SetFoldFlags((int)Settings.FoldFlags);//Globals.Settings.FoldFlags);
            sci.EmptyUndoBuffer(); ApplySciSettings(sci);
            //UITools.Manager.ListenTo(sci);
            return sci;
        }

        /// <summary>
        /// Selects a correct codepage for the editor
        /// </summary>
        public static int SelectCodePage(int codepage)
        {
            if (codepage == 65001 || codepage == 1201 || codepage == 1200) return 65001;
            return 0; // Disable multibyte support
        }
    }
}