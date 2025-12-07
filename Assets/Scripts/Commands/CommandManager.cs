using System.Collections.Generic;
using UnityEngine;

namespace Commands
{
    public class CommandManager : MonoBehaviour
    {
        public static CommandManager Instance { get; private set; }

        private Stack<Command> undoStack = new();
        private Stack<Command> redoStack = new();

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void Execute(Command command)
        {
            command.Execute();
            undoStack.Push(command);
            redoStack.Clear();
        }

        public void Undo()
        {
            if (undoStack.Count <= 0) return;
            Command cmd = undoStack.Pop();
            cmd.Undo();
            redoStack.Push(cmd);
        }

        public void Redo()
        {
            if (redoStack.Count <= 0) return;
            Command cmd = redoStack.Pop();
            cmd.Redo();
            undoStack.Push(cmd);
        }
    }
}
