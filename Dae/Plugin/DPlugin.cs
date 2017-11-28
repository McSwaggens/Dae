using System;
using System.Collections.Generic;

namespace Dae.Plugin
{
	public class DPlugin
	{
		internal DPluginInformation information;
		internal List<DCustomComponentDefinition> customComponents = new List<DCustomComponentDefinition> ();

		public virtual bool Load ()
		{
			throw new NotImplementedException ();
		}

		public virtual void Unload ()
		{
		}
	}
}