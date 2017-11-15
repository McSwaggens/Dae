using System;
using System.Collections.Generic;

namespace Dae
{
	public class Graphics
	{
		// List of all Shaders in the program
		private List<Shader> shaders = new List<Shader> ();

		// List of all Materials
		private List<Material> materials = new List<Material> ();

		// List of all RenderTargets
		private List<RenderTarget> renderTargets = new List<RenderTarget> ();

		public EnableStack<RenderTarget> renderTargetStack = new EnableStack<RenderTarget> (false);

		public void RenderTextBuffer (CBuffer buffer)
		{
		}

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

			renderTargets.ForEach (rt => rt.Dispose ());
			renderTargets.Clear ();
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

		internal void RegisterRenderTarget (RenderTarget renderTarget)
		{
			renderTargets.Add (renderTarget);
		}
	}
}