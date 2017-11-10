using System;
using System.Collections.Generic;
using System.IO;

namespace Dae
{
	public class Buffer
	{
		public static Vector GenerateScreenSize (Vector pixelSize) => new Vector (pixelSize.x / Dae.UnitPixelSize.x, pixelSize.y / Dae.UnitPixelSize.y);

		public static Buffer OpenFile (string filePath)
		{
			StreamReader streamReader = File.OpenText (filePath);
			string content = streamReader.ReadToEnd ();

			List<string> lines = new List<string> (content.Split ('\n'));

			for (int i = 0; i < lines.Count; i++)
			{
				lines[i] = lines[i].TrimEnd ('\r');
			}

			lines.ForEach (s => Console.WriteLine (s));

			Buffer buffer = new Buffer ();
			return buffer;
		}

		public RenderTarget renderTarget;
		private CharUnit[,] grid;
		public IVector Size => new IVector (grid.GetLength (0), grid.GetLength (1));

		public void Resize (IVector newSize)
		{
			grid = new CharUnit[newSize.x, newSize.y];
		}

		public Buffer (CharUnit[,] inputGrid)
		{
			grid = inputGrid;
		}
	}
}