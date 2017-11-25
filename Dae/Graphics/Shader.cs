using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;

namespace Dae
{
	public class Shader : IDisposable
	{
		private static readonly Dictionary<ShaderType, OpenTK.Graphics.OpenGL4.ShaderType> OGLShaderTypeDict =
			new Dictionary<ShaderType, OpenTK.Graphics.OpenGL4.ShaderType> ()
		{
				{ ShaderType.VERTEX,  OpenTK.Graphics.OpenGL4.ShaderType.VertexShader },
				{ ShaderType.FRAGMENT,  OpenTK.Graphics.OpenGL4.ShaderType.FragmentShader },
				{ ShaderType.GEOMETRY,  OpenTK.Graphics.OpenGL4.ShaderType.GeometryShader }
		};

		private static OpenTK.Graphics.OpenGL4.ShaderType GetOGLShaderType ( ShaderType type )
		{
			return OGLShaderTypeDict[type];
		}

		public static Shader CreateShader ( string source )
		{
			ShaderSourceGroup shaderGroup = new ShaderPreProcessor ().Parse (source);

			Shader shader;

			int status = 0;
			int programId = GL.CreateProgram ();

			foreach (ShaderSource shaderSource in shaderGroup.shaderSources)
			{
				OpenTK.Graphics.OpenGL4.ShaderType oglShaderType = GetOGLShaderType (shaderSource.shaderType);

				int shaderId = GL.CreateShader (oglShaderType);
				GL.ShaderSource (shaderId, shaderSource.source);
				GL.CompileShader (shaderId);

				GL.GetShader (shaderId, ShaderParameter.CompileStatus, out status);
				if (status != 1)
				{
					Logger.Log ("[SHADER COMPILE ERROR]: " + GL.GetShaderInfoLog (shaderId));

					return null;
				}

				GL.AttachShader (programId, shaderId);

				Logger.Log ($"Successfully compiled {oglShaderType.ToString ()}");
			}

			GL.LinkProgram (programId);

			GL.GetProgram (programId, GetProgramParameterName.LinkStatus, out status);

			if (status != 1)
			{
				Logger.Log ("[SHADER COMPILE ERROR]: " + GL.GetProgramInfoLog (programId));

				return null;
			}

			Logger.Log ("Successfully linked program.");

			shader = new Shader (programId);

			return shader;
		}

		public static List<Shader> allShaders = new List<Shader> ();

		internal static void ClearAll ()
		{
			allShaders.ForEach (s => s.Dispose ());
			allShaders.Clear ();
		}

		private int programId;

		private Shader ( int programId )
		{
			this.programId = programId;

			allShaders.Add (this);
		}

		public int GetUniform ( string name )
		{
			return GL.GetUniformLocation (programId, name);
		}

		public void Enable ()
		{
			GL.UseProgram (programId);
		}

		public void Dispose ()
		{
			GL.DeleteProgram (programId);
		}
	}
}