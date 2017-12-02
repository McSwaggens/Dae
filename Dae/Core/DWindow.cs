using OpenTK;
using OpenTK.Graphics;
using System;
using System.Drawing;

namespace Dae
{
	public enum WindowMode
	{
		FullScreen,
		WindowedFullScreen,
		Windowed
	}

	public class DWindow : IDisposable
	{
		private GameWindow window;
		internal bool closing = false;

		public int Width
		{
			get
			{
				return window.Width;
			}
		}

		public int Height
		{
			get
			{
				return window.Height;
			}
		}

		public string Title
		{
			get
			{
				return window.Title;
			}
			set
			{
				window.Title = value;
			}
		}

		public Vector size;

		public DWindow ()
		{
			window = new GameWindow (1920, 1080, GraphicsMode.Default, "DAE", GameWindowFlags.Default, DisplayDevice.Default, 4, 4, GraphicsContextFlags.ForwardCompatible);
			window.Visible = true;

			window.Closing += TkGameWindow_Closing;
			window.Resize += OnResized;
			window.VSync = VSyncMode.On;
			SetWindowMode (WindowMode.Windowed);

			Dae.AlertWindowCreated (this);
			Dae.AlertWindowResized (this);
		}

		public Vector PixelToScreenPosition ( Vector vector )
		{
			Vector cursorWindow = CorrectMousePosition (vector);
			//cursorWindow.y = 1 - cursorWindow.y;
			return ( new Vector (( cursorWindow.x / Width ), ( ( Height - cursorWindow.y ) / Height )) * 2f ) - 1f;
		}

		public Vector CorrectMousePosition ( Vector vector )
		{
			return window.PointToClient (new Point ((int)vector.x, (int)vector.y));
		}

		public void SetWindowMode ( WindowMode mode )
		{
			if (mode == WindowMode.FullScreen)
			{
				window.WindowState = WindowState.Fullscreen;
				window.WindowBorder = WindowBorder.Hidden;
			}
			else
			if (mode == WindowMode.WindowedFullScreen)
			{
				window.WindowState = WindowState.Maximized;
				window.WindowBorder = WindowBorder.Hidden;
			}
			else
			if (mode == WindowMode.Windowed)
			{
				window.WindowState = WindowState.Normal;
				window.WindowBorder = WindowBorder.Resizable;
			}
		}

		private void OnResized ( object sender, EventArgs e )
		{
			size = new Vector (Width, Height);
			Dae.AlertWindowResized (this);
		}

		private void TkGameWindow_Closing ( object sender, System.ComponentModel.CancelEventArgs e )
		{
			Dae.AlertWindowClosing (this);
		}

		internal void ProcessEvents ()
		{
			window?.ProcessEvents ();
		}

		public void RenderFrameToScreen ()
		{
			window?.SwapBuffers ();
		}

		public void Dispose ()
		{
			window.Dispose ();
			window = null;
		}
	}
}