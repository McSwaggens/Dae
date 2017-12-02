using Dae;
using Dae.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandBar
{
	[DPluginInformation (name = "CommandBar", description = "A bar that you can enter commands into.", author = "Daniel Jones")]
	public class CommandBarPlugin : DPlugin
	{
		public override bool Load ()
		{
			Logger.Log ("CommandBar Plugin has been loaded!");

			return true;
		}
	}
}