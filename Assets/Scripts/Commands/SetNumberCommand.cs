using UnityEngine;

namespace Commands
{
    public class SetNumberCommand : Command
    {
        private Cell cell;
        private int newValue;
        private int oldValue;
        public SetNumberCommand(Cell cell, int number)
        {
            this.cell = cell;
            oldValue = newValue;
            newValue = number;
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
