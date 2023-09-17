using Godot;
using System;

public partial class DifficultySelect : Label
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<OptionButton>("OptionButton").AddItem("Easy");
		GetNode<OptionButton>("OptionButton").AddItem("Medium");
		GetNode<OptionButton>("OptionButton").AddItem("Hard");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
