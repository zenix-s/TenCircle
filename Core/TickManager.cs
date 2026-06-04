using Godot;
using System;

public partial class TickManager : Node
{
	public static TickManager Instance { get; private set; }

	[Signal]
	public delegate void TickEventHandler();

	public float TickRate = 1f;
	private float _accumulator = 0f;

	public override void _EnterTree()
	{
		Instance = this;
	}

	public override void _Process(double delta)
	{
		_accumulator += (float)delta;
		while (_accumulator >= TickRate)
		{
			_accumulator -= TickRate;
			EmitSignal(SignalName.Tick);
		}
	}
}
