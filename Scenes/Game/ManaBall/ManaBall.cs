using Godot;

public partial class ManaBall : Node2D
{
	public Vector2 Velocity = Vector2.Zero;
	public ManaBallData Data { get; set; } = new();

	private const float Radius = 16f;
	private static readonly Color BallColor = new(0.4f, 0.8f, 1f);

	public override void _Process(double delta)
	{
		Position += Velocity * (float)delta;

		Rect2 bounds = GetViewport().GetVisibleRect().Grow(Radius);
		if (!bounds.HasPoint(Position))
			QueueFree();
	}

	public override void _Draw()
	{
		DrawCircle(Vector2.Zero, Radius, BallColor);
	}
}
