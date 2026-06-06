using Godot;
using System;
using System.Collections.Generic;

public enum ManaType
{
    Unrefined,
    Fire,
    Water
}

public enum UpgradeType
{
    Efficiency,
}

public enum UpgradeTargetType
{
    General,
    Specific
}

public class Mana
{
    public ManaType Type { get; set; }
    public float Amount { get; set; }
    public float RefineEfficiency { get; set; }

    public int Level { get; set; }
}

public class Upgrade
{
    public UpgradeType Type { get; set; }
    public UpgradeTargetType TargetType { get; set; }
    public ManaType? TargetManaType { get; set; }
    public int Level { get; set; } = 0;
    public List<float> EffectValues { get; set; } = [];

    public void LevelUp()
    {
        if (Level < EffectValues.Count)
            Level++;
    }
}

public class ManaManager
{
    public Dictionary<ManaType, Mana> RefinedMana { get; private set; } = new()
    {
        [ManaType.Unrefined] = new Mana()
        {
            Type = ManaType.Unrefined,
            Amount = 0,
            RefineEfficiency = 0
        },
        [ManaType.Fire] = new Mana()
        {
            Type = ManaType.Fire,
            Amount = 0,
            RefineEfficiency = 0.1f
        },
        [ManaType.Water] = new Mana()
        {
            Type = ManaType.Water,
            Amount = 0,
            RefineEfficiency = 0.1f
        }
    };

    public void RefineMana()
    {
        float mana = RefinedMana[ManaType.Unrefined].Amount;
        RefinedMana[ManaType.Unrefined].Amount = 0;

        foreach (var refined in RefinedMana.Values)
        {
            if (refined.Type is ManaType.Unrefined)
                continue;

            float refinedAmount = mana * refined.RefineEfficiency;
            refined.Amount += refinedAmount;
        }
    }
}

public class StatManager(UpgradeManager upgradeManager)
{
    public float GetStat(UpgradeType type, UpgradeTargetType targetType, ManaType? targetManaType = null)
    {
        float stat = 0;

        foreach (Upgrade upgrade in upgradeManager.Upgrades)
        {
            if (upgrade.Type != type || upgrade.TargetType != targetType)
                continue;

            if (targetType == UpgradeTargetType.Specific && upgrade.TargetManaType != targetManaType)
                continue;

            if (upgrade.Level > 0)
                stat += upgrade.EffectValues[upgrade.Level - 1];
        }

        return stat;
    }
}

public class UpgradeManager
{
    public List<Upgrade> Upgrades { get; private set; } = [
        new Upgrade()
        {
            Type = UpgradeType.Efficiency,
            TargetType = UpgradeTargetType.General,
            EffectValues = [0.02f, 0.05f]
        },
        new Upgrade()
        {
            Type = UpgradeType.Efficiency,
            TargetType = UpgradeTargetType.Specific,
            TargetManaType = ManaType.Fire,
            EffectValues = [0.05f, 0.1f, 0.15f]
        },
        new Upgrade()
        {
            Type = UpgradeType.Efficiency,
            TargetType = UpgradeTargetType.Specific,
            TargetManaType = ManaType.Water,
            EffectValues = [0.05f, 0.1f, 0.15f]
        }
    ];
}



public partial class TenCircle : Node
{
    public static TenCircle Instance { get; private set; }

    public ManaManager ManaManager { get; private set; }
    public UpgradeManager UpgradeManager { get; private set; }
    public StatManager StatManager { get; private set; }

    public override void _EnterTree()
    {
        Instance = this;
        UpgradeManager = new UpgradeManager();
        ManaManager = new ManaManager();
        StatManager = new StatManager(UpgradeManager);
    }

    public override void _Ready()
    {
    }

    public override void _Process(double delta)
    {
    }

    public void AddMana()
    {
        ManaManager.RefinedMana[ManaType.Unrefined].Amount += 10;
    }

}
