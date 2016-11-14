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
        public string namaButton;
        public int X { set; get; }
        public int Y { set; get; }
        public Guid btnID { get; set; }
        public ButtonObject(int x, int y, Guid id)
        {
            this.btnID = id;
            this.X = x;
            this.Y = y;
        }
        public Button getButton()
        {
            return button;
        }
        public Button InitiateButton()
        {
            button = new Button();
            button.Text = "Add";
            button.Location = new Point(X, Y);
            return button;
        }
        public void Selected()
        {
            MessageBox.Show("Tombol dengan ID " + btnID + " telah ditekan.");
        }
    }
}
