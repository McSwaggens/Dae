using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dae
{
	internal class Shape
	{
		public static readonly Quad quad = new Quad ();

		internal static IShape currentShape = null;

		public static void Enable (IShape shape)
		{
			if (currentShape != shape)
			{
				currentShape?.Disable ();
				shape.Enable ();
				currentShape = shape;
			}
		}

		public static void Initialize ()
		{
			quad.Initialize ();
		}

		public static void Dispose ()
		{
			quad.Dispose ();
		}

		public static void Draw ()
		{
			currentShape.Render ();
		}
	}
}