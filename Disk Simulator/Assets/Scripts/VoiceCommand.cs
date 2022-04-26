using System.Collections.Generic;


public class VoiceCommand {
    public static VoiceCommand SummonMonsterAtk = new VoiceCommand("I summon a monster in attack");
    public static VoiceCommand SummonMonsterDef = new VoiceCommand("I summon a monster in defense");


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
