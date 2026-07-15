using Godot;
using System;
using System.Linq;

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

		string upgradeStatsText = string.Join(", ", _targetUpgrade.Effects.Keys.Select(UpgradeManager.StatDisplayName));
		string targetLevelText = TenCircle.Instance.GetUpgradeLevel(_targetUpgrade).ToString();
		string targetMaxLevelText = _targetUpgrade.Effects.Values.Max(v => v.Count).ToString();

		LblUpgradeName.Text = $"{upgradeStatsText} (Level {targetLevelText}/{targetMaxLevelText})";

	}

	private void OnBtnUpgradePressed()
	{
		TenCircle.Instance.LevelUpUpgrade(_targetUpgrade);
	}
}
