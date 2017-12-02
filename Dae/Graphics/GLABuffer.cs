using OpenTK.Graphics.OpenGL4;
using System;
using System.Runtime.InteropServices;

namespace Dae
{
	public enum GLABufferUsage : int
	{
		PerVertex = 0,
		PerDraw = 1
	}

	public class GLABuffer<T> : IDisposable where T : struct, IGLType
	{
		public static void DisableVertexAttribute ( params int[] attributes )
		{
			for (int i = 0; i < attributes.Length; i++)
			{
				GL.DisableVertexAttribArray (attributes[i]);
			}
		}

		public static void Delete ( params GLABuffer<T>[] buffers )
		{
			foreach (GLABuffer<T> buffer in buffers)
			{
				if (buffer.IsAlive)
				{
					buffer.Dispose ();
				}
			}
		}

		public int BufferId => bufferId;
		public bool IsAlive => BufferId != -1;
		public int Size => size;

		public readonly int individualDataSize = 0;

		private int bufferId = -1;
		private int size;
		private int attribute;
		private GLABufferUsage usage;

		private BufferUsageHint GLBufferUsage
		{
			get
			{
				switch (usage)
				{
					case GLABufferUsage.PerVertex:
						return BufferUsageHint.StaticDraw;

					case GLABufferUsage.PerDraw:
						return BufferUsageHint.StreamDraw;

					default:
						throw new Exception ("Unknown GLABufferUsage");
				}
			}
		}

		private GLABuffer ()
		{
			individualDataSize = Marshal.SizeOf (typeof (T));
		}

		public GLABuffer ( int size, int attribute, T[] data = null, GLABufferUsage usage = GLABufferUsage.PerVertex ) : this ()
		{
			this.attribute = attribute;
			this.usage = usage;

			Resize (size);

			bufferId = GL.GenBuffer ();

			if (data != null)
			{
				Upload (data);
			}
		}

		public void Upload ( T[] data )
		{
			if (data.Length != size)
			{
				Resize (data.Length);
			}

			GL.BindBuffer (BufferTarget.ArrayBuffer, bufferId);
			GL.BufferData (BufferTarget.ArrayBuffer, size * individualDataSize, data, GLBufferUsage);
		}

		public void EnableVertex ()
		{
			GL.EnableVertexAttribArray (attribute);
			GL.BindBuffer (BufferTarget.ArrayBuffer, bufferId);
			GL.VertexAttribPointer (attribute, Vector.elements, VertexAttribPointerType.Float, false, 0, 0);

			GL.VertexAttribDivisor (attribute, (int)usage);
		}

		public void EnableColor3 ()
		{
			GL.EnableVertexAttribArray (attribute);
			GL.BindBuffer (BufferTarget.ArrayBuffer, bufferId);
			GL.VertexAttribPointer (attribute, Color.elements, VertexAttribPointerType.Float, false, 0, 0);

			GL.VertexAttribDivisor (attribute, (int)usage);
		}

		public void Resize ( int size )
		{
			this.size = size;
		}

		public void Dispose ()
		{
			GL.DeleteBuffer (bufferId);
		}
	}
}