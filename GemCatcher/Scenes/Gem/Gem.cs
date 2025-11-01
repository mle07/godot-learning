using Godot;
using System;

public partial class Gem : Area2D
{
	[Export]
	float _speed = 100f;
	[Signal]
	public delegate void OnScoredEventHandler();
	[Signal]
	public delegate void OnGemHitOffScreenEventHandler();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AreaEntered += OnAreaEntered;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Position += new Vector2(0, _speed * (float)delta);

		CheckHitBottom();
	}

	private void CheckHitBottom()
	{

		if (Position.Y > GetViewportRect().End.Y + 100)
		{
			EmitSignal(SignalName.OnGemHitOffScreen);
			SetProcess(false);
			QueueFree();
		}
	}

	private void OnAreaEntered(Area2D area)
	{
		GD.Print("SCORED!");
		EmitSignal(SignalName.OnScored);
		QueueFree();
	}
}