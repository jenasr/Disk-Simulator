using UnityEngine;
using YuGiOh;

public class MainGP_Main : MonoBehaviour {
    public GameObjectReferences references;
    public PlayerHighlighter ph;
    GameStateController gsc;
    Game g;
    GameActionStack gas;

    void Start() {
        g = new Game();
        gas = new GameActionStack();
        gsc = new GameStateController(g, gas, references);
        ph.Init(g);
        references.backgroundButton.onClick.AddListener(() => InputManager.Set.CancelRequested());
        references.cardActionMenuBehaviour.OnActionSelect += (a) => {
            InputManager.Set.ActionRequested(a);
        };
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            InputManager.Set.CardScanned(32339440);
        }
        if (Input.GetKeyDown(KeyCode.W)) {
            InputManager.Set.CardScanned(38033121);
        }
        if (Input.GetKeyDown(KeyCode.P)) {
            InputManager.Set.ActionRequested(SetPlayerTurnAction.Get(g, (g.currentPlayer + 1) % g.players.Length));
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            InputManager.Set.CancelRequested();
        }
        if (Input.GetKey(KeyCode.X) && Input.GetKey(KeyCode.LeftControl))
        {
            Application.Quit();
        }
        gsc.Next();
        InputManager.Clear();
    }
}
