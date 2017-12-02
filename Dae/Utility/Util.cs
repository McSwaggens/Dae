using System;

namespace Dae
{
	public static class Util
	{
		public static string HomeDirectory
		{
			get
			{
				if (Environment.OSVersion.Platform == PlatformID.Unix || Environment.OSVersion.Platform == PlatformID.MacOSX)
				{
					return Environment.GetEnvironmentVariable ("HOME") + "/";
				}
				else if (Environment.OSVersion.Platform == PlatformID.Win32Windows || Environment.OSVersion.Platform == PlatformID.Win32NT)
				{
					return Environment.ExpandEnvironmentVariables ("%HOMEDRIVE%%HOMEPATH%") + "\\";
				}

				throw new Exception ("Cannot find home directory for your Operating System...");
			}
		}

		public static string DocumentsDirectory => HomeDirectory + "Documents/";
		public static string CurrentPath => Environment.CurrentDirectory + "/";
		public static string PluginsDirectory => CurrentPath + "Plugins";
	}
}