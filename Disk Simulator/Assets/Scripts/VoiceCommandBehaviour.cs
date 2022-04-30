using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;



public class VoiceCommandBehaviour : MonoBehaviour {
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    
    void Start() {
        foreach (var vac in VoiceCommand.all) {
            foreach (var kw in vac.keywords) {
                actions.Add(kw, () => InputManager.Set.VoiceCommandRecieved(vac));
            }
        }

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech) {
        actions[speech.text].Invoke();

    }

    public void StartListening() {
        keywordRecognizer.Start();
    }

    public void StopListening() {
        keywordRecognizer.Stop();
    }
}
