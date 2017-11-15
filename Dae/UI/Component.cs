using System;

namespace Dae.UI
{
	public class Component : DObject
	{
		public bool focused;
		public bool visible;
		public Vector position;
		public Vector size;
		public CBuffer buffer;
		public Canvas parent;

		public bool HasParent => parent != null;

		public virtual void Render ()
		{
			throw new NotImplementedException ("The default Component Render function was called... For some reason...");
		}
	}
}