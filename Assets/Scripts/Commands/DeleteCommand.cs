using UnityEngine;

namespace Commands
{
    public class DeleteCommand : Command
    {
        private Cell cell;
        private int newValue;
        private int oldValue;
        public DeleteCommand(Cell cell)
        {
            this.cell = cell;
            this.newValue = 0;
        }

        public override void Execute()
        {
            cell.SetValue(newValue, notify: false);
        }

        public override void Undo()
        {
            cell.SetValue(oldValue, countAsMistake: false);
        }

        public override void Redo()
        {
            cell.SetValue(newValue, notify: false, countAsMistake: false);
        }
    }
}
