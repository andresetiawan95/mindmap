﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mindmap.States
{
    public class PreviewState : DrawingState
    {
        private static DrawingState instance;

        public static DrawingState GetInstance()
        {
            Debug.WriteLine("masuk pada inisialiasi PreviewState pada class PreviewState");
            if (instance == null)
            {
                instance = new PreviewState();
            }
            return instance;
        }

        public override void Draw(DrawingObject obj)
        {
            obj.RenderOnPreview();
        }

        public override void Select(DrawingObject obj)
        {
            obj.ChangeState(EditState.GetInstance());
        }
    }
}
