﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mindmap.Shapes
{
    public class LineSegment : DrawingObject, IObservable, IObserver
    {
        private const double EPSILON = 3.0;
        public int xstart { set; get; }
        public int ystart { set; get; }
        public int xfinish { set; get; }
        public int yfinish { set; get; }
        private DrawingObject startVertexObject;
        private DrawingObject endVertexObject;

        public Point Startpoint { get; set; }
        public Point Endpoint { get; set; }

        private List<IObserver> observerObjects;

        private Pen pen;

        public LineSegment()
        {
            this.observerObjects = new List<IObserver>();
            this.pen = new Pen(Color.Black);
            pen.Width = 1.5f;
        }

        public LineSegment(Point startpoint) :
            this()
        {
            this.Startpoint = startpoint;
        }

        public LineSegment(Point startpoint, Point endpoint) :
            this(startpoint)
        {
            this.Endpoint = endpoint;
        }

        public void setPoint(Point startpoint, Point endpoint)
        {
            this.Startpoint = startpoint;
            this.Endpoint = endpoint;
        }

        public override void RenderOnStaticView()
        {
            pen.Color = Color.Black;
            pen.Width = 1.5f;
            pen.DashStyle = DashStyle.Solid;

            if (this.GetGraphics() != null)
            {
                this.GetGraphics().SmoothingMode = SmoothingMode.AntiAlias;
                this.GetGraphics().DrawLine(pen, this.Startpoint, this.Endpoint);
            }
        }

        public override void RenderOnEditingView()
        {
            pen.Color = Color.Blue;
            pen.Width = 1.5f;
            pen.DashStyle = DashStyle.Solid;

            if (this.GetGraphics() != null)
            {
                this.GetGraphics().SmoothingMode = SmoothingMode.AntiAlias;
                this.GetGraphics().DrawLine(pen, this.Startpoint, this.Endpoint);
            }
        }

        public override void RenderOnPreview()
        {
            pen.Color = Color.Red;
            pen.Width = 1.5f;
            pen.DashStyle = DashStyle.DashDotDot;

            if (this.GetGraphics() != null)
            {
                this.GetGraphics().SmoothingMode = SmoothingMode.AntiAlias;
                this.GetGraphics().DrawLine(pen, this.Startpoint, this.Endpoint);
            }
        }

        public override bool Intersect(int xTest, int yTest)
        {
            double m = GetSlope();
            double b = Endpoint.Y - m * Endpoint.X;
            double y_point = m * xTest + b;

            if (Math.Abs(yTest - y_point) < EPSILON)
            {
                Debug.WriteLine("Object " + ID + " is selected.");
                return true;
            }
            return false;
        }

        public double GetSlope()
        {
            double m = (double)(Endpoint.Y - Startpoint.Y) / (double)(Endpoint.X - Startpoint.X);
            return m;
        }

        public override void Translate(int x, int y, int xAmount, int yAmount)
        {
            this.Startpoint = new Point(this.Startpoint.X + xAmount, this.Startpoint.Y + yAmount);
            this.Endpoint = new Point(this.Endpoint.X + xAmount, this.Endpoint.Y + yAmount);
        }

        public override bool Add(DrawingObject obj)
        {
            return false;
        }

        public override bool Remove(DrawingObject obj)
        {
            return false;
        }

        public override RectangleSegment getRect()
        {
            //do nothing
            return null;
        }

        public void Subscribe(IObserver observer)
        {
            this.observerObjects.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            throw new NotImplementedException();
        }

        public void Update(int x, int y)
        {

            //throw new NotImplementedException();
        }
        public void UpdateVertexCoordinate(int xAmount, int yAmount, DrawingObject drwObj)
        {
            //if (vertex == this.Startpoint)
            //{
            //    Debug.WriteLine("Line vertex Sama dengan startpoint");
            //   Startpoint.X += xAmount;
            // Startpoint.Y += yAmount;
            //}
            //else if (vertex == this.Endpoint)
            //{
            //   Debug.WriteLine("Line vertex Sama dengan endpoint");
            //  this.Endpoint = new Point(this.Endpoint.X + xAmount, this.Endpoint.Y + yAmount);
            //}
            if (drwObj == startVertexObject)
            {
                this.Startpoint = new Point(this.Startpoint.X + xAmount, this.Startpoint.Y + yAmount);
                Debug.WriteLine("Startpoint aktif");
            }
            else
            {
                this.Endpoint = new Point(this.Endpoint.X + xAmount, this.Endpoint.Y + yAmount);
                Debug.WriteLine("End aktif");
            }
        }
        public void AddVertexStartObject(DrawingObject obj)
        {
            this.startVertexObject = obj;
        }
        public void AddVertexEndObject(DrawingObject obj)
        {
            this.endVertexObject = obj;
        }
    }
}
