using System.Collections.Generic;

public class UpgradeDefinition
{
    public string Id { get; set; }
    public Dictionary<StatType, List<float>> Effects { get; set; } = new();
}

public class UpgradeManager
{
    public List<UpgradeDefinition> Definitions { get; private set; } = [
        new UpgradeDefinition
        {
            Id = "efficiency_general",
            Effects = new() { [StatType.ManaEfficiencyGeneral] = [0.02f, 0.05f] }
        },
        new UpgradeDefinition
        {
            Id = "efficiency_fire",
            Effects = new() { [StatType.ManaEfficiencyFire] = [0.05f, 0.1f, 0.15f] }
        },
        new UpgradeDefinition
        {
            Id = "efficiency_water",
            Effects = new() { [StatType.ManaEfficiencyWater] = [0.05f, 0.1f, 0.15f] }
        }
    ];

    public static string StatDisplayName(StatType type) => type switch
    {
        StatType.ManaEfficiencyGeneral => "General Efficiency",
        StatType.ManaEfficiencyFire => "Fire Efficiency",
        StatType.ManaEfficiencyWater => "Water Efficiency",
        _ => "Error"
    };
}
