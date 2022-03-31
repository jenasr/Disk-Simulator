namespace YuGiOh {
    public class CreateCardToHand : GameAction {
        Game g;
        int player; 
        CardEntity c;
        
        public CreateCardToHand(Game g, int player, CardEntity c) {
            this.g = g;
            this.player = player;
            this.c = c;
        }

        public override void Execute() {
            GameEvents.GetEvents(g).cardAddedToPlayersHand.InvokePre(c);
            g.players[player].hand.Add(c);
            c.zone = Zone.hand;
            c.orientation = CardOrientation.faceup; // should this be facedown
            c.owner = player;
            c.controller = player;
            GameEvents.GetEvents(g).cardAddedToPlayersHand.InvokePost(c);
        }

        public override GameAction GetUndo() {
            return new _Undo(this);
        }



        class _Undo : GameAction {
            CreateCardToHand c;

            public _Undo(CreateCardToHand c) {
                this.c = c;
            }

            public override void Execute() {
                // remove from hand
                var h = c.g.players[c.player].hand;
                h.RemoveAt(h.Count - 1);
                
                // card does not exist
                c.c.zone = Zone.none;
                
                // no need to set other values
            }

            public override GameAction GetUndo() {
                // Should never call this
                throw new System.NotImplementedException();
            }
        }
    }

}