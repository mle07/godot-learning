using Godot;
using System;

public partial class Hud : Control
{
	[Export]
	Label score;
	public override void _Ready()
	{
		SignalManager.Instance.OnScored += OnScoredEventHandler;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override void _ExitTree()
	{
		SignalManager.Instance.OnScored -= OnScoredEventHandler;
	}

	private void OnScoredEventHandler()
	{
		score.Text = ScoreManager.GetScore().ToString("0000");
	}
}
