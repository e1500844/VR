using SimpleJSON;

class Cube
{	
	float x;
	float y;
	float z;
	string color;
	string type;
	int angle;

	public Cube(float x, float y, float z, string color, string type, int angle)
	{
		this.x = x;
		this.y = y;
		this.z = z;
		this.color = color;
		this.type = type;
		this.angle = angle;
	}

	public float X
	{
		get
		{
			return x;
		}

		set
		{
			x = value;
		}
	}

	public float Y
	{
		get
		{
			return y;
		}

		set
		{
			y = value;
		}
	}

	public float Z
	{
		get
		{
			return z;
		}

		set
		{
			z = value;
		}
	}

	public string Color
	{
		get
		{
			return color;
		}

		set
		{
			color = value;
		}
	}

	public string Type
	{
		get
		{
			return type;
		}

		set
		{
			type = value;
		}
	}

	public int Angle
	{
		get
		{
			return angle;
		}

		set
		{
			angle = value;
		}
	}

	public static Cube[] CreateCube(string jono, int amount){
		
		Cube[] cubes = new Cube[amount];
		var N = JSON.Parse(jono);

		for (int i = 0; i < amount; i++) 
		{
			cubes [i] = new Cube (N [i] ["x"].AsFloat, N [i] ["y"].AsFloat, N [i] ["z"].AsFloat, N [i] ["color"], N [i] ["type"], N [i] ["angle"].AsInt);
		}

		return cubes;
	}
}