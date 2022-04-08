using System;
namespace YuGiOh {
    [Flags]
    public enum CardType {
        none = 0,
        monster = 1 << 0,
        spell = 1 << 1,
        trap = 1 << 2,
        all = (1 << 3) - 1
    }
}