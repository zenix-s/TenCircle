using Godot;
using System;

public partial class Main : Node2D
{
	public override void _Ready()
	{
		Node container = GetNode<Node>("%SceneContainer");
		SceneManager.Instance.Initialize(container);
		SceneManager.Instance.Load(SceneManager.Scene.Game);
	}
}
