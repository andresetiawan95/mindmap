using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using mindmap.Shapes;
using mindmap.Command;

namespace mindmap
{
    public interface IPanel
    {
        String Name { get; set; }
        ITool GetActiveTool();
        void SetBackgroundColor(Color color);
        void Repaint();
        void SetActiveTool(ITool itool);

        void AddDrawingObject(DrawingObject drawingObject);
        void RemoveDrawingObject(DrawingObject drawingObject);
        void AddRectangleObject(RectangleSegment rect);
        void AddMindmapObject(MindmapTree mindmap);
        int sumDrawingObjects();
        DrawingObject GetObjectAt(int x, int y);
        DrawingObject SelectObjectAt(int x, int y);
        DrawingObject getFromListObject(int index);
        RectangleSegment GetRectangleObjectAt(int x, int y);
        ButtonObject SelectButtonObjectByID(Guid id);
        UnDoRedo UnDoObject { get; set; }
        void MoveButton(int x, int y);
        void DeselectAllObjects();
        void AddButtonObject(ButtonObject buttonObject);
        void GetButton(Guid id);
        void RemoveButtonObject(ButtonObject buttonObject);
    }
}
