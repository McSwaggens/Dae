using System;
using static Dae.Util.PositionAnchor;

namespace Dae
{
	public class Component
	{
		public bool focused;
		public bool visible;
		public IVector position;
		public IVector Size => buffer.Size;
		public CBuffer buffer;
		public Canvas parent;
		internal bool signalRender = true;
		internal bool receiveTicks = true;

		// Virtual because it might be the root canvas (doesn't have a parent)
		public virtual void SignalRender ()
		{
			signalRender = true;
			parent?.SignalRender ();
		}

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
			SignalRender ();
		}

		public void SetPosition ( IVector desiredPosition, AnchorX xAnchor, AnchorY yAnchor ) => position = AnchorComponent (this, desiredPosition, xAnchor, yAnchor);

		public virtual void OnParentSizeChanged ( Canvas parent, IVector parentNewSize )
		{
		}

		public virtual void Tick ()
		{
		}

		public virtual void Render ()
		{
			throw new NotImplementedException ("The default Component Render function was called... For some reason...");
		}

		public virtual void OnSizeChanged ( IVector size )
		{
		}

		public virtual void OnKeyDown ( DKey key, DModifiers modifiers )
		{
		}

		public virtual void OnKeyUp ( DKey key, DModifiers modifiers )
		{
		}

		public virtual void OnKeyPressed ( DKey key, DModifiers modifiers )
		{
		}
	}
}