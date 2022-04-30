using YuGiOh;

public class GameStateController {
    public WaitForInputState _waitForInputState;
    public NewCardActionState _newCardActionState;


    public Game g;
    public GameActionStack actionStack;
    public GameObjectReferences references;
    GameState currentState;


    public GameStateController(Game g, GameActionStack gas, GameObjectReferences refs) {
        this.g = g;
        actionStack = gas;
        references = refs;

        _waitForInputState = new WaitForInputState(this);
        _newCardActionState = new NewCardActionState(this);
        currentState = _waitForInputState;
    }

    public void Next() {
        var newState = currentState.Next() ?? currentState;

        if (newState != currentState) {
            currentState.Exit();
            currentState = newState;
        }
    }


    public WaitForInputState WaitForInputState() {
        return _waitForInputState;
    }
    public NewCardActionState NewCardActionState(CardEntity c) {
        _newCardActionState.SetCard(c);
        return _newCardActionState;
    }

}
