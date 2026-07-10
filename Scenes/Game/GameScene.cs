using Godot;
using System;

public partial class GameScene : Node2D
{
	public Label LblMana { get; private set; }
	public Button BtnAddMana { get; private set; }

	public Button BtnRefineMana { get; private set; }
	public Label LblFireMana { get; private set; }
	public Label LblWaterMana { get; private set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		LblMana = GetNode<Label>("%LblMana");
		BtnAddMana = GetNode<Button>("%BtnAddMana");
		BtnAddMana.Pressed += OnBtnAddManaPressed;
		
		BtnRefineMana = GetNode<Button>("%BtnRefineMana");
		BtnRefineMana.Pressed += OnBtnRefineManaPressed;
		LblFireMana = GetNode<Label>("%LblFireMana");
		LblWaterMana = GetNode<Label>("%LblWaterMana");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		LblMana.Text = $"Mana: {TenCircle.Instance.GameStateManager.RefinedMana[ManaType.Unrefined].Amount}";

		LblFireMana.Text = $"Fire Mana: {TenCircle.Instance.GameStateManager.RefinedMana[ManaType.Fire].Amount}";
		LblWaterMana.Text = $"Water Mana: {TenCircle.Instance.GameStateManager.RefinedMana[ManaType.Water].Amount}";
	}

	private void OnBtnAddManaPressed()
	{
		TenCircle tenCircle = TenCircle.Instance;
		tenCircle.AddMana();
	}

	private void OnBtnRefineManaPressed()
	{
		TenCircle.Instance.RefineMana();
	}
}
