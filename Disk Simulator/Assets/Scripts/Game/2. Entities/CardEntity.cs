namespace YuGiOh {
    public class CardEntity {
        public CardData data;
        public CardOrientation orientation = CardOrientation.faceup;
        public Zone zone = Zone.none;
        public int zonePlacement; // Where in the zone is this card.  Mighht not be needed
        
        public int owner;       // does not change 
        public int controller;  // usually same as owner, but could change

        // TODO 
        // Counters
    }
}