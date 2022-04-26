using YuGiOh;

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
