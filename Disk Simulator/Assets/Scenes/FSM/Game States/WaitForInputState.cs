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
