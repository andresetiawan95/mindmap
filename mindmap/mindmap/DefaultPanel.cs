using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using mindmap.Shapes;
using mindmap.Command;

namespace mindmap
{
    public class DefaultPanel: Control, IPanel
    {
        private ITool tools;
        private List<DrawingObject> drawingObjects;
        private List<ButtonObject> buttonObjects;
        private List<RectangleSegment> rectangleObjects;
        private List<MindmapTree> treeList;
        private Button button;
        private ButtonObject btnObject;
        private MindmapTree mindmapTree;
        //Constructor untuk menginisiasi Panel
        public DefaultPanel()
        {
            this.drawingObjects = new List<DrawingObject>();
            this.buttonObjects = new List<ButtonObject>();
            this.rectangleObjects = new List<RectangleSegment>();
            this.treeList = new List<MindmapTree>();
            this.DoubleBuffered = true;
            this.BackColor = Color.White;
            this.Dock = DockStyle.Fill;
            this.Paint += DefaultCanvas_Paint;
            this.MouseDown += DefaultCanvas_MouseDown;
            this.MouseUp += DefaultCanvas_MouseUp;
            this.MouseMove += DefaultCanvas_MouseMove;
        }

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

        private void Button_Click(object sender, EventArgs e)
        {
            RectangleSegment node = findRectangleByGuid(btnObject.btnID);
            if (node!=null)Debug.WriteLine("rectangle ID = " + node.ID);
            mindmapTree = findNodeTree(node.ID);
            if (mindmapTree == null)
            {
                mindmapTree = new MindmapTree(node, this.tools.TargetCanvas);
                AddMindmapObject(mindmapTree);
            }
            mindmapTree.UnDoObject = _UnDoObject;
            mindmapTree.AddChild();
            Repaint();
            //MessageBox.Show("Tombol dengan ID "+btnObject.btnID+" telah diklik.");

        }
        private RectangleSegment findRectangleByGuid(Guid id)
        {
            foreach (RectangleSegment drwObj in rectangleObjects)
            {
                if (drwObj.ID == id) return drwObj;
            }
            return null;
        }
        private MindmapTree findNodeTree(Guid id)
        {
            foreach (MindmapTree tree in treeList)
            {
                if (tree.nodeID == id)
                {
                    return tree;
                }
            }
            return null;
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
                //Debug.WriteLine("Tool Mouse Move dijalankan dari class DefaultPanel.cs...");
                this.tools.ToolMouseMove(sender, e);
                this.Repaint();
            }
        }

        private void DefaultCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.tools != null)
            {
               // Debug.WriteLine("Tool Mouse Up dijalankan dari class DefaultPanel.cs...");
                this.tools.ToolMouseUp(sender, e);
                this.Repaint();
            }
        }

        private void DefaultCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.tools != null)
            {
                //Debug.WriteLine("Tool Mouse Down dijalankan dari class DefaultPanel.cs...");
                this.tools.ToolMouseDown(sender, e);
               // Debug.WriteLine("Perintah repaint akan dijalankan (method DefaultCanvas_MouseDown");
                this.Repaint();
            }
        }

        private void DefaultCanvas_Paint(object sender, PaintEventArgs e)
        {
            foreach (DrawingObject obj in drawingObjects)
            {
               // Debug.WriteLine("Masuk ke Method DefaultCanvas_Paint");
                obj.SetGraphics(e.Graphics);
                obj.Draw();
            }
        }
        public void AddDrawingObject(DrawingObject drawingObject)
        {
            this.drawingObjects.Add(drawingObject);
        }
        public void AddRectangleObject(RectangleSegment rect)
        {
            this.rectangleObjects.Add(rect);
        }
        public void AddMindmapObject(MindmapTree mindmap)
        {
            this.treeList.Add(mindmap);
        }
        public void AddButtonObject(ButtonObject buttonObject)
        {
            Button btn = buttonObject.InitiateButton();
            this.buttonObjects.Add(buttonObject);
            this.Controls.Add(btn);
            
            this.button = btn;

            this.button.Click += Button_Click;
        }
        public void RemoveButtonObject(ButtonObject buttonObject)
        {
            this.Controls.Remove(buttonObject.getButton());
        }
        public void RemoveDrawingObject(DrawingObject drawingObject)
        {
            this.drawingObjects.Remove(drawingObject);
        }
        public int sumDrawingObjects()
        {
            return drawingObjects.Count;
        }
        public DrawingObject getFromListObject(int index)
        {
            return drawingObjects[index];
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

        public RectangleSegment GetRectangleObjectAt(int x, int y)
        {
            foreach (RectangleSegment rect in rectangleObjects)
            {
                if (rect.Intersect(x, y))
                {
                    return rect;
                }
            }
            return null;
        }

        public void DeselectAllObjects()
        {
            foreach (DrawingObject drawObj in drawingObjects)
            {
                drawObj.Deselect();
            }
        }
        public void GetButton(Guid id)
        {
            foreach (ButtonObject btnObj in buttonObjects)
            {
                if (btnObj.btnID == id)
                {
                    btnObject = btnObj;
                    this.button = btnObj.getButton();
                }
            }
        }
        public ButtonObject SelectButtonObjectByID(Guid id)
        {
            foreach (ButtonObject btnObj in buttonObjects)
            {
                if (btnObj.btnID == id)
                {
                    return btnObj;
                }
            }
            return null;
        }
        public void MoveButton(int x, int y)
        {
            this.button.Left = x;
            this.button.Top = y;
        }
    }
}
