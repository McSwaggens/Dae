using System;
using OpenTK.Graphics.OpenGL4;

namespace Dae
{
#pragma warning disable CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)
#pragma warning disable CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()

	public struct Vector : IGLType
#pragma warning restore CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()
#pragma warning restore CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)
	{
		public static readonly Vector one = new Vector (1, 1);
		public static readonly Vector zero = new Vector (0, 0);
		public static readonly Vector left = new Vector (-1, 0);
		public static readonly Vector right = new Vector (1, 0);
		public static readonly Vector up = new Vector (0, 1);
		public static readonly Vector down = new Vector (0, -1);

		public static readonly int elements = 2;

		public float x, y;

		public Vector (float x = 0, float y = 0)
		{
			this.x = x;
			this.y = y;
		}

		public float Magnitude => (float)Math.Sqrt (( x * x ) + ( y * y ));

		public float Distance (Vector vector)
		{
			return ( this - vector ).Magnitude;
		}

		public Vector Normalized
		{
			get
			{
				float m = Magnitude;
				return m > 0 ? this / m : zero;
			}
		}

		public static float Dot (Vector a, Vector b)
		{
			return ( a.x * b.x ) + ( a.y * b.y );
		}

		public static float PerpDot (Vector a, Vector b)
		{
			return ( a.x * b.x ) + -( a.y * b.y );
		}

		public override string ToString () => "x: {x}, y: {y}";

		public static bool operator == (Vector a, Vector b)
		{
			return a.x == b.x && a.y == b.y;
		}

		public static bool operator != (Vector a, Vector b)
		{
			return a.x != b.x && a.y != b.y;
		}

		public static Vector operator + (Vector a, Vector b)
		{
			return new Vector (a.x + b.x, a.y + b.y);
		}

		public static Vector operator + (Vector a, float b)
		{
			return new Vector (a.x + b, a.y + b);
		}

		public static Vector operator - (Vector a, Vector b)
		{
			return new Vector (a.x - b.x, a.y - b.y);
		}

		public static Vector operator - (Vector a, float b)
		{
			return new Vector (a.x - b, a.y - b);
		}

		public static Vector operator * (Vector a, Vector b)
		{
			return new Vector (a.x * b.x, a.y * b.y);
		}

		public static Vector operator * (Vector a, float b)
		{
			return new Vector (a.x * b, a.y * b);
		}

		public static Vector operator / (Vector a, Vector b)
		{
			return new Vector (a.x / b.x, a.y / b.y);
		}

		public static Vector operator / (Vector a, float b)
		{
			return new Vector (a.x / b, a.y / b);
		}

		public static implicit operator Vector (System.Drawing.Point point)
		{
			return new Vector (point.X, point.Y);
		}

		public static implicit operator Vector (float f)
		{
			return new Vector (f, f);
		}

		public static implicit operator Vector (IVector vector)
		{
			return new Vector (vector.x, vector.y);
		}

		public OpenTK.Vector2 ToTK () => new OpenTK.Vector2 (x, y);

		public void UniformUpload (int locationId)
		{
			GL.Uniform2 (locationId, x, y);
		}

		public object Clone ()
		{
			return new Vector (x, y);
		}
	}
}