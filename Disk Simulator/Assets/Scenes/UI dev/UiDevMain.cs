using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YuGiOh;


public class UiDevMain : MonoBehaviour {
    public CardActionMenuBehaviour cardActionMenuBehaviour;
    public Game_MainBehaviour gameBehaviour;
    public Button backgroundButton;
    Game g;
    GameActionStack gas;


    IEnumerator Start() {
        g = new Game();
        gas = new GameActionStack();
        gameBehaviour.Init(g);

        cardActionMenuBehaviour.OnActionSelect += (a) => {
            gas.AddExecute(a);
            cardActionMenuBehaviour.ClearOptions();
        };

        CreateInitialCards();
        backgroundButton.onClick.AddListener(() => {
            cardActionMenuBehaviour.ClearOptions();
        });
        yield return null;



        var p1 = g.players[0];
        var graveyard = p1.graveyard;

        for (int i = 0; i < graveyard.count; i++) {
            CardEntity c = graveyard[i];
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.P)) {
            var a = SetPlayerTurnAction.Get(g, (g.currentPlayer + 1) % 2);
            gas.AddExecute(a);
        }
        if (Input.GetKeyDown(KeyCode.Q)) {
            // just for testing
            CardEntity c = new CardEntity();

            c.data = CardData.FromJson(Resources.Load<TextAsset>($"Card Data/3").text);
            gameBehaviour.CreateCard(c, OnClickCard);
            CreateCardToHand ccth1 = new CreateCardToHand(g, g.currentPlayer, c);
            gas.AddExecute(ccth1);
            var tz1 = ToZoneCardAction.Get(g, c, ZoneType.monster);
            gas.AddExecute(tz1);
        }
    }
    void CreateInitialCards() {
        // create cards
        CardEntity card1 = new CardEntity();
        CardEntity card2 = new CardEntity();
        CardEntity card3 = new CardEntity();
        CardEntity card4 = new CardEntity();
        CardEntity card5 = new CardEntity();
        CardEntity card6 = new CardEntity();

        card1.data = CardData.FromJson(Resources.Load<TextAsset>($"Card Data/3").text);
        card2.data = CardData.FromJson(Resources.Load<TextAsset>($"Card Data/32339440").text);
        card3.data = CardData.FromJson(Resources.Load<TextAsset>($"Card Data/68601507").text);
        card4.data = CardData.FromJson(Resources.Load<TextAsset>($"Card Data/3").text);
        card5.data = CardData.FromJson(Resources.Load<TextAsset>($"Card Data/32339440").text);
        card6.data = CardData.FromJson(Resources.Load<TextAsset>($"Card Data/68601507").text);

        // create behaviours
        // TODO - Refactor 'OnClickCard' placement to gameBehaviour.Init()
        gameBehaviour.CreateCard(card1, OnClickCard);
        gameBehaviour.CreateCard(card2, OnClickCard);
        gameBehaviour.CreateCard(card3, OnClickCard);
        gameBehaviour.CreateCard(card4, OnClickCard);
        gameBehaviour.CreateCard(card5, OnClickCard);
        gameBehaviour.CreateCard(card6, OnClickCard);

        // add card to hand
        CreateCardToHand ccth1 = new CreateCardToHand(g, 0, card1);
        CreateCardToHand ccth2 = new CreateCardToHand(g, 0, card2);
        CreateCardToHand ccth3 = new CreateCardToHand(g, 0, card3);
        CreateCardToHand ccth4 = new CreateCardToHand(g, 0, card4);
        CreateCardToHand ccth5 = new CreateCardToHand(g, 0, card5);
        CreateCardToHand ccth6 = new CreateCardToHand(g, 0, card6);

        gas.AddExecute(ccth1);
        gas.AddExecute(ccth2);
        gas.AddExecute(ccth3);
        gas.AddExecute(ccth4);
        gas.AddExecute(ccth5);
        gas.AddExecute(ccth6);

        // move cards to monster zone (for testing)
        var tz1 = ToZoneCardAction.Get(g, card1, ZoneType.monster);
        var tz2 = ToZoneCardAction.Get(g, card2, ZoneType.monster);
        var tz3 = ToZoneCardAction.Get(g, card3, ZoneType.monster);
        var tz4 = ToZoneCardAction.Get(g, card4, ZoneType.monster);
        var tz5 = ToZoneCardAction.Get(g, card5, ZoneType.monster);
        var tz6 = ToZoneCardAction.Get(g, card6, ZoneType.spell);

        gas.AddExecute(tz1);
        gas.AddExecute(tz2);
        gas.AddExecute(tz3);
        gas.AddExecute(tz4);
        gas.AddExecute(tz5);
        gas.AddExecute(tz6);
    }


    void OnClickCard(CardEntity c) {
        cardActionMenuBehaviour.ClearOptions();


        if (Input.GetMouseButtonUp(1)) {
            var a = SetCardOrientationAction.Get(c, CardOrientation.faceup);

            gas.AddExecute(a);
            return;
        }

        if (c.zone == ZoneType.graveyard) {
            print("Graveyard actions not implemented");
            return;
        }
        if (c.zone == ZoneType.banished) {
            print("Banished actions not implemented");
            return;
        }


        CardOptionUtility.AddOptions(g, c, cardActionMenuBehaviour);


        cardActionMenuBehaviour.SetPostion();
    }
}
