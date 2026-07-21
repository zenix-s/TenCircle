using Godot;
using System;

public partial class GameScene : Node2D
{
	public Label LblMana { get; private set; }
	public Button BtnToggleMenu { get; private set; }
	public Control Body { get; private set; }

	public Button BtnRefineMana { get; private set; }
	public Label LblFireMana { get; private set; }
	public Label LblWaterMana { get; private set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		LblMana = GetNode<Label>("%LblMana");

		Body = GetNode<Control>("%Body");
		BtnToggleMenu = GetNode<Button>("%BtnToggleMenu");
		BtnToggleMenu.Pressed += OnBtnToggleMenuPressed;

		BtnRefineMana = GetNode<Button>("%BtnRefineMana");
		BtnRefineMana.Pressed += OnBtnRefineManaPressed;
		LblFireMana = GetNode<Label>("%LblFireMana");
		LblWaterMana = GetNode<Label>("%LblWaterMana");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		LblMana.Text = $"Mana: {TenCircle.Instance.GameStateManager.RefinedMana[ManaType.Unrefined].Amount}";

		LblFireMana.Visible = TenCircle.Instance.IsManaTypeUnlocked(ManaType.Fire);
		if (LblFireMana.Visible)
			LblFireMana.Text = $"Fire Mana: {TenCircle.Instance.GameStateManager.RefinedMana[ManaType.Fire].Amount}";

		LblWaterMana.Visible = TenCircle.Instance.IsManaTypeUnlocked(ManaType.Water);
		if (LblWaterMana.Visible)
			LblWaterMana.Text = $"Water Mana: {TenCircle.Instance.GameStateManager.RefinedMana[ManaType.Water].Amount}";
	}

	private void OnBtnToggleMenuPressed()
	{
		Body.Visible = !Body.Visible;
	}

	private void OnBtnRefineManaPressed()
	{
		TenCircle.Instance.RefineMana();
	}
}
