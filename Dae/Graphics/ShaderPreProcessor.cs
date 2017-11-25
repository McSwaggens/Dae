using System.Collections.Generic;

namespace Dae
{
	internal enum ShaderPPStage : uint
	{
		VERTEX = ShaderType.VERTEX,
		FRAGMENT = ShaderType.FRAGMENT,
		GEOMETRY = ShaderType.GEOMETRY,
		HEADER = ShaderType.GEOMETRY + 1
	}

	internal class ShaderPreProcessor
	{
		private List<ShaderSource> shaderSources;
		private ShaderPPStage stage;
		private string header;
		private string current;

		private void Push ()
		{
			if (stage != ShaderPPStage.HEADER)
			{
				ShaderSource shaderSource = new ShaderSource ((ShaderType)stage, current);
				shaderSources.Add (shaderSource);
			}

			current = header;
		}

		private void PushLine ( string line )
		{
			line += "\n";
			if (stage == ShaderPPStage.HEADER)
			{
				header += line;
			}
			else
			{
				current += line;
			}
		}

		public ShaderSourceGroup Parse ( string source )
		{
			// Reset all fields
			shaderSources = new List<ShaderSource> ();
			stage = ShaderPPStage.HEADER;
			header = "";
			current = "";

			// Get an array of all the lines in the file
			string[] lines = source.Replace ("\r", "").Split ('\n');

			// Loop through all lines in the file
			foreach (string line in lines)
			{
				if (line == "#frag")
				{
					Push ();
					stage = ShaderPPStage.FRAGMENT;
				}
				else
				if (line == "#vert")
				{
					Push ();
					stage = ShaderPPStage.VERTEX;
				}
				else
				if (line == "#geometry")
				{
					Push ();
					stage = ShaderPPStage.GEOMETRY;
				}
				else
				{
					PushLine (line);
				}
			}

			Push ();

			return new ShaderSourceGroup (shaderSources.ToArray ());
		}
	}
}