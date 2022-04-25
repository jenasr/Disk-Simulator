using YuGiOh;

public abstract class GameState {
    public GameStateController controller;

    public GameObjectReferences references => controller.references;
    public Game g => controller.g;
    public GameActionStack actionStack => controller.actionStack;


    public GameState(GameStateController gsc) {
        controller = gsc;
    }

    public abstract GameState Next();
}
