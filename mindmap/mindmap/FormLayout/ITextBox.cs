using System;
using System.Collections.Generic;
using System.Linq;
using mindmap.Shapes;
using System.Text;
using System.Threading.Tasks;

namespace mindmap.FormLayout
{
    public interface ITextBox
    {
        IPanel getCanvas { set; get; }
        TextSegment getText { set; get; }

        void SetText(String text);
        void SetInitText();
        void UpdateText();
    }
}
