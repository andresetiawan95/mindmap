using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace mindmap
{
    public interface IPanel
    {
        void SetColorBackground(Color color);
        void Repaint();
        void SetActiveTools(ITool itool);
    }
}
