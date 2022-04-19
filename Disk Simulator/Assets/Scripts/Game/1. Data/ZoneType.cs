using System;

namespace YuGiOh {
    [Flags]
    public enum ZoneType {
        none = 0,
        deck = 1 << 0,
        hand = 1 << 1,
        graveyard = 1 << 2,
        monster = 1 << 3,
        spell = 1 << 4,
        field = 1 << 5,
        banished = 1 << 6,
        all = (1 << 7) - 1
    }

    public static class ZoneTypeExtensions {
        public static CardOrientation ValidOrientations(this ZoneType z) {
            CardOrientation result = 0;
            const ZoneType validFaceupZones = ZoneType.monster | ZoneType.graveyard | ZoneType.banished | ZoneType.field | ZoneType.spell;
            const ZoneType validFacedownZones = ZoneType.spell | ZoneType.hand; // monster zone?
            const ZoneType validSidewaysZones = ZoneType.monster;
            const ZoneType validFacedownSidewaysZones = ZoneType.monster;

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