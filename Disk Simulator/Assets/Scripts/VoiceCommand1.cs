using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;


public class VoiceCommandOption {
    public static VoiceCommandOption SummonMonsterAtk = new VoiceCommandOption("I summon a monster in attack");
    public static VoiceCommandOption SummonMonsterDef = new VoiceCommandOption("I summon a monster in defense");
    
    
    public static VoiceCommandOption[] all = {
        SummonMonsterAtk,
        SummonMonsterDef
    };

    public List<string> keywords;

    VoiceCommandOption(params string[] keywords) {
        this.keywords = new List<string>();
        this.keywords.AddRange(keywords);
    }
}



public class VoiceCommand1 : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    // Start is called before the first frame update
    private string text = "None";
    void Start()
    {
        foreach (var vac in VoiceCommandOption.all) {
            foreach (var kw in vac.keywords) {
                actions.Add(kw, () => InputManager.Set.VoiceCommandRecieved(vac));
            }
        }

        //actions.Add("I summon a monster in attack", SummonMonster);
        //actions.Add("I summon a monster in defense", SummonMonster);
        /*actions.Add("I set a monster", FacedownDef);
        actions.Add("I activate a spell card", ActivateCard);
        actions.Add("I activate a field spell card", ActivateCard);
        //actions.Add("I activate a trap card", ActivateCard);
        actions.Add("I set a card facedown", SetCard);
        actions.Add("I set a field spell card facedown", SetCard);
        actions.Add("I discard a card to the graveyard", DiscardCard);
        actions.Add("I banish a card from hand", DiscardCard);*/ 
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

    private void SummonMonster()
    {
       if (text.Contains("attack"))
        {
            print("Summon in attack position");
        }
       else if (text.Contains("defense"))
        {
            print("Summon in defense position");
        }
    }

    private void ActivateCard()
    {
        if (text.Contains("field"))
        {
            print("Activate field spell");
        }
        else 
        {
            print("Activate spell card");
        }
    }

    private void SetCard()
    {
        if (text.Contains("field"))
        {
            print("Set field spell");
        }
        else
        {
            print("Set spell/trap card");
        }
    }

    private void DiscardCard()
    {
        if (text.Contains("graveyard"))
        {
            print("Send to graveyard");
        }
        else if (text.Contains("banish"))
        {
            print("Banish card");
        }
    }

    private void FacedownDef()
    {
        print("Set a monster in facedown defense position");
    }
}
