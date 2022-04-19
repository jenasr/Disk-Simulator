using YuGiOh;

public static class CardOptionUtility {
    public static void AddOptions(Game g, CardEntity c, CardActionMenuBehaviour cardActionMenuBehaviour) {
        Player player = g.players[g.currentPlayer];

        // TODO - Get available Options
        if (c.zone != ZoneType.monster && (c.data.validZones & ZoneType.monster) != 0 && player.monsterZone.count < player.monsterZone.capacity) {
            cardActionMenuBehaviour.AddOption("To Monster Zone", ToZoneCardAction.Get(g, c, ZoneType.monster));
        }
        if (c.zone != ZoneType.spell) {
            cardActionMenuBehaviour.AddOption("To Spell Zone", ToZoneCardAction.Get(g, c, ZoneType.spell));
        }

        cardActionMenuBehaviour.AddOption("To Spell Zone 5", ToZoneCardAction.Get(g, c, ZoneType.spell, 4)); // probably don't need this

        cardActionMenuBehaviour.AddOption("To Face Up", SetCardOrientationAction.Get(c, CardOrientation.faceup));
        cardActionMenuBehaviour.AddOption("To Face Down", SetCardOrientationAction.Get(c, CardOrientation.facedown));

        cardActionMenuBehaviour.AddOption("To Graveyard", ToZoneCardAction.Get(g, c, ZoneType.graveyard));
    }
}
