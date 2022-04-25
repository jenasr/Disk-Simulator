using YuGiOh;

public class GameStateController {
    public WaitForInputState waitForInputState;


    public Game g;
    public GameActionStack actionStack;
    public GameObjectReferences references;
    GameState currentState;


    public GameStateController(Game g, GameActionStack gas, GameObjectReferences refs) {
        this.g = g;
        actionStack = gas;
        references = refs;

        waitForInputState = new WaitForInputState(this);

        currentState = waitForInputState;
    }

    public void Next() {
        currentState = currentState.Next() ?? currentState;
    }
}
