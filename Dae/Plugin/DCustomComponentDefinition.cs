using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dae.Plugin
{
	internal class DCustomComponentDefinition
	{
		public DCustomComponent information;
		public Type type;

		public DCustomComponentDefinition ( DCustomComponent information, Type type )
		{
			this.information = information;
			this.type = type;
		}
	}
}