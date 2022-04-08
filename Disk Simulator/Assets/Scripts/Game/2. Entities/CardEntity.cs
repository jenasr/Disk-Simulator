namespace YuGiOh {
    public class CardEntity {
        public CardData data;
        public CardOrientation orientation = CardOrientation.faceup;
        public ZoneType zone = ZoneType.none;
        public int zonePlacement;
        
        public int owner;       // does not change 
        public int controller;  // usually same as owner, but could change

        // TODO 
        // Counters
    }
}