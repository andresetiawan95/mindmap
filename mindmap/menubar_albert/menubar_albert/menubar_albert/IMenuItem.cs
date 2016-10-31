using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace menubar_albert
{
    public interface IMenuItem
    {
        string Text { get; set; }
        void AddMenuItem(IMenuItem menuitem);
    }
}
