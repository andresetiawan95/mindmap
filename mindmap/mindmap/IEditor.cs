using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mindmap
{
    public interface IEditor
    {
        IToolbox Toolbox { get; set; }
        void AddCanvas(IPanel canvas);
        void SelectCanvas(IPanel canvas);
        IPanel GetSelectedCanvas();
        void RemoveCanvas(IPanel canvas);
        void RemoveSelectedCanvas();
    }
}
