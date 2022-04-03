namespace YuGiOh {
    public class CardEntity {
        public CardData data;
        public CardOrientation orientation = CardOrientation.faceup;
        public Zone zone = Zone.none;
        public int zonePlacement;
        
        public int owner;       // does not change 
        public int controller;  // usually same as owner, but could change

        // TODO 
        // Counters
    }
}