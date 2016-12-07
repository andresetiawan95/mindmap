using mindmap.Shapes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mindmap
{
    public class MindmapTree
    {
        public List<RectangleSegment> rectChild;
        public List<MindmapTree> child;
        public RectangleSegment node;
        public ButtonObject childBtnObject;
        private TextSegment textSegmentChild;
        private IPanel canvas;
        public Guid nodeID;
        public int X { set; get; }
        public int Y { set; get; }
        public MindmapTree(RectangleSegment rec, IPanel panel)
        {
            this.child = new List<MindmapTree>();
            this.rectChild = new List<RectangleSegment>();
            this.node = rec;
            this.canvas = panel;
            this.nodeID = rec.ID;
        }
        public void AddChild()
        {
            this.X = node.X;
            this.Y = (node.Y - 60);
             
            RectangleSegment newRect = new RectangleSegment(X, Y, 70, 50);
            
            rectChild.Add(newRect);
            this.canvas.AddDrawingObject(newRect);
            this.canvas.AddRectangleObject(newRect);
            MindmapTree mindmapTree = new MindmapTree(newRect, this.canvas);
            child.Add(mindmapTree);
            this.canvas.AddMindmapObject(mindmapTree);
            childBtnObject = new ButtonObject(X, Y, newRect.ID, this.canvas);
            this.canvas.AddButtonObject(childBtnObject);
            textSegmentChild = new TextSegment();
            textSegmentChild.Value = "Textchild";
            textSegmentChild.Position = new System.Drawing.PointF((float)((newRect.X + (newRect.X + newRect.Width))/2), (float)((newRect.Y + (newRect.Y + newRect.Height)) / 2));
            bool added = newRect.Add(textSegmentChild);
            newRect.Select();
            Debug.WriteLine("Jumlah Node : " + NumofChild());
        }
        public void traverse()
        {

            //implemented later
        }
        public int NumofChild()
        {
            return rectChild.Count;
        }
    }
}
