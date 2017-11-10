using System;

namespace Dae
{
	public struct IVector
	{
		public static readonly IVector one = new IVector (1, 1);
		public static readonly IVector zero = new IVector (0, 0);
		public static readonly IVector left = new IVector (-1, 0);
		public static readonly IVector right = new IVector (1, 0);
		public static readonly IVector up = new IVector (0, 1);
		public static readonly IVector down = new IVector (0, -1);

		public int x, y;

		public IVector (int x = 0, int y = 0)
		{
			this.x = x;
			this.y = y;
		}

		public int Magnitude => (int)Math.Sqrt (( x * x ) + ( y * y ));

		public int Distance (IVector IVector)
		{
			return ( this - IVector ).Magnitude;
		}

		public IVector Normalized
		{
			get
			{
				int m = Magnitude;
				return m > 0 ? this / m : zero;
			}
		}

		public static int Dot (IVector a, IVector b)
		{
			return ( a.x * b.x ) + ( a.y * b.y );
		}

		public static int PerpDot (IVector a, IVector b)
		{
			return ( a.x * b.x ) + -( a.y * b.y );
		}

		public override string ToString () => "x: {x}, y: {y}";

		public static IVector operator + (IVector a, IVector b)
		{
			return new IVector (a.x + b.x, a.y + b.y);
		}

		public static IVector operator + (IVector a, int b)
		{
			return new IVector (a.x + b, a.y + b);
		}

		public static IVector operator - (IVector a, IVector b)
		{
			return new IVector (a.x - b.x, a.y - b.y);
		}

		public static IVector operator - (IVector a, int b)
		{
			return new IVector (a.x - b, a.y - b);
		}

		public static IVector operator * (IVector a, IVector b)
		{
			return new IVector (a.x * b.x, a.y * b.y);
		}

		public static IVector operator * (IVector a, int b)
		{
			return new IVector (a.x * b, a.y * b);
		}

		public static IVector operator / (IVector a, IVector b)
		{
			return new IVector (a.x / b.x, a.y / b.y);
		}

		public static IVector operator / (IVector a, int b)
		{
			return new IVector (a.x / b, a.y / b);
		}

		public static implicit operator IVector (System.Drawing.Point point)
		{
			return new IVector (point.X, point.Y);
		}

		public static implicit operator IVector (int f)
		{
			return new IVector (f, f);
		}

		public static implicit operator IVector (Vector vector)
		{
			return new IVector ((int)vector.x, (int)vector.y);
		}

		public OpenTK.Vector2 ToTK () => new OpenTK.Vector2 (x, y);
	}
}