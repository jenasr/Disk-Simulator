using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YuGiOh;


public class GameTestMain : MonoBehaviour {
    public Game_MainBehaviour gameBehaviour;
    Game g;
    GameActionStack gas = new GameActionStack();

    private IEnumerator Start() {
        g = new Game();
        gameBehaviour.Init(g);
        yield return new WaitForSeconds(1);


        CardEntity card = new CardEntity();
        card.data = CardData.FromJson(Resources.Load<TextAsset>("Card Data/1").text);
        CreateCardToHand ccth = new CreateCardToHand(g, 0, card);
        gas.Add(ccth);
        ccth.Execute();
        print("Create Card");
        yield return new WaitForSeconds(1);


        var a = CardAction<ToMonsterUpCardAction>.action.AsGameAction(g, card);
        gas.Add(a);
        a.Execute();
        print("Sideways");
        yield return new WaitForSeconds(1);


        a = SetCardOrientationAction.Get(card, CardOrientation.facedown); 
        gas.Add(a);
        a.Execute();
        print("Facedown");
        yield return new WaitForSeconds(1);


        gas.ExecuteUndo();
        print("Undo");
        yield return new WaitForSeconds(1);


        gas.ExecuteUndo();
        print("Undo");
        yield return new WaitForSeconds(1);


        gas.ExecuteRedo();
        print("Redo");
    }
}
