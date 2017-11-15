using OpenTK.Graphics.OpenGL4;
using System;

namespace Dae
{
	public class Cell : IEnable, ICloneable
	{
		public IGLType value;
		public int id;
		public string name;

		public Cell (string name, IGLType value = null)
		{
			this.name = name;
			this.value = value;
		}

		public void Initialize (Material material)
		{
			int id = material.shader.GetUniform (name);

			if (id == -1)
			{
				Logger.Print ("[ERROR] Unknown uniform named " + name + " in shader.");
				//throw new GraphicsException();
			}

			this.id = id;
		}

		public void Enable ()
		{
			value.UniformUpload (id);
		}

		public object Clone ()
		{
			Cell cell = new Cell (name, (IGLType)value.Clone ());
			cell.id = id;
			return cell;
		}
	}

	public class Material : IDisable
	{
		public Shader shader;
		public Cell[] cells;
		public static int textureStack;

		private int timeId;

		public Material (Shader shader, Cell[] cells)
		{
			this.shader = shader;
			this.cells = cells;

			foreach (Cell cell in this.cells)
			{
				cell.Initialize (this);
			}

			timeId = shader.GetUniform ("time");
		}

		public Material (Material material)
		{
			shader = material.shader;

			cells = new Cell[material.cells.Length];

			for (int i = 0; i < cells.Length; i++)
			{
				cells[i] = (Cell)material.cells[i].Clone ();
			}

			timeId = material.timeId;
		}

		public void Enable (float[] matrix = null)
		{
			shader.Enable ();

			foreach (Cell cell in cells)
			{
				cell.value?.UniformUpload (cell.id);
			}

			if (timeId != -1)
			{
				GL.Uniform1 (timeId, Time.now);
			}
		}

		public void Disable ()
		{
			textureStack = 0;
		}

		public int GetIndex (string name)
		{
			for (int i = 0; i < cells.Length; i++)
			{
				Cell cell = cells[i];
				if (cell.name == name)
				{
					return i;
				}
			}

			return -1;
		}

		public void Set (string name, IGLType uniformVariable)
		{
			foreach (Cell cell in cells)
			{
				if (cell.name == name)
				{
					cell.value = uniformVariable;
				}
			}
		}

		public void Set (int index, IGLType uniformVariable)
		{
			cells[index].value = uniformVariable;
		}
	}
}