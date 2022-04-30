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
        AddOrientationOption(CardOrientation.faceup, "To Face Up");
        AddOrientationOption(CardOrientation.facedown, "To Face Down");
        AddOrientationOption(CardOrientation.faceupSideways, "To Face Up Sideways");
        AddOrientationOption(CardOrientation.facedownsideways, "To Face Down Sideways");


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

            cardActionMenuBehaviour.AddOption(txt, ToZoneCardAction.Get(g, c, z));
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
