using Dae.UI;
using System.Collections.Generic;
using System;
using Dae.Properties;
using OpenTK.Graphics.OpenGL4;

namespace Dae
{
	public class Dae
	{
		private static Graphics graphics;

		public static Graphics Graphics
		{
			get { return graphics; }
			internal set { graphics = value; }
		}

		private static bool running = false;

		public static bool IsRunning
		{
			get
			{
				return running;
			}
			set
			{
				running = value;
			}
		}

		private static bool shuttingDown;

		public static bool IsShuttingDown
		{
			get { return shuttingDown; }
			internal set { shuttingDown = value; }
		}

		private static Canvas rootCanvas;
		public static Canvas RootCanvas => rootCanvas;

		private static IVector unitPixelSize = new IVector (1920 / 40, 1080 / 20);
		public static IVector UnitPixelSize => unitPixelSize;

		public static IVector GetGridSize (IVector resolution) => new IVector (resolution.x / UnitPixelSize.x, resolution.y / UnitPixelSize.y);

		private static List<DWindow> windows = new List<DWindow> ();

		internal static void AlertWindowCreated (DWindow window)
		{
			windows.Add (window);
		}

		public static void Register (DObject obj)
		{
			// Register our blank object (An object without identification) to its correct place in the program
			// For example, if we register a Shader, we need to pass it to our Graphics class etc...

			if (/* Shader */ obj is Shader)
			{
				Graphics.RegisterShader ((Shader)obj);
			}

			if (/* Material */ obj is Material)
			{
				Graphics.RegisterMaterial ((Material)obj);
			}

			if (/* RenderTarget */ obj is RenderTarget)
			{
				Graphics.RegisterRenderTarget ((RenderTarget)obj);
			}
		}

		// Initialize/Start DAE
		internal static void Start ()
		{
			DWindow window = new DWindow ();

			// Initialize the Shape API
			Shape.Initialize ();

			// Create a graphics context ...
			graphics = new Graphics ();

			// ... and initialize it.
			Graphics.Initialize ();

			rootCanvas = new Canvas (null, window.size);

			// Enter the main loop
			Run ();

			// Actually shutdown/free DAE from the system (GPU, Hooks & Memory)
			Shutdown ();
		}

		private static Material unitMaterial;

		// Main Loop
		private static void Run ()
		{
			IsRunning = true;

			Initialize ();

			// Loop while DAE is supposed to be running
			while (IsRunning)
			{
				CheckWindowInput ();

				Render ();

				// Check if we're supposed to shutdown DAE and thus, the main loop we're currently in
				if (IsShuttingDown)
				{
					IsRunning = false; // Essentually a break...
				}
			}

			// End of the Run method, the Start method should begin shutting down DAE now...
		}

		private static void Initialize ()
		{
			unitMaterial = new Material (Shader.CreateShader (Resources.Unit), new Cell[] { });
		}

		private static void CheckWindowInput ()
		{
			foreach (DWindow window in windows)
			{
				window.ProcessEvents ();
			}

			windows.RemoveAll (window => window.closing);

			if (windows.Count == 0)
			{
				// All windows have been closed!

				MarkShutdown ();
			}
		}

		/// <summary>
		/// Display the current frame to the screen
		/// </summary>
		private static void Render ()
		{
			GL.ClearColor (0.05f, 0.05f, 0.05f, 1);

			unitMaterial.Enable ();

			Shape.Enable (Shape.quad);
			Shape.Draw ();

			foreach (DWindow window in windows)
			{
				window.RenderFrameToScreen ();
			}
		}

		internal static void AlertWindowResized (DWindow window)
		{
		}

		internal static void AlertWindowClosing (DWindow window)
		{
			window.closing = true;
		}

		public static void MarkShutdown ()
		{
			// Mark the engine to be shutdown
			IsShuttingDown = true;
		}

		internal static void Shutdown ()
		{
			// Shutdown everything related to graphics
			Graphics.Shutdown ();
		}
	}
}