using System;


namespace YuGiOh {
    [Flags]
    public enum CardOrientation {
        faceup = 1 << 0,
        facedown = 1 << 1,
        faceupSideways = 1 << 2,
        facedownsideways = 1 << 3,
    }
}