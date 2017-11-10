using System.Diagnostics;

namespace Dae
{
	public class Logger
	{
		public static void Print (object str)
		{
			Debug.WriteLine ("[LOG]: " + str.ToString ());
		}
	}
}