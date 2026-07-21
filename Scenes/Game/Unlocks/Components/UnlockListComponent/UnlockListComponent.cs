using Godot;

public partial class UnlockListComponent : VBoxContainer
{
	private UnlockItemComponent _unlockItemScene;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_unlockItemScene = GetNode<UnlockItemComponent>("%UnlockItemComponentScene");

		foreach (ManaTypeUnlockDefinition unlock in TenCircle.Instance.ManaUnlockManager.Definitions)
		{
			UnlockItemComponent unlockItem = _unlockItemScene.Duplicate() as UnlockItemComponent;
			unlockItem.Initialize(unlock);
			AddChild(unlockItem);
		}

		_unlockItemScene.QueueFree();
	}
}
