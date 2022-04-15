namespace YuGiOh {
    public abstract class GameAction {
        public abstract void Execute();
        public abstract GameAction GetUndo();

        /// <summary>
        /// No longer part of the GameActionStack
        /// </summary>
        public virtual void Return() { }
    }

}