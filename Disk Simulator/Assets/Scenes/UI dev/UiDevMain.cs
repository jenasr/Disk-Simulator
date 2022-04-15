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
            // gas.AddExecute(a); // not ready yet
            print(a); 
        };

        CreateInitialCards();
        backgroundButton.onClick.AddListener(()=> {
            cardActionMenuBehaviour.ClearOptions();
        });
        yield return null;
    }

    void CreateInitialCards() {
        // create cards
        CardEntity card1 = new CardEntity();
        CardEntity card2 = new CardEntity();
        CardEntity card3 = new CardEntity();

        card1.data = CardData.FromJson(Resources.Load<TextAsset>($"Card Data/3").text);
        card2.data = CardData.FromJson(Resources.Load<TextAsset>($"Card Data/32339440").text);
        card3.data = CardData.FromJson(Resources.Load<TextAsset>($"Card Data/68601507").text);

        // create behaviours
        // TODO - Refactor 'OnClickCard' placement to gameBehaviour.Init()
        gameBehaviour.CreateCard(card1, OnClickCard);
        gameBehaviour.CreateCard(card2, OnClickCard);
        gameBehaviour.CreateCard(card3, OnClickCard);

        // add card to hand
        CreateCardToHand ccth1 = new CreateCardToHand(g, 0, card1);
        CreateCardToHand ccth2 = new CreateCardToHand(g, 0, card2);
        CreateCardToHand ccth3 = new CreateCardToHand(g, 0, card3);

        gas.AddExecute(ccth1);
        gas.AddExecute(ccth2);
        gas.AddExecute(ccth3);

        // move cards to monster zone (for testing)
        var tz1 = ToZoneCardAction.Get(g, card1, ZoneType.monster);
        var tz2 = ToZoneCardAction.Get(g, card2, ZoneType.monster);
        var tz3 = ToZoneCardAction.Get(g, card3, ZoneType.monster);

        gas.AddExecute(tz1);
        gas.AddExecute(tz2);
        gas.AddExecute(tz3);

    }
    void Update() {

    }

    void OnClickCard(CardEntity c) {
        cardActionMenuBehaviour.ClearOptions();
        cardActionMenuBehaviour.AddOption("Bye", null);
        cardActionMenuBehaviour.SetPostion();
    }
}

