using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using mindmap.Command;

namespace mindmap.Tools
{
    public class SelectionTool : ToolStripButton, ITool
    {
        private IPanel canvas;
        /*private DrawingObject selectedObject;
        private ButtonObject selectedBtnObject;
        private int xInitial;
        private int yInitial;*/
        private MoveCommand move;

        public Cursor cursor
        {
            get
            {
                return Cursors.Arrow;
            }
        }

        public IPanel TargetCanvas
        {
            get
            {
                return this.canvas;
            }

            set
            {
                this.canvas = value;
            }
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

        public SelectionTool()
        {
            this.Name = "Selection tool";
            this.ToolTipText = "Selection tool";
            this.Image = IconSet.cursor;
            this.CheckOnClick = true;
        }

        public void ToolMouseDown(object sender, MouseEventArgs e)
        {
            /*this.xInitial = e.X;
            this.yInitial = e.Y;*/
            this.move = new MoveCommand();
            this.move.xInitial = e.X;
            this.move.yInitial = e.Y;
            this.move.xInit = e.X;
            this.move.yInit = e.Y;

            if (e.Button == MouseButtons.Left && canvas != null)
            {
                canvas.DeselectAllObjects();
                Debug.WriteLine("Memanggil method SelectObjectAt pada Panel(DefaultPanel) melalui SelectionTool.cs..");
                //selectedObject = canvas.SelectObjectAt(e.X, e.Y);
                this.move.selectedObject = canvas.SelectObjectAt(e.X, e.Y);
                if (this.move.selectedObject != null)
                {
                    canvas.GetButton(this.move.selectedObject.ID);
                    //selectedBtnObject = canvas.SelectButtonObjectByID(selectedObject.ID);
                    this.move.selectedBtnObject = canvas.SelectButtonObjectByID(this.move.selectedObject.ID);
                }
                Debug.WriteLine("Sudah selesai menjalankan method SelectObjectAt pada Panel(DefaultPanel) melalui SelectionTool.cs..");
            }
        }

        public void ToolMouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && canvas != null)
            {
                if (this.move.selectedObject != null)
                {
                    this.move.xAmount = e.X - this.move.xInitial;
                    //int xAmount = e.X - xInitial;
                    this.move.yAmount = e.Y - this.move.yInitial;
                    //int yAmount = e.Y - yInitial;
                    this.move.xInitial = e.X;
                    this.move.yInitial = e.Y;

                    this.move.e = e;

                    this.move.move();
                }
            }
        }

        public void ToolMouseUp(object sender, MouseEventArgs e)
        {
            if (this.move.selectedObject != null)
            {
                UnDoObject.InsertInUnDoRedoForMove(move);
            }
        }
    }
}
