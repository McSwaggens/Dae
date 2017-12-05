using System;
using System.IO;
using System.Windows.Forms;

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
		public static string CurrentPath => Path.GetDirectoryName (Application.ExecutablePath) + "/";
		public static string PluginsDirectory => CurrentPath + "Plugins";

		public static class PositionAnchor
		{
			public enum AnchorY
			{
				Bottom,
				Top,
				Middle
			}

			public enum AnchorX
			{
				Right,
				Left,
				Middle
			}

			public static IVector AnchorComponent ( Component c, IVector desiredPosition, AnchorX xAnchor, AnchorY yAnchor ) => Anchor (c.Size, desiredPosition, xAnchor, yAnchor);

			public static IVector Anchor ( IVector size, IVector desiredPosition, AnchorX xAnchor, AnchorY yAnchor )
			{
				IVector pos = IVector.zero;

				if (xAnchor == AnchorX.Left)
				{
					pos.x = desiredPosition.x;
				}

				if (xAnchor == AnchorX.Right)
				{
					pos.x = desiredPosition.x - size.x;
				}

				if (xAnchor == AnchorX.Middle)
				{
					pos.x = desiredPosition.x - ( size.x / 2 );
				}

				if (yAnchor == AnchorY.Top)
				{
					pos.y = desiredPosition.y;
				}

				if (yAnchor == AnchorY.Bottom)
				{
					pos.y = desiredPosition.y - size.y;
				}

				if (yAnchor == AnchorY.Middle)
				{
					pos.y = desiredPosition.y - ( size.y / 2 );
				}

				return pos;
			}

			public static IVector AnchorSize ( IVector yourSize, IVector parentSize, AnchorX xAnchor, AnchorY yAnchor )
			{
				IVector pos = IVector.zero;

				if (xAnchor == AnchorX.Left)
				{
					pos.x = 0;
				}

				if (xAnchor == AnchorX.Right)
				{
					pos.x = parentSize.x - yourSize.x;
				}

				if (xAnchor == AnchorX.Middle)
				{
					pos.x = ( parentSize.x / 2 ) - ( yourSize.x / 2 );
				}

				if (yAnchor == AnchorY.Top)
				{
					pos.y = 0;
				}

				if (yAnchor == AnchorY.Bottom)
				{
					pos.y = parentSize.y - yourSize.y;
				}

				if (yAnchor == AnchorY.Middle)
				{
					pos.y = ( parentSize.y / 2 ) - ( yourSize.y / 2 );
				}

				return pos;
			}
		}
	}
}