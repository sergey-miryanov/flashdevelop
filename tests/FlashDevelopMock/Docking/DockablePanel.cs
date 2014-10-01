﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace FlashDevelop.Mock.Docking
{
    class DockablePanel : DockContent
    {
        private String pluginGuid;

        public DockablePanel(Control ctrl, String pluginGuid)
        {
            this.Text = ctrl.Text;
            ctrl.Dock = DockStyle.Fill;
            this.DockPanel = MainForm.Instance.DockPanel;
            if (ctrl.Tag != null) this.TabText = ctrl.Tag.ToString();
            this.DockAreas = DockAreas.DockBottom | DockAreas.DockLeft | DockAreas.DockRight | DockAreas.DockTop | DockAreas.Float;
            this.Font = MainForm.Instance.Settings.DefaultFont;
            this.pluginGuid = pluginGuid;
            this.HideOnClose = true;
            this.Controls.Add(ctrl);
            //this.Show();
        }

        /// <summary>
        /// Retrieves the guid of the document
        /// </summary>
        public override String GetPersistString()
        {
            return this.pluginGuid;
        }
    }
}