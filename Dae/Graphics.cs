using System.Collections.Generic;

namespace Dae
{
	public class Graphics
	{
		// List of all Shaders in the program
		private List<Shader> shaders = new List<Shader> ();

		// List of all Materials
		private List<Material> materials = new List<Material> ();

		public EnableStack<RenderTarget> renderTargetStack = new EnableStack<RenderTarget> (false);

		// Initialize the graphics library
		internal void Initialize ()
		{
			renderTargetStack.Push (RenderTarget.defaultRenderTarget);
		}

		// Free the graphics library
		internal void Shutdown ()
		{
			materials.Clear ();

			shaders.ForEach (shader => shader.Dispose ());
			shaders.Clear ();
		}

		// Register a shader
		internal void RegisterShader (Shader shader)
		{
			shaders.Add (shader);
		}

		// Register a material
		internal void RegisterMaterial (Material material)
		{
			materials.Add (material);
		}
	}
}