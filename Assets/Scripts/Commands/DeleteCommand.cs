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
            cell.SetValue(newValue);
        }

        public override void Undo()
        {
            cell.SetValue(oldValue);
        }
    }
}
