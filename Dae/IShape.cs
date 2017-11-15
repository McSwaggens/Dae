using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dae
{
	internal interface IShape : IEndi, IInitialize, IDisposable, IRender
	{
	}
}