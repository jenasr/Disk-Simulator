using System;

namespace YuGiOh {
    [Flags]
    public enum Zone {
        none = 0,
        deck = 1 << 0,
        hand = 1 << 1,
        graveyard = 1 << 2,
        monster = 1 << 3,
        spell = 1 << 4,
        field = 1 << 5,
        all = (1 << 6) - 1
    }

    public static class ZoneExtensions {
        public static CardOrientation ValidOrientations(this Zone z) {
            CardOrientation result = 0;
            const Zone validFaceupZones = Zone.monster | Zone.graveyard | Zone.field | Zone.spell;
            const Zone validFacedownZones = Zone.spell | Zone.hand; // monster zone?
            const Zone validSidewaysZones = Zone.monster;
            const Zone validFacedownSidewaysZones = Zone.monster;

            if ((z & validFaceupZones) != 0) {
                result |= CardOrientation.faceup;
            }
            if ((z & validFacedownZones) != 0) {
                result |= CardOrientation.facedown;
            }
            if ((z & validSidewaysZones) != 0) {
                result |= CardOrientation.faceupideways;
            }
            if ((z & validFacedownSidewaysZones) != 0) {
                result |= CardOrientation.facedownsideways;
            }

            return result;
        }
    }
}