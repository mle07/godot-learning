using Godot;
using System;

public partial class GameOver : Control
{
	[Export]
	Label spaceLabel;
	[Export]
	Label gameOverLabel;
	[Export]
	Timer timer;
	[Export]
	AudioStreamPlayer effect;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SignalManager.Instance.OnPlaneDied += OnPlaneDied;
		timer.Timeout += OnTimerTimeOut;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("fly") && spaceLabel.Visible)
		{
			GameManager.LoadMain();
		}
	}

	public override void _ExitTree()
	{
		SignalManager.Instance.OnPlaneDied -= OnPlaneDied;
	}

	private void OnPlaneDied()
	{
		timer.Start();
		Show();
		effect.Play();
	}


	private void OnTimerTimeOut()
	{
		gameOverLabel.Hide();
		spaceLabel.Show();
	}

}
