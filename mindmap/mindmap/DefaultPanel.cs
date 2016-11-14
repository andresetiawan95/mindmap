using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mindmap
{
    public class DefaultPanel: Control, IPanel
    {
        private ITool tools;
        private List<DrawingObject> drawingObjects;

        //Constructor untuk menginisiasi Panel
        public DefaultPanel()
        {
            this.drawingObjects = new List<DrawingObject>();
            this.DoubleBuffered = true;
            this.BackColor = Color.White;
            this.Dock = DockStyle.Fill;

            this.Paint += DefaultCanvas_Paint;
            this.MouseDown += DefaultCanvas_MouseDown;
            this.MouseUp += DefaultCanvas_MouseUp;
            this.MouseMove += DefaultCanvas_MouseMove;
        }
        
        public ITool GetActiveTool()
        {
            return this.tools;
        }
        public void Repaint()
        {
            this.Invalidate();
            this.Update();
        }
        public void SetBackgroundColor(Color color)
        {
            this.BackColor = color;
        }
        public void SetActiveTool (ITool tool)
        {
            this.tools = tool;
        }
        private void DefaultCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.tools != null)
            {
                this.tools.ToolMouseMove(sender, e);
                this.Repaint();
            }
        }

        private void DefaultCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.tools != null)
            {
                this.tools.ToolMouseUp(sender, e);
                this.Repaint();
            }
        }

        private void DefaultCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.tools != null)
            {
                this.tools.ToolMouseDown(sender, e);
                this.Repaint();
            }
        }

        private void DefaultCanvas_Paint(object sender, PaintEventArgs e)
        {
            foreach (DrawingObject obj in drawingObjects)
            {
                Debug.WriteLine("Masuk ke Method DefaultCanvas_Paint");
                obj.Graphics = e.Graphics;
                obj.Draw();
            }
        }
        public void AddDrawingObject(DrawingObject drawingObject)
        {
            this.drawingObjects.Add(drawingObject);
        }

        public void RemoveDrawingObject(DrawingObject drawingObject)
        {
            this.drawingObjects.Remove(drawingObject);
        }

        public DrawingObject GetObjectAt(int x, int y)
        {
            Debug.WriteLine("Masuk ke method GetObjectAt...");
            foreach (DrawingObject obj in drawingObjects)
            {
                if (obj.Intersect(x, y))
                {
                    return obj;
                }
            }
            return null;
        }

        public DrawingObject SelectObjectAt(int x, int y)
        {
            Debug.WriteLine("Masuk ke method SelectObjectAt...");
            DrawingObject obj = GetObjectAt(x, y);
            if (obj != null)
            {
                obj.Select();
            }

            return obj;
        }

        public void DeselectAllObjects()
        {
            foreach (DrawingObject drawObj in drawingObjects)
            {
                drawObj.Deselect();
            }
        }
    }
}
