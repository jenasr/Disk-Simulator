using YuGiOh;


public class WaitForInputState : GameState {
    public WaitForInputState(GameStateController gsc) : base(gsc) { }

    public override GameState Next() {
        if (InputManager.ActionRequested.yes) {
            actionStack.AddExecute(InputManager.ActionRequested.action);
            references.cardActionMenuBehaviour.ClearOptions();
            InputManager.Set.ActionUsed();
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

        // Voice Command is dissabled in this state, so this should never be called
        // will leave incase this changes
        //if (InputManager.VoiceCommandRecieved.command == VoiceCommand.EndTurn) {
        //    var a = SetPlayerTurnAction.Get(g, (g.currentPlayer + 1) % g.players.Length);
        //    actionStack.AddExecute(a);
        //    references.cardActionMenuBehaviour.ClearOptions();
        //}

        return null;
    }
}
