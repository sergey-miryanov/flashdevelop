using PluginCore;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

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

        public WeifenLuo.WinFormsUI.Docking.DockContent OpenEditableDocument(string file, bool restoreFileState)
        {
            throw new NotImplementedException();
        }

        public WeifenLuo.WinFormsUI.Docking.DockContent OpenEditableDocument(string file)
        {
            throw new NotImplementedException();
        }

        public WeifenLuo.WinFormsUI.Docking.DockContent CreateCustomDocument(Control ctrl)
        {
            throw new NotImplementedException();
        }

        public WeifenLuo.WinFormsUI.Docking.DockContent CreateEditableDocument(string file, string text, int codepage)
        {
            throw new NotImplementedException();
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

        public System.Type GetType()
        {
            return System.Type.GetType("FlashDevelop.MainForm");
        }
    }
}