using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using mindmap.Shapes;

namespace mindmap.Tools
{
    public class LineTool : ToolStripButton, ITool
    {
        private IPanel canvas;
        private LineSegment lineSegment;

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

        public LineTool()
        {
            this.Name = "Stateful Line tool";
            this.ToolTipText = "Stateful Line tool";
            this.Image = IconSet.diagonal_line;
            this.CheckOnClick = true;
        }

        public void ToolMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                lineSegment = new LineSegment(new System.Drawing.Point(e.X, e.Y));
                lineSegment.Endpoint = new System.Drawing.Point(e.X, e.Y);
                canvas.AddDrawingObject(lineSegment);
            }
        }

        public void ToolMouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (this.lineSegment != null)
                {
                    lineSegment.Endpoint = new System.Drawing.Point(e.X, e.Y);
                }
            }
        }

        public void ToolMouseUp(object sender, MouseEventArgs e)
        {
            if (this.lineSegment != null)
            {
                if (e.Button == MouseButtons.Left)
                {
                    lineSegment.Endpoint = new System.Drawing.Point(e.X, e.Y);
                    lineSegment.Select();
                }
                else if (e.Button == MouseButtons.Right)
                {
                    canvas.RemoveDrawingObject(this.lineSegment);
                }
            }
        }
    }
}
