using System;

namespace Dae
{
	public interface IUniform : ICloneable
	{
		void Upload (int locationId);
	}
}