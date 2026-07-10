using Godot;
using System;

public partial class UpgradeItemComponent : HBoxContainer
{
	private UpgradeDefinition _targetUpgrade;
	private bool _isInitialized = false;

	public Label LblUpgradeName { get; private set; }
	public Button BtnUpgrade { get; private set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		LblUpgradeName = GetNode<Label>("%LblUpgradeName");
		BtnUpgrade = GetNode<Button>("%BtnUpgrade");
		BtnUpgrade.Pressed += OnBtnUpgradePressed;
	}

	public void Initialize(UpgradeDefinition upgrade)
	{
		_targetUpgrade = upgrade;
		_isInitialized = true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (!_isInitialized)
			return;

		string upgradeTypeText = _targetUpgrade.Type switch
		{
			UpgradeType.Efficiency => "Efficiency",
			_ => "Error"
		};

		string targetTypeText = _targetUpgrade.TargetType switch
		{
			UpgradeTargetType.General => "General",
			UpgradeTargetType.Specific => _targetUpgrade.TargetManaType switch
			{
				ManaType.Fire => "Fire Mana",
				ManaType.Water => "Water Mana",
				_ => "Error"
			},
			_ => "Error"
		};

		string targetLevelText = TenCircle.Instance.GetUpgradeLevel(_targetUpgrade).ToString();
		string targetMaxLevelText = _targetUpgrade.EffectValues.Count.ToString();

		LblUpgradeName.Text = $"{upgradeTypeText} - {targetTypeText} (Level {targetLevelText}/{targetMaxLevelText})";

	}

	private void OnBtnUpgradePressed()
	{
		TenCircle.Instance.LevelUpUpgrade(_targetUpgrade);
	}
}
