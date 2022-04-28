using System.Collections.Generic;


public class VoiceCommand {
    // Only applies to recently scanned card only
    public static VoiceCommand SummonMonsterAtk = new VoiceCommand("I summon a monster in attack");
    public static VoiceCommand SummonMonsterDef = new VoiceCommand("I summon a monster in defense");
    public static VoiceCommand SummonMonsterSet = new VoiceCommand("I set a monster");
    public static VoiceCommand SetSpell = new VoiceCommand("I set a field spell card facedown");
    public static VoiceCommand DiscardToGraveyard = new VoiceCommand("I discard a card to the graveyard");

    public static VoiceCommand[] all = {
        SummonMonsterAtk,
        SummonMonsterDef
    };

    public List<string> keywords;

    VoiceCommand(params string[] keywords) {
        this.keywords = new List<string>();
        this.keywords.AddRange(keywords);
    }
}
