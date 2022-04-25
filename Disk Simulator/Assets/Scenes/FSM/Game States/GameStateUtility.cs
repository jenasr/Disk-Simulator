using UnityEngine;
using YuGiOh;

public static class GameStateUtility {
    public static CardEntity CreateNewCard(this GameStateController gsc, int id) {
        CardEntity result = new CardEntity();

        result.data = CardData.FromJson(Resources.Load<TextAsset>($"Card Data/{id}").text);
        gsc.references.gameBehaviour.CreateCard(result, (c) => InputManager.Set.CardSelected(c));
        CreateCardToHand ccth1 = new CreateCardToHand(gsc.g, gsc.g.currentPlayer, result);
        gsc.actionStack.AddExecute(ccth1);

        return result;
    }

    public static void OpenCardActionMenu(this GameStateController gsc, CardEntity c) {
        gsc.references.cardActionMenuBehaviour.ClearOptions();


        //if (Input.GetMouseButtonUp(1)) {
        //    var a = SetCardOrientationAction.Get(c, CardOrientation.faceup);
        //    gsc.actionStack.AddExecute(a);
        //    return;
        //}

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