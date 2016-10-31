using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace menubar_albert.MenuItems
{
    public partial class DefaultMenuItem : ToolStripMenuItem,IMenuItem
    {
        public DefaultMenuItem()
        {
            this.Name = "exampleToolStripMenuItem";
            this.Size = new System.Drawing.Size(37, 20);
        }

        public DefaultMenuItem(string text) : base()
        {
            this.Text = text;

        }
        public void AddMenuItem(IMenuItem menuItem)
        {
            this.DropDownItems.Add((ToolStripMenuItem)menuItem);
        }
    }
}
