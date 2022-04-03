using LPE;


namespace YuGiOh {
    public abstract class ToZoneCardAction : CardAction {
        Zone zone;
        CardOrientation orientation;
        string name;
        string displayText;

        protected ToZoneCardAction(Zone zone, CardOrientation orientation, string name, string displayText) {
            this.orientation = orientation;
            this.zone = zone;
            this.name = name;
            this.displayText = displayText;
        }

        public override bool Check(Game g, CardEntity c) {
            // TODO - check Zone has space
            return 
                (c.data.validZones & zone) != 0 &&                 // valid zone
                zone != c.zone                                     // not same zone
            ;  
        }
        public override void Execute(Game g, CardEntity c) {
            Player controller = g.players[c.controller];
            // remove from current Zone
            switch (c.zone) {
                case Zone.deck:
                    break;
                case Zone.hand:
                    controller.hand.Remove(c);
                    break;
                case Zone.graveyard:
                    controller.graveyard.Remove(c);
                    break;
                case Zone.monster:
                    controller.monsterZone[c.zonePlacement] = null;
                    break;
                case Zone.spell:
                    controller.spellZone[c.zonePlacement] = null;
                    break;
                case Zone.field:
                    g.fieldZone = null;
                    break;
            }

            // update card info
            c.orientation = orientation;
            c.zone = zone;


            // add to new zone
            switch (zone) {
                case Zone.deck:
                    throw new System.NotImplementedException();
                case Zone.hand:
                    c.zonePlacement = controller.hand.Count;
                    controller.hand.Add(c);
                    break;
                case Zone.graveyard:
                    c.zonePlacement = controller.graveyard.Count;
                    controller.graveyard.Add(c);
                    break;
                case Zone.monster:
                    for (int i = 0; i < controller.monsterZone.Length; i++) {
                        if (controller.monsterZone[i] == null) {
                            c.zonePlacement = i;
                            controller.monsterZone[i] = c;
                            break;
                        }
                    }
                    break;
                case Zone.spell:
                    for (int i = 0; i < controller.spellZone.Length; i++) {
                        if (controller.spellZone[i] == null) {
                            c.zonePlacement = i;
                            controller.spellZone[i] = c;
                            break;
                        }
                    }
                    break;
                case Zone.field:
                    c.zonePlacement = 0;
                    g.fieldZone = c;
                    break;
            }

        }
        public override CardAction GetUndo(Game g, CardEntity c) {
            return ToZoneAndPosition.Get(c.zone, c.orientation, c.zonePlacement);
        }

        public override string Name(Game g, CardEntity c) => name;
        public override string DisplayText(Game g, CardEntity c) => displayText;

        public override GameEvents PreExecuteEvents(Game g, CardEntity c) {
            var result = base.PreExecuteEvents(g, c);
            result.zoneEvents[c.zone].OnLeave.InvokePre(c);
            result.zoneEvents[zone].OnEnter.InvokePre(c);
            return result;
        }
        public override GameEvents PostExecuteEvents(Game g, CardEntity c) {
            var result = base.PostExecuteEvents(g, c);
            result.zoneEvents[c.zone].OnLeave.InvokePost(c);
            result.zoneEvents[zone].OnEnter.InvokePost(c);
            return result;
        }


        //**********************************************************************************************************************
        // Class for Undo
        //**********************************************************************************************************************

        class ToZoneAndPosition : ToZoneCardAction {
            static ObjectPool<ToZoneAndPosition> _pool = new ObjectPool<ToZoneAndPosition>(() => new ToZoneAndPosition());
            public static ToZoneAndPosition Get(Zone zone, CardOrientation ori, int position) {
                var result = _pool.Get();
                result.zone = zone;
                result.orientation = ori;
                result.position = position;
                return result;
            }

            int position;

            private ToZoneAndPosition() : base(0, 0, "", "") { }

            public override void Execute(Game g, CardEntity c) {
                Player controller = g.players[c.controller];
                // remove from current Zone
                switch (c.zone) {
                    case Zone.deck:
                        break;
                    case Zone.hand:
                        controller.hand.Remove(c);
                        break;
                    case Zone.graveyard:
                        controller.graveyard.Remove(c);
                        break;
                    case Zone.monster:
                        controller.monsterZone[c.zonePlacement] = null;
                        break;
                    case Zone.spell:
                        controller.spellZone[c.zonePlacement] = null;
                        break;
                    case Zone.field:
                        g.fieldZone = null;
                        break;
                }

                // update card info
                c.orientation = orientation;
                c.zone = zone;
                c.zonePlacement = position;


                // add to new zone
                switch (zone) {
                    case Zone.deck:
                        throw new System.NotImplementedException();
                    case Zone.hand:
                        controller.hand.Insert(position, c);
                        break;
                    case Zone.graveyard:
                        controller.graveyard.Insert(position, c);
                        break;
                    case Zone.monster:
                        controller.monsterZone[position] = c;
                        break;
                    case Zone.spell:
                        controller.spellZone[position] = c;
                        break;
                    case Zone.field:
                        g.fieldZone = c;
                        break;
                }

            }

            public override void OnRemove() {
                _pool.Return(this);
            }
        }
    }

    //**********************************************************************************************************************
    // Implementations
    //**********************************************************************************************************************


    public class ToDeckCardAction : ToZoneCardAction {
        public ToDeckCardAction() : base(Zone.deck, CardOrientation.facedown, "To Deck", "Send Card to Deck") { }
    }
    public class ToFieldCardAction : ToZoneCardAction {
        public ToFieldCardAction() : base(Zone.field, CardOrientation.faceup, "To Field", "Send Card to Field Zone") { }
    }
    public class ToGraveyardCardAction : ToZoneCardAction {
        public ToGraveyardCardAction() : base(Zone.graveyard, CardOrientation.faceup, "To Graveyard", "Send Card to Graveyard") { }
    }
    public class ToHandCardAction : ToZoneCardAction {
        public ToHandCardAction() : base(Zone.hand, CardOrientation.facedown, "To Hand", "Send Card to Hand") { }
    }

    // monster zones
    public class ToMonsterUpCardAction : ToZoneCardAction {
        public ToMonsterUpCardAction() 
            : base(Zone.monster, CardOrientation.faceup, "To Monster Zone Up", "Send Card to Monster Zone Attack") { }
    }
    public class ToMonsterSidewaysCardAction : ToZoneCardAction {
        public ToMonsterSidewaysCardAction()
            : base(Zone.monster, CardOrientation.faceupideways, "To Monster Zone Up Defense", "Send Card to Monster Zone Defense") { }
    }
    public class ToMonsterDownCardAction : ToZoneCardAction {
        public ToMonsterDownCardAction() 
            : base(Zone.monster, CardOrientation.facedownsideways, "To Monster Zone Up Set", "Send Card to Monster Zone Set") { }
    }

    // spell zones
    public class ToSpellUpCardAction : ToZoneCardAction {
        public ToSpellUpCardAction()
            : base(Zone.spell, CardOrientation.faceup, "To Spell Zone Up", "Send Card to Spell Zone Faceup") { }
    }
    public class ToSpellDownCardAction : ToZoneCardAction {
        public ToSpellDownCardAction()
            : base(Zone.spell, CardOrientation.facedown, "To Spell Zone Up", "Send Card to Spell Zone Facedwon") { }
    }
}