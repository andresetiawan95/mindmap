using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using mindmap.ToolbarItems;

namespace mindmap
{
    public partial class MainWindow : Form
    {
        private IToolbar toolbar;
        private IPanel panel;

        public MainWindow()
        {
            InitializeComponent();
            InitUI();
        }

        private void InitUI()
        {
            Debug.WriteLine("Initializing UI objects.");

            #region Toolbar

            // Initializing toolbar
            Debug.WriteLine("Loading toolbar...");
            this.toolbar = new DefaultToolbar();
            this.toolStripContainer1.TopToolStripPanel.Controls.Add((Control)this.toolbar);

            ToolbarItem toolItem1 = new ToolbarItem();
            //toolItem1.SetCommand(whiteCanvasBgCmd);
            ToolbarItem toolItem2 = new ToolbarItem();
            //toolItem2.SetCommand(blackCanvasBgCmd);

            this.toolbar.AddToolbarItem(toolItem1);
            this.toolbar.AddSeparator();
            this.toolbar.AddToolbarItem(toolItem2);

            #endregion

            #region Panel
            this.panel = new DefaultPanel();
            this.toolStripContainer1.ContentPanel.Controls.Add((Control)panel);
            #endregion
        }
    }
}
