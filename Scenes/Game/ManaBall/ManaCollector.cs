using Godot;

public partial class ManaCollector : Node2D
{
	private const float CollectRadius = 50f;
	private static readonly Color IndicatorColor = new(1f, 1f, 1f, 0.25f);

	private bool _isPressed;

	public override void _UnhandledInput(InputEvent @event)
	{
		switch (@event)
		{
			case InputEventScreenTouch touch:
				SetPressed(touch.Pressed, touch.Position);
				break;
			case InputEventScreenDrag drag when _isPressed:
				MoveTo(drag.Position);
				break;
			case InputEventMouseButton mouseButton when mouseButton.ButtonIndex == MouseButton.Left:
				SetPressed(mouseButton.Pressed, mouseButton.Position);
				break;
			case InputEventMouseMotion mouseMotion when _isPressed:
				MoveTo(mouseMotion.Position);
				break;
		}
	}

	public override void _Process(double delta)
	{
		if (!_isPressed)
			return;

		foreach (Node child in GetParent().GetChildren())
		{
			if (child is not ManaBall ball)
				continue;

			if (ball.Position.DistanceTo(Position) <= CollectRadius)
			{
				TenCircle.Instance.AddMana(ball.Data.ManaValue);
				ball.QueueFree();
			}
		}
	}

	public override void _Draw()
	{
		if (_isPressed)
			DrawCircle(Vector2.Zero, CollectRadius, IndicatorColor);
	}

	private void SetPressed(bool pressed, Vector2 position)
	{
		_isPressed = pressed;
		if (_isPressed)
			MoveTo(position);
		else
			QueueRedraw();
	}

	private void MoveTo(Vector2 position)
	{
		Position = position;
		QueueRedraw();
	}
}
