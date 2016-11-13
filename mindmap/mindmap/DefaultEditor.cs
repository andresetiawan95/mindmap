using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mindmap
{
    public class DefaultEditor : TabControl, IEditor
    {
        private List<IPanel> canvases;
        private IPanel selectedCanvas;
        private IToolbox toolbox;

        public IToolbox Toolbox
        {
            get
            {
                return this.toolbox;
            }

            set
            {
                this.toolbox = value;
            }
        }

        public DefaultEditor()
        {
            Dock = DockStyle.Fill;
            canvases = new List<IPanel>();

            this.Selected += DefaultEditor_Selected;
        }

        private void DefaultEditor_Selected(object sender, TabControlEventArgs e)
        {
            this.selectedCanvas = (IPanel)e.TabPage.Controls[0];
            //this.toolbox.ActiveTool = this.selectedCanvas.GetActiveTool();
        }

        public void AddCanvas(IPanel canvas)
        {
            canvases.Add(canvas);
            TabPage tabPage = new TabPage(canvas.Name);
            tabPage.Controls.Add((Control)canvas);
            this.Controls.Add(tabPage);
            this.SelectedTab = tabPage;
            this.selectedCanvas = canvas;
        }

        public IPanel GetSelectedCanvas()
        {
            return this.selectedCanvas;
        }

        public void RemoveCanvas(IPanel canvas)
        {
            throw new NotImplementedException();
        }

        public void RemoveSelectedCanvas()
        {
            //TabPage selectedTab = this.SelectedTab;

        }

        public void SelectCanvas(IPanel canvas)
        {
            this.selectedCanvas = canvas;
        }
    }
}
