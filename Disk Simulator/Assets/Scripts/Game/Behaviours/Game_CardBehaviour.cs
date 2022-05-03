using UnityEngine;
using UnityEngine.EventSystems;
using YuGiOh;
using System;
using System.Collections.Generic;


public class Game_CardBehaviour : MonoBehaviour, IPointerClickHandler {
    const string CardBackResourcePath = "Card Back";
    static Sprite _cardbackSprite;
    static Sprite cardbackSprite => _cardbackSprite = _cardbackSprite ?? Resources.Load<Sprite>(CardBackResourcePath);

    SpriteRenderer sr;
    Game_MainBehaviour m;
    Action<CardEntity> clickCB;
    CardEntity c;
    CardOrientation prevOri;
    Sprite cardSprite;

    private void Awake() {
        sr = GetComponent<SpriteRenderer>();
    }

    
    void Update() {
        SetPosition();
        SetOrientation();

    }   

 
    void SetOrientation() {
        if (prevOri == c.orientation) {
            // no change
            return;
        }

        switch (c.orientation) {
            case CardOrientation.faceup:
                transform.rotation = Quaternion.identity;
                sr.sprite = cardSprite;
                break;
            case CardOrientation.facedown:
                transform.rotation = Quaternion.identity;
                sr.sprite = cardbackSprite;
                break;
            case CardOrientation.faceupSideways:
                transform.rotation = Quaternion.Euler(0, 0, -90);
                sr.sprite = cardSprite;
                break;
            case CardOrientation.facedownsideways:
                transform.rotation = Quaternion.Euler(0, 0, -90);
                sr.sprite = cardbackSprite;
                break;
            default:
                break;
        }
        prevOri = c.orientation;

        if (c.controller == 1) {
            transform.eulerAngles += new Vector3(0, 0, 180);
        }
    }
    void SetPosition() {
        var player = c.controller == 0 ? m.player1 : m.player2;
        // set position
        switch (c.zone) {
            case ZoneType.none:
                throw new System.ArgumentException("Card has 'none' zone");
            case ZoneType.deck:
                throw new System.NotImplementedException();
            case ZoneType.hand:
                // TODO - set to hand position
                transform.position = Vector3.zero;
                break;
            case ZoneType.graveyard:
                // TODO - set sprite sorting order
                transform.position = player.graveyardZone.transform.position;
                break;
            case ZoneType.monster:
                transform.position = player.monsterZones[c.zonePlacement].transform.position;
                break;
            case ZoneType.spell:
                transform.position = player.spellZones[c.zonePlacement].transform.position;
                break;
            case ZoneType.field:
                throw new System.NotImplementedException();
            case ZoneType.all:
                throw new System.NotImplementedException();
        }

    }
    
  
    public void OnPointerClick(PointerEventData eventData) {
        clickCB?.Invoke(c);
    }

    public void SetCard(Game_MainBehaviour m, CardEntity c, Action<CardEntity> clickCB) {
        cardSprite = c.data.GetSprite();
        sr.sprite = cardSprite;
        this.m = m;
        this.c = c;
        this.clickCB = clickCB;


        // TODO - Make more efficient
        var shape = new List<Vector2>();
        sr.sprite.GetPhysicsShape(0, shape);
        GetComponent<PolygonCollider2D>().SetPath(0, shape.ToArray());
    }

}
