using UnityEngine;
using System.Collections;

public class BrickColor
{
	int id;

	public int Id {
		get { return id;}
	}

	Color color;

	public Color C {
		get { return color;}
	}
	
	public BrickColor (int id, Color color)
	{
		this.id = id;
		this.color = color;
	}

	public const int ID_RED = 0;
	public const int ID_GREEN = 1;
	public const int ID_BLUE = 2;
	public const int ID_YELLOW = 3;
	public const int ID_MAGENTA = 4;
	public const int ID_CYAN = 5;
	public const int ID_WHITE = 6;
	public const int ID_BLACK = 7;
	static BrickColor BC_RED = new BrickColor (ID_RED, new Color (1, 0, 0));
	static BrickColor BC_GREEN = new BrickColor (ID_GREEN, new Color (0, 1, 0));
	static BrickColor BC_BLUE = new BrickColor (ID_BLUE, new Color (0, 0, 1));
	static BrickColor BC_YELLOW = new BrickColor (ID_YELLOW, new Color (1, 1, 0));
	static BrickColor BC_MAGENTA = new BrickColor (ID_MAGENTA, new Color (1, 0, 1));
	static BrickColor BC_CYAN = new BrickColor (ID_CYAN, new Color (0, 1, 1));
	static BrickColor BC_WHITE = new BrickColor (ID_WHITE, new Color (1, 1, 1));
	static BrickColor BC_BLACK = new BrickColor (ID_BLACK, new Color (0, 0, 0));
	public static BrickColor[] COLORS = {
		BC_RED,
		BC_GREEN,
		BC_BLUE,
		BC_YELLOW,
		BC_MAGENTA,
		BC_CYAN,
		BC_WHITE,
		BC_BLACK
	};

	public static BrickColor GetRandomColor ()
	{
		int rId = Random.Range (0, COLORS.Length);
		return COLORS [rId];
	}

	public static BrickColor GetMixColor (BrickColor bc1, BrickColor bc2)
	{ 
		return null;
	}
	
}


