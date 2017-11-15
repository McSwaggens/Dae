using OpenTK.Graphics.OpenGL4;

namespace Dae
{
	public class Color : IGLType
	{
		public static readonly Color white = new Color (1, 1, 1);
		public static readonly Color black = new Color ();
		public static readonly Color red = new Color (1);
		public static readonly Color green = new Color (0, 1);
		public static readonly Color blue = new Color (0, 0, 1);
		public static readonly Color pink = new Color (1, 0, 1);

		public float r, g, b, a;

		public Color (float r = 0, float g = 0, float b = 0, float a = 1)
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

		public void UniformUpload (int locationId)
		{
			GL.Uniform4 (locationId, r, g, b, a);
		}

		public static implicit operator Color (Vector v)
		{
			return new Color (v.x, v.y);
		}

		public static implicit operator Color (float f)
		{
			return new Color (f, f, f);
		}
	}
}