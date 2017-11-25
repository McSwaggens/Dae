using OpenTK.Graphics.OpenGL4;

namespace Dae
{
	public struct Color : IGLType
	{
		public static readonly int elements = 4;

		public static readonly Color white = new Color (1, 1, 1);
		public static readonly Color black = new Color ();
		public static readonly Color red = new Color (1);
		public static readonly Color green = new Color (0, 1);
		public static readonly Color blue = new Color (0, 0, 1);
		public static readonly Color pink = new Color (1, 0, 1);

		public float r, g, b, a;

		public Color ( float r = 0, float g = 0, float b = 0, float a = 1 )
		{
			this.r = r;
			this.g = g;
			this.b = b;
			this.a = a;
		}

		public object Clone ()
		{
			return new Color (r, g, b, a);
		}

		public void UniformUpload ( int locationId )
		{
			GL.Uniform4 (locationId, r, g, b, a);
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

	public struct Color3 : IGLType
	{
		public static readonly int elements = 3;

		public static readonly Color3 white = new Color3 (1, 1, 1);
		public static readonly Color3 black = new Color3 ();
		public static readonly Color3 red = new Color3 (1);
		public static readonly Color3 green = new Color3 (0, 1);
		public static readonly Color3 blue = new Color3 (0, 0, 1);
		public static readonly Color3 pink = new Color3 (1, 0, 1);

		public float r, g, b;

		public Color3 ( float r = 0, float g = 0, float b = 0 )
		{
			this.r = r;
			this.g = g;
			this.b = b;
		}

		public object Clone ()
		{
			return new Color3 (r, g, b);
		}

		public void UniformUpload ( int locationId )
		{
			GL.Uniform3 (locationId, r, g, b);
		}

		public static implicit operator Color3 ( Vector v )
		{
			return new Color3 (v.x, v.y);
		}

		public static implicit operator Color3 ( float f )
		{
			return new Color3 (f, f, f);
		}
	}
}