using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dae
{
	public struct DModifiers
	{
		public bool shift;
		public bool control;
		public bool alt;

		public DModifiers ( bool shift, bool control, bool alt )
		{
			this.shift = shift;
			this.control = control;
			this.alt = alt;
		}
	}
}