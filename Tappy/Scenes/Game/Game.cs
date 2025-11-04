using Godot;
using System;

public partial class Game : Node2D
{
	[Export]
	Marker2D spawnUpper;
	[Export]
	Marker2D spawnLower;
	[Export]
	PackedScene pipes;
	[Export]
	Node2D pipesHolder;
	[Export]
	Timer spawnPipeTimer;
	[Export]
	Plane plane;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		spawnPipeTimer.Timeout += SpawnPipe;
		SignalManager.Instance.OnPlaneDied += GameOver;

		ScoreManager.ResetScore();
		SpawnPipe();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("quit"))
		{
			GameManager.LoadMain();
		}
	}

	public override void _ExitTree()
	{
		SignalManager.Instance.OnPlaneDied -= GameOver;
	}

	public float GetSpawnY()
	{
		return (float)GD.RandRange(spawnUpper.Position.Y, spawnLower.Position.Y);
	}

	private void SpawnPipe()
	{
		Pipes np = pipes.Instantiate<Pipes>();
		pipesHolder.AddChild(np);

		np.GlobalPosition = new Vector2(spawnUpper.Position.X, GetSpawnY());
	}

	private void GameOver()
	{
		spawnPipeTimer.Stop();
	}
}
