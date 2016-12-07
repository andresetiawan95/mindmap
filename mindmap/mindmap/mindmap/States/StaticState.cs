using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mindmap.States
{
    public class StaticState : DrawingState
    {
        private static DrawingState instance;

        public static DrawingState GetInstance()
        {
            Debug.WriteLine("masuk pada inisialiasi StaticState pada class StaticState");
            if (instance == null)
            {
                instance = new StaticState();
            }
            return instance;
        }

        public override void Draw(DrawingObject obj)
        {
            obj.RenderOnStaticView();
        }

        public override void Select(DrawingObject obj)
        {
            obj.ChangeState(EditState.GetInstance());
        }
    }
}
