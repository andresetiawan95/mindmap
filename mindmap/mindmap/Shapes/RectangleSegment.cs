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
    public class RectangleSegment : DrawingObject, IObservable, IObserver
    {
        public Guid nodeID;
        public int X { get; set; }
        public int Y { get; set; }
        public int X2 { get; set; }
        public int Y2 { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        //koordinat tiap vertex pada rectangle
        public int VX1 { get; set; }
        public int VY1 { get; set; }
        public int VX2 { get; set; }
        public int VY2 { get; set; }
        public int VX3 { get; set; }
        public int VY3 { get; set; }
        public int VX4 { get; set; }
        public int VY4 { get; set; }

        public int lineVertex { set; get; }
        private Pen pen;
        private List<DrawingObject> drawingObjects;
        private List<IObserver> observerObjects;
        private TextSegment text;

        public RectangleSegment()
        {
            Debug.WriteLine("Inisialisasi Class Rectangle");
            this.pen = new Pen(Color.Black);
            pen.Width = 1.5f;
            drawingObjects = new List<DrawingObject>();
            observerObjects = new List<IObserver>();
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
            this.VX1 += xAmount;
            this.VY1 += yAmount;
            this.VX2 = this.VX1 + Width;
            this.VY2 = this.VY1;

            this.VX3 = this.VX1;
            this.VY3 = this.VY1 + Height;

            this.VX4 = this.VX1 + Width;
            this.VY4 = this.VY1 + Height;
            foreach (IObserver obsObj in observerObjects)
            {
                obsObj.UpdateVertexCoordinate(xAmount, yAmount, this);
            }
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
            bool isEmpty = !drawingObjects.Any();
            if (!isEmpty)
            {
                Debug.WriteLine("Ada obj Text disini");
                return false;
            }
            else
            {
                Debug.WriteLine("Tidak ada obj Text disini");
                drawingObjects.Add(obj);
                return true;
            }
        }

        public override bool Remove(DrawingObject obj)
        {
            drawingObjects.Remove(obj);

            return true;
        }

        public override RectangleSegment getRect()
        {
            //do nothing
            return this;
        }

        public void Subscribe(IObserver observer)
        {
            this.observerObjects.Add(observer);
            Debug.WriteLine("Line observer added to rectangle");
            //throw new NotImplementedException();
        }

        public void Unsubscribe(IObserver observer)
        {
            throw new NotImplementedException();
        }

        public void Update(int x, int y)
        {
            foreach (IObserver obsObj in observerObjects)
            {
                obsObj.UpdateVertexCoordinate(x, y, this);
            }
            //throw new NotImplementedException();
        }
        public void UpdateVertexCoordinate(int x, int y, DrawingObject drwObj) { }
    }
}