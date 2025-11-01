using Godot;
using System;

public partial class Game : Node2D
{
	[Export]
	private PackedScene _gemScene;
	[Export]
	private Timer _timer;
	[Export]
	private int _score = 0;
	[Export]
	private Label _scoreLabel;
	[Export]
	private AudioStreamPlayer _music;
	[Export]
	private AudioStreamPlayer2D _scoreEffect;
	private static readonly AudioStream EXPLODE_SOUND = GD.Load<AudioStream>("res://assets/explode.wav");
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_timer.Timeout += SpawnGem;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void SpawnGem()
	{
		var gem = _gemScene.Instantiate<Gem>();
		AddChild(gem);

		Rect2 vpr = GetViewportRect();
		float randomPosX = (float)GD.RandRange(vpr.Position.X, vpr.End.X);
		gem.Position = new Vector2(randomPosX, -100);

		gem.OnScored += OnScored;

		gem.OnGemHitOffScreen += GameOver;
	}

	private void OnScored()
	{
		_scoreEffect.Play();
		_score += 1;
		_scoreLabel.Text = $"Score: {_score}";
	}

	private void GameOver()
	{
		foreach (Node node in GetChildren())
		{
			node.SetProcess(false);
		}

		_timer.Stop();
		_music.Stop();

		_scoreEffect.Stop();

		_scoreEffect.Stream = EXPLODE_SOUND;
		_scoreEffect.Play();
	}
}

