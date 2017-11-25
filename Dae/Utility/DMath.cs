using System;

namespace Dae
{
	public class DMath
	{
		public static readonly float PI = (float)Math.PI;

		#region 01

		public static float P01ToN1P1 ( float f ) => ( f * 2f ) - 1f;

		public static Vector P01ToN1P1 ( Vector v ) => ( v * 2f ) - 1f;

		public static Vector P01ToN1P1 ( float x, float y ) => P01ToN1P1 (new Vector (x, y));

		#endregion 01

		#region Clamp

		public static float Clamp ( float a, float min, float max )
		{
			return Min (Max (a, max), min);
		}

		public static float Min ( float a, float min )
		{
			return a < min ? min : a;
		}

		public static float Max ( float a, float max )
		{
			return a > max ? max : a;
		}

		#endregion Clamp

		#region Rotation

		public static readonly float DEGREES = 180 / PI;
		public static readonly float RADIANS = PI / 180;

		public static float ToDegrees ( float radians )
		{
			return radians * DEGREES;
		}

		public static float ToRadians ( float degrees )
		{
			return degrees * RADIANS;
		}

		#endregion Rotation

		#region Abs

		public static Vector Abs ( Vector v )
		{
			return new Vector (Math.Abs (v.x), Math.Abs (v.y));
		}

		#endregion Abs
	}
}