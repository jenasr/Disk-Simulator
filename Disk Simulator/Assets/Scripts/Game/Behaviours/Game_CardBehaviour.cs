using UnityEngine;
using YuGiOh;


public class Game_CardBehaviour : MonoBehaviour {
    SpriteRenderer sr;
    Game_MainBehaviour m;
    CardEntity c;


    private void Awake() {
        sr = GetComponent<SpriteRenderer>();
    }

    public void SetCard(Game_MainBehaviour m, CardEntity c) {
        sr.sprite = c.data.GetSprite();
        this.m = m;
        this.c = c;
    }

    public void UpdateBehaviour() {
        switch (c.zone) {
            case Zone.none:
                throw new System.ArgumentException("Card has 'none' zone");
            case Zone.deck:
                throw new System.NotImplementedException();
            case Zone.hand:
                // TODO - set to hand position
                transform.position = Vector3.zero;
                break;
            case Zone.graveyard:
                throw new System.NotImplementedException();
            case Zone.monster:
                throw new System.NotImplementedException();
            case Zone.spell:
                throw new System.NotImplementedException();
            case Zone.field:
                throw new System.NotImplementedException();
            case Zone.all:
                throw new System.NotImplementedException();
        }

        // TODO - set face down sprite
        switch (c.orientation) {
            case CardOrientation.faceup:
                transform.rotation = Quaternion.identity;
                sr.color = new Color(1, 1, 1, 1);
                break;
            case CardOrientation.facedown:
                transform.rotation = Quaternion.identity;
                sr.color = new Color(.5f, .5f, .5f, 1);
                break;
            case CardOrientation.faceupideways:
                transform.rotation = Quaternion.Euler(0, 0, -90);
                sr.color = new Color(1, 1, 1, 1);
                break;
            case CardOrientation.facedownsideways:
                transform.rotation = Quaternion.Euler(0, 0, -90);
                sr.color = new Color(.5f, .5f, .5f, 1);
                break;
            default:
                break;
        }
    }
}

