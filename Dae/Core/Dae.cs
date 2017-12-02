using Dae.Plugin;
using Dae.Properties;
using Dae.Scripting;
using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;
using System.Reflection;

namespace Dae
{
	public static class Dae
	{
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
			get
			{
				return shuttingDown;
			}
			internal set
			{
				shuttingDown = value;
			}
		}

		private static CBuffer rootBuffer;
		private static RootCanvas rootCanvas;
		public static RootCanvas RootCanvas => rootCanvas;

		private static Vector GetUnitSize ( IVector size ) => ( 1f / (Vector)( size ) );

		private static Vector unitScale = new Vector (1.2f, 1.1f);

		internal static readonly IVector hdScale = new IVector (100, 40);

		private static Vector cachedUnitSize;

		public static IVector gridSize = IVector.one;

		private static List<DWindow> windows = new List<DWindow> ();

		internal static void AlertWindowCreated ( DWindow window )
		{
			windows.Add (window);
		}

		// Initialize/Start DAE
		internal static void Start ()
		{
			rootBuffer = new CBuffer (new IVector (0, 0));
			rootCanvas = new RootCanvas (rootBuffer, RenderTarget.defaultRenderTarget);

			DWindow window = new DWindow ();
			window.ProcessEvents ();
			window.RenderFrameToScreen ();

			// Initialize the static Graphics class
			Graphics.Initialize ();

			// Clear out the screen (make sure it's not white when we start up the window) (Burns my eyes)
			GL.ClearColor (0, 0, 0, 1);
			GL.Clear (ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

			window.Title = "LOADING ...";

			window.RenderFrameToScreen ();

			// Initialize PluginSystem
			PluginSystem.LoadPluginsFromAssembly (Assembly.GetExecutingAssembly ());

			PluginSystem.LoadDllsFromPath (Util.CurrentPath + "Plugins/");

			Loader.PrepareScript ();
			Loader.Run ();

			Button button = new Button (new IVector (30, 20));
			rootCanvas.AddComponent (button);
			button.position = 4;

			window.Title = "Dae";

			// Enter the main loop
			Run ();

			// Actually shutdown/free DAE from the system (GPU, Hooks & Memory)
			Shutdown ();
		}

		internal static float averageRenderTime = 0f;
		internal static float largestRenderTime = 0f;

		private static Material unitMaterial;

		// Main Loop
		private static void Run ()
		{
			IsRunning = true;

			Initialize ();

			double _largestRenderTime = 0D;
			double renderTimeSum = 0D;
			int renderCount = 0;

			new Scheduler (() =>
			{
				averageRenderTime = (float)( renderTimeSum / renderCount );
				largestRenderTime = (float)( _largestRenderTime );

				//Print ($"Performance summary:\n\tAverage Render Time: {averageRenderTime}\n\tLargest Render Time: {largestRenderTime}");

				_largestRenderTime = 0D;
				renderTimeSum = 0D;
				renderCount = 0;
			}).Start (1f);

			// Loop while DAE is supposed to be running
			while (IsRunning)
			{
				Time.Update ();

				CheckWindowInput ();

				Scheduler.allSchedulers.actual.ForEach (sch => sch.Update ());

				rootBuffer.Clear (Color.black);

				double renderTime = Performance.Analize (() => Render ());

				if (renderTime > _largestRenderTime)
				{
					_largestRenderTime = renderTime;
				}

				renderTimeSum += renderTime;

				renderCount++;

				//Logger.Print ("Rendering took " + ( timer.Elapsed.TotalMilliseconds ) + "ms to complete.");

				Scheduler.allSchedulers.Update ();

				Present ();

				// Check if we're supposed to shutdown DAE and thus, the main loop we're currently in
				if (IsShuttingDown)
				{
					IsRunning = false; // Essentually a break...
				}
			}

			// End of the Run method, the Start method should begin shutting down DAE now...
		}

		private static Texture fontTexture;

		private static void Initialize ()
		{
			windows.ForEach (w => AlertWindowResized (w));

			Time.SyncTime ();

			fontTexture = new Texture (Resources.DeJaVuFontSheet, TextureFilter.Linear);

			unitMaterial = new Material (Shader.CreateShader (Resources.Unit), new Cell[]
			{
				new Cell("size", GetUnitSize(gridSize) * unitScale),
				new Cell("font", fontTexture)
			});
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

		public static Vector GetCorrectedPosition ( Vector position )
		{
			return ( cachedUnitSize * position ) + ( cachedUnitSize / 2f );
		}

		/// <summary>
		/// Render everything to a frameBuffer
		/// DOES NOT SWAP THE BUFFER
		/// </summary>
		private static void Render ()
		{
			GL.ClearColor (0.30f, 0.30f, 0.30f, 1);

			rootCanvas.Render ();

			unitMaterial.Enable ();

			Quad.Enable ();
			Quad.Render (cachedUnitScreenPositions, cachedUnitFontPositions, cachedUnitForegroundColors, cachedUnitBackgroundColors);
		}

		private static void Present ()
		{
			foreach (DWindow window in windows)
			{
				window.RenderFrameToScreen ();
			}
		}

		internal static Vector[] cachedUnitScreenPositions;
		internal static Vector[] cachedUnitFontPositions;
		internal static Color[] cachedUnitForegroundColors;
		internal static Color[] cachedUnitBackgroundColors;

		internal static void AlertWindowResized ( DWindow window )
		{
			GL.Viewport (0, 0, window.Width, window.Height);

			IVector res = new IVector (window.Width, window.Height);
			gridSize = res / ( new IVector (1920, 1080) / hdScale );

			cachedUnitSize = GetUnitSize (gridSize);
			cachedUnitScreenPositions = new Vector[gridSize.y * gridSize.x];
			cachedUnitFontPositions = new Vector[gridSize.y * gridSize.x];
			cachedUnitForegroundColors = new Color[gridSize.y * gridSize.x];
			cachedUnitBackgroundColors = new Color[gridSize.y * gridSize.x];

			IVector oldSize = rootCanvas.Size;

			bool rootCanvasSizeChanged = gridSize != oldSize;

			CBuffer oldBuffer = rootBuffer;
			rootCanvas.ChangeSize (gridSize);
			rootBuffer = rootCanvas.buffer;

			if (rootCanvasSizeChanged)
			{
				window.Title = $"Dae | Size: {gridSize.x}x{gridSize.y}";
			}

			unitMaterial?.Set ("size", GetUnitSize (gridSize) * unitScale);

			CBuffer.Blit (oldBuffer, rootBuffer, IVector.zero, oldBuffer.Size, IVector.zero);

			for (int y = 0; y < gridSize.y; y++)
			{
				for (int x = 0; x < gridSize.x; x++)
				{
					CUnit unit = rootBuffer[x, y];
					int aPos = ( y * gridSize.x ) + x;
					cachedUnitScreenPositions[aPos] = DMath.P01ToN1P1 (GetCorrectedPosition (new Vector (x, y))).ReversedYFull;
				}
			}
		}

		internal static void AlertWindowClosing ( DWindow window )
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