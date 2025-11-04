using Godot;
using System;

public partial class Plane : CharacterBody2D
{
	const float GRAVITY = 1200.0f;
	[Export]
	float jumpForce = -300.0f;

	[Export]
	AudioStreamPlayer engineSound;

	private AnimationPlayer animationPlayer;
	private AnimatedSprite2D animatedSprite2D;


	public override void _Ready()
	{
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	public override void _Process(double delta)
	{
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		velocity.Y += GRAVITY * (float)delta;

		Velocity = velocity;
		MoveAndSlide();

		HandleFly();

		if (IsOnFloor() || IsOnCeiling())
		{
			Die();
		}
	}

	private void HandleFly()
	{
		if (Input.IsActionJustPressed("fly") == true)
		{
			animationPlayer.Play("jump");
			Vector2 v = Velocity;

			v.Y = jumpForce;

			Velocity = v;
		}
	}

	public void Die()
	{
		SignalManager.EmitOnPlaneDied();
		animatedSprite2D.Stop();
		engineSound.Stop();
		SetPhysicsProcess(false);
	}
}
