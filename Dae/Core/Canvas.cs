using System.Collections.Generic;

namespace Dae
{
	public class Canvas : Component
	{
		internal static bool checkSignalRender = true;

		// All components found inside of this Canvas
		public List<Component> subComponents = new List<Component> ();

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
	}
}