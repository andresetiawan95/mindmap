using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using mindmap.Shapes;
using mindmap.Command;

namespace mindmap.Tools
{
    public class LineTool : ToolStripButton, ITool
    {
        private int newXStart { set; get; }
        private int newYStart { set; get; }
        private int newXFinish { set; get; }
        private int newYFinish { set; get; }
        private IPanel canvas;
        private LineSegment lineSegment;
        private RectangleSegment selectedRectangle;
        private RectangleSegment selectedRectangle2;

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

        private UnDoRedo _UnDoObject;

        public UnDoRedo UnDoObject
        {
            get
            {
                return _UnDoObject;
            }
            set
            {
                _UnDoObject = value;
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
        private int IntersectOnVertex(int xs1, int ys1, int xs2, int ys2, RectangleSegment selectedRectangle)
        {
            if (doIntersect(selectedRectangle.VX1, selectedRectangle.VY1, selectedRectangle.VX2, selectedRectangle.VY2, xs1, ys1, xs2, ys2)) return 1;
            if (doIntersect(selectedRectangle.VX2, selectedRectangle.VY2, selectedRectangle.VX4, selectedRectangle.VY4, xs1, ys1, xs2, ys2)) return 2;
            if (doIntersect(selectedRectangle.VX3, selectedRectangle.VY3, selectedRectangle.VX4, selectedRectangle.VY4, xs1, ys1, xs2, ys2)) return 3;
            if (doIntersect(selectedRectangle.VX1, selectedRectangle.VY1, selectedRectangle.VX3, selectedRectangle.VY3, xs1, ys1, xs2, ys2)) return 4;
            return 0;
        }
        private void SetNewIntersectCoordinateStart(int position)
        {
            if (position == 1)
            {
                this.newXStart = (int)((this.selectedRectangle.VX1 * this.selectedRectangle.VY2 - this.selectedRectangle.VY1 * this.selectedRectangle.VX2) * (this.lineSegment.xstart - this.lineSegment.xfinish) - (this.selectedRectangle.VX1 - this.selectedRectangle.VX2) * (this.lineSegment.xstart * this.lineSegment.yfinish - this.lineSegment.ystart * this.lineSegment.xfinish))
                    / ((this.selectedRectangle.VX1 - this.selectedRectangle.VX2) * (this.lineSegment.ystart - this.lineSegment.yfinish) - (this.selectedRectangle.VY1 - this.selectedRectangle.VY2) * (this.lineSegment.xstart - this.lineSegment.xfinish));
                this.newYStart = (int)((this.selectedRectangle.VX1 * this.selectedRectangle.VY2 - this.selectedRectangle.VY1 * this.selectedRectangle.VX2) * (this.lineSegment.ystart - this.lineSegment.yfinish) - (this.selectedRectangle.VY1 - this.selectedRectangle.VY2) * (this.lineSegment.xstart * this.lineSegment.yfinish - this.lineSegment.ystart * this.lineSegment.xfinish))
                    / ((this.selectedRectangle.VX1 - this.selectedRectangle.VX2) * (this.lineSegment.ystart - this.lineSegment.yfinish) - (this.selectedRectangle.VY1 - this.selectedRectangle.VY2) * (this.lineSegment.xstart - this.lineSegment.xfinish));
            }
            if (position == 2)
            {
                this.newXStart = (int)((this.selectedRectangle.VX2 * this.selectedRectangle.VY4 - this.selectedRectangle.VY2 * this.selectedRectangle.VX4) * (this.lineSegment.xstart - this.lineSegment.xfinish) - (this.selectedRectangle.VX2 - this.selectedRectangle.VX4) * (this.lineSegment.xstart * this.lineSegment.yfinish - this.lineSegment.ystart * this.lineSegment.xfinish))
                    / ((this.selectedRectangle.VX2 - this.selectedRectangle.VX4) * (this.lineSegment.ystart - this.lineSegment.yfinish) - (this.selectedRectangle.VY2 - this.selectedRectangle.VY4) * (this.lineSegment.xstart - this.lineSegment.xfinish));
                this.newYStart = (int)((this.selectedRectangle.VX2 * this.selectedRectangle.VY4 - this.selectedRectangle.VY2 * this.selectedRectangle.VX4) * (this.lineSegment.ystart - this.lineSegment.yfinish) - (this.selectedRectangle.VY2 - this.selectedRectangle.VY4) * (this.lineSegment.xstart * this.lineSegment.yfinish - this.lineSegment.ystart * this.lineSegment.xfinish))
                    / ((this.selectedRectangle.VX2 - this.selectedRectangle.VX4) * (this.lineSegment.ystart - this.lineSegment.yfinish) - (this.selectedRectangle.VY2 - this.selectedRectangle.VY4) * (this.lineSegment.xstart - this.lineSegment.xfinish));
            }
            if (position == 3)
            {
                this.newXStart = (int)((this.selectedRectangle.VX3 * this.selectedRectangle.VY4 - this.selectedRectangle.VY3 * this.selectedRectangle.VX4) * (this.lineSegment.xstart - this.lineSegment.xfinish) - (this.selectedRectangle.VX3 - this.selectedRectangle.VX4) * (this.lineSegment.xstart * this.lineSegment.yfinish - this.lineSegment.ystart * this.lineSegment.xfinish))
                    / ((this.selectedRectangle.VX3 - this.selectedRectangle.VX4) * (this.lineSegment.ystart - this.lineSegment.yfinish) - (this.selectedRectangle.VY3 - this.selectedRectangle.VY4) * (this.lineSegment.xstart - this.lineSegment.xfinish));
                this.newYStart = (int)((this.selectedRectangle.VX3 * this.selectedRectangle.VY4 - this.selectedRectangle.VY3 * this.selectedRectangle.VX4) * (this.lineSegment.ystart - this.lineSegment.yfinish) - (this.selectedRectangle.VY3 - this.selectedRectangle.VY4) * (this.lineSegment.xstart * this.lineSegment.yfinish - this.lineSegment.ystart * this.lineSegment.xfinish))
                    / ((this.selectedRectangle.VX3 - this.selectedRectangle.VX4) * (this.lineSegment.ystart - this.lineSegment.yfinish) - (this.selectedRectangle.VY3 - this.selectedRectangle.VY4) * (this.lineSegment.xstart - this.lineSegment.xfinish));
            }
            if (position == 4)
            {
                this.newXStart = (int)((this.selectedRectangle.VX1 * this.selectedRectangle.VY3 - this.selectedRectangle.VY1 * this.selectedRectangle.VX3) * (this.lineSegment.xstart - this.lineSegment.xfinish) - (this.selectedRectangle.VX1 - this.selectedRectangle.VX3) * (this.lineSegment.xstart * this.lineSegment.yfinish - this.lineSegment.ystart * this.lineSegment.xfinish))
                    / ((this.selectedRectangle.VX1 - this.selectedRectangle.VX3) * (this.lineSegment.ystart - this.lineSegment.yfinish) - (this.selectedRectangle.VY1 - this.selectedRectangle.VY3) * (this.lineSegment.xstart - this.lineSegment.xfinish));
                this.newYStart = (int)((this.selectedRectangle.VX1 * this.selectedRectangle.VY3 - this.selectedRectangle.VY1 * this.selectedRectangle.VX3) * (this.lineSegment.ystart - this.lineSegment.yfinish) - (this.selectedRectangle.VY1 - this.selectedRectangle.VY3) * (this.lineSegment.xstart * this.lineSegment.yfinish - this.lineSegment.ystart * this.lineSegment.xfinish))
                    / ((this.selectedRectangle.VX1 - this.selectedRectangle.VX3) * (this.lineSegment.ystart - this.lineSegment.yfinish) - (this.selectedRectangle.VY1 - this.selectedRectangle.VY3) * (this.lineSegment.xstart - this.lineSegment.xfinish));
            }
        }
        private void SetNewIntersectCoordinateFinish(int position)
        {
            if (position == 1)
            {
                this.newXFinish = (int)((this.selectedRectangle2.VX1 * this.selectedRectangle2.VY2 - this.selectedRectangle2.VY1 * this.selectedRectangle2.VX2) * (this.lineSegment.xstart - this.lineSegment.xfinish) - (this.selectedRectangle2.VX1 - this.selectedRectangle2.VX2) * (this.lineSegment.xstart * this.lineSegment.yfinish - this.lineSegment.ystart * this.lineSegment.xfinish))
                    / ((this.selectedRectangle2.VX1 - this.selectedRectangle2.VX2) * (this.lineSegment.ystart - this.lineSegment.yfinish) - (this.selectedRectangle2.VY1 - this.selectedRectangle2.VY2) * (this.lineSegment.xstart - this.lineSegment.xfinish));
                this.newYFinish = (int)((this.selectedRectangle2.VX1 * this.selectedRectangle2.VY2 - this.selectedRectangle2.VY1 * this.selectedRectangle2.VX2) * (this.lineSegment.ystart - this.lineSegment.yfinish) - (this.selectedRectangle2.VY1 - this.selectedRectangle2.VY2) * (this.lineSegment.xstart * this.lineSegment.yfinish - this.lineSegment.ystart * this.lineSegment.xfinish))
                    / ((this.selectedRectangle2.VX1 - this.selectedRectangle2.VX2) * (this.lineSegment.ystart - this.lineSegment.yfinish) - (this.selectedRectangle2.VY1 - this.selectedRectangle2.VY2) * (this.lineSegment.xstart - this.lineSegment.xfinish));
            }
            if (position == 2)
            {
                this.newXFinish = (int)((this.selectedRectangle2.VX2 * this.selectedRectangle2.VY4 - this.selectedRectangle2.VY2 * this.selectedRectangle2.VX4) * (this.lineSegment.xstart - this.lineSegment.xfinish) - (this.selectedRectangle2.VX2 - this.selectedRectangle2.VX4) * (this.lineSegment.xstart * this.lineSegment.yfinish - this.lineSegment.ystart * this.lineSegment.xfinish))
                    / ((this.selectedRectangle2.VX2 - this.selectedRectangle2.VX4) * (this.lineSegment.ystart - this.lineSegment.yfinish) - (this.selectedRectangle2.VY2 - this.selectedRectangle2.VY4) * (this.lineSegment.xstart - this.lineSegment.xfinish));
                this.newYFinish = (int)((this.selectedRectangle2.VX2 * this.selectedRectangle2.VY4 - this.selectedRectangle2.VY2 * this.selectedRectangle2.VX4) * (this.lineSegment.ystart - this.lineSegment.yfinish) - (this.selectedRectangle2.VY2 - this.selectedRectangle2.VY4) * (this.lineSegment.xstart * this.lineSegment.yfinish - this.lineSegment.ystart * this.lineSegment.xfinish))
                    / ((this.selectedRectangle2.VX2 - this.selectedRectangle2.VX4) * (this.lineSegment.ystart - this.lineSegment.yfinish) - (this.selectedRectangle2.VY2 - this.selectedRectangle2.VY4) * (this.lineSegment.xstart - this.lineSegment.xfinish));
            }
            if (position == 3)
            {
                this.newXFinish = (int)((this.selectedRectangle2.VX3 * this.selectedRectangle2.VY4 - this.selectedRectangle2.VY3 * this.selectedRectangle2.VX4) * (this.lineSegment.xstart - this.lineSegment.xfinish) - (this.selectedRectangle2.VX3 - this.selectedRectangle2.VX4) * (this.lineSegment.xstart * this.lineSegment.yfinish - this.lineSegment.ystart * this.lineSegment.xfinish))
                    / ((this.selectedRectangle2.VX3 - this.selectedRectangle2.VX4) * (this.lineSegment.ystart - this.lineSegment.yfinish) - (this.selectedRectangle2.VY3 - this.selectedRectangle2.VY4) * (this.lineSegment.xstart - this.lineSegment.xfinish));
                this.newYFinish = (int)((this.selectedRectangle2.VX3 * this.selectedRectangle2.VY4 - this.selectedRectangle2.VY3 * this.selectedRectangle2.VX4) * (this.lineSegment.ystart - this.lineSegment.yfinish) - (this.selectedRectangle2.VY3 - this.selectedRectangle2.VY4) * (this.lineSegment.xstart * this.lineSegment.yfinish - this.lineSegment.ystart * this.lineSegment.xfinish))
                    / ((this.selectedRectangle2.VX3 - this.selectedRectangle2.VX4) * (this.lineSegment.ystart - this.lineSegment.yfinish) - (this.selectedRectangle2.VY3 - this.selectedRectangle2.VY4) * (this.lineSegment.xstart - this.lineSegment.xfinish));
            }
            if (position == 4)
            {
                this.newXFinish = (int)((this.selectedRectangle2.VX1 * this.selectedRectangle2.VY3 - this.selectedRectangle2.VY1 * this.selectedRectangle2.VX3) * (this.lineSegment.xstart - this.lineSegment.xfinish) - (this.selectedRectangle2.VX1 - this.selectedRectangle2.VX3) * (this.lineSegment.xstart * this.lineSegment.yfinish - this.lineSegment.ystart * this.lineSegment.xfinish))
                    / ((this.selectedRectangle2.VX1 - this.selectedRectangle2.VX3) * (this.lineSegment.ystart - this.lineSegment.yfinish) - (this.selectedRectangle2.VY1 - this.selectedRectangle2.VY3) * (this.lineSegment.xstart - this.lineSegment.xfinish));
                this.newYFinish = (int)((this.selectedRectangle2.VX1 * this.selectedRectangle2.VY3 - this.selectedRectangle2.VY1 * this.selectedRectangle2.VX3) * (this.lineSegment.ystart - this.lineSegment.yfinish) - (this.selectedRectangle2.VY1 - this.selectedRectangle2.VY3) * (this.lineSegment.xstart * this.lineSegment.yfinish - this.lineSegment.ystart * this.lineSegment.xfinish))
                    / ((this.selectedRectangle2.VX1 - this.selectedRectangle2.VX3) * (this.lineSegment.ystart - this.lineSegment.yfinish) - (this.selectedRectangle2.VY1 - this.selectedRectangle2.VY3) * (this.lineSegment.xstart - this.lineSegment.xfinish));
            }
        }
        public void ToolMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                lineSegment = new LineSegment(new System.Drawing.Point(e.X, e.Y));
                lineSegment.Endpoint = new System.Drawing.Point(e.X, e.Y);
                canvas.AddDrawingObject(lineSegment);
                selectedRectangle = this.canvas.GetRectangleObjectAt(e.X, e.Y);
                lineSegment.xstart = e.X;
                lineSegment.ystart = e.Y;
                if (selectedRectangle != null)
                {
                    //implemented later
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
                    lineSegment.xfinish = e.X;
                    lineSegment.yfinish = e.Y;
                    this.selectedRectangle2 = this.canvas.GetRectangleObjectAt(e.X, e.Y);
                    int result = IntersectOnVertex(lineSegment.xstart, lineSegment.ystart, lineSegment.xfinish, lineSegment.yfinish, selectedRectangle);
                    SetNewIntersectCoordinateStart(result);
                    lineSegment.Startpoint = new System.Drawing.Point(this.newXStart, this.newYStart);
                    int resultfinish = IntersectOnVertex(lineSegment.xstart, lineSegment.ystart, lineSegment.xfinish, lineSegment.yfinish, selectedRectangle2);
                    SetNewIntersectCoordinateFinish(resultfinish);
                    lineSegment.Endpoint = new System.Drawing.Point(this.newXFinish, this.newYFinish);
                    Debug.WriteLine("Objek yang terintersect : " + result);
                    lineSegment.Select();
                }
                else if (e.Button == MouseButtons.Right)
                {
                    canvas.RemoveDrawingObject(this.lineSegment);
                }
            }
        }

        public void ToolMouseDoubleClick(object sender, MouseEventArgs e)
        {
            
        }
    }
}
