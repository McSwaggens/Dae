using System;
using System.Collections.Generic;
using System.IO;

namespace Dae
{
	public class CBuffer
	{
		//public static Vector GenerateScreenSize (Vector pixelSize) => new Vector (pixelSize.x / Dae.UnitPixelSize.x, pixelSize.y / Dae.UnitPixelSize.y);

		public static void Blit ( CBuffer from, CBuffer to, IVector fromStart, IVector fromEnd, IVector toStart )
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

		public static List<string> OpenFile ( string filePath )
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

		private CUnit[,] grid;
		public IVector Size => new IVector (grid.GetLength (0), grid.GetLength (1));

		public CUnit this[IVector loc]
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

		public CUnit this[int x, int y]
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

		public void Resize ( IVector newSize )
		{
			if (grid == null || Size != newSize)
			{
				grid = new CUnit[newSize.x, newSize.y];
			}
		}

		public CBuffer ( IVector size )
		{
			Resize (size);
		}

		public CBuffer ( CUnit[,] inputGrid )
		{
			grid = inputGrid;

			Resize (Size);
		}

		/// <summary>
		/// Quick method that reallocates the buffer that has the effect of clearing everything to black
		/// </summary>
		public void Blank ()
		{
			grid = new CUnit[Size.x, Size.y];
		}

		public void Write ( string str, Color3 foreground, Color3 background, IVector location )
		{
			for (int i = 0; i < str.Length; i++)
			{
				char c = str[i];

				int x = location.x + i;

				if (x >= Size.x || location.y >= Size.y)
					break;

				grid[x, location.y].SetCharacter (c);
				grid[x, location.y].foregroundColor = foreground;
				grid[x, location.y].backgroundColor = background;
			}
		}

		public void Clear ( Color3 color )
		{
			for (int x = 0; x < Size.x; x++)
			{
				for (int y = 0; y < Size.y; y++)
				{
					grid[x, y].Character = ' ';
					grid[x, y].backgroundColor = color;
				}
			}
		}

		public void DrawRectangleTo ( IVector start, IVector end, Color3 foregroundColor, Color3 backgroundColor, char fillCharacter = ' ' )
		{
			for (int x = start.x; x < end.x && x < Size.x; x++)
			{
				for (int y = start.y; y < end.y && y < Size.y; y++)
				{
					grid[x, y].Character = fillCharacter;
					grid[x, y].backgroundColor = backgroundColor;
					grid[x, y].foregroundColor = foregroundColor;
				}
			}
		}

		public void DrawRectangleTo ( IVector start, IVector end, Color3 backgroundColor )
		{
			for (int x = start.x; x < end.x && x < Size.x; x++)
			{
				for (int y = start.y; y < end.y && y < Size.y; y++)
				{
					grid[x, y].Character = ' ';
					grid[x, y].backgroundColor = backgroundColor;
					grid[x, y].foregroundColor = Color3.white;
				}
			}
		}

		public void DrawRectangle ( IVector start, IVector end, Color3 foregroundColor, Color3 backgroundColor, char fillCharacter = ' ' )
		{
			DrawRectangleTo (start, start + end, foregroundColor, backgroundColor, fillCharacter);
		}

		public void DrawRectangle ( IVector start, IVector end, Color3 backgroundColor )
		{
			DrawRectangleTo (start, start + end, backgroundColor);
		}
	}
}