using System;

namespace Dae
{
	public class Component
	{
		public bool focused;
		public bool visible;
		public IVector position;
		public IVector Size => buffer.Size;
		public CBuffer buffer;

		public virtual void Render ()
		{
			throw new NotImplementedException ("The default Component Render function was called... For some reason...");
		}
	}
}