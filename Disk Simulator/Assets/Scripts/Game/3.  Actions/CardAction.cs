using System;
using LPE;

namespace YuGiOh {
    /// <summary>
    /// Singleton wrapper for CardAction
    /// </summary>
    public class CardAction<T> where T : CardAction, new() {
        public static T action = new T();
    }


    public abstract class CardAction {
        public static CardAction[] all = {
            CardAction<ToFacedownAction>.action,
            CardAction<ToFacedownSidewaysAction>.action,
            CardAction<ToFaceupAction>.action,
            CardAction<ToFaceupSidewaysAction>.action,

            CardAction<ToDeckCardAction>.action,
            CardAction<ToFieldCardAction>.action,
            CardAction<ToGraveyardCardAction>.action,
            CardAction<ToHandCardAction>.action,

            CardAction<ToMonsterUpCardAction>.action,
            CardAction<ToMonsterSidewaysCardAction>.action,
            CardAction<ToMonsterDownCardAction>.action,

            CardAction<ToSpellUpCardAction>.action,
            CardAction<ToSpellDownCardAction>.action,
        };

        //**********************************************************************************************************************
        // Main
        //**********************************************************************************************************************

        public abstract string Name(Game g, CardEntity c);
        public abstract string DisplayText(Game g, CardEntity c);
        public abstract bool Check(Game g, CardEntity c);
        public abstract void Execute(Game g, CardEntity c);
        public abstract CardAction GetUndo(Game g, CardEntity c);

        public virtual GameEvents PreExecuteEvents(Game g, CardEntity c) {
            var result = GameEvents.GetEvents(g);
            var events = result.cardActions[this];
            var events2 = result.cardModified;

            events.InvokePre(c);
            events2.InvokePre(c);
            return result;
        }
        public virtual GameEvents PostExecuteEvents(Game g, CardEntity c) {
            var result = GameEvents.GetEvents(g);
            var events = result.cardActions[this];
            var events2 = result.cardModified;

            events.InvokePost(c);
            events2.InvokePost(c);
            return result;
        }

        public virtual void OnRemove() { }
       

        //**********************************************************************************************************************
        // Game Action Wrapper
        //**********************************************************************************************************************
        public GameAction AsGameAction(Game g, CardEntity c) {
            return GameActionWrapper.Get(g, c, this);

        }

        class GameActionWrapper : GameAction {
            static ObjectPool<GameActionWrapper> _pool = new ObjectPool<GameActionWrapper>(() => new GameActionWrapper());
            public static GameActionWrapper Get(Game g, CardEntity c, CardAction a) {
                var result = _pool.Get();
                result.g = g;
                result.c = c;
                result.a = a;

                return result;
            }

            CardAction a;
            Game g;
            CardEntity c;


            public override void Execute() {
                a.PreExecuteEvents(g, c);
                a.Execute(g, c);
                a.PostExecuteEvents(g, c);
            }
            public override GameAction GetUndo() {
                return a.GetUndo(g, c).AsGameAction(g, c);
            }
            public override void OnRemove() {
                a.OnRemove();
                a = null;
                _pool.Return(this);
            }
        }
    }


}