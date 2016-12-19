using mindmap.Shapes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mindmap.Command;

namespace mindmap
{
    public class MindmapTree
    {
        private AddChildCommand addChild;
        public Guid nodeID;
        public List<RectangleSegment> rectChild;
        public List<MindmapTree> child;
        public RectangleSegment node;
        public ButtonObject childBtnObject;
        private TextSegment textSegmentChild;
        private RectangleSegment newRect;
        private LineSegment lineChild;
        private IPanel canvas;
        public int X { set; get; }
        public int Y { set; get; }

        private UnDoRedo _UnDoObject;

        public UnDoRedo UnDoObject
        {
            get
            {
                return _UnDoObject;
            }
            set
            {
                _UnDoObject = value;
            }
        }

        public MindmapTree(RectangleSegment rec, IPanel panel)
        {
            this.nodeID = rec.ID;
            this.child = new List<MindmapTree>();
            this.rectChild = new List<RectangleSegment>();
            this.node = rec;
            this.canvas = panel;
        }
        public void AddChild()
        {
            this.addChild = new AddChildCommand(this.canvas, new RectangleSegment(), new LineSegment(), new TextSegment()
            , new ButtonObject(), this.nodeID, this.rectChild, this.child, this.node);
            this.addChild.Execute();
            UnDoObject.InsertInUnDoRedo(this.addChild);
        }
        public void traverse()
        {

            //implemented later
        }
        
    }
}
