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
        private RectangleSegment selectedRectangle;

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

        //checking intersection
        private int orientation(int xp, int yp, int xq, int yq, int xr, int yr)
        {
            int val = (yq - yp) * (xr - xq) - (xq - xp) * (yr - yq);
            if (val == 0) return 0;
            return (val > 0) ? 1 : 2;
        }
        private bool doIntersect (int xp1, int yp1, int xq1, int yq1, int xp2, int yp2, int xq2, int yq2)
        {
            int o1 = orientation(xp1, yp1, xq1, yq1, xp2, yp2);
            int o2 = orientation(xp1, yp1, xq1, yq1, xq2, yq2);
            int o3 = orientation(xp2, yp2, xq2, yq2, xp1, yp1);
            int o4 = orientation(xp2, yp2, xq2, yq2, xq1, yq1);
            if (o1 != o2 && o3 != o4) return true;

            return false;
        }

        public void ToolMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                lineSegment = new LineSegment(new System.Drawing.Point(e.X, e.Y));
                lineSegment.Endpoint = new System.Drawing.Point(e.X, e.Y);
                canvas.AddDrawingObject(lineSegment);
                selectedRectangle = this.canvas.GetRectangleObjectAt(e.X, e.Y);
                if (selectedRectangle != null)
                {

                }
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
