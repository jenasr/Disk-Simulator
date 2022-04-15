using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using YuGiOh;
using System;

public class CardActionMenuBehaviour : MonoBehaviour {
    public event Action<GameAction> OnActionSelect;
    
    [SerializeField] Button optionButtonSrc;
    
    RectTransform rt;
    Vector3[] corners = new Vector3[4];

    // Do not clear entries
    List<Entries> entries = new List<Entries>();
    int numEntries; 


    void Start() {
        rt = GetComponent<RectTransform>();
        optionButtonSrc.gameObject.SetActive(false);
    }

    public void SetPostion() {
        rt.pivot = GetPivot();
        rt.position = Input.mousePosition;
    }
    public void AddOption(string text, GameAction action) {
        if (entries.Count == numEntries) {
            var newButton = Instantiate(optionButtonSrc, optionButtonSrc.transform.parent);
            entries.Add(new Entries(newButton, (a) => OnActionSelect?.Invoke(a)));
        }

        var e = entries[numEntries];
        e.text = text;
        e.action = action;
        e.selected = false;
        e.button.gameObject.SetActive(true);
        e.button.GetComponentInChildren<TMPro.TMP_Text>().text = text;

        numEntries++;
    }

    public void ClearOptions() {
        foreach (var e in entries) {
            if (!e.selected) {
                e?.action.Return();
                e.button.gameObject.SetActive(false);
            }
            e.action = null;
        }
    }


    Vector2 GetPivot() {
        var size = GetSizeOnScreen();

        float x = size.x > Screen.width - Input.mousePosition.x ? 1 : 0; 
        float y = size.y > Screen.height - Input.mousePosition.y ? 1 : 0;

        return new Vector2(x, y);
    }

    Vector2 GetSizeOnScreen() {
        rt.GetWorldCorners(corners);
        return corners[2] - corners[0];
    }

    class Entries {
        public string text;
        public Button button;
        public GameAction action;
        public bool selected;

        public Entries(Button b, Action<GameAction> cb) {
            button = b;
            b.onClick.AddListener(() => {
                cb(action);
                selected = true;
            });
        }
    }
}
