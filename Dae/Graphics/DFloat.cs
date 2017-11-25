using OpenTK.Graphics.OpenGL4;

namespace Dae
{
	public struct DFloat : IGLType
	{
		public float floatingPointValue;

		public DFloat ( float f = 0.0f )
		{
			this.floatingPointValue = f;
		}

		public object Clone ()
		{
			return new DFloat (floatingPointValue);
		}

		public void UniformUpload ( int locationId )
		{
			GL.Uniform1 (locationId, floatingPointValue);
		}

		public static implicit operator DFloat ( float v )
		{
			return new DFloat (v);
		}
	}
}