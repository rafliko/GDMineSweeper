using Godot;
using System;

public partial class cell : Node2D
{
	int value = 0;
	int posx = 0;
	int posy = 0;
	state st = state.Covered;
	bool startpoint = false;
	bool clear = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(st==state.Covered) GetNode<Sprite2D>("Sprite2D").RegionRect = new Rect2(10*main.size,0,main.size,main.size);
		else if(st==state.Uncovered) GetNode<Sprite2D>("Sprite2D").RegionRect = new Rect2(value*main.size,0,main.size,main.size);
		else if(st==state.Flag) GetNode<Sprite2D>("Sprite2D").RegionRect = new Rect2(11*main.size,0,main.size,main.size);

		if(value==0 && st==state.Uncovered && !clear)
		{
			foreach(var c in GetParent().GetChildren())
			{
				for(int i=-1; i<=1; i++)
				{
					for(int j=-1; j<=1; j++)
					{
						if((int)c.Get("posx")==posx+i && (int)c.Get("posy")==posy+j)
							if(posx+i>=0 && posx+i<main.W && posy+j>=0 && posy+j<main.H)
								c.Set("st", (int)state.Uncovered);
					}
				}
			}
			clear = true;
		}

		//GD.Print(Engine.GetFramesPerSecond());
	}

	public override void _Input(InputEvent @event)
	{
		// Mouse in viewport coordinates.
		if (@event is InputEventMouseButton eventMouseButton && eventMouseButton.Pressed)
		{
			//GD.Print("Mouse Click/Unclick at: ", eventMouseButton.Position, eventMouseButton.ButtonIndex);
			if(new Rect2(Position.X,Position.Y,main.resize,main.resize).HasPoint(eventMouseButton.Position))
			{
				if(eventMouseButton.ButtonIndex == MouseButton.Right)
				{
					if(st == state.Covered) 
					{
						st = state.Flag;
					}
					else if(st == state.Flag)
					{
						st = state.Covered;
					}
					
				}
				else if(eventMouseButton.ButtonIndex == MouseButton.Left)
				{
					if(st == state.Covered)
					{
						if(!main.started)
						{
							startpoint = true;
							GetParent().GetParent().Call("Setup");
						}
						st = state.Uncovered;
						if(value==9)
						{
							foreach(var c in GetParent().GetChildren())
							{
								c.Set("st", (int)state.Uncovered);
							}
						}
					}
				}
			}
		}
	}
}