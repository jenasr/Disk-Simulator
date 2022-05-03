using UnityEngine;
using YuGiOh;

public class PlayerHighlighter : MonoBehaviour {
    public GameObject player1;
    public GameObject player2;

    Game g;

    public void Init(Game g) {
        this.g = g;
    }


    private void Update() {
        player1.transform.localScale = g.currentPlayer == 0 ? new Vector3(1.1f, 1.1f, 1) : new Vector3(1, 1, 1);
        player2.transform.localScale = g.currentPlayer == 1 ? new Vector3(1.1f, 1.1f, 1) : new Vector3(1, 1, 1);
    }
}
