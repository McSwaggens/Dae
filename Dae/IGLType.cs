using System;

namespace Dae
{
	public interface IGLType : ICloneable
	{
		void UniformUpload (int locationId);
	}
}