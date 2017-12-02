using System;

namespace Dae.Plugin
{
	[DCustomComponent (name = "Button")]
	internal class Button : Component
	{
		public Button ( IVector size ) : base (size)
		{
		}

		public override void OnParentSizeChanged ( IVector parentNewSize )
		{
			ChangeSize (parentNewSize.Quarter);
		}

		public override void Render ()
		{
			buffer.Blank ();
			buffer.DrawFrame (Color.red);
			buffer.Write ("Hello", Color.white, Color.black, IVector.one);
		}
	}

	[DPluginInformation (name = "TestPlugin", author = "Daniel", description = "A simple plugin for testing our DAE's plugin system")]
	internal class TestPlugin : DPlugin
	{
		public override bool Load ()
		{
			Logger.Log ("Hello World from the TestPlugin Loader function!");

			return true; // Ran successfully
		}

		public override void Unload ()
		{
		}

		public void HelloWorld ()
		{
			Logger.Log ("Hello World!");
		}
	}
}