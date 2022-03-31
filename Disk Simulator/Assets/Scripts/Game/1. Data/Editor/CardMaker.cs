using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Collections.Generic;


namespace YuGiOh {
    /// <summary>
    /// Window to help create new card data files easily
    /// </summary>
    public class CardMakerWindow : EditorWindow {

        [MenuItem("Window/Card Maker")]
        public static void ShowWindow() {
            GetWindow< CardMakerWindow>("Card Maker");
        }

        //**********************************************************************************************************************************
        // Cache

        //**********************************************************************************************************************************
        [SerializeField]
        CardData card = new CardData();
        Dictionary<string, bool> foldoutCache = new Dictionary<string, bool>();
        [NonSerialized]
        Vector2 scroll;

        [NonSerialized]
        bool noSprite = true;

        [NonSerialized]
        bool fileExists = false;

        [NonSerialized]
        int prevCardID = -1;

        //**********************************************************************************************************************************
        // GUI
        //**********************************************************************************************************************************

        void OnGUI() {
            scroll = EditorGUILayout.BeginScrollView(scroll);
            Undo.RecordObject(this, "edit card");

            // TODO - load existing card (for modifications or shortcut to similar cards)
            CommonData();
            MonsterData();
            CreateCardDataButton();

            EditorGUILayout.EndScrollView();
        }
        void CommonData() {
            // header
            var style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold };
            GUILayout.Label("Common Data", style);

            // fields
            IDField();
            NoSpriteWarning();
            card.name = EditorGUILayout.TextField("Name", card.name);
            card.validZones = DisplayFlagsAsToggles(card.validZones, "Valid Zones");
            card.cardType = DisplayEnumAsToggles(card.cardType, "Card Type");
        }

        void MonsterData() {
            // only for monster cards
            if (card.cardType != CardType.monster) {
                return;
            }

            // header
            var style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold };
            EditorGUILayout.Space();
            GUILayout.Label("Monster Data", style);

            // fields
            card.level = EditorGUILayout.IntField("Level", card.level);
            card.atk = EditorGUILayout.IntField("Attack", card.atk);
            card.def = EditorGUILayout.IntField("Defense", card.def);

            card.monsterType = DisplayFlagsAsToggles(card.monsterType, "Monster Type");
            card.attribute = DisplayEnumAsToggles(card.attribute, "Attribute");

            // range correction
            card.level = card.level < 1 ? 1 : card.level;
            card.atk = card.atk < 0 ? 0 : card.atk;
            card.def = card.def < 0 ? 0 : card.def;
        }

        void CreateCardDataButton() {
            EditorGUILayout.Space();
            EditorGUILayout.Space();

            bool create = GUILayout.Button("Create File");

            if (!create) {
                return;
            }


            // Check if file exists
            string path = Application.dataPath + $"/Resources/Card Data/{card.id}.json";
            bool alreadyExist = File.Exists(path);

            if (alreadyExist) {
                // check with user before replacing file
                bool replace = EditorUtility.DisplayDialog(
                    $"Card {card.id} already exists",
                    "Do you want to overwrite existing Card?",
                    "Replace Card",
                    "Do Not Replace");
                if (replace) {
                    File.WriteAllText(path, card.ToJson());
                }
            }
            else {
                File.WriteAllText(path, card.ToJson());
            }
        }

        //**********************************************************************************************************************************
        // GUI helper Functions
        //**********************************************************************************************************************************

        void IDField() {
            EditorGUILayout.BeginHorizontal();
            // ID field
            card.id = EditorGUILayout.IntField("ID", card.id);
            LoadCardFromID();
            EditorGUILayout.EndHorizontal();
        }
        void LoadCardFromID() {
            // cache
            TextAsset t = null;

            // check if file already exists
            bool check =
                prevCardID != card.id // change ID
                || (UnityEngine.Random.value < .1f); // randomly check to see if sprite has been updated

            //check
            if (check) {
                t = Resources.Load<TextAsset>($"Card Data/{card.id}");
                fileExists = t != null;
            }

            // load card button
            if (fileExists) {
                bool load = GUILayout.Button("Load Card");
                // TODO - maybe add confirmation dialog (existing car)
                if (load) {
                    card = CardData.FromJson(t.text);
                }
            }

            if (t == null) {
                Resources.UnloadAsset(t);
            }
        }
        void NoSpriteWarning() {
            // When to check
            bool check =
                prevCardID != card.id // change ID
                || (UnityEngine.Random.value < .1f); // randomly check to see if sprite has been updated
            prevCardID = card.id;

            // check
            if (check) {
                Sprite s = Resources.Load<Sprite>($"Card Images/{card.id}");
                noSprite = s == null;
                Resources.UnloadAsset(s);
            }

            // display warning
            if (noSprite) {
                GUIStyle style = new GUIStyle();
                style.fontStyle = FontStyle.Italic;
                style.normal.textColor = Color.red;
                EditorGUILayout.LabelField($"No image for this ID found. Expecting to find 'Card Images/{card.id}' in a Resources folder", style);
            }
        }

        bool CheckFoldout(string label) {
            // create if doesn't exist
            if (!foldoutCache.ContainsKey(label)) {
                foldoutCache.Add(label, false);
            }

            // foldout field
            bool result = EditorGUILayout.Foldout(foldoutCache[label], label);
            foldoutCache[label] = result;
            return result;
        }

        T DisplayFlagsAsToggles<T>(T value, string label) where T : Enum {
            if (!CheckFoldout(label)) {
                // foldout closed, skip
                return value;
            }
            // can't work with Enum, so convert to int
            int result = Convert.ToInt32(value);

            foreach (int t in (int[])Enum.GetValues(typeof(T))) {
                if (t == 0) {
                    // skip 'none'
                    continue;
                }

                // format + toggle
                EditorGUILayout.BeginHorizontal();
                bool valid = EditorGUILayout.Toggle(" ", (result & t) == t, GUILayout.ExpandWidth(false));
                EditorGUILayout.LabelField(Enum.GetName(typeof(T), t));
                EditorGUILayout.EndHorizontal();

                // update result
                if (valid) {
                    result |= t;
                }
                else if ((result & t) == t) {
                    // deselect value
                    result &= ~t;
                }
            }

            // cast back to correct type
            return (T)Enum.ToObject(typeof(T), result);
        }


        T DisplayEnumAsToggles<T>(T value, string label) where T : Enum {
            if (!CheckFoldout(label)) {
                // foldout closed, skip
                return value;
            }

            // can't work with Enum, so convert to ints
            int orig = Convert.ToInt32(value);
            int result = orig;
            
            foreach (int t in (int[])Enum.GetValues(typeof(T))) {
                if (t == 0) {
                    // skip 'none'
                    continue;
                }
                string enumName = Enum.GetName(typeof(T), t);
                if (enumName == "all") {
                    // can only select single value, 'all' makes no sense here
                    continue;
                }
                // format + toggle
                EditorGUILayout.BeginHorizontal();
                bool valid = EditorGUILayout.Toggle(" ", (result & t) == t, GUILayout.ExpandWidth(false));
                EditorGUILayout.LabelField(enumName);
                EditorGUILayout.EndHorizontal();

                if (valid && t != orig) {
                    // t != orig is to stop original selection from overriding result
                    result = t;
                }
            }

            return (T)Enum.ToObject(typeof(T), result);
        }


        //**********************************************************************************************************************************
        // Persist state between sessions
        //**********************************************************************************************************************************

        void OnEnable() {
            var cardJson = EditorPrefs.GetString("Card Maker Data", card.ToJson());
            card = CardData.FromJson(cardJson);
        }

        void OnDisable() {
            var data = card.ToJson();
            EditorPrefs.SetString("Card Maker Data", data);

        }
    }
}