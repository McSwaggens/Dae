namespace Dae
{
	public static class Graphics
	{
		public static EnableStack<RenderTarget> renderTargetStack = new EnableStack<RenderTarget> (false);

		// Initialize the graphics library
		internal static void Initialize ()
		{
			Quad.Initialize ();
		}

		// Free the graphics library
		internal static void Shutdown ()
		{
			Texture.ClearAll ();
			Shader.ClearAll ();
			RenderTarget.ClearAll ();
		}
	}
}