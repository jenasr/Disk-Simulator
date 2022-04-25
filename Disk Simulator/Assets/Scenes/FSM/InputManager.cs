using YuGiOh;

public static class InputManager {
    static bool no = false;

    public static class ActionRequested {
        public static bool yes;
        public static GameAction action;
    }

    public static class CancelRequested {
        public static bool yes;
    }

    public static class CardScanned {
        public static bool yes;
        public static int id;
    }
    
    public static class CardSelected {
        public static bool yes;
        public static CardEntity card;
    }

    public static class Set {
        public static void ActionRequested(GameAction action) {
            InputManager.ActionRequested.yes = true;
            InputManager.ActionRequested.action = action;
        }
        public static void CancelRequested() {
            InputManager.CancelRequested.yes = true;
        }
        public static void CardScanned(int id) {
            InputManager.CardScanned.yes = true;
            InputManager.CardScanned.id = id;
        }
        public static void CardSelected(CardEntity card) {
            InputManager.CardSelected.yes = true;
            InputManager.CardSelected.card = card;
        }
    }

    public static void Clear() {
        ActionRequested.yes = no;
        CancelRequested.yes = no;
        CardScanned.yes = no;
        CardSelected.yes = no;
    }
}
