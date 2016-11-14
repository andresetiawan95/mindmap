using mindmap.Shapes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mindmap
{
    class MindmapTree
    {
        public List<RectangleSegment> rectChild;
        public RectangleSegment node;
        private IPanel canvas;
        public int X { set; get; }
        public int Y { set; get; }
        public MindmapTree(RectangleSegment rec, IPanel panel)
        {
            this.rectChild = new List<RectangleSegment>();
            this.node = rec;
            this.canvas = panel;
        }
        public void AddChild()
        {
            this.X = (node.X - 20);
            this.Y = (node.Y - 20);
             
            RectangleSegment newRect = new RectangleSegment(X, Y, 60, 30);
            rectChild.Add(newRect);
            this.canvas.AddDrawingObject(newRect);
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
