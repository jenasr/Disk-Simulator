using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class VoiceCommand : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    // Start is called before the first frame update
    void Start()
    {
        actions.Add("summon", SummonMonster);
        actions.Add("activate", ActivateCard);
        actions.Add("set", SetCard);
        actions.Add("discard", DiscardCard);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.Start();
    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }
    
    private void SummonMonster()
    {
        print("Summon");
    }

    private void ActivateCard()
    {
        print("Activate");
    }

    private void SetCard()
    {
        print("Set");
    }

    private void DiscardCard()
    {
        print("Discard");
    }
}
