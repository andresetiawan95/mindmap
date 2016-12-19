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
using mindmap.Tools;
using mindmap.Command;

namespace mindmap
{
    public partial class MainWindow : Form
    {
        private IToolbar toolbar;
        private IPanel panel;
        private IMenubar menubar;
        private IToolbox toolbox;
        private IEditor editor;
        public MainWindow()
        {
            InitializeComponent();
            InitUI();
        }

        private void InitUI()
        {
            Debug.WriteLine("Initializing UI objects.");

            IPanel canvas1 = new DefaultPanel();

            #region Toolbar

            // Initializing toolbar
            Debug.WriteLine("Loading toolbar...");
            this.toolbar = new DefaultToolbar();
            this.toolStripContainer1.TopToolStripPanel.Controls.Add((Control)this.toolbar);

            UnDoRedo undoredo = new UnDoRedo();

            ToolbarItem toolItem1 = new ToolbarItem("undo", IconSet.undo, canvas1);
            toolItem1.UnDoObject = undoredo;
            //toolItem1.SetCommand(whiteCanvasBgCmd);
            ToolbarItem toolItem2 = new ToolbarItem("redo", IconSet.redo, canvas1);
            toolItem2.UnDoObject = undoredo;
            //toolItem2.SetCommand(blackCanvasBgCmd);

            this.toolbar.AddToolbarItem(toolItem1);
            this.toolbar.AddSeparator();
            this.toolbar.AddToolbarItem(toolItem2);

            #endregion

            #region Editor and Panel
            Debug.WriteLine("Loading panel...");
            this.editor = new DefaultEditor();
            this.toolStripContainer1.ContentPanel.Controls.Add((Control)this.editor);

            
            canvas1.Name = "Untitled-1";
            canvas1.UnDoObject = undoredo;
            this.editor.AddCanvas(canvas1);

            /*IPanel canvas2 = new DefaultPanel();
            canvas2.Name = "Untitled-2";
            this.editor.AddCanvas(canvas2);*/
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
            DefaultMenuItem exampleMenuItem31 = new DefaultMenuItem("Image");
            exampleMenuItem3.AddMenuItem(exampleMenuItem31);
            DefaultMenuItem exampleMenuItem311 = new DefaultMenuItem("JPEG");
            exampleMenuItem31.AddMenuItem(exampleMenuItem311);
            DefaultMenuItem exampleMenuItem312 = new DefaultMenuItem("PNG");
            exampleMenuItem31.AddMenuItem(exampleMenuItem312);
            DefaultMenuItem exampleMenuItem32 = new DefaultMenuItem("PDF");
            exampleMenuItem3.AddMenuItem(exampleMenuItem32);
            #endregion

            #region Toolbox

            // Initializing toolbox
            Debug.WriteLine("Loading toolbox...");
            this.toolbox = new DefaultToolbox();
            this.toolStripContainer1.LeftToolStripPanel.Controls.Add((Control)this.toolbox);
            //this.editor.Toolbox = toolbox;

            #endregion

            #region Tools

            // Initializing tools
            Debug.WriteLine("Loading tools...");
            ITool selectionTool = new SelectionTool();
            selectionTool.UnDoObject = undoredo;
            this.toolbox.AddTool(selectionTool);
            this.toolbox.AddSeparator();
            this.toolbox.AddTool(new LineTool());
            this.toolbox.AddTool(new RectangleTool());
            this.toolbox.AddTool(new TextTool());
            this.toolbox.ToolSelected += Toolbox_ToolSelected;
            #endregion
        }
        private void Toolbox_ToolSelected(ITool tool)
        {
            if (this.editor != null)
            {
                Debug.WriteLine("Tool " + tool.Name + " is selected");
                IPanel canvas = this.editor.GetSelectedCanvas();
                canvas.SetActiveTool(tool);
                tool.TargetCanvas = canvas;
            }
        }
    }
}
