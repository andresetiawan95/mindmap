using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mindmap.Tools
{
    public class SelectionTool : ToolStripButton, ITool
    {
        private IPanel canvas;
        private DrawingObject selectedObject;
        private ButtonObject selectedBtnObject;
        private int xInitial;
        private int yInitial;

        public Cursor cursor
        {
            get
            {
                return Cursors.Arrow;
            }
        }

        public IPanel TargetCanvas
        {
            get
            {
                return this.canvas;
            }

            set
            {
                this.canvas = value;
            }
        }

        public SelectionTool()
        {
            this.Name = "Selection tool";
            this.ToolTipText = "Selection tool";
            this.Image = IconSet.cursor;
            this.CheckOnClick = true;
        }

        public void ToolMouseDown(object sender, MouseEventArgs e)
        {
            this.xInitial = e.X;
            this.yInitial = e.Y;

            if (e.Button == MouseButtons.Left && canvas != null)
            {
                canvas.DeselectAllObjects();
                Debug.WriteLine("Memanggil method SelectObjectAt pada Panel(DefaultPanel) melalui SelectionTool.cs..");
                selectedObject = canvas.SelectObjectAt(e.X, e.Y);
                if (selectedObject != null)
                {
                    canvas.GetButton(selectedObject.ID);
                    selectedBtnObject = canvas.SelectButtonObjectByID(selectedObject.ID);
                }
                Debug.WriteLine("Sudah selesai menjalankan method SelectObjectAt pada Panel(DefaultPanel) melalui SelectionTool.cs..");
            }
        }

        public void ToolMouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && canvas != null)
            {
                if (selectedObject != null)
                {
                    int xAmount = e.X - xInitial;
                    int yAmount = e.Y - yInitial;
                    xInitial = e.X;
                    yInitial = e.Y;

                    selectedObject.Translate(e.X, e.Y, xAmount, yAmount);
                    selectedBtnObject.Translate(e.X, e.Y, xAmount, yAmount);
                }
            }
        }

        public void ToolMouseUp(object sender, MouseEventArgs e)
        {

        }
    }
}
