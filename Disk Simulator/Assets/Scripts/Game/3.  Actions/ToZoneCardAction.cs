using LPE;


namespace YuGiOh {
    public class ToZoneCardAction : GameAction {
        static ObjectPool<ToZoneCardAction> _pool = new ObjectPool<ToZoneCardAction>(() => new ToZoneCardAction());

        public static ToZoneCardAction Get(Game g, CardEntity c, ZoneType z, int p = -1) {
            var result = _pool.Get();
            result.g = g;
            result.targetEntity = c;
            result.targetZone = z;
            result.targetPosition = p;
            return result;
        }

        Game g;
        CardEntity targetEntity;
        ZoneType targetZone;
        int targetPosition;

        public override void Execute() {
            // get zones
            Zone oldZone = null;
            Zone newZone = null;

            switch (targetEntity.zone) {
                case ZoneType.deck:
                    break;
                case ZoneType.hand:
                    oldZone = g.players[targetEntity.controller].hand;
                    break;
                case ZoneType.graveyard:
                    oldZone = g.players[targetEntity.controller].graveyard;
                    break;
                case ZoneType.monster:
                    oldZone = g.players[targetEntity.controller].monsterZone;
                    break;
                case ZoneType.spell:
                    oldZone = g.players[targetEntity.controller].spellZone;
                    break;
                case ZoneType.field:
                    break;
            }
            switch (targetZone) {
                case ZoneType.deck:
                    break;
                case ZoneType.hand:
                    newZone = g.players[targetEntity.controller].hand;
                    break;
                case ZoneType.graveyard:
                    newZone = g.players[targetEntity.controller].graveyard;
                    break;
                case ZoneType.monster:
                    newZone = g.players[targetEntity.controller].monsterZone;
                    break;
                case ZoneType.spell:
                    newZone = g.players[targetEntity.controller].spellZone;
                    break;
                case ZoneType.field:
                    break;
            }

            // update
            oldZone?.RemoveAt(targetEntity.zonePlacement);
            newZone?.AddCard(targetEntity, targetPosition);
        }

        public override GameAction GetUndo() {
            return Get(g, targetEntity, targetEntity.zone, targetEntity.zonePlacement);
        }

        public override void Return() {
            _pool.Return(this);
        }
    }

}