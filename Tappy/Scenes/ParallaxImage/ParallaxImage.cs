using Godot;
using System;

public partial class ParallaxImage : Parallax2D
{
	[Export] Texture2D srcTexture;
	[Export] Sprite2D sprite;
	[Export] float speedScale;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Autoscroll = new Vector2(-GameManager.Instance.speed * speedScale, 0);
		float scaleFactor = GetViewportRect().Size.Y / srcTexture.GetHeight();

		sprite.Texture = srcTexture;

		sprite.Scale = new Vector2(scaleFactor, scaleFactor);

		RepeatSize = new Vector2(srcTexture.GetWidth() * scaleFactor, 0);

		SignalManager.Instance.OnPlaneDied += OnPlaneDied;
	}

	public override void _ExitTree()
	{
		SignalManager.Instance.OnPlaneDied -= OnPlaneDied;
	}

	private void OnPlaneDied()
	{
		Autoscroll = Vector2.Zero;
	}
}
