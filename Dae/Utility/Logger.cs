using System;
using System.Diagnostics;

namespace Dae
{
	public static class Logger
	{
		/// <summary>
		/// Un-important information and/or debug information not relevant to the operator of the program.
		/// This information should be ignored.
		/// </summary>
		/// <param name="msg"></param>
		public static void Log ( object msg )
		{
			Debug.WriteLine ("[LOG]: " + msg.ToString ());
		}

		/// <summary>
		/// Something has gone wrong, noteworthy and/or important for the operator to see,
		///	not too serious that the program should shutdown but may malfunction
		/// </summary>
		/// <param name="msg">Information on this warning</param>
		public static void Warning ( object msg )
		{
			Debug.WriteLine ("[WARNING]: " + msg.ToString ());
		}

		/// <summary>
		/// An error is where a critical part of the program was not able to process correctly, where there is no other option but to terminate the process.
		/// </summary>
		/// <param name="msg"></param>
		public static void Error ( object msg )
		{
			Debug.WriteLine ("[ERROR]: " + msg.ToString ());
		}

		public static void Exception ( Exception exception )
		{
			Debug.WriteLine ("[ERROR] [EXCEPTION]: " + exception.ToString ());
		}
	}
}