using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mindmap.Shapes
{
    public class RectangleSegment : DrawingObject
    {
        public Guid nodeID;
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        private Pen pen;
        private List<DrawingObject> drawingObjects;

        public RectangleSegment()
        {
            Debug.WriteLine("Inisialisasi Class Rectangle");
            this.pen = new Pen(Color.Black);
            pen.Width = 1.5f;
            drawingObjects = new List<DrawingObject>();
        }

        public RectangleSegment(int x, int y)
            : this()
        {
            this.X = x;
            this.Y = y;
        }

        public RectangleSegment(int x, int y, int width, int height)
            : this(x, y)
        {
            this.Width = width;
            this.Height = height;
        }

        public override bool Intersect(int xTest, int yTest)
        {
            if ((xTest >= X && xTest <= X + Width) && (yTest >= Y && yTest <= Y + Height))
            {
                Debug.WriteLine("Object " + ID + " is selected (From Rectangle.cs class).");
                return true;
            }
            return false;
        }

        public override void RenderOnStaticView()
        {
            this.pen.Color = Color.Black;
            this.pen.DashStyle = DashStyle.Solid;
            GetGraphics().DrawRectangle(this.pen, X, Y, Width, Height);

            foreach (DrawingObject obj in drawingObjects)
            {
                obj.SetGraphics(GetGraphics());
                obj.RenderOnStaticView();
            }
        }

        public override void RenderOnEditingView()
        {
            //Debug.WriteLine("gambar ulang rectangle nya dengan warna biru...");
            this.pen.Color = Color.Blue;
            this.pen.DashStyle = DashStyle.Solid;
            GetGraphics().DrawRectangle(this.pen, X, Y, Width, Height);
            //Graphics.DrawImage(IconSet.add_mindmap_tree, X ,Y);

            foreach (DrawingObject obj in drawingObjects)
            {
                obj.SetGraphics(GetGraphics());
                obj.RenderOnEditingView();
            }
        }

        public override void RenderOnPreview()
        {
            this.pen.Color = Color.Red;
            this.pen.DashStyle = DashStyle.DashDot;
            GetGraphics().DrawRectangle(this.pen, X, Y, Width, Height);

            foreach (DrawingObject obj in drawingObjects)
            {
                obj.SetGraphics(GetGraphics());
                obj.RenderOnPreview();
            }
        }

        public override void Translate(int x, int y, int xAmount, int yAmount)
        {
            this.X += xAmount;
            this.Y += yAmount;

            foreach (DrawingObject obj in drawingObjects)
            {
                obj.Translate(x, y, xAmount, yAmount);
            }
        }
        public int getX()
        {
            return this.X;
        }
        public int getY()
        {
            return this.Y;
        }

        public override bool Add(DrawingObject obj)
        {
            drawingObjects.Add(obj);

            return true;
        }

        public override bool Remove(DrawingObject obj)
        {
            drawingObjects.Remove(obj);

            return true;
        }
    }
}