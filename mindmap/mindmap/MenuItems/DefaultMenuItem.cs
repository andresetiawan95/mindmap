using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mindmap.MenuItems
{
    public class DefaultMenuItem : ToolStripMenuItem, IMenuItem
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
