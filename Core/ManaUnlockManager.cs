using System.Collections.Generic;

public class ManaTypeUnlockDefinition
{
    public ManaType Type { get; set; }
    public float Cost { get; set; }
}

public class ManaUnlockManager
{
    public List<ManaTypeUnlockDefinition> Definitions { get; private set; } = [
        new ManaTypeUnlockDefinition { Type = ManaType.Fire, Cost = 50f },
        new ManaTypeUnlockDefinition { Type = ManaType.Water, Cost = 50f }
    ];
}
