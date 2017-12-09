using System.Collections.Generic;

namespace Dae
{
	public class Canvas : Component
	{
		internal static bool checkSignalRender = true;

		// All components found inside of this Canvas
		public SwapStack<Component> subComponents = new SwapStack<Component> ();

		public Component FocusedComponent => subComponents[0];

		public Canvas ( CBuffer buffer )
		{
			this.buffer = buffer;
		}

		public Canvas ( IVector size ) : base (size)
		{
		}

		public override void Render ()
		{
			// Render all components found inside of this canvas
			RenderSubComponents ();
		}

		public override void ReleaseFocus ()
		{
			base.ReleaseFocus ();
			foreach (Component component in subComponents)
			{
				if (component.focused)
				{
					component.ReleaseFocus ();
				}
			}
		}

		public override void Focus ()
		{
			focused = true;
			parent?.Focus ();
		}

		public void AddComponent ( Component component )
		{
			if (component.parent == null)
			{
				subComponents.Add (component);
				component.parent = this;
			}
			else
			{
				Logger.Warning ("Attempted to add a component to multiple canvases, if you want to move the component from one to another, use component.Move(canvas)");
			}
		}

		public override void Tick ()
		{
			TickSubComponents ();
		}

		internal void TickSubComponents ()
		{
			foreach (Component component in subComponents)
			{
				if (component.receiveTicks)
				{
					component.Tick ();
				}
			}
		}

		internal void RenderSubComponents ()
		{
			// Remove all null components
			subComponents.RemoveAll (component => component == null);

			// Render all sub components
			foreach (Component component in subComponents)
			{
				if (component.Size.x > 0 && component.Size.y > 0)
				{
					if (component.signalRender || !checkSignalRender)
					{
						component.signalRender = false;
						component.Render ();
					}
					CBuffer.Blit (component.buffer, buffer, Vector.zero, component.Size, component.position);
				}
			}
		}

		public override void ChangeSize ( IVector newSize )
		{
			base.ChangeSize (newSize);

			subComponents.ForEach (c => c.OnParentSizeChanged (this, newSize));
		}

		public override void OnKeyDown ( DKey key, DModifiers modifiers )
		{
			foreach (Component component in subComponents)
			{
				if (component.focused)
				{
					component.OnKeyDown (key, modifiers);
				}
			}
		}

		public override void OnKeyPressed ( DKey key, DModifiers modifiers )
		{
			foreach (Component component in subComponents)
			{
				if (component.focused)
				{
					component.OnKeyPressed (key, modifiers);
				}
			}
		}

		public override void OnKeyUp ( DKey key, DModifiers modifiers )
		{
			foreach (Component component in subComponents)
			{
				if (component.focused)
				{
					component.OnKeyUp (key, modifiers);
				}
			}
		}

		public override void OnMouseMove ( IVector localPosition )
		{
			foreach (Component component in subComponents)
			{
				if (localPosition >= component.position && localPosition < component.position + component.Size)
				{
					IVector newLocalPosition = localPosition - component.position;

					if (!component.mouseInside)
					{
						component.OnMouseEnter (newLocalPosition);
					}

					component.OnMouseMove (newLocalPosition);
				}
				else if (component.mouseInside)
				{
					component.OnMouseLeave ();
				}
			}
		}

		public override void OnMouseLeave ()
		{
			base.OnMouseLeave ();
			foreach (Component component in subComponents)
			{
				component.OnMouseLeave ();
			}
		}
	}
}