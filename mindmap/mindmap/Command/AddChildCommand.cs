using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using mindmap.Shapes;

namespace mindmap.Command
{
    public class AddChildCommand : ICommand
    {
        public Guid nodeID;
        public List<RectangleSegment> rectChild;
        public List<MindmapTree> child;
        public RectangleSegment node;
        public ButtonObject childBtnObject;
        private TextSegment textSegmentChild;
        private RectangleSegment newRect;
        private LineSegment lineChild;
        private MindmapTree mindmapTree;
        private IPanel canvas;
        public int X { set; get; }
        public int Y { set; get; }

        public AddChildCommand(IPanel canvas, RectangleSegment newRect, LineSegment lineChild, TextSegment textSegmentChild
            , ButtonObject childBtnObject, Guid nodeID, List<RectangleSegment> rectChild, List<MindmapTree> child, RectangleSegment node)
        {
            this.childBtnObject = childBtnObject;
            this.textSegmentChild = textSegmentChild;
            this.newRect = newRect;
            this.lineChild = lineChild;
            this.canvas = canvas;
            this.nodeID = nodeID;
            this.rectChild = rectChild;
            this.child = child;
            this.node = node;
            
        }

        public void Execute()
        {
            if (NumofChild() < 3)
            {
                DrawChild(node.VX1, node.VY1, node.VX2, node.VY2, 75, -120, 4, 4, node.VX1, node.VY1, 0);
            }
            else if (NumofChild() < 6)
            {
                DrawChild(node.VX3, node.VY3, node.VX4, node.VY4, 75, 120, 4, 4, node.VX3, node.VY3, 2);

                //DrawChild(node.VX2, node.VY2, node.VX4, node.VY4, 80, 75, 3, 3, node.VX2, node.VY2, 1);
            }
            else if (NumofChild() < 8)
            {
                //DrawChild(node.VX3, node.VY3, node.VX4, node.VY4, 75, 120, 4, 5, node.VX3, node.VY3, 2);
            }
            else if (NumofChild() < 10)
            {
                //DrawChild(node.VX1, node.VY1, node.VX3, node.VY3, -80, -75, 3, 8, node.VX1, node.VY1, 3);
            }
        }
        private void DrawChild(int xStart, int yStart, int xFinish, int yFinish, int addX, int addY, int divisor, int modder, int startingpointX, int startingpointY, int counterside)
        {
            //this.X = node.VX1 + 75 * NumofChild();
            //this.Y = (node.VY1 - 120);
            int newRectStart = 0, newRectFinish = 0, newRectStagnan = 0;
            int multiplier = 0;

            if (counterside % 2 == 0)
            {
                if (counterside == 0)
                {
                    this.X = xStart + addX * (NumofChild() % modder);
                    this.Y = yStart + addY;
                }
                else
                {
                    this.X = xStart + addX * ((NumofChild() + 1) % modder);
                    this.Y = yStart + addY;
                }

            }
            else
            {
                this.X = xStart + addX;
                this.Y = yStart + addY * (NumofChild() % modder);
            }

            /*if (counterside == 0)
            {
                this.X = xStart + addX * (NumofChild() % modder);
                this.Y = yStart + addY;
            }
            else if (counterside == 2)
            {
                this.X = xStart + addX * (NumofChild()+1 % modder);
                this.Y = yStart + addY;
            }
            */
            this.newRect = new RectangleSegment(X, Y, 70, 50);

            rectChild.Add(newRect);
            this.canvas.AddDrawingObject(newRect);
            this.canvas.AddRectangleObject(newRect);
            if (counterside == 0)
            {
                multiplier = NumofChild() % modder;
            }
            else if (counterside == 2)
            {
                multiplier = (NumofChild() % modder) + 1;
            }
            //set koordinat VX1
            this.newRect.VX1 = this.newRect.X;
            this.newRect.VY1 = this.newRect.Y;

            //set koordinat VX2, VY2
            this.newRect.VX2 = this.newRect.VX1 + this.newRect.Width;
            this.newRect.VY2 = this.newRect.VY1;

            this.newRect.VX3 = this.newRect.VX1;
            this.newRect.VY3 = this.newRect.VY1 + this.newRect.Height;

            this.newRect.VX4 = this.newRect.VX1 + this.newRect.Width;
            this.newRect.VY4 = this.newRect.VY1 + this.newRect.Height;

            if (counterside == 0)
            {
                newRectStart = this.newRect.VX3;
                newRectFinish = this.newRect.VX4;
                newRectStagnan = this.newRect.VY3;
                lineChild = new LineSegment(new System.Drawing.Point(((xFinish - xStart) / divisor) * multiplier + startingpointX, startingpointY), new System.Drawing.Point((newRectStart + newRectFinish) / 2, newRectStagnan));
            }
            else if (counterside == 1)
            {
                newRectStart = this.newRect.VY1;
                newRectFinish = this.newRect.VY3;
                newRectStagnan = this.newRect.VX1;
                lineChild = new LineSegment(new System.Drawing.Point(((xFinish - xStart) / divisor) * multiplier + startingpointX, startingpointY), new System.Drawing.Point(newRectStagnan, 50));
                //lineChild = new LineSegment(new System.Drawing.Point( startingpointX, ((yFinish - yStart) / divisor) * multiplier + startingpointY), new System.Drawing.Point(newRectStagnan, (newRectFinish - newRectStart) / 2));
                //  Debug.WriteLine("HASIL PADA COUNTERSIDE 1 koordinat Y : " + ((yFinish - yStart) / divisor) * multiplier + startingpointY + " FINISH : " + (newRectStart + newRectFinish) / 2);
            }
            else if (counterside == 2)
            {
                newRectStart = this.newRect.VX1;
                newRectFinish = this.newRect.VX2;
                newRectStagnan = this.newRect.VY1;
                lineChild = new LineSegment(new System.Drawing.Point(((xFinish - xStart) / divisor) * multiplier + startingpointX, startingpointY), new System.Drawing.Point((newRectStart + newRectFinish) / 2, newRectStagnan));
            }
            else if (counterside == 3)
            {
                newRectStart = this.newRect.VY2;
                newRectFinish = this.newRect.VY4;
                newRectStagnan = this.newRect.VX2;
                lineChild = new LineSegment(new System.Drawing.Point(startingpointX, ((yFinish - yStart) / divisor) * multiplier + startingpointY), new System.Drawing.Point(newRectStagnan, (newRectStart + newRectFinish) / 2));
            }
            //lineChild = new LineSegment(new System.Drawing.Point(((node.VX2 - node.VX1) / 4) * NumofChild() + node.VX1, node.VY1), new System.Drawing.Point((newRect.VX3 + newRect.VX4) / 2, newRect.VY3));
            lineChild = new LineSegment(new System.Drawing.Point(((xFinish - xStart) / divisor) * multiplier + startingpointX, startingpointY), new System.Drawing.Point((newRectStart + newRectFinish) / 2, newRectStagnan));
            this.canvas.AddDrawingObject(lineChild);
            node.Subscribe(lineChild);
            newRect.Subscribe(lineChild);
            lineChild.AddVertexStartObject(node);
            lineChild.AddVertexEndObject(newRect);

            MindmapTree mindmapTree = new MindmapTree(newRect, this.canvas);
            child.Add(mindmapTree);
            this.canvas.AddMindmapObject(mindmapTree);
            childBtnObject = new ButtonObject(X, Y, newRect.ID, this.canvas);
            this.canvas.AddButtonObject(childBtnObject);
            textSegmentChild = new TextSegment();
            textSegmentChild.Value = "Child";
            textSegmentChild.Position = new System.Drawing.PointF((float)((newRect.X + (newRect.X + newRect.Width)) / 2), (float)((newRect.Y + (newRect.Y + newRect.Height)) / 2));
            bool added = newRect.Add(textSegmentChild);
            newRect.SetTextSegment(textSegmentChild);
            newRect.Select();
            Debug.WriteLine("Jumlah Node : " + NumofChild());
        }
        public void Unexecute()
        {
            rectChild.Remove(newRect);
            this.canvas.RemoveDrawingObject(newRect);
            this.canvas.RemoveDrawingObject(lineChild);
            this.canvas.RemoveDrawingObject(textSegmentChild);
            /*for (int x = 0; x < this.canvas.sumDrawingObjects(); x++)
            {
                DrawingObject obj = this.canvas.getFromListObject(x);
                Debug.WriteLine("Object ID in Unexecute: " + obj.ID);
            }*/
            Debug.WriteLine("Jumlah Node : " + NumofChild());
            this.canvas.RemoveButtonObject(childBtnObject);
        }
        public int NumofChild()
        {
            return rectChild.Count;
        }
        
    }
}
