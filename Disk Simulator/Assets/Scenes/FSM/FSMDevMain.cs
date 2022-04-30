using System.Collections.Generic;
using UnityEngine;
using YuGiOh;


public class FSMDevMain : MonoBehaviour {
    public GameObjectReferences references;

    GameStateController gsc;
    Game g;
    GameActionStack gas;

    void Start() {
        g = new Game();
        gas = new GameActionStack();
        gsc = new GameStateController(g, gas, references);

        references.backgroundButton.onClick.AddListener(() => InputManager.Set.CancelRequested());
        references.cardActionMenuBehaviour.OnActionSelect += (a) => {
            InputManager.Set.ActionRequested(a);
        };
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            InputManager.Set.CardScanned(32339440);
        }
        if (Input.GetKeyDown(KeyCode.P)) {
            InputManager.Set.ActionRequested(SetPlayerTurnAction.Get(g, (g.currentPlayer + 1) % g.players.Length));
        }
        gsc.Next();
        InputManager.Clear();
    }
}
