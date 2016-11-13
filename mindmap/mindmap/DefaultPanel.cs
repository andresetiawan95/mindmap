using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mindmap
{
    public class DefaultPanel: Control, IPanel
    {
        ITool tools;

        //Constructor untuk menginisiasi Panel
        public DefaultPanel()
        {
            this.BackColor = Color.White;
            this.Dock = DockStyle.Fill;
        }
        public void Repaint()
        {
            this.Invalidate();
            this.Update();
        }
        public void SetColorBackground(Color color)
        {
            this.BackColor = color;
        }
        public void SetActiveTools (ITool tool)
        {
            this.tools = tool;
        }
    }
}
