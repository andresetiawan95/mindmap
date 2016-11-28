using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mindmap
{
    public interface IObserver
    {
        void Update(int x, int y);
    }
}
