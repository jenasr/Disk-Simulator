using UnityEngine;
using System.Collections.Generic;


namespace YuGiOh {
    public class Game_MainBehaviour : MonoBehaviour {
        public Game_PlayerBehaviour player1;
        public Game_PlayerBehaviour player2;
        public Game_CardBehaviour cardSrc;

        Game g;
        Dictionary<CardEntity, Game_CardBehaviour> card2behaviour = new Dictionary<CardEntity, Game_CardBehaviour>();


        public void Init(Game g) {
            this.g = g;
            GameEvents.GetEvents(g).cardAddedToPlayersHand.OnPostExecute += CreateCard;
            GameEvents.GetEvents(g).cardModified.OnPostExecute += UpdateCard;
        }

        void CreateCard(CardEntity c) {
            var behaviour = Instantiate(cardSrc);
            behaviour.gameObject.SetActive(true);

            behaviour.SetCard(this, c);

            // TODO - set to hand position
            behaviour.transform.position = Vector3.zero;
            card2behaviour.Add(c, behaviour);
        }

        void UpdateCard(CardEntity c) {
            card2behaviour[c].UpdateBehaviour();
        }
    }
}