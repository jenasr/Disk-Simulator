using System;
using System.Collections.Generic;


namespace YuGiOh {
    public class GameEvents {
        // don't want to add GameEvents to Game (different layers)
        // not static, not signleton allows for multiple instances of games
        static Dictionary<Game, GameEvents> _events = new Dictionary<Game, GameEvents>();
        public static GameEvents GetEvents(Game g) {
            if (!_events.ContainsKey(g)) {
                _events.Add(g, new GameEvents(g));
            }

            return _events[g];
        }


        private GameEvents(Game g) { }


        public CardActionEvents cardActions = new CardActionEvents();
        public PrePostExecutionEvents<CardEntity> cardAddedToPlayersHand;
        public PrePostExecutionEvents<CardEntity> cardModified;
    }

    // Wraps a dictionary
    public class CardActionEvents { 
        public PrePostExecutionEvents<CardEntity> this[CardAction c] {
            get {
                if (!_events.ContainsKey(c)) {
                    _events.Add(c, new PrePostExecutionEvents<CardEntity>());
                }

                return _events[c];
            }
        }
        Dictionary<CardAction, PrePostExecutionEvents<CardEntity>> _events = new Dictionary<CardAction, PrePostExecutionEvents<CardEntity>>();

    }

}