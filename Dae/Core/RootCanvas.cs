namespace Dae
{
	public class RootCanvas : Canvas
	{
		private RenderTarget renderTarget;

		/// <summary>
		/// Initializes a root canvas
		/// </summary>
		/// <param name="renderTarget">Specifies a RenderTarget that the root canvas should render too, this should be the default renderTarget (0)</param>
		public RootCanvas ( CBuffer buffer, RenderTarget renderTarget ) : base (buffer)
		{
			this.renderTarget = renderTarget;
		}

		public override void Render ()
		{
			base.Render ();

			for (int y = 0; y < Dae.gridSize.y; y++)
			{
				for (int x = 0; x < Dae.gridSize.x; x++)
				{
					CUnit unit = buffer[x, y];
					int aPos = ( y * Dae.gridSize.x ) + x;
					Dae.cachedUnitScreenPositions[aPos] = DMath.P01ToN1P1 (Dae.GetCorrectedPosition (new Vector (x, y))).ReversedYFull;
					int ascii = unit.ascii;
					int div = ( ascii / 16 );
					Dae.cachedUnitFontPositions[aPos] = new Vector (ascii - ( 16 * div ), div);
					Dae.cachedUnitForegroundColors[aPos] = unit.foregroundColor;
					Dae.cachedUnitBackgroundColors[aPos] = unit.backgroundColor;
				}
			}
		}

		public void ForceRenderAll ()
		{
			checkSignalRender = false;
			Render ();
			checkSignalRender = true;
		}

		public override void SignalRender ()
		{
			signalRender = true;
		}
	}
}