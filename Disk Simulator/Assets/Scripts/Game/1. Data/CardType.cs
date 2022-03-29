﻿using System;
namespace Game {
    [Flags]
    public enum CardType {
        none = 0,
        monster = 1 << 0,
        magic = 1 << 1,
        trap = 1 << 2,
        all = (1 << 3) - 1
    }
}