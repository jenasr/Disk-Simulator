using System.Collections.Generic;

namespace YuGiOh {
    public class HandZone : Zone {
        public override CardEntity this[int i] {
            get {
                return _cards[i];
            }
        }


        public override int count => _cards.Count;

        public override int capacity => 10000;

        List<CardEntity> _cards = new List<CardEntity>();


        public override void AddCard(CardEntity c, int pos = -1) {
            c.zone = ZoneType.hand;

            if (pos == -1) {
                c.zonePlacement = count;
                _cards.Add(c);
            }
            else {
                _cards.Insert(pos, c);

                // update positions of all cards in front of new card
                for (int i = pos; i < count; i++) {
                    _cards[i].zonePlacement = i;
                }
            }

        }

        public override void RemoveAt(int pos) {
            _cards.RemoveAt(pos);
            // update placements of all cards above this card
            for (int i = pos; i < count; i++) {
                _cards[i].zonePlacement--;
            }
        }

    }
}