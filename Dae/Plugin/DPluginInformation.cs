using System;

namespace Dae.Plugin
{
	[AttributeUsage (AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
	public class DPluginInformation : Attribute
	{
		public string name;
		public string author;
		public string description;
	}
}