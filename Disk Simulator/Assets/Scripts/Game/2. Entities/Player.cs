using LPE;
using System.Collections;

namespace YuGiOh {
    public class Player {
        public int lifePoints;
        public int playerNum;

        // cards in play - TODO create a wrapper for each of these, hide underlying collection, include methods for manipulation
        public MonsterSpellZone monsterZone = new MonsterSpellZone(ZoneType.monster);
        public MonsterSpellZone spellZone = new MonsterSpellZone(ZoneType.spell);
        public GraveyardZone graveyard = new GraveyardZone();
        public HandZone hand = new HandZone();
        // no need to track decks

        public Zone GetZone(ZoneType type) {
            switch (type) {
                case ZoneType.hand:
                    return hand;
                case ZoneType.graveyard:
                    return graveyard;
                case ZoneType.monster:
                    return monsterZone;
                case ZoneType.spell:
                    return spellZone;
                default:
                    return null;
            }
        }
    }
}