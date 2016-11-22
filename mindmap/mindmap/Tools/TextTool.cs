using System;
using mindmap.Shapes;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace mindmap.Tools
{
    public class TextTool : ToolStripButton, ITool
    {
        private TextSegment text;
        private RectangleSegment rect;
        private IPanel canvas;

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

        public TextTool()
        {
            this.Name = "Text tool";
            this.ToolTipText = "Text tool";
            this.Image = IconSet.font;
            this.CheckOnClick = true;
        }

        public void ToolMouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        public void ToolMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                text = new TextSegment();
                text.Value = "Text";

                DrawingObject obj = canvas.SelectObjectAt(e.X, e.Y);

                if (obj != null)
                {
                    

                    rect = obj.getRect();

                    int widthrect = rect.Width;
                    int heightrect = rect.Height;
                    int xrect = rect.X;
                    int yrect = rect.Y;
                    int xrect2 = rect.X2;
                    int yrect2 = rect.Y2;
                    text.Position = new System.Drawing.PointF((float)((xrect + xrect2) / 2) - (widthrect/10), (float)((yrect + yrect2) / 2) - (heightrect/10));
                    bool allowed = obj.Add(text);
                }
            }
        }
        public void ToolMouseMove(object sender, MouseEventArgs e)
        {

        }

        public void ToolMouseUp(object sender, MouseEventArgs e)
        {

        }

        public void ToolKeyUp(object sender, KeyEventArgs e)
        {

        }

        public void ToolKeyDown(object sender, KeyEventArgs e)
        {

        }

        public void ToolHotKeysDown(object sender, Keys e)
        {

        }
    }
}
