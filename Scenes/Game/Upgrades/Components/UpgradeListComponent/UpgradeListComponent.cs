using Godot;
using System;

public partial class UpgradeListComponent : VBoxContainer
{
	private bool _isInitialized = false;

	private UpgradeItemComponent _upgradeItemScene;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_upgradeItemScene = GetNode<UpgradeItemComponent>("%UpgradeItemComponentScene");

		foreach (UpgradeDefinition upgrade in TenCircle.Instance.UpgradeManager.Definitions)
		{
			UpgradeItemComponent upgradeItem = _upgradeItemScene.Duplicate() as UpgradeItemComponent;
			upgradeItem.Initialize(upgrade);
			AddChild(upgradeItem);
		}

		_upgradeItemScene.QueueFree();
		_isInitialized = true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
