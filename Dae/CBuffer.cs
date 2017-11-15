using System;
using System.Collections.Generic;
using System.IO;

namespace Dae
{
	public class CBuffer
	{
		public static Vector GenerateScreenSize (Vector pixelSize) => new Vector (pixelSize.x / Dae.UnitPixelSize.x, pixelSize.y / Dae.UnitPixelSize.y);

		public static void Blit (CBuffer from, CBuffer to, IVector fromStart, IVector fromEnd, IVector toStart)
		{
			for (int fy = fromStart.y; fy < fromEnd.y && fy + toStart.y < to.Size.y; fy++)
			{
				for (int fx = fromStart.x; fx < fromEnd.x && fx + toStart.x < to.Size.x; fx++)
				{
					IVector fLoc = new IVector (fx, fy);
					IVector toLoc = toStart + fLoc;

					// Assignment
					to[toLoc] = from[fLoc];
				}
			}
		}

		public static List<string> OpenFile (string filePath)
		{
			StreamReader streamReader = File.OpenText (filePath);
			string content = streamReader.ReadToEnd ();

			List<string> lines = new List<string> (content.Split ('\n'));

			for (int i = 0; i < lines.Count; i++)
			{
				lines[i] = lines[i].TrimEnd ('\r');
			}

			lines.ForEach (s => Console.WriteLine (s));

			return lines;
		}

		private CharUnit[,] grid;
		public IVector Size => new IVector (grid.GetLength (0), grid.GetLength (1));

		public CharUnit this[IVector loc]
		{
			get
			{
				return grid[loc.x, loc.y];
			}
			set
			{
				grid[loc.x, loc.y] = value;
			}
		}

		public CharUnit this[int x, int y]
		{
			get
			{
				return grid[x, y];
			}
			set
			{
				grid[x, y] = value;
			}
		}

		public void Resize (IVector newSize)
		{
			if (grid == null || Size != newSize)
			{
				grid = new CharUnit[newSize.x, newSize.y];
			}
		}

		public CBuffer (IVector size)
		{
			Resize (size);
		}

		public CBuffer (CharUnit[,] inputGrid)
		{
			grid = inputGrid;

			Resize (Size);
		}
	}
}