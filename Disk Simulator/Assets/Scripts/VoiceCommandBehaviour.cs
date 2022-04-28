using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;



public class VoiceCommandBehaviour : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    // Start is called before the first frame update
    private string text = "None";
    void Start()
    {
        foreach (var vac in VoiceCommand.all) {
            foreach (var kw in vac.keywords) {
                actions.Add(kw, () => InputManager.Set.VoiceCommandRecieved(vac));
            }
        }


        /*
        actions.Add("I summon a monster in attack", SummonMonster);
        actions.Add("I summon a monster in defense", SummonMonster);
        actions.Add("I set a monster", FacedownDef);
        actions.Add("I activate a spell card", ActivateCard);
        actions.Add("I activate a field spell card", ActivateCard);
        actions.Add("I activate a trap card", ActivateCard);
        actions.Add("I set a card facedown", SetCard);
        actions.Add("I set a field spell card facedown", SetCard);
        actions.Add("I discard a card to the graveyard", DiscardCard);
        actions.Add("I banish a card from hand", DiscardCard);
        */


        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        //The command below should be executed once a card is being scanned
        keywordRecognizer.Start();
        
    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        //keywordRecognizer.Stop();
        //I intend that after a card has been used, it should no longer try to hear a voice command
        text = speech.text;
        actions[speech.text].Invoke();
        
    }
}
