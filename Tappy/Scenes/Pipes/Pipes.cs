using Godot;
using System;

public partial class Pipes : Node2D
{

	VisibleOnScreenNotifier2D visibleOnScreenNotifier2D;

	[Export] private Area2D upperPipe;
	[Export] private Area2D lowerPipe;
	[Export] private Area2D laser;
	[Export] private AudioStreamPlayer scoreSound;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		visibleOnScreenNotifier2D = GetNode<VisibleOnScreenNotifier2D>("VisibleOnScreenNotifier2D");
		visibleOnScreenNotifier2D.ScreenExited += OnScreenExited;

		upperPipe.BodyEntered += OnPipeBodyEntered;
		lowerPipe.BodyEntered += OnPipeBodyEntered;

		laser.BodyEntered += OnLaserBodyEntered;

		SignalManager.Instance.OnPlaneDied += OnPlaneDied;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Position -= new Vector2(GameManager.Instance.speed * (float)delta, 0);
	}

	public override void _ExitTree()
	{
		SignalManager.Instance.OnPlaneDied -= OnPlaneDied;
	}


	private void OnScreenExited()
	{
		QueueFree();
	}

	private void OnPipeBodyEntered(Node2D body)
	{
		if (body is Plane)
		{
			(body as Plane).Die();
		}
	}

	private void OnLaserBodyEntered(Node2D body)
	{
		ScoreManager.IncrementScore();
		scoreSound.Play();
	}

	private void OnPlaneDied()
	{
		SetProcess(false);
	}
}
