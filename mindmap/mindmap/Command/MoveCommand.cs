using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace mindmap.Command
{
    public class MoveCommand : ICommand
    {
        public DrawingObject selectedObject { get; set; }
        public ButtonObject selectedBtnObject { get; set; }
        public int xInitial { get; set; }
        public int yInitial { get; set; }
        public int xAmount { get; set; }
        public int yAmount { get; set; }
        public int xInit { get; set; }
        public int yInit { get; set; }
        public MouseEventArgs e { get; set; }

        public MoveCommand()
        {

        }

        public void Execute()
        {
            if (selectedObject != null && selectedBtnObject != null)
            {
                selectedObject.Translate(e.X, e.Y, xInitial - xInit, yInitial - yInit);
                selectedBtnObject.Translate(e.X, e.Y, xInitial - xInit, yInitial - yInit);
                Debug.WriteLine("e.X: " + e.X + " e.Y: " + e.Y + " xAmount: " + xAmount + " yAmmount: " + yAmount);
            }
        }
        
        public void move()
        {
            if (selectedObject != null && selectedBtnObject != null)
            {
                selectedObject.Translate(e.X, e.Y, xAmount, yAmount);
                selectedBtnObject.Translate(e.X, e.Y, xAmount, yAmount);
                Debug.WriteLine("e.X: " + e.X + " e.Y: " + e.Y + " xAmount: " + xAmount + " yAmmount: " + yAmount);
            }
        }

        public void Unexecute()
        {
            if (selectedObject != null && selectedBtnObject != null)
            {
                selectedObject.Translate(xInit, yInit, xInit - xInitial, yInit - yInitial);
                selectedBtnObject.Translate(xInit, yInit, xInit - xInitial, yInit - yInitial);
                Debug.WriteLine("xInitial: " + xInitial + " yInitial: " + yInitial + " deltaX: " + xInit + " deltaY: " + yInit);
            }
        }
    }
}
