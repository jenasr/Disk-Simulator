using UnityEngine;


namespace YuGiOh {
    [System.Serializable]
    public class CardData {
        public int id;
        public string name;
        public Zone validZones = Zone.hand | Zone.deck | Zone.graveyard;
        public CardType cardType;

        // monster only
        public MonsterType monsterType;
        public Attribute attribute;
        public int level;
        public int atk;
        public int def;

        public string ToJson() {
            return JsonUtility.ToJson(this, true);
        }

        public Sprite GetSprite() {
            return CardSpriteLoader.Get(id);
        }

        public static CardData FromJson(string src) {
            return JsonUtility.FromJson<CardData>(src);
        }
    }
}