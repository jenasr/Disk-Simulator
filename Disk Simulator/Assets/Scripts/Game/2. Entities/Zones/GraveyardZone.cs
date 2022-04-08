using System.Collections.Generic;

namespace YuGiOh {
    public class GraveyardZone : Zone {
        public override CardEntity this[int i] {
            get {
                // start from back
                return _cards[count - 1 - i];
            }
        }


        public override int count => _cards.Count;

        public override int capacity => 10000;


        // top of graveyard is last entry in list
        // this will behave similarly to a stack
        // first card (this[0]) refers to last card added
        List<CardEntity> _cards = new List<CardEntity>();


        public override void AddCard(CardEntity c, int pos = -1) {
            c.zone = ZoneType.graveyard;
            if (pos == -1) {
                // top
                _cards.Add(c);
            }
            else {
                // insert
                int insertPos = count - pos;
                _cards.Insert(insertPos, c); // slow, but should not be too common
            }

            // update placements of all cards 
            for (int i = 0 ; i < count; i++) {
                _cards[count - i - 1].zonePlacement = i;
            }
        }

        public override void RemoveAt(int pos) {
            int removePos = count - pos - 1;

            _cards.RemoveAt(removePos);

            // update placements of all cards below position
            for (int i = 0; i < removePos; i++) {
                _cards[count - i - 1].zonePlacement = i;
            }
        }
    }
}