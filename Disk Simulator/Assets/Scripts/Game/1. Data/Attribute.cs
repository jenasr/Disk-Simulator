using System;

namespace Game {
    [Flags]
    public enum Attribute {
        none = 0,
        dark = 1 << 0,
        divine = 1 << 1,
        earth = 1 << 2,
        fire = 1 << 3,
        light = 1 << 4,
        water = 1 << 5,
        wind = 1 << 6,
        all = (1 << 7) - 1
    }
}