using System;
using Godot;

public partial class Animal : RigidBody2D
{
	public enum AnimalState
	{
		READY,
		DRAG,
		RELEASE
	}

	private static readonly Vector2 DRAG_LIM_MIN = new Vector2(-60, 0);
	private static readonly Vector2 DRAG_LIM_MAX = new Vector2(0, 60);

	[Export]
	Label debugLabel;
	[Export]
	AudioStreamPlayer2D strechSound;
	[Export]
	AudioStreamPlayer2D launchSound;
	[Export]
	AudioStreamPlayer2D kickSound;
	[Export]
	Sprite2D arrow;
	[Export]
	VisibleOnScreenNotifier2D visibleOnScreenNotifier2D;

	private AnimalState state = AnimalState.READY;
	private float arrowScaleX = 0.0f;
	private Vector2 start = Vector2.Zero;
	private Vector2 dragStart = Vector2.Zero;
	private Vector2 draggedVector = Vector2.Zero;
	private Vector2 lastDraggedVector = Vector2.Zero;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ConnectSignals();
		InitalizeVariables();
	}

	private void ConnectSignals()
	{
		visibleOnScreenNotifier2D.ScreenExited += OnScreenExited;
		SleepingStateChanged += OnSleepingStateChanged;
		InputEvent += OnInputEvent;
	}

	private void InitalizeVariables()
	{
		start = Position;
		arrowScaleX = arrow.Scale.X;
		arrow.Hide();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		UpdateState();


		UpdateDebugLabel();
	}

	private void StartDragging()
	{
		dragStart = GetGlobalMousePosition();
		arrow.Show();
	}

	private void ConstrainDragWithinLimit()
	{
		lastDraggedVector = draggedVector;
		draggedVector = draggedVector.Clamp(DRAG_LIM_MIN, DRAG_LIM_MAX);
		Position = start + draggedVector;
	}

	private void PlayStrechSound()
	{
		Vector2 diff = draggedVector - lastDraggedVector;
		if (diff.Length() > 0 && !strechSound.Playing)
		{
			strechSound.Play();
		}
	}

	private void UpdateDraggedVector()
	{
		draggedVector = GetGlobalMousePosition() - dragStart;
	}

	private void HandleDragging()
	{
		UpdateDraggedVector();
		PlayStrechSound();
		ConstrainDragWithinLimit();
	}

	private void StartRelease()
	{

	}

	private void UpdateState()
	{
		switch (state)
		{
			case AnimalState.DRAG:
				HandleDragging();
				break;
			case AnimalState.RELEASE:
				break;
		}
	}

	private void ChangeState(AnimalState newState)
	{
		state = newState;

		switch (state)
		{
			case AnimalState.DRAG:
				StartDragging();
				break;
			case AnimalState.RELEASE:
				StartRelease();
				break;
		}
	}

	private void UpdateDebugLabel()
	{
		debugLabel.Text = $"St:{state} Sl:{Sleeping}\n";
		debugLabel.Text += $"dragStart:{dragStart}\n";
		debugLabel.Text += $"draggedVector:{draggedVector}";
	}

	private void OnInputEvent(Node viewport, InputEvent @event, long shapeIdx)
	{
		if (state == AnimalState.READY && @event.IsActionPressed("drag"))
		{
			ChangeState(AnimalState.DRAG);
		}
	}

	private void OnSleepingStateChanged()
	{
	}

	private void OnScreenExited()
	{
		GD.Print("???");
	}
}
