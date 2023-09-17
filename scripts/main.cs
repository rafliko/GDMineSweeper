using Godot;
using System;
using System.Threading;

public partial class main : Node2D
{
	public static int size = 64;
	public static float scale = 1f;
	public static int resize = Convert.ToInt32(size*scale);
	public static int W = 16;
	public static int H = 16;
	public static int mineCount = 40;
	public static bool started = false;

	int[,] board = new int[W,H];
	RandomNumberGenerator rng = new RandomNumberGenerator();

	PackedScene Cell = GD.Load<PackedScene>("res://prefabs/cell.tscn");

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		resize = Convert.ToInt32(size*scale);
		GetWindow().Size = new Vector2I(W*resize,(H+1)*resize);

		var c = Cell.Instantiate();
		AddChild(c);
		c.GetNode<Node2D>(".").Set("value",12);
		c.GetNode<Node2D>(".").Set("st",(int)state.Uncovered);
		c.GetNode<Node2D>(".").Scale = new Vector2(scale,scale);
		c.GetNode<Node2D>(".").Position = new Vector2(0,0);

		for(int x=0; x<W; x++)
		{
			for(int y=0; y<H; y++)
			{
				c = Cell.Instantiate();
				GetNode<Node>("Board").AddChild(c);
				c.GetNode<Node2D>(".").Set("posx",x);
				c.GetNode<Node2D>(".").Set("posy",y);
				c.GetNode<Node2D>(".").Scale = new Vector2(scale,scale);
				c.GetNode<Node2D>(".").Position = new Vector2(x*resize,(y+1)*resize);
			}
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	void Setup()
	{
		int startx = 0;
		int starty = 0;
		foreach(var c in GetNode("Board").GetChildren())
		{
			int x = (int)c.Get("posx");
			int y = (int)c.Get("posy");
			if((bool)c.Get("startpoint"))
			{
				startx = x;
				starty = y;
				break;
			}
		}

		for(int m=0; m<mineCount; m++)
		{
			int x = rng.RandiRange(0,W-1);
			int y = rng.RandiRange(0,H-1);
			if(board[x,y]!=9) 
			{
				board[x,y] = 9;
				for(int i=-1; i<=1; i++)
				{
					for(int j=-1; j<=1; j++)
					{
						if(startx+i==x && starty+j==y) 
						{ 
							board[x,y] = 0;
							m--;
						}
					}
				}
			}
			else m--;
		}

		for(int x=0; x<W; x++)
		{
			for(int y=0; y<H; y++)
			{
				int count = 0;
				if(board[x,y]!=9)
				{
					for(int i=-1; i<=1; i++)
					{
						for(int j=-1; j<=1; j++)
						{
							if(x+i>=0 && x+i<W && y+j>=0 && y+j<H)
								if(board[x+i,y+j]==9) count++;
						}
					}
					board[x,y] = count;
				}
			}
		}

		foreach(var c in GetNode("Board").GetChildren())
		{
			int x = (int)c.Get("posx");
			int y = (int)c.Get("posy");
			c.Set("value",board[x,y]);
		}

		started = true;
	}
}

enum state
{
	Covered,
	Uncovered,
	Flag
}