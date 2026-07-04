using System.Collections.Generic;


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
