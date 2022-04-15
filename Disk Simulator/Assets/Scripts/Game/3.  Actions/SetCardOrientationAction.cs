using System.Linq;
using LPE;

namespace YuGiOh {
    public class SetCardOrientationAction : GameAction {
        static ObjectPool<SetCardOrientationAction> _pool = new ObjectPool<SetCardOrientationAction>(() => new SetCardOrientationAction());

        public static SetCardOrientationAction Get(CardEntity c, CardOrientation o) {
            var result = _pool.Get();
            result.targetEntity = c;
            result.targetOrientation = o;
            return result;
        }

        CardEntity targetEntity;
        CardOrientation targetOrientation;


        public override void Execute() {
            targetEntity.orientation = targetOrientation;
        }

        public override GameAction GetUndo() {
            return Get(targetEntity, targetEntity.orientation);
        }

        public override void Return() {
            _pool.Return(this);
        }
    }
}