using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;
using Dae;

namespace Dae
{
	internal class Quad : IShape
	{
		private GLABuffer<Vector> vertexBuffer, uvBuffer, positionBuffer;

		public void Initialize ()
		{
			Vector[] vertexBufferArray =
			{
				new Vector(-1, -1),
				new Vector(-1, 1),

				new Vector(1, 1),
				new Vector(1, -1),
			};

			// Divide every vertex by 10
			for (int i = 0; i < vertexBufferArray.Length; i++)
			{
				vertexBufferArray[i] = vertexBufferArray[i] / 2f;
			}

			vertexBuffer = new GLABuffer<Vector> (4, 0, vertexBufferArray, GLABufferUsage.PerVertex);

			Vector[] uvBufferArray =
			{
				new Vector(0, 0),
				new Vector(0, 1),

				new Vector(1, 1),
				new Vector(1, 0),
			};

			uvBuffer = new GLABuffer<Vector> (4, 1, uvBufferArray, GLABufferUsage.PerVertex);

			Vector v = new Vector (0.5f, 0.0f);
			Vector v2 = new Vector (-0.5f, 0.3f);

			positionBuffer = new GLABuffer<Vector> (2, 2, new Vector[] { v, v2 }, GLABufferUsage.PerDraw);
		}

		public void Enable ()
		{
			vertexBuffer.EnableVertex (Vector.elements);
			uvBuffer.EnableVertex (Vector.elements);
			positionBuffer.EnableVertex (Vector.elements);
		}

		public void Disable ()
		{
			GLABuffer<Vector>.DisableVertexAttribute (0, 1, 2);
		}

		public void Dispose ()
		{
			GLABuffer<Vector>.Delete (vertexBuffer, uvBuffer, positionBuffer);
		}

		public void Render ()
		{
			GL.VertexAttribDivisor (0, 0);
			GL.VertexAttribDivisor (1, 0);
			GL.VertexAttribDivisor (2, 1);

			GL.DrawArraysInstanced (PrimitiveType.TriangleFan, 0, 4, 2);
		}
	}
}