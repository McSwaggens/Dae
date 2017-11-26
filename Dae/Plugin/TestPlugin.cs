using System;

namespace Dae.Plugin
{
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