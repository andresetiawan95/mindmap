using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mindmap
{
    public class ButtonObject
    {
        public Button button;
        private IPanel canvas;
        private Size size;
        public string namaButton;
        public int X { set; get; }
        public int Y { set; get; }
        public Guid btnID { get; set; }
        public ButtonObject()
        {
            this.size = new Size(20, 20);
        }
        public ButtonObject(int x, int y, Guid id, IPanel panel)
        {
            this.size = new Size(20, 20);
            this.btnID = id;
            this.X = x;
            this.Y = y;
            this.canvas = panel;
        }
        public void setButton(int x, int y, Guid id, IPanel panel)
        {
            this.btnID = id;
            this.X = x;
            this.Y = y;
            this.canvas = panel;
        }
        public Button getButton()
        {
            return button;
        }
        public Button InitiateButton()
        {
            button = new Button();
            button.Text = "+";
            button.Location = new Point(X, Y);
            button.Size = size;
            return button;
        }
        public void Selected()
        {
            //implemented later
        }
        public void Translate(int x, int y, int xAmount, int yAmount)
        {
            this.X += xAmount;
            this.Y += yAmount;
            this.canvas.MoveButton(this.X, this.Y);
        }
    }
}
