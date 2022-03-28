using System;

namespace Game {
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
}