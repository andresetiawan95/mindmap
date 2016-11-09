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
        String name { get; set; }
        ITool GetActiveTool();
        void SetBackgroundColor(Color color);
        void Repaint();
        void SetActiveTools(ITool itool);

        void AddDrawingObject(DrawingObject drawingObject);
        void RemoveDrawingObject(DrawingObject drawingObject);

        DrawingObject GetObjectAt(int x, int y);
        DrawingObject SelectObjectAt(int x, int y);
        void DeselectAllObjects();
    }
}
