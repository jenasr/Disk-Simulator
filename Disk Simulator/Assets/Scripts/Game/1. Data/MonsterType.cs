using System;
namespace Game {
    [Flags]
    public enum MonsterType {
        none = 0,
        Aqua = 1 << 0,
        Beast = 1 << 1,
        BeastWarrior = 1 << 2,
        Cyberse = 1 << 3,
        Dinosaur = 1 << 4,
        DivineBeast = 1 << 5,
        Dragon = 1 << 6,
        Fairy = 1 << 7,
        Fiend = 1 << 8,
        Fish = 1 << 9,
        Insect = 1 << 10,
        Machine = 1 << 11,
        Plant = 1 << 12,
        Psychic = 1 << 13,
        Pyro = 1 << 14,
        Reptile = 1 << 15,
        Rock = 1 << 16,
        SeaSerpent = 1 << 17,
        Spellcaster = 1 << 18,
        Thunder = 1 << 19,
        Warrior = 1 << 20,
        WingedBeast = 1 << 21,
        Wyrm = 1 << 22,
        Zombie = 1 << 23,
        all = (1 << 24) - 1
    }
}