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
        if (InputManager.VoiceCommandRecieved.yes) {
            print(InputManager.VoiceCommandRecieved.command.keywords[0]);
        }
        if (Input.GetKeyDown(KeyCode.Q)) {
            InputManager.Set.CardScanned(3);
        }
        gsc.Next();
        InputManager.Clear();
    }
}
