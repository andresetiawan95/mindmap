using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using mindmap.Command;

namespace mindmap
{
    //ITool : interface untuk setiap tools yang akan diimplementasikan
    public interface ITool
    {
        String Name { get; set; }
        Cursor cursor { get; }
        IPanel TargetCanvas { get; set; }
        UnDoRedo UnDoObject { get; set; }
        
        void ToolMouseDown(object sender, MouseEventArgs e);
        void ToolMouseUp(object sender, MouseEventArgs e);
        void ToolMouseMove(object sender, MouseEventArgs e);

    }
}
