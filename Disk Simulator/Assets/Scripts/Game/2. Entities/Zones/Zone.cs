namespace YuGiOh {
    public abstract class Zone {
        public abstract CardEntity this[int i] { get; }
        public abstract int count { get; }
        public abstract int capacity { get; }


        /// <summary>
        /// Add Card to specific slot. Also sets the zone and position of all affected cards
        /// </summary>
        /// <param name="c">What card to add</param>
        /// <param name="i">Position in Zone. -1 for first available slot</param>
        public abstract void AddCard(CardEntity c, int i = -1);

   
        public abstract void RemoveAt(int i);

    }
}