using Godot;
using System;

public partial class ScoreManager : Node
{
	public static ScoreManager Instance { get; private set; }

	private const string SCORE_FILE = "user://tappy.txt";

	private int score = 0;
	private int highScore = 0;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = this;

		LoadScoreToFile();
	}

	public override void _ExitTree()
	{
		SaveScoreToFile();
	}

	public static int GetScore()
	{
		return Instance.score;
	}

	public static int GetHighScore()
	{
		return Instance.highScore;
	}

	public static void SetScore(int value)
	{
		Instance.score = value;

		if (Instance.score > Instance.highScore)
		{
			Instance.highScore = Instance.score;
		}

		SignalManager.EmitOnScored();
	}

	public static void ResetScore()
	{
		SetScore(0);
	}

	public static void IncrementScore()
	{
		SetScore(GetScore() + 1);
	}

	private void SaveScoreToFile()
	{
		using FileAccess file = FileAccess.Open(SCORE_FILE, FileAccess.ModeFlags.Write);
		if (file != null)
		{
			file.StoreString(highScore.ToString());
		}

		GD.Print("SAVED");
	}

	private void LoadScoreToFile()
	{
		using FileAccess file = FileAccess.Open(SCORE_FILE, FileAccess.ModeFlags.Read);
		if (file != null)
		{
			highScore = int.Parse(file.GetAsText());
		}

	}
}
