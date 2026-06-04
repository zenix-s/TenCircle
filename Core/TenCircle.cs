using Godot;
using System;
using System.Collections.Generic;

public enum RefinedManaType
{
    Fire,
    Water
}

public class RefinedMana
{
    public RefinedManaType Type { get; }
    public float Amount { get; set; }

    /// <summary> 
    /// Mana multiplier when refining.
    /// </summary>
    public float RefineEfficiency { get; set; }

    public RefinedMana(RefinedManaType type, float amount)
    {
        Type = type;
        Amount = amount;
    }
} 

public partial class TenCircle : Node
{
    public static TenCircle Instance { get; private set; }

    public Dictionary<RefinedManaType, RefinedMana> RefinedMana { get; private set; } = new()
    {
        [RefinedManaType.Fire] = new RefinedMana(RefinedManaType.Fire, 0) { RefineEfficiency = 0.1f },
        [RefinedManaType.Water] = new RefinedMana(RefinedManaType.Water, 0) { RefineEfficiency = 0.1f }
    };

    // TODO: Crear clase BigNumber
    public float Mana { get; set; } = 0;

    public override void _Ready()
    {
        Instance = this;
    }

    public override void _Process(double delta)
    {
    }

    public void RefineMana()
    {
        float mana = Mana;
        Mana = 0;

        foreach (var refined in RefinedMana.Values)
        {
            float refinedAmount = mana * refined.RefineEfficiency;
            refined.Amount += refinedAmount;
        }
    }
}
