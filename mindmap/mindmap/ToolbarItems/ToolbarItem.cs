using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using mindmap.Command;

namespace mindmap.ToolbarItems
{
    public class ToolbarItem : ToolStripButton, IToolbarItem, ITool
    {
        private ICommand command;
        private IPanel canvas;

        public ToolbarItem(String name, System.Drawing.Image icon, IPanel canvas)
        {
            this.Name = name;
            this.ToolTipText = name;
            this.Image = icon;
            this.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.canvas = canvas;
            this.Click += ExampleToolbarItem_Click;
        }

        private void ExampleToolbarItem_Click(object sender, EventArgs e)
        {
            if (UnDoObject != null)
            {
                if (this.Name.Equals("undo"))
                {
                    UnDoObject.Undo(1);
                    this.canvas.Repaint();
                }
                else if (this.Name.Equals("redo"))
                {
                    UnDoObject.Redo(1);
                    this.canvas.Repaint();
                }
            }
        }

        public void SetCommand(ICommand command)
        {
            this.command = command;
        }


        public Cursor cursor
        {
            get { throw new NotImplementedException(); }
        }

        public IPanel TargetCanvas { get; set; }

        private UnDoRedo _UnDoObject;
        public UnDoRedo UnDoObject
        {
            get { return _UnDoObject; }
            set
            {
                _UnDoObject = value;

            }
        }

        public void ToolMouseDown(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void ToolMouseUp(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void ToolMouseMove(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }
        public void ToolMouseDoubleClick(object sender, MouseEventArgs e)
        {

        }
    }
}
