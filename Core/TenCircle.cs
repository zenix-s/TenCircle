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
}

public class GameStateManager
{
    public Dictionary<ManaType, Mana> RefinedMana { get; private set; }
    public Dictionary<string, int> UpgradeLevels { get; private set; } = new();

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

    public override void _EnterTree()
    {
        Instance = this;
        UpgradeManager = new UpgradeManager();

        GameStateManager = new GameStateManager();
    }

    public override void _Ready()
    {
    }

    public override void _Process(double delta)
    {
    }

    public float GetStat(UpgradeType type, UpgradeTargetType targetType, ManaType? targetManaType = null)
    {
        float stat = 0;

        foreach (UpgradeDefinition upgrade in UpgradeManager.Definitions)
        {
            if (upgrade.Type != type || upgrade.TargetType != targetType)
                continue;

            if (targetType == UpgradeTargetType.Specific && upgrade.TargetManaType != targetManaType)
                continue;

            int level = GetUpgradeLevel(upgrade);
            if (level > 0)
                stat += upgrade.EffectValues[level - 1];
        }

        return stat;
    }

    public int GetUpgradeLevel(UpgradeDefinition definition)
    {
        return GameStateManager.UpgradeLevels.GetValueOrDefault(definition.Id, 0);
    }

    public void LevelUpUpgrade(UpgradeDefinition definition)
    {
        int currentLevel = GetUpgradeLevel(definition);
        if (currentLevel < definition.EffectValues.Count)
            GameStateManager.UpgradeLevels[definition.Id] = currentLevel + 1;
    }

    public void AddMana()
    {
        GameStateManager.RefinedMana[ManaType.Unrefined].Amount += 10;
    }

    public void RefineMana()
    {
        float mana = GameStateManager.RefinedMana[ManaType.Unrefined].Amount;
        GameStateManager.RefinedMana[ManaType.Unrefined].Amount = 0;

        foreach (var refined in GameStateManager.RefinedMana.Values)
        {
            if (refined.Type is ManaType.Unrefined)
                continue;

            // float refinedAmount = mana * StatManager.GetStat(UpgradeType.Efficiency, UpgradeTargetType.General);
            float GeneralEfficiency = GetStat(UpgradeType.Efficiency, UpgradeTargetType.General);
            float SpecificEfficiency = GetStat(UpgradeType.Efficiency, UpgradeTargetType.Specific, refined.Type);
            float totalEfficiency = GeneralEfficiency + SpecificEfficiency;
            float refinedAmount = mana * totalEfficiency;

            refined.Amount += refinedAmount;
        }
    }
}
