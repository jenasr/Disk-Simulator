using System;

namespace YuGiOh {
    public class MonsterSpellZone : Zone {

        public override CardEntity this[int i] => _cards[i];

        public override int count => _count;

        public override int capacity => 5;
        public MonsterSpellZone(ZoneType t) {
            zt = t;
        }

        CardEntity[] _cards = new CardEntity[5];
        int _count = 0;
        ZoneType zt;

        public override void AddCard(CardEntity c, int pos = -1) {
            // if i == -1, look for first available slot
            if (pos < 0) {
                for (int i = 0; i < 5; i++) {
                    if (_cards[i] == null) {
                        pos = i;
                        break;
                    }
                }

                // error
                if (pos == -1) {
                    throw new InvalidOperationException("Cannot add card to MonsterSpellZone - All slots taken");
                }
            }

            // add card 
            _cards[pos] = c;
            _count++;
            c.zone = zt;
            c.zonePlacement = pos;
        }

        public override void RemoveAt(int i) {
            if (_cards[i] != null) {
                _count--;
            }
            _cards[i] = null;
        }
    }
}