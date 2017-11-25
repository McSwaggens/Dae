namespace Dae
{
	public class ShaderSource
	{
		public ShaderType shaderType;
		public string source;

		public ShaderSource ( ShaderType shaderType, string source )
		{
			this.shaderType = shaderType;
			this.source = source;
		}
	}
}