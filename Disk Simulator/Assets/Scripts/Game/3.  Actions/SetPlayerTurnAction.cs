using LPE;

namespace YuGiOh {
    public class SetPlayerTurnAction : GameAction {
        static ObjectPool<SetPlayerTurnAction> _pool = new ObjectPool<SetPlayerTurnAction>(() => new SetPlayerTurnAction());

        public static SetPlayerTurnAction Get(Game g, int targetPlayer) {
            var result = _pool.Get();
            result.targetPlayer = targetPlayer;
            result.g = g;
            return result;
        }

        Game g;
        int targetPlayer;


        public override void Execute() {
            g.currentPlayer = targetPlayer;
        }

        public override GameAction GetUndo() {
            return Get(g, g.currentPlayer);
        }

        public override void Return() {
            _pool.Return(this);
        }

    }
}