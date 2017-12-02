using OpenTK.Graphics.OpenGL4;

namespace Dae
{
	public struct Color4 : IGLType
	{
		public static readonly int elements = 4;

		public static readonly Color4 white = new Color4 (1, 1, 1);
		public static readonly Color4 black = new Color4 ();
		public static readonly Color4 red = new Color4 (1);
		public static readonly Color4 green = new Color4 (0, 1);
		public static readonly Color4 blue = new Color4 (0, 0, 1);
		public static readonly Color4 pink = new Color4 (1, 0, 1);

		public float r, g, b, a;

		public Color4 ( float r = 0, float g = 0, float b = 0, float a = 1 )
		{
			this.r = r;
			this.g = g;
			this.b = b;
			this.a = a;
		}

		public object Clone ()
		{
			return new Color4 (r, g, b, a);
		}

		public void UniformUpload ( int locationId )
		{
			GL.Uniform4 (locationId, r, g, b, a);
		}

		public static implicit operator Color4 ( Vector v )
		{
			return new Color4 (v.x, v.y);
		}

		public static implicit operator Color4 ( float f )
		{
			return new Color4 (f, f, f);
		}
	}

	public struct Color : IGLType
	{
		public static readonly int elements = 3;

		public static readonly Color white = new Color (1, 1, 1);
		public static readonly Color black = new Color ();
		public static readonly Color red = new Color (1);
		public static readonly Color green = new Color (0, 1);
		public static readonly Color blue = new Color (0, 0, 1);
		public static readonly Color pink = new Color (1, 0, 1);

		public float r, g, b;

		public Color ( float r = 0, float g = 0, float b = 0 )
		{
			this.r = r;
			this.g = g;
			this.b = b;
		}

		private static float shadowDivision = 2.5f;

		public Color Shadow ()
		{
			return new Color (r / shadowDivision, g / shadowDivision, b / shadowDivision);
		}

		public Color Lighter ()
		{
			float Lighten ( float f ) => f + ( ( 1 - f ) / 2 );
			return new Color (Lighten (r), Lighten (g), Lighten (b));
		}

		public object Clone ()
		{
			return new Color (r, g, b);
		}

		public void UniformUpload ( int locationId )
		{
			GL.Uniform3 (locationId, r, g, b);
		}

		public static implicit operator Color ( Vector v )
		{
			return new Color (v.x, v.y);
		}

		public static implicit operator Color ( float f )
		{
			return new Color (f, f, f);
		}
	}
}