using LPE;


namespace YuGiOh {
    public class CompoundAction : GameAction {
        static ObjectPool<CompoundAction> _pool = new ObjectPool<CompoundAction>(() => new CompoundAction());
        public static CompoundAction Get(GameAction a1, GameAction a2) {
            var result = _pool.Get();
            result.a1 = a1;
            result.a2 = a2;
            return result;
        }

        GameAction a1;
        GameAction a2;
        public override void Execute() {
            a1.Execute();
            a2.Execute();
        }

        public override GameAction GetUndo() {
            return Get(a2.GetUndo(), a1.GetUndo());
        }

        public override void Return() {
            _pool.Return(this);
        }
    }

    public static class CompoundActionUtility {
        public static GameAction Then(this GameAction a, GameAction next) {
            return CompoundAction.Get(a, next);
        }
    }

}