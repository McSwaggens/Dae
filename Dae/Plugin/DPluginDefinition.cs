using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dae.Plugin
{
	internal class DPluginDefinition
	{
		public DPluginInformation information;
		public Type plugin;
		public List<DCustomComponentDefinition> customComponents = new List<DCustomComponentDefinition> ();

		public DPluginDefinition ( DPluginInformation information, Type plugin )
		{
			this.information = information;
			this.plugin = plugin;
		}
	}
}