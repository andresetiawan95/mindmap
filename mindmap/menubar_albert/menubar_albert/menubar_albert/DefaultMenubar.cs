using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace menubar_albert
{
    public class DefaultMenubar : MenuStrip,IMenubar
    {
        public DefaultMenubar()
        {
            this.Location = new Point(0, 0);
            this.Name = "menu";
            this.Size = new Size(624, 24);
            this.TabIndex = 0;
            this.Text = "menu";
        }
       public void AddMenuItem(IMenuItem menuitem)
            {
                this.Items.Add((ToolStripMenuItem)menuitem);
            }
  }
}
