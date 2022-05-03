using YuGiOh;

public static class CardOptionUtility {
    public static void AddOptions(Game g, CardEntity c, CardActionMenuBehaviour cardActionMenuBehaviour) {
        Player player = g.players[g.currentPlayer];
   
        AddToZoneOption(ZoneType.monster, "To Monster Zone");
        AddToZoneOption(ZoneType.spell, "To Spell Zone");
        AddToZoneOption(ZoneType.graveyard, "To Graveyard");

        if (c.zone == ZoneType.hand) {
            // if in hand, don't set orientation
            return;
        }

        // orientations
        AddOrientationOption(CardOrientation.faceup, c.zone == ZoneType.monster ? "To Attack" : "Activate");
        AddOrientationOption(CardOrientation.facedown, "Set");
        AddOrientationOption(CardOrientation.faceupSideways, "To Defense");
        AddOrientationOption(CardOrientation.facedownsideways, "Set");


        void AddToZoneOption(ZoneType z, string txt) {
            if (c.zone == z) {
                // alread in zone
                return;
            }

            if ((c.data.validZones & z) == 0) {
                // card cannot go to this zone
                return;
            }

            if (player.GetZone(z).count >= player.GetZone(z).capacity) {
                // zone is full
                return;
            }
            GameAction a = ToZoneCardAction.Get(g, c, z);

            if (z == ZoneType.graveyard) {
                a = a.Then(SetCardOrientationAction.Get(c, CardOrientation.faceup));
            }
            cardActionMenuBehaviour.AddOption(txt, a);
        }
        void AddOrientationOption(CardOrientation ori, string txt) {
            if (c.orientation == ori) {
                // alread in orientation
                return;
            }

            if ((c.zone.ValidOrientations() & ori) == 0) {
                // not valid for zone
                return;
            }
            cardActionMenuBehaviour.AddOption(txt, SetCardOrientationAction.Get(c, ori));
        }
    }
}
