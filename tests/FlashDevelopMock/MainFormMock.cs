using FlashDevelop.Docking;
using PluginCore;
using PluginCore.Helpers;
using PluginCore.Managers;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace FlashDevelopMock
{
    public class MainFormMock : IMainForm, IMessageFilter
    {
        public MainFormMock()
        {
            Type type = typeof(PluginBase);
            foreach (var member in type.GetMembers(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static))
            {
                if (member.Name == "instance")
                {
                    ((FieldInfo)member).SetValue(null, this);//PluginBase.Initialize(this);
                    break;
                }
            }
        }

        public void RefreshUI()
        {
            throw new NotImplementedException();
        }

        public void RefreshSciConfig()
        {
            throw new NotImplementedException();
        }

        public void ClearTemporaryFiles(string file)
        {
            throw new NotImplementedException();
        }

        public void ShowSettingsDialog(string itemName)
        {
            throw new NotImplementedException();
        }

        public void ShowErrorDialog(object sender, Exception ex)
        {
            throw new NotImplementedException();
        }

        public void ShowSettingsDialog(string itemName, string filter)
        {
            throw new NotImplementedException();
        }

        public void AutoUpdateMenuItem(ToolStripItem item, string action)
        {
            throw new NotImplementedException();
        }

        public void RegisterShortcutItem(string id, Keys keys)
        {
            throw new NotImplementedException();
        }

        public void RegisterShortcutItem(string id, ToolStripMenuItem item)
        {
            throw new NotImplementedException();
        }

        public void FileFromTemplate(string templatePath, string newFilePath)
        {
            throw new NotImplementedException();
        }

        public DockContent OpenEditableDocument(String org, Encoding encoding, Boolean restorePosition)
        {
            DockContent createdDoc;
            EncodingFileInfo info;
            String file = PathHelper.GetPhysicalPathName(org);
            TextEvent te = new TextEvent(EventType.FileOpening, file);
            EventManager.DispatchEvent(this, te);
            if (te.Handled)
            {
                if (this.Documents.Length == 0)
                {
                    this.New(null, null);
                    return null;
                }
                else return null;
            }
            else if (file.EndsWith(".delete.fdz"))
            {
                this.CallCommand("RemoveZip", file);
                return null;
            }
            else if (file.EndsWith(".fdz"))
            {
                this.CallCommand("ExtractZip", file);
                if (file.ToLower().IndexOf("theme") != -1)
                {
                    String currentTheme = Path.Combine(PathHelper.ThemesDir, "CURRENT");
                    //if (File.Exists(currentTheme)) ThemeManager.LoadTheme(currentTheme);
                    this.RefreshSciConfig();
                    //this.Refresh();
                }
                return null;
            }
            try
            {
                Int32 count = this.Documents.Length;
                for (Int32 i = 0; i < count; i++)
                {
                    if (this.Documents[i].IsEditable && this.Documents[i].FileName.ToUpper() == file.ToUpper())
                    {
                        this.Documents[i].Activate();
                        return null;
                    }
                }
            }
            catch { }
            if (encoding == null)
            {
                info = FileHelper.GetEncodingFileInfo(file);
                if (info.CodePage == -1) return null; // If the file is locked, stop.
            }
            else
            {
                info = FileHelper.GetEncodingFileInfo(file);
                if (info.CodePage == -1) return null; // If the file is locked, stop.
                info.Contents = FileHelper.ReadFile(file, encoding);
                info.CodePage = encoding.CodePage;
            }
            DataEvent de = new DataEvent(EventType.FileDecode, file, null);
            EventManager.DispatchEvent(this, de); // Lets ask if a plugin wants to decode the data..
            if (de.Handled)
            {
                info.Contents = de.Data as String;
                info.CodePage = Encoding.UTF8.CodePage; // assume plugin always return UTF8
            }
            try
            {
                if (this.CurrentDocument != null && this.CurrentDocument.IsUntitled && !this.CurrentDocument.IsModified && this.Documents.Length == 1)
                {
                    //this.closingForOpenFile = true;
                    this.CurrentDocument.Close();
                    //this.closingForOpenFile = false;
                    createdDoc = this.CreateEditableDocument(file, info.Contents, info.CodePage);
                }
                else createdDoc = this.CreateEditableDocument(file, info.Contents, info.CodePage);
                //ButtonManager.AddNewReopenMenuItem(file);
            }
            catch
            {
                createdDoc = this.CreateEditableDocument(file, info.Contents, info.CodePage);
                //ButtonManager.AddNewReopenMenuItem(file);
            }
            TabbedDocument document = (TabbedDocument)createdDoc;
            Debug.WriteLine("info != null: " + (info != null));
            document.SciControl.SaveBOM = info.ContainsBOM;
            document.SciControl.BeginInvoke((MethodInvoker)delegate
            {
                //if (this.appSettings.RestoreFileStates)
                //{
                //    FileStateManager.ApplyFileState(document, restorePosition);
                //}
            });
            //ButtonManager.UpdateFlaggedButtons();
            return createdDoc;
        }
        public DockContent OpenEditableDocument(String file, Boolean restorePosition)
        {
            return this.OpenEditableDocument(file, null, restorePosition);
        }
        public DockContent OpenEditableDocument(String file)
        {
            return this.OpenEditableDocument(file, null, true);
        }

        public WeifenLuo.WinFormsUI.Docking.DockContent CreateCustomDocument(Control ctrl)
        {
            throw new NotImplementedException();
        }

        public WeifenLuo.WinFormsUI.Docking.DockContent CreateEditableDocument(string file, string text, int codepage)
        {
            try
            {
                //this.notifyOpenFile = true;
                TabbedDocument tabbedDocument = new TabbedDocument();
                //tabbedDocument.Closing += new System.ComponentModel.CancelEventHandler(this.OnDocumentClosing);
                //tabbedDocument.Closed += new System.EventHandler(this.OnDocumentClosed);
                //tabbedDocument.TabPageContextMenuStrip = this.tabMenu;
                //tabbedDocument.ContextMenuStrip = this.editorMenu;
                tabbedDocument.Text = Path.GetFileName(file);
                tabbedDocument.AddEditorControls(file, text, codepage);
                tabbedDocument.Show();
                return tabbedDocument;
            }
            catch (Exception ex)
            {
                //ErrorManager.ShowError(ex);
                return null;
            }
        }

        public WeifenLuo.WinFormsUI.Docking.DockContent CreateDockablePanel(Control form, string guid, System.Drawing.Image image, WeifenLuo.WinFormsUI.Docking.DockState defaultDockState)
        {
            throw new NotImplementedException();
        }

        public bool CallCommand(string command, string arguments)
        {
            throw new NotImplementedException();
        }

        public System.Collections.Generic.List<ToolStripItem> FindMenuItems(string name)
        {
            throw new NotImplementedException();
        }

        public ToolStripItem FindMenuItem(string name)
        {
            throw new NotImplementedException();
        }

        public string ProcessArgString(string args)
        {
            throw new NotImplementedException();
        }

        public Keys GetShortcutItemKeys(string id)
        {
            throw new NotImplementedException();
        }

        public string GetThemeValue(string id)
        {
            throw new NotImplementedException();
        }

        public System.Drawing.Color GetThemeColor(string id)
        {
            throw new NotImplementedException();
        }

        public IPlugin FindPlugin(string guid)
        {
            throw new NotImplementedException();
        }

        public System.Drawing.Image FindImage(string data)
        {
            throw new NotImplementedException();
        }

        public ISettings Settings
        {
            get { throw new NotImplementedException(); }
        }

        public ToolStrip ToolStrip
        {
            get { throw new NotImplementedException(); }
        }

        public MenuStrip MenuStrip
        {
            get { throw new NotImplementedException(); }
        }

        public ScintillaNet.Configuration.Scintilla SciConfig
        {
            get { throw new NotImplementedException(); }
        }

        public WeifenLuo.WinFormsUI.Docking.DockPanel DockPanel
        {
            get { throw new NotImplementedException(); }
        }

        public string[] StartArguments
        {
            get { throw new NotImplementedException(); }
        }

        public System.Collections.Generic.List<Argument> CustomArguments
        {
            get { throw new NotImplementedException(); }
        }

        public StatusStrip StatusStrip
        {
            get { throw new NotImplementedException(); }
        }

        public string WorkingDirectory
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public ToolStripPanel ToolStripPanel
        {
            get { throw new NotImplementedException(); }
        }

        public ToolStripStatusLabel StatusLabel
        {
            get { throw new NotImplementedException(); }
        }

        public ToolStripStatusLabel ProgressLabel
        {
            get { throw new NotImplementedException(); }
        }

        public ToolStripProgressBar ProgressBar
        {
            get { throw new NotImplementedException(); }
        }

        public Control.ControlCollection Controls
        {
            get { throw new NotImplementedException(); }
        }

        public ContextMenuStrip TabMenu
        {
            get { throw new NotImplementedException(); }
        }

        public ContextMenuStrip EditorMenu
        {
            get { throw new NotImplementedException(); }
        }

        public ITabbedDocument CurrentDocument
        {
            get { throw new NotImplementedException(); }
        }

        public ITabbedDocument[] Documents
        {
            get { throw new NotImplementedException(); }
        }

        public bool HasModifiedDocuments
        {
            get { throw new NotImplementedException(); }
        }

        public bool ClosingEntirely
        {
            get { throw new NotImplementedException(); }
        }

        public bool ProcessIsRunning
        {
            get { throw new NotImplementedException(); }
        }

        public bool ReloadingDocument
        {
            get { throw new NotImplementedException(); }
        }

        public bool ProcessingContents
        {
            get { throw new NotImplementedException(); }
        }

        public bool RestoringContents
        {
            get { throw new NotImplementedException(); }
        }

        public bool SavingMultiple
        {
            get { throw new NotImplementedException(); }
        }

        public bool PanelIsActive
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsFullScreen
        {
            get { throw new NotImplementedException(); }
        }

        public bool StandaloneMode
        {
            get { throw new NotImplementedException(); }
        }

        public bool MultiInstanceMode
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsFirstInstance
        {
            get { throw new NotImplementedException(); }
        }

        public bool RestartRequested
        {
            get { throw new NotImplementedException(); }
        }

        public bool RefreshConfig
        {
            get { throw new NotImplementedException(); }
        }

        public System.Collections.Generic.List<Keys> IgnoredKeys
        {
            get { throw new NotImplementedException(); }
        }

        public string ProductVersion
        {
            get { throw new NotImplementedException(); }
        }

        public string ProductName
        {
            get { throw new NotImplementedException(); }
        }

        public IntPtr Handle
        {
            get { throw new NotImplementedException(); }
        }

        public bool PreFilterMessage(ref Message m)
        {
            throw new NotImplementedException();
        }

        public void New(Object sender, EventArgs e)
        {
            String fileName = DocumentManager.GetNewDocumentName(null);
            TextEvent te = new TextEvent(EventType.FileNew, fileName);
            EventManager.DispatchEvent(this, te);
            if (!te.Handled)
            {
                this.CreateEditableDocument(fileName, "", Encoding.UTF8.CodePage);
            }
        }
    }
}