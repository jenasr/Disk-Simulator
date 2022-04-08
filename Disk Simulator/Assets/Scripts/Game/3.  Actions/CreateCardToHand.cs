namespace YuGiOh {
    /// <summary>
    /// Test 
    /// TODO - better implementation
    /// </summary>
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
            c.zonePlacement = g.players[player].hand.count;
            g.players[player].hand.AddCard(c);
            c.zone = ZoneType.hand;
            c.orientation = CardOrientation.faceup; // should this be facedown
            c.owner = player;
            c.controller = player;
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
                h.RemoveAt(h.count - 1);
                
                // card does not exist
                c.c.zone = ZoneType.none;
                
                // no need to reset other values, or do we?
            }

            public override GameAction GetUndo() {
                // Should never call this
                throw new System.NotImplementedException();
            }
        }
    }

}