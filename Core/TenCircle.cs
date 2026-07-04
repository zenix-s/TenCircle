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

public partial class TenCircle : Node
{
    public static TenCircle Instance { get; private set; }

    public ManaManager ManaManager { get; private set; }
    public UpgradeManager UpgradeManager { get; private set; }

    public override void _EnterTree()
    {
        Instance = this;
        UpgradeManager = new UpgradeManager();
        ManaManager = new ManaManager();
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

        foreach (Upgrade upgrade in UpgradeManager.Upgrades)
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

    public void AddMana()
    {
        ManaManager.RefinedMana[ManaType.Unrefined].Amount += 10;
    }

    public void RefineMana()
    {
        float mana = ManaManager.RefinedMana[ManaType.Unrefined].Amount;
        ManaManager.RefinedMana[ManaType.Unrefined].Amount = 0;

        foreach (var refined in ManaManager.RefinedMana.Values)
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
