using System.Collections.Generic;

namespace Dae.UI
{
	public class Canvas : Component
	{
		// All components found inside of this Canvas
		public List<Component> subComponents = new List<Component> ();

		public Canvas (Canvas parentCanvas, Vector size, CBuffer buffer = null)
		{
			this.size = size;
			this.parent = parentCanvas;
			buffer = buffer ?? new CBuffer (size);
		}

		public override void Render ()
		{
			// Render all components found inside of this canvas
			RenderSubComponents ();

			// Render this canvas to the parent canvas
		}

		internal void RenderSubComponents ()
		{
			// Remove all null components
			subComponents.RemoveAll (component => component == null);

			// Render all sub components
			foreach (Component component in subComponents)
			{
				component.Render ();
			}
		}
	}
}