using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YuGiOh;


public class GameTestMain : MonoBehaviour {
    public Game_MainBehaviour gameBehaviour;
    Game g;
    GameActionStack gas = new GameActionStack();

    private IEnumerator Start() {
        // initialize
        g = new Game();
        gameBehaviour.Init(g);
        yield return new WaitForSeconds(1);

        // create cards
        print("Creating cards");
        CardEntity card1 = new CardEntity();
        card1.data = CardData.FromJson(Resources.Load<TextAsset>("Card Data/1").text);
        CardEntity card2 = new CardEntity();
        card2.data = CardData.FromJson(Resources.Load<TextAsset>("Card Data/2").text);
        CardEntity card3 = new CardEntity();
        card3.data = CardData.FromJson(Resources.Load<TextAsset>("Card Data/3").text);
        yield return new WaitForSeconds(1);


        // add cards to hand
        print("Adding Cards to hand");
        CreateCardToHand ccth1 = new CreateCardToHand(g, 0, card1);
        CreateCardToHand ccth2 = new CreateCardToHand(g, 0, card2);
        CreateCardToHand ccth3 = new CreateCardToHand(g, 0, card3);
        gameBehaviour.CreateCard(card1);
        gameBehaviour.CreateCard(card2);
        gameBehaviour.CreateCard(card3);
        gas.Add(ccth1);
        ccth1.Execute();

        gas.Add(ccth2);
        ccth2.Execute();

        gas.Add(ccth3);
        ccth3.Execute();
        yield return new WaitForSeconds(1);


        // move card 1 to monster
        print("Moving card 1 to monster");
        GameAction a = ToZoneCardAction.Get(g, card1, ZoneType.monster);
        gas.Add(a);
        a.Execute();
        yield return new WaitForSeconds(1);


        // move card 3 to spell
        print("Moving card 3 to spell");
        a = ToZoneCardAction.Get(g, card3, ZoneType.spell);
        gas.Add(a);
        a.Execute();
        yield return new WaitForSeconds(1);


        // move card 1 to graveyard
        print("Moving card 1 to graveyard");
        a = ToZoneCardAction.Get(g, card1, ZoneType.graveyard);
        gas.Add(a);
        a.Execute();
        for (int i = 0; i < g.players[g.currentPlayer].graveyard.count; i++) {
            print(g.players[g.currentPlayer].graveyard[i].data.name);
        }
        yield return new WaitForSeconds(1);


        // move card 2 to graveyard
        print("Moving card 2 to graveyard");
        a = ToZoneCardAction.Get(g, card2, ZoneType.graveyard);
        gas.Add(a);
        a.Execute();
        for (int i = 0; i < g.players[g.currentPlayer].graveyard.count; i++) {
            print(g.players[g.currentPlayer].graveyard[i].data.name);
        }
        yield return new WaitForSeconds(1);


        // move card 2 to graveyard
        print("Moving card 3 to graveyard");
        a = ToZoneCardAction.Get(g, card3, ZoneType.graveyard);
        gas.Add(a);
        a.Execute();
        for (int i = 0; i < g.players[g.currentPlayer].graveyard.count; i++) {
            print(g.players[g.currentPlayer].graveyard[i].data.name);
        }
        yield return new WaitForSeconds(1);

        // move card 2 to monster
        print("Moving card 2 to monster");
        a = ToZoneCardAction.Get(g, card2, ZoneType.monster);
        gas.Add(a);
        a.Execute();
        for (int i = 0; i < g.players[g.currentPlayer].graveyard.count; i++) {
            print(g.players[g.currentPlayer].graveyard[i].data.name);
        }
        yield return new WaitForSeconds(1);

        // undo last move
        print("Undo");
        gas.ExecuteUndo();
        for (int i = 0; i < g.players[g.currentPlayer].graveyard.count; i++) {
            print(g.players[g.currentPlayer].graveyard[i].data.name);
        }
        yield return new WaitForSeconds(1);

        // move card 1 to spell
        print("Moving card 1 to spell");
        a = ToZoneCardAction.Get(g, card1, ZoneType.spell);
        gas.Add(a);
        a.Execute();
        yield return new WaitForSeconds(1);

        // move card 3 to monster
        print("Moving card 3 to monster");
        a = ToZoneCardAction.Get(g, card3, ZoneType.monster);
        gas.Add(a);
        a.Execute();
        yield return new WaitForSeconds(1);

        // undo last move
        print("Undo");
        gas.ExecuteUndo();
        yield return new WaitForSeconds(1);

        // move card 2 to spell
        print("Moving card 2 to spell");
        a = ToZoneCardAction.Get(g, card2, ZoneType.spell);
        gas.Add(a);
        a.Execute();
        yield return new WaitForSeconds(1);

    }
}
