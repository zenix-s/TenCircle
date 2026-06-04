using Godot;
using System.Collections.Generic;

public partial class SceneManager : Node
{
	public enum Scene { Game }

	private static readonly Dictionary<Scene, string> Paths = new()
	{
		[Scene.Game] = "res://Scenes/Game/GameScene.tscn"
	};

	public static SceneManager Instance { get; private set; }

	private Node _container;

	public override void _EnterTree()
	{
		Instance = this;
	}

	public void Initialize(Node container)
	{
		_container = container;
	}

	public void Load(Scene scene)
	{
		foreach (Node child in _container.GetChildren())
			child.QueueFree();

		PackedScene packed = GD.Load<PackedScene>(Paths[scene]);
		_container.AddChild(packed.Instantiate());
	}
}
