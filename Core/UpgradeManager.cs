using System.Collections.Generic;


public class UpgradeDefinition
{
    public string Id { get; set; }
    public UpgradeType Type { get; set; }
    public UpgradeTargetType TargetType { get; set; }
    public ManaType? TargetManaType { get; set; }
    public List<float> EffectValues { get; set; } = [];
}

public class UpgradeManager
{
    public List<UpgradeDefinition> Definitions { get; private set; } = [
        new UpgradeDefinition()
        {
            Id = "efficiency_general",
            Type = UpgradeType.Efficiency,
            TargetType = UpgradeTargetType.General,
            EffectValues = [0.02f, 0.05f]
        },
        new UpgradeDefinition()
        {
            Id = "efficiency_fire",
            Type = UpgradeType.Efficiency,
            TargetType = UpgradeTargetType.Specific,
            TargetManaType = ManaType.Fire,
            EffectValues = [0.05f, 0.1f, 0.15f]
        },
        new UpgradeDefinition()
        {
            Id = "efficiency_water",
            Type = UpgradeType.Efficiency,
            TargetType = UpgradeTargetType.Specific,
            TargetManaType = ManaType.Water,
            EffectValues = [0.05f, 0.1f, 0.15f]
        }
    ];
}
