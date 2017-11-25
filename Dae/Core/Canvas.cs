using System.Collections.Generic;

namespace Dae
{
	public class Canvas : Component
	{
		// All components found inside of this Canvas
		public List<Component> subComponents = new List<Component> ();

		public Canvas ( CBuffer buffer )
		{
			this.buffer = buffer;
		}

		public Canvas ( IVector size )
		{
			this.buffer = new CBuffer (size);
		}

		public override void Render ()
		{
			// Render all components found inside of this canvas
			RenderSubComponents ();
		}

		public void AddComponent ( Component component )
		{
			this.subComponents.Add (component);
		}

		internal void RenderSubComponents ()
		{
			// Remove all null components
			subComponents.RemoveAll (component => component == null);

			// Render all sub components
			foreach (Component component in subComponents)
			{
				component.Render ();
				CBuffer.Blit (component.buffer, buffer, Vector.zero, component.Size, component.position);
			}
		}
	}
}