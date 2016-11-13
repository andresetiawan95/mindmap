using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mindmap.States
{
    public class EditState : DrawingState
    {
        private static DrawingState instance;

        public static DrawingState GetInstance()
        {
            Debug.WriteLine("masuk pada inisialiasi EditState pada class EditState");
            if (instance == null)
            {
                instance = new EditState();
            }
            return instance;
        }

        public override void Draw(DrawingObject obj)
        {
            obj.RenderOnEditingView();
        }

        public override void Deselect(DrawingObject obj)
        {
            obj.ChangeState(StaticState.GetInstance());
        }
    }
}
