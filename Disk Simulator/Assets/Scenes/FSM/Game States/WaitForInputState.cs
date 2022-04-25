using YuGiOh;


public class WaitForInputState : GameState {
    public WaitForInputState(GameStateController gsc) : base(gsc) { }

    public override GameState Next() {
        if (InputManager.ActionRequested.yes) {
            actionStack.AddExecute(InputManager.ActionRequested.action);
            references.cardActionMenuBehaviour.ClearOptions();
        }

        if (InputManager.CardScanned.yes) {
            var newCard = controller.CreateNewCard(InputManager.CardScanned.id);
            return controller.NewCardActionState(newCard);
        }

        if (InputManager.CardSelected.yes) {
            controller.OpenCardActionMenu(InputManager.CardSelected.card);
        }

        if (InputManager.CancelRequested.yes) {
            references.cardActionMenuBehaviour.ClearOptions();
        }

        return null;
    }
}

public class NewCardActionState : GameState {
    public NewCardActionState(GameStateController gsc) : base(gsc) { }


    CardEntity card;

    public void SetCard(CardEntity c) {
        card = c;
        controller.OpenCardActionMenu(card);
        references.cardActionMenuBehaviour.SetPostionCenter();
    }


    public override GameState Next() {
        if (InputManager.ActionRequested.yes) {
            actionStack.AddExecute(InputManager.ActionRequested.action);
            references.cardActionMenuBehaviour.ClearOptions();
            return controller.WaitForInputState();
        }

        return null;
    }
}
