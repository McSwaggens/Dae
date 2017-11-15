using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;
using System.Runtime.InteropServices;

namespace Dae
{
	public enum GLABufferUsage
	{
		PerVertex,
		PerDraw
	}

	public class GLABuffer<T> : IDisposable where T : struct, IGLType
	{
		public static void DisableVertexAttribute (params int[] attributes)
		{
			for (int i = 0; i < attributes.Length; i++)
			{
				GL.DisableVertexAttribArray (attributes[i]);
			}
		}

		public static void Delete (params GLABuffer<T>[] buffers)
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

		public GLABuffer (int size, int attribute, T[] data = null, GLABufferUsage usage = GLABufferUsage.PerVertex) : this ()
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

		public void Upload (T[] data)
		{
			if (data.Length != size)
			{
				throw new Exception ("Input data length is incorrect, it should be the exact same size as the GLABuffer.Size");
			}

			GL.BindBuffer (BufferTarget.ArrayBuffer, bufferId);
			GL.BufferData (BufferTarget.ArrayBuffer, size * individualDataSize, data, GLBufferUsage);
		}

		public void EnableVertex (int elements)
		{
			GL.EnableVertexAttribArray (attribute);
			GL.BindBuffer (BufferTarget.ArrayBuffer, bufferId);
			GL.VertexAttribPointer (attribute, elements, VertexAttribPointerType.Float, false, 0, 0);
		}

		public void Resize (int size)
		{
			this.size = size;
		}

		public void Dispose ()
		{
			GL.DeleteBuffer (bufferId);
		}
	}
}