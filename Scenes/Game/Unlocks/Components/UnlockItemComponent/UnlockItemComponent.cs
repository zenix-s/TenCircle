using Godot;

public partial class UnlockItemComponent : HBoxContainer
{
	private ManaTypeUnlockDefinition _targetUnlock;
	private bool _isInitialized = false;

	public Label LblUnlockName { get; private set; }
	public Button BtnUnlock { get; private set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		LblUnlockName = GetNode<Label>("%LblUnlockName");
		BtnUnlock = GetNode<Button>("%BtnUnlock");
		BtnUnlock.Pressed += OnBtnUnlockPressed;
	}

	public void Initialize(ManaTypeUnlockDefinition unlock)
	{
		_targetUnlock = unlock;
		_isInitialized = true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (!_isInitialized)
			return;

		bool unlocked = TenCircle.Instance.IsManaTypeUnlocked(_targetUnlock.Type);

		LblUnlockName.Text = unlocked
			? $"{_targetUnlock.Type} (Desbloqueado)"
			: $"{_targetUnlock.Type} (Coste: {_targetUnlock.Cost})";
		BtnUnlock.Disabled = unlocked;
	}

	private void OnBtnUnlockPressed()
	{
		TenCircle.Instance.TryUnlockManaType(_targetUnlock);
	}
}
