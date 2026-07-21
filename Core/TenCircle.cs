using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public enum ManaType
{
    Unrefined,
    Fire,
    Water
}

public enum StatType
{
    ManaEfficiencyGeneral,
    ManaEfficiencyFire,
    ManaEfficiencyWater,
}

public class Mana
{
    public ManaType Type { get; set; }
    public float Amount { get; set; }
}

public class GameStateManager
{
    public Dictionary<ManaType, Mana> RefinedMana { get; private set; }
    public Dictionary<string, int> UpgradeLevels { get; private set; } = [];
    public HashSet<ManaType> UnlockedManaTypes { get; private set; } = new() { ManaType.Unrefined };

    public GameStateManager()
    {
        RefinedMana = new Dictionary<ManaType, Mana>
        {
            { ManaType.Unrefined, new Mana { Type = ManaType.Unrefined, Amount = 0 } },
            { ManaType.Fire, new Mana { Type = ManaType.Fire, Amount = 0 } },
            { ManaType.Water, new Mana { Type = ManaType.Water, Amount = 0 } }
        };
    }
}

public partial class TenCircle : Node
{
    public static TenCircle Instance { get; private set; }

    public GameStateManager GameStateManager { get; private set; }

    public UpgradeManager UpgradeManager { get; private set; }

    public ManaUnlockManager ManaUnlockManager { get; private set; }

    private const float UnlockBaseEfficiency = 0.1f;

    public override void _EnterTree()
    {
        Instance = this;
        UpgradeManager = new UpgradeManager();
        ManaUnlockManager = new ManaUnlockManager();

        GameStateManager = new GameStateManager();
    }

    public override void _Ready()
    {
    }

    public override void _Process(double delta)
    {
    }

    public float GetStat(StatType statType)
    {
        float stat = 0;

        foreach (UpgradeDefinition upgrade in UpgradeManager.Definitions)
        {
            if (!upgrade.Effects.TryGetValue(statType, out List<float> values))
                continue;

            int level = GetUpgradeLevel(upgrade);
            if (level > 0 && level <= values.Count)
                stat += values[level - 1];
        }

        return stat;
    }

    public int GetUpgradeLevel(UpgradeDefinition definition)
    {
        return GameStateManager.UpgradeLevels.GetValueOrDefault(definition.Id, 0);
    }

    public void LevelUpUpgrade(UpgradeDefinition definition)
    {
        int maxLevel = definition.Effects.Values.Max(v => v.Count);
        int currentLevel = GetUpgradeLevel(definition);
        if (currentLevel < maxLevel)
            GameStateManager.UpgradeLevels[definition.Id] = currentLevel + 1;
    }

    public void AddMana(float amount)
    {
        GameStateManager.RefinedMana[ManaType.Unrefined].Amount += amount;
    }

    public void RefineMana()
    {
        float mana = GameStateManager.RefinedMana[ManaType.Unrefined].Amount;
        GameStateManager.RefinedMana[ManaType.Unrefined].Amount = 0;

        foreach (var refined in GameStateManager.RefinedMana.Values)
        {
            if (refined.Type is ManaType.Unrefined)
                continue;

            if (!IsManaTypeUnlocked(refined.Type))
                continue;

            float generalEfficiency = GetStat(StatType.ManaEfficiencyGeneral);
            float specificEfficiency = GetStat(SpecificManaEfficiencyStat(refined.Type));
            refined.Amount += mana * (UnlockBaseEfficiency + generalEfficiency + specificEfficiency);
        }
    }

    public bool IsManaTypeUnlocked(ManaType type)
    {
        return GameStateManager.UnlockedManaTypes.Contains(type);
    }

    public bool TryUnlockManaType(ManaTypeUnlockDefinition definition)
    {
        if (IsManaTypeUnlocked(definition.Type))
            return false;

        Mana unrefined = GameStateManager.RefinedMana[ManaType.Unrefined];
        if (unrefined.Amount < definition.Cost)
            return false;

        unrefined.Amount -= definition.Cost;
        GameStateManager.UnlockedManaTypes.Add(definition.Type);
        return true;
    }

    private static StatType SpecificManaEfficiencyStat(ManaType type) => type switch
    {
        ManaType.Fire => StatType.ManaEfficiencyFire,
        ManaType.Water => StatType.ManaEfficiencyWater,
        _ => throw new ArgumentOutOfRangeException(nameof(type))
    };
}
