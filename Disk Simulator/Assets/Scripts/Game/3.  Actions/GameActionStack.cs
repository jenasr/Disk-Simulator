using System.Collections.Generic;


namespace YuGiOh {
    /// <summary>
    /// Allows for undo/redo. Could also be used to save the games history.
    /// </summary>
    public class GameActionStack {
        public bool UndoAvailable => lastAction >= 0;
        public bool RedoAvailable => lastAction < actions.Count - 1;

        List<StackEntry> actions = new List<StackEntry>();
        int lastAction = -1;

        public void Add(GameAction g) {
            lastAction++;
            if (lastAction == actions.Count) {
                // list is full
                actions.Add(new StackEntry() { redo = g, undo = g.GetUndo() });
            }
            else {
                // clear entries
                for (int i = lastAction; i < actions.Count; i++) {
                    actions[i].redo.OnRemove();
                    actions[i].undo.OnRemove();
                }

                // add action
                actions[lastAction] = new StackEntry() { redo = g, undo = g.GetUndo() };
            }
        }

        public void ExecuteUndo() {
            actions[lastAction].undo.Execute();
            lastAction--;
        }
        public void ExecuteRedo() {
            lastAction++;
            actions[lastAction].redo.Execute();
        }

        struct StackEntry {
            public GameAction redo;
            public GameAction undo;
        }
    }

}