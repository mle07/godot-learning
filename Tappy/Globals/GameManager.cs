using Godot;

public partial class GameManager : Node
{
	public static GameManager Instance { get; private set; }

	private PackedScene gameScene = GD.Load<PackedScene>("res://Scenes/Game/Game.tscn");
	private PackedScene mainScene = GD.Load<PackedScene>("res://Scenes/Main/Main.tscn");
	private PackedScene transitionScene = GD.Load<PackedScene>("res://Scenes/TransitionScene/TransitionScene.tscn");
	private PackedScene nextScene;

	[Export]
	public float speed = 120f;

	public static void LoadMain()
	{
		LoadNextScene(Instance.mainScene);
	}

	public static void LoadGame()
	{
		LoadNextScene(Instance.gameScene);
	}

	public static PackedScene GetNextScene()
	{
		return Instance.nextScene;
	}

	public static void LoadNextScene(PackedScene ns)
	{
		Instance.nextScene = ns;
		Instance.GetTree().ChangeSceneToPacked(Instance.transitionScene);
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = this;
	}
}
