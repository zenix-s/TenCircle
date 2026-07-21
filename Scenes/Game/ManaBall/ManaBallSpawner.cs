using Godot;

public partial class ManaBallSpawner : Node2D
{
	private const float BallSpeed = 200f;

	private static readonly PackedScene ManaBallScene =
		GD.Load<PackedScene>("res://Scenes/Game/ManaBall/ManaBall.tscn");

	public override void _Ready()
	{
		TickManager.Instance.Tick += OnTick;
	}

	public override void _ExitTree()
	{
		if (TickManager.Instance != null)
			TickManager.Instance.Tick -= OnTick;
	}

	private void OnTick()
	{
		Rect2 rect = GetViewport().GetVisibleRect();
		int edge = GD.RandRange(0, 3);

		Vector2 start = EdgePoint(rect, edge);
		Vector2 end = EdgePoint(rect, OppositeEdge(edge));

		ManaBall ball = ManaBallScene.Instantiate<ManaBall>();
		ball.Position = start;
		ball.Velocity = (end - start).Normalized() * BallSpeed;

		GetParent().AddChild(ball);
	}

	private static int OppositeEdge(int edge) => edge switch
	{
		0 => 1,
		1 => 0,
		2 => 3,
		_ => 2
	};

	private static Vector2 EdgePoint(Rect2 rect, int edge)
	{
		float x = (float)GD.RandRange(rect.Position.X, rect.End.X);
		float y = (float)GD.RandRange(rect.Position.Y, rect.End.Y);

		return edge switch
		{
			0 => new Vector2(rect.Position.X, y),
			1 => new Vector2(rect.End.X, y),
			2 => new Vector2(x, rect.Position.Y),
			_ => new Vector2(x, rect.End.Y)
		};
	}
}
