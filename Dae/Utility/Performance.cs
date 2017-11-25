using System;
using System.Diagnostics;

namespace Dae
{
	public static class Performance
	{
		public static double Analize ( Action action )
		{
			Stopwatch stopwatch = new Stopwatch ();
			stopwatch.Start ();
			action ();
			stopwatch.Stop ();
			return stopwatch.Elapsed.TotalMilliseconds;
		}

		public static (R returnValue, double time) Analize<R> ( Func<R> action )
		{
			Stopwatch stopwatch = new Stopwatch ();
			stopwatch.Start ();
			R r = action ();
			stopwatch.Stop ();
			return (r, stopwatch.Elapsed.TotalMilliseconds);
		}

		public static double AnalizePrint ( Action action, Action<double> printAction )
		{
			double time = Analize (action);
			printAction.Invoke (time);
			return time;
		}

		public static double AnalizePrint ( Action action, object obj = null )
		{
			double time = Analize (action);
			Logger.Log ($"Function " + ( obj == null ? "" : obj.ToString () + "." ) + action.Method.Name + $" took {time}ms to complete.");
			return time;
		}
	}
}