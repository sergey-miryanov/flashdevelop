using FlashDevelop.Mock.Managers;
using PluginCore;
using ScintillaNet;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace FlashDevelop.Mock.Docking
{
    public class TabbedDocument : DockContent, ITabbedDocument
    {
        private ScintillaControl editor;
        private ScintillaControl editor2;
        private ScintillaControl lastEditor;
        private SplitContainer splitContainer;
        private bool isModified;

        public TabbedDocument()
        {
        }

        /// <summary>
        /// Path of the document
        /// </summary>
        public string FileName
        {
            get
            {
                if (this.IsEditable) return this.SciControl.FileName;
                else return null;
            }
        }

        public bool UseCustomIcon
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        /// <summary>
        /// Current ScintillaControl of the document
        /// </summary>
        public ScintillaNet.ScintillaControl SciControl
        {
            get
            {
                foreach (Control ctrl in this.Controls)
                {
                    if (ctrl is ScintillaControl && !this.Disposing) return ctrl as ScintillaControl;
                    else if (ctrl is SplitContainer && ctrl.Name == "fdSplitView" && !this.Disposing)
                    {
                        SplitContainer casted = ctrl as SplitContainer;
                        ScintillaControl sci1 = casted.Panel1.Controls[0] as ScintillaControl;
                        ScintillaControl sci2 = casted.Panel2.Controls[0] as ScintillaControl;
                        if (sci2.IsFocus) return sci2;
                        else if (sci1.IsFocus) return sci1;
                        else if (this.lastEditor != null && this.lastEditor.Visible)
                        {
                            return this.lastEditor;
                        }
                        else return sci1;
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// First splitted ScintillaControl 
        /// </summary>
        public ScintillaControl SplitSci1
        {
            get
            {
                if (this.editor != null) return this.editor;
                else return null;
            }
        }

        /// <summary>
        /// Second splitted ScintillaControl
        /// </summary>
        public ScintillaControl SplitSci2
        {
            get
            {
                if (this.editor2 != null) return this.editor2;
                else return null;
            }
        }

        /// <summary>
        /// SplitContainer of the document
        /// </summary>
        public SplitContainer SplitContainer
        {
            get
            {
                if (this.splitContainer != null) return this.splitContainer;
                else return null;
            }
        }

        /// <summary>
        /// Sets or gets if the file is modified
        /// </summary> 
        public bool IsModified
        {
            get { return this.isModified; }
            set
            {
                if (!this.IsEditable) return;
                if (this.isModified != value)
                {
                    this.isModified = value;
                    //ButtonManager.UpdateFlaggedButtons();
                    this.RefreshTexts();
                }
            }
        }

        /// <summary>
        /// Are we splitted in to two sci controls?
        /// </summary>
        public bool IsSplitted
        {
            get
            {
                if (!this.IsEditable || this.splitContainer.Panel2Collapsed) return false;
                else return true;
            }
            set
            {
                if (this.IsEditable)
                {
                    this.splitContainer.Panel2Collapsed = !value;
                    if (value) this.splitContainer.Panel2.Show();
                    else this.splitContainer.Panel2.Hide();
                }
            }
        }

        public bool IsBrowsable
        {
            get { throw new System.NotImplementedException(); }
        }

        public bool IsUntitled
        {
            get { throw new System.NotImplementedException(); }
        }

        /// <summary>
        /// Do we contain a ScintillaControl?
        /// </summary>
        public bool IsEditable
        {
            get
            {
                if (this.SciControl == null) return false;
                else return true;
            }
        }

        public void RefreshTexts()
        {
            throw new System.NotImplementedException();
        }

        public void Reload(bool showQuestion)
        {
            throw new System.NotImplementedException();
        }

        public void Revert(bool showQuestion)
        {
            throw new System.NotImplementedException();
        }

        public void Save(string file)
        {
            throw new System.NotImplementedException();
        }

        public void Save()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Adds a new scintilla control to the document
        /// </summary>
        public void AddEditorControls(string file, string text, int codepage)
        {
            this.editor = ScintillaManager.CreateControl(file, text, codepage);
            this.editor.Dock = DockStyle.Fill;
            this.editor2 = ScintillaManager.CreateControl(file, text, codepage);
            this.editor2.Dock = DockStyle.Fill;
            this.splitContainer = new SplitContainer();
            this.splitContainer.Name = "fdSplitView";
            this.splitContainer.Orientation = Orientation.Horizontal;
            this.splitContainer.BackColor = SystemColors.Control;
            this.splitContainer.Panel1.Controls.Add(this.editor);
            this.splitContainer.Panel2.Controls.Add(this.editor2);
            this.splitContainer.Dock = DockStyle.Fill;
            this.splitContainer.Panel2Collapsed = true;
            this.editor2.DocPointer = this.editor.DocPointer;
            //this.editor.SavePointLeft += delegate
            //{
            //    Globals.MainForm.OnDocumentModify(this);
            //};
            this.editor.SavePointReached += delegate
            {
                this.editor.MarkerDeleteAll(2);
                this.IsModified = false;
            };
            this.editor.FocusChanged += new FocusHandler(this.EditorFocusChanged);
            this.editor2.FocusChanged += new FocusHandler(this.EditorFocusChanged);
            this.editor.UpdateSync += new UpdateSyncHandler(this.EditorUpdateSync);
            this.editor2.UpdateSync += new UpdateSyncHandler(this.EditorUpdateSync);
            this.Controls.Add(this.splitContainer);
        }

        /// <summary>
        /// When the user changes to sci, block events from inactive sci
        /// </summary>
        private void EditorFocusChanged(ScintillaControl sender)
        {
            if (sender.IsFocus)
            {
                this.lastEditor = sender;
                this.editor.DisableAllSciEvents = (sender == editor2);
                this.editor2.DisableAllSciEvents = (sender == editor);
            }
        }

        /// <summary>
        /// Syncs both of the scintilla editors
        /// </summary>
        private void EditorUpdateSync(ScintillaControl sender)
        {
            if (!this.IsEditable) return;
            ScintillaControl e1 = editor;
            ScintillaControl e2 = editor2;
            if (sender == editor2)
            {
                e1 = editor2;
                e2 = editor;
            }
            e2.UpdateSync -= new UpdateSyncHandler(this.EditorUpdateSync);
            ScintillaManager.UpdateSyncProps(e1, e2);
            ScintillaManager.ApplySciSettings(e2);
            e2.UpdateSync += new UpdateSyncHandler(this.EditorUpdateSync);
            //Globals.MainForm.RefreshUI();
        }
    }
}