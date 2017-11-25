using System;
using static Dae.Logger;

namespace Dae
{
	public class Time
	{
		public delegate void TimeEventHandler ();

		public static event TimeEventHandler OnSecond;

		public static float delta = 1.0f;
		public static float now = 0.0f;
		public static ulong fps = 0;
		public static bool printDebugInformation = false;
		public static ulong frames = 0;
		public static ulong frameStep = 0;

		internal static long startTimeMS = 0;
		private static double frameStepTime = 0;

		public static long currentTimeMS
		{
			get
			{
				return DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
			}
		}

		internal static void SyncTime ()
		{
			startTimeMS = currentTimeMS;
		}

		internal static void Update ()
		{
			frames++;

			long nowTime = currentTimeMS;
			double lastTime = now;

			now = ( ( nowTime - startTimeMS ) ) / 1000.0f;
			float lastDelta = delta;
			delta = (float)( now - lastTime );

			if (now - frameStepTime >= 1.0)
			{
				fps = frames - frameStep;
				frameStep = frames;
				frameStepTime = now;

				OnSecond?.Invoke ();

				if (printDebugInformation)
				{
					Log ($"SysTime Debug Information: \n\tfps = {fps}\n\ttime = {now}\n\tdelta_time = {delta}\n\tframes = {frames}\n\tframe_step = {frameStep}\n\tframe_step_time = {frameStepTime}\n\tstart_time_ms = {startTimeMS}\n");
				}
			}
		}
	}
}