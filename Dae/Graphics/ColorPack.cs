namespace Dae
{
	public struct ColorPack
	{
		public Color3 foreground;
		public Color3 background;

		public ColorPack ( Color3 foreground, Color3 background )
		{
			this.foreground = foreground;
			this.background = background;
		}
	}
}