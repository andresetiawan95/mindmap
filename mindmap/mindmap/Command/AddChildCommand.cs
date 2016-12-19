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
            this.X = node.VX1 + 75 * NumofChild();
            this.Y = (node.VY1 - 120);

            newRect.setInitRectangle(X, Y, 70, 50);

            rectChild.Add(newRect);
            this.canvas.AddDrawingObject(newRect);
            this.canvas.AddRectangleObject(newRect);

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

            lineChild.setPoint(new System.Drawing.Point(((node.VX2 - node.VX1) / 4) * NumofChild() + node.VX1, node.VY1), new System.Drawing.Point((newRect.VX3 + newRect.VX4) / 2, newRect.VY3));
            this.canvas.AddDrawingObject(lineChild);
            node.Subscribe(lineChild);
            newRect.Subscribe(lineChild);
            lineChild.AddVertexStartObject(node);
            lineChild.AddVertexEndObject(newRect);

            mindmapTree = new MindmapTree(newRect, this.canvas);
            child.Add(mindmapTree);
            this.canvas.AddMindmapObject(mindmapTree);
            childBtnObject.setButton(X, Y, newRect.ID, this.canvas);
            this.canvas.AddButtonObject(childBtnObject);
            textSegmentChild.Value = "Child";
            textSegmentChild.Position = new System.Drawing.PointF((float)((newRect.X + (newRect.X + newRect.Width)) / 2), (float)((newRect.Y + (newRect.Y + newRect.Height)) / 2));
            bool added = newRect.Add(textSegmentChild);
            newRect.Select();
            Debug.WriteLine("Jumlah Node : " + NumofChild());
            for (int x = 0; x < this.canvas.sumDrawingObjects(); x++)
            {
                DrawingObject obj = this.canvas.getFromListObject(x);
                Debug.WriteLine("Object ID in Execute: " + obj.ID);
            }
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
