using Godot;
using System;

public partial class Start : Button
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Pressed += StartGame;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	void StartGame()
	{
		string diff = GetNode<OptionButton>("../Difficulty/OptionButton").Text;
		switch(diff)
		{
			case "Easy":
				main.W = 8;
				main.H = 8;
				main.mineCount = 10;
				main.scale = 1f;
				break;
			case "Medium":
				main.W = 16;
				main.H = 16;
				main.mineCount = 40;
				main.scale = 0.7f;
				break;
			case "Hard":
				main.W = 30;
				main.H = 16;
				main.mineCount = 99;
				main.scale = 0.5f;
				break;
		}
		GetTree().ChangeSceneToFile("res://scenes/main.tscn");
	}
}
