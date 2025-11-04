using Godot;
using System;

public partial class TransitionScene : Control
{
	[Export]
	Timer timer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		timer.Timeout += OnTimeOut;
	}

	private void OnTimeOut()
	{
		GetTree().ChangeSceneToPacked(GameManager.GetNextScene());
	}

}
