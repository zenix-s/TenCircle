using System.Collections.Generic;

public class ManaManager
{
    public Dictionary<ManaType, Mana> RefinedMana { get; private set; } = new()
    {
        [ManaType.Unrefined] = new Mana()
        {
            Type = ManaType.Unrefined,
            Amount = 0,
        },
        [ManaType.Fire] = new Mana()
        {
            Type = ManaType.Fire,
            Amount = 0,
        },
        [ManaType.Water] = new Mana()
        {
            Type = ManaType.Water,
            Amount = 0,
        }
    };
}
