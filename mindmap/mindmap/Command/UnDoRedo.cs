using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace mindmap.Command
{
    public class UnDoRedo
    {

        public Stack<ICommand> _Undocommands = new Stack<ICommand>();
        public Stack<ICommand> _Redocommands = new Stack<ICommand>();

        public event EventHandler EnableDisableUndoRedoFeature;


        private DefaultPanel _Container;

        public DefaultPanel Container
        {
            get { return _Container; }
            set { _Container = value; }
        }

        public void Redo(int levels)
        {
            Debug.WriteLine("Stack redocommand di Redo count: " + _Redocommands.Count);
            for (int i = 1; i <= levels; i++)
            {
                if (_Redocommands.Count != 0)
                {
                    ICommand command = _Redocommands.Pop();
                    command.Execute();
                    _Undocommands.Push(command);
                }

            }
            if (EnableDisableUndoRedoFeature != null)
            {
                EnableDisableUndoRedoFeature(null, null);
            }
        }

        public void Undo(int levels)
        {
            for (int i = 1; i <= levels; i++)
            {
                if (_Undocommands.Count != 0)
                {
                    ICommand command = _Undocommands.Pop();
                    command.Unexecute();
                    _Redocommands.Push(command);
                }

            }
            if (EnableDisableUndoRedoFeature != null)
            {
                EnableDisableUndoRedoFeature(null, null);
            }
            Debug.WriteLine("Stack undocommand di Undo count: " + _Undocommands.Count);
            Debug.WriteLine("Stack redocommand di Undo count: " + _Redocommands.Count);
        }

        #region UndoHelperFunctions

        /*public void InsertInUnDoRedoForInsert(FrameworkElement ApbOrDevice)
        {
            ICommand cmd = new InsertCommand(ApbOrDevice, Container);
            _Undocommands.Push(cmd); _Redocommands.Clear();
            if (EnableDisableUndoRedoFeature != null)
            {
                EnableDisableUndoRedoFeature(null, null);
            }
        }

        public void InsertInUnDoRedoForDelete(FrameworkElement ApbOrDevice)
        {
            ICommand cmd = new DeleteCommand(ApbOrDevice, Container);
            _Undocommands.Push(cmd); _Redocommands.Clear();
            if (EnableDisableUndoRedoFeature != null)
            {
                EnableDisableUndoRedoFeature(null, null);
            }
        }*/

        public void InsertInUnDoRedo(ICommand command)
        {
            _Undocommands.Push(command); _Redocommands.Clear();
            if (EnableDisableUndoRedoFeature != null)
            {
                EnableDisableUndoRedoFeature(null, null);
            }
            Debug.WriteLine("Stack undocommand count: "+_Undocommands.Count);
        }

        /*public void InsertInUnDoRedoForResize(Point margin, double width, double height, FrameworkElement UIelement)
        {
            ICommand cmd = new ResizeCommand(new Thickness(margin.X, margin.Y, 0, 0), width, height, UIelement);
            _Undocommands.Push(cmd); _Redocommands.Clear();
            if (EnableDisableUndoRedoFeature != null)
            {
                EnableDisableUndoRedoFeature(null, null);
            }
        }*/

        #endregion

        public bool IsUndoPossible()
        {
            if (_Undocommands.Count != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsRedoPossible()
        {

            if (_Redocommands.Count != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
