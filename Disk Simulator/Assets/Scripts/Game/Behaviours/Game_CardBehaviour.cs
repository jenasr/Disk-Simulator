using UnityEngine;
using UnityEngine.EventSystems;
using YuGiOh;
using System;
using System.Collections.Generic;


public class Game_CardBehaviour : MonoBehaviour, IPointerClickHandler {

    SpriteRenderer sr;
    Game_MainBehaviour m;
    Action<CardEntity> clickCB;
    CardEntity c;


    private void Awake() {
        sr = GetComponent<SpriteRenderer>();
    }

    
    void Update() {
        SetPosition();
        SetOrientation();
    }   

 
    void SetOrientation() {
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
            case CardOrientation.faceupSideways:
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
    public void SetCard(Game_MainBehaviour m, CardEntity c, Action<CardEntity> clickCB) {
        sr.sprite = c.data.GetSprite();
        this.m = m;
        this.c = c;
        this.clickCB = clickCB;


        // TODO - Make more efficient
        var shape = new List<Vector2>();
        sr.sprite.GetPhysicsShape(0, shape);
        GetComponent<PolygonCollider2D>().SetPath(0, shape.ToArray());
    }

    public void OnPointerClick(PointerEventData eventData) {
        clickCB?.Invoke(c);
    }
}
