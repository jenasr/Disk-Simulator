using UnityEngine;
using YuGiOh;

public static class GameStateUtility {
    public static void CreateNewCard(this GameStateController gsc, int id) {
        CardEntity c = new CardEntity();

        c.data = CardData.FromJson(Resources.Load<TextAsset>($"Card Data/{id}").text);
        gsc.references.gameBehaviour.CreateCard(c, (c) => InputManager.Set.CardSelected(c));
        CreateCardToHand ccth1 = new CreateCardToHand(gsc.g, gsc.g.currentPlayer, c);
        gsc.actionStack.AddExecute(ccth1);

        // TODO - request action state
        var tz1 = ToZoneCardAction.Get(gsc.g, c, ZoneType.monster);
        gsc.actionStack.AddExecute(tz1);
    }

    public static void OpenCardActionMenu(this GameStateController gsc, CardEntity c) {
        gsc.references.cardActionMenuBehaviour.ClearOptions();


        if (Input.GetMouseButtonUp(1)) {
            var a = SetCardOrientationAction.Get(c, CardOrientation.faceup);

            gsc.actionStack.AddExecute(a);
            return;
        }

        if (c.zone == ZoneType.graveyard) {
            Debug.Log("Graveyard actions not implemented");
            return;
        }
        if (c.zone == ZoneType.banished) {
            Debug.Log("Banished actions not implemented");
            return;
        }


        CardOptionUtility.AddOptions(gsc.g, c, gsc.references.cardActionMenuBehaviour);


        gsc.references.cardActionMenuBehaviour.SetPostion();
    }
}