using System.Collections.Generic;

namespace YuGiOh {
    public class Player {
        public int lifePoints;
        public int playerNum;

        // cards in play - TODO create a wrapper for each of these, hide underlying collection, include methods for manipulation
        public CardEntity[] monsterZone = new CardEntity[Constants.NUM_MONSTER_ZONE];
        public CardEntity[] spellZone = new CardEntity[Constants.NUM_SPELL_ZONE];
        public List<CardEntity> graveyard = new List<CardEntity>();
        public List<CardEntity> hand = new List<CardEntity>();

        // no need to track decks
    }
}