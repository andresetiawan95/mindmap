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
using mindmap.MenuItems;

namespace mindmap
{
    public partial class MainWindow : Form
    {
        private IToolbar toolbar;
        private IPanel panel;
        private IMenubar menubar;
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

            #region Menubar
            Debug.WriteLine("Loading menubar...");
            this.menubar = new DefaultMenubar();
            this.Controls.Add((Control)this.menubar);

            DefaultMenuItem exampleMenuItem1 = new DefaultMenuItem("File");
            this.menubar.AddMenuItem(exampleMenuItem1);

            DefaultMenuItem exampleMenuItem11 = new DefaultMenuItem("New Mindmap");
            exampleMenuItem1.AddMenuItem(exampleMenuItem11);
            DefaultMenuItem exampleMenuItem12 = new DefaultMenuItem("Open Mindmap");
            exampleMenuItem1.AddMenuItem(exampleMenuItem12);
            DefaultMenuItem exampleMenuItem13 = new DefaultMenuItem("Close");
            exampleMenuItem1.AddMenuItem(exampleMenuItem13);

            DefaultMenuItem exampleMenuItem2 = new DefaultMenuItem("Edit");
            this.menubar.AddMenuItem(exampleMenuItem2);

            DefaultMenuItem exampleMenuItem21 = new DefaultMenuItem("Add Proccess");
            exampleMenuItem2.AddMenuItem(exampleMenuItem21);

            DefaultMenuItem exampleMenuItem3 = new DefaultMenuItem("Export");
            this.menubar.AddMenuItem(exampleMenuItem3);

            #endregion
        }
    }
}
