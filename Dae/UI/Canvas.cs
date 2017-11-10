using System.Collections.Generic;

namespace Dae.UI
{
	public class Canvas : Component
	{
		// All components found inside of this Canvas
		public List<Component> subComponents = new List<Component> ();

		public Buffer buffer;

		public Canvas (Canvas parentCanvas, Vector size, Buffer buffer = null)
		{
			this.size = size;
			this.parent = parentCanvas;
			buffer = buffer ?? new Buffer ();
		}

		public override void Render ()
		{
			// Enable the buffers renderTarget by pushing it to the renderTarget stack
			Dae.Graphics.renderTargetStack.Push (buffer.renderTarget);

			// Render all components found inside of this canvas
			RenderSubComponents ();

			// Pop the current renderTarget, enabling the last renderTarget
			Dae.Graphics.renderTargetStack.Pop ();

			// Render this canvas to the parent canvas
			// TODO: Render to parent canvas
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