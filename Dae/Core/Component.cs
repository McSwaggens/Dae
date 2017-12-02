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

		public Component ()
		{
		}

		public Component ( IVector size )
		{
			buffer = new CBuffer (size);
		}

		public virtual void ChangeSize ( IVector newSize )
		{
			buffer.Resize (newSize);
			OnSizeChanged (newSize);
		}

		public virtual void OnParentSizeChanged ( IVector parentNewSize )
		{
		}

		public virtual void Render ()
		{
			throw new NotImplementedException ("The default Component Render function was called... For some reason...");
		}

		public virtual void OnSizeChanged ( IVector size )
		{
		}
	}
}