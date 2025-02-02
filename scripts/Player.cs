using Godot;
using System;

public partial class Player : Area2D
{
	[Export]
	public int Speed {get;set;} = 400;
	
	public Vector2 ScreenSize;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ScreenSize = GetViewportRect().Size;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Walk(delta);
	}

	private void Walk(double delta)
	{
		var velocity = Vector2.Zero;
		if(Input.IsActionPressed("move_right"))
			velocity.X += 1;
		if(Input.IsActionPressed("move_left"))
			velocity.X -= 1;
		if(Input.IsActionPressed("move_up"))
			velocity.Y -= 1;
		if(Input.IsActionPressed("move_down"))
			velocity.Y += 1;

		var as2d = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		if (velocity.Length() > 0)
		{
			velocity= velocity.Normalized() * Speed;
			as2d.Play();
		}
		else
			as2d.Stop();



		Position += velocity * (float)delta;
		Position = new Vector2(
			x: Mathf.Clamp(Position.X, 0 , ScreenSize.X),
			y: Mathf.Clamp(Position.Y, 0, ScreenSize.Y));
	}
}
