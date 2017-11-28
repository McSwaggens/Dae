using System;

namespace Dae.Plugin
{
	[DCustomComponent (name = "Button")]
	internal class Button : Component
	{
		public override void Render ()
		{
			buffer.Clear (Color3.white);
			buffer.Write ("Click Me!", Color3.black, Color3.white, new IVector (1, Size.y / 2));
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