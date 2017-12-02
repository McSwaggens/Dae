using OpenTK.Graphics.OpenGL4;

namespace Dae
{
	internal static class Quad
	{
		public static GLABuffer<Vector> vertexBuffer, uvBuffer, positionBuffer, fontPositionBuffer;

		public static GLABuffer<Color> foregroundBuffer, backgroundBuffer;

		internal static void Initialize ()
		{
			Vector[] vertexBufferArray =
			{
				new Vector(-1, -1),
				new Vector(-1, 1),

				new Vector(1, 1),
				new Vector(1, -1),
			};

			vertexBuffer = new GLABuffer<Vector> (4, 0, vertexBufferArray, GLABufferUsage.PerVertex);

			Vector[] uvBufferArray =
			{
				new Vector(0, 0),
				new Vector(0, 1),

				new Vector(1, 1),
				new Vector(1, 0),
			};

			uvBuffer = new GLABuffer<Vector> (4, 1, uvBufferArray, GLABufferUsage.PerVertex);
			positionBuffer = new GLABuffer<Vector> (0, 2, null, GLABufferUsage.PerDraw);
			fontPositionBuffer = new GLABuffer<Vector> (0, 3, null, GLABufferUsage.PerDraw);
			foregroundBuffer = new GLABuffer<Color> (0, 4, null, GLABufferUsage.PerDraw);
			backgroundBuffer = new GLABuffer<Color> (0, 5, null, GLABufferUsage.PerDraw);
		}

		public static void Enable ()
		{
			vertexBuffer.EnableVertex ();
			uvBuffer.EnableVertex ();
			positionBuffer.EnableVertex ();
			fontPositionBuffer.EnableVertex ();
			foregroundBuffer.EnableColor3 ();
			backgroundBuffer.EnableColor3 ();
		}

		public static void Disable ()
		{
			GLABuffer<Vector>.DisableVertexAttribute (0, 1, 2, 3, 4, 5);
		}

		public static void Dispose ()
		{
			GLABuffer<Vector>.Delete (vertexBuffer, uvBuffer, positionBuffer, fontPositionBuffer);
			GLABuffer<Color>.Delete (foregroundBuffer, backgroundBuffer);
		}

		public static void Render ()
		{
			GL.DrawArraysInstanced (PrimitiveType.TriangleFan, 0, 4, positionBuffer.Size);
		}

		public static void Render ( Vector[] unitPositions, Vector[] fontPositions, Color[] foregroundColors, Color[] backgroundColors )
		{
			positionBuffer.Upload (unitPositions);
			fontPositionBuffer.Upload (fontPositions);
			foregroundBuffer.Upload (foregroundColors);
			backgroundBuffer.Upload (backgroundColors);

			Render ();
		}
	}
}