using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using mindmap.Shapes;
using System.Diagnostics;

namespace mindmap.Tools
{
    public class RectangleTool : ToolStripButton, ITool
    {
        //merupakan class untuk implementasi Rectangle Tool (menggambar, menghapus, select object)
        private IPanel canvas;
        private RectangleSegment rectangle;
        private ButtonObject buttonObject;

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

        public RectangleTool()
        {
            this.Name = "Rectangle tool";
            this.ToolTipText = "Rectangle tool";
            this.Image = IconSet.bounding_box;
            this.CheckOnClick = true;
        }

        public void ToolMouseDown(object sender, MouseEventArgs e)
        {
            Debug.WriteLine("Rectangle mouse down (untuk menggambar object baru) --> activated (Via class RectangleTool.cs");
            if (e.Button == MouseButtons.Left)
            {
                this.rectangle = new RectangleSegment(e.X, e.Y);
                Debug.WriteLine("Rectangle sudah digambar... (via class RectangleTool.cs)");
                this.canvas.AddDrawingObject(this.rectangle);
                this.canvas.AddRectangleObject(this.rectangle);
                Debug.WriteLine("Rectangle dimasukkan kedalam AddDrawingObject pada kanvas (via class RectangleTool.cs)");
                this.buttonObject = new ButtonObject(e.X, e.Y, this.rectangle.ID);
                this.canvas.AddButtonObject(this.buttonObject);
                
            }
        }

        public void ToolMouseMove(object sender, MouseEventArgs e)
        {
           // Debug.WriteLine("Rectangle mouse move --> activated");
            if (e.Button == MouseButtons.Left)
            {
                if (this.rectangle != null)
                {
                    int width = e.X - this.rectangle.X;
                    int height = e.Y - this.rectangle.Y;

                    if (width > 0 && height > 0)
                    {
                        this.rectangle.Width = width;
                        this.rectangle.Height = height;
                    }
                }
            }
        }

        public void ToolMouseUp(object sender, MouseEventArgs e)
        {
            if (rectangle != null)
            {
                if (e.Button == MouseButtons.Left)
                {
                    //Debug.WriteLine("Rectangle mouse up left selected --> activated");
                    this.rectangle.Select();
                }
                else if (e.Button == MouseButtons.Right)
                {
                    //Debug.WriteLine("Rectangle mouse up right --> activated");
                    canvas.RemoveDrawingObject(this.rectangle);
                }
            }
        }
    }
}
