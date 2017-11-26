using System;

namespace Dae.Plugin
{
	public class DPlugin
	{
		internal DPluginInformation information;

		public virtual bool Load ()
		{
			throw new NotImplementedException ();
		}

		public virtual void Unload ()
		{
		}
	}
}