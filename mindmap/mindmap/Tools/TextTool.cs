using System;
using mindmap.Shapes;
using System.Windows.Forms;

namespace mindmap.Tools
{
    public class TextTool : ToolStripButton, ITool
    {
        private TextSegment text;
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
                text.Position = new System.Drawing.PointF((float)e.X, (float)e.Y);

                DrawingObject obj = canvas.SelectObjectAt(e.X, e.Y);

                if (obj == null)
                {
                    canvas.AddDrawingObject(text);
                }
                else
                {
                    bool allowed = obj.Add(text);

                    if (!allowed)
                    {
                        canvas.AddDrawingObject(text);
                    }
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
