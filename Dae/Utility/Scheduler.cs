using System;

namespace Dae
{
	public class Scheduler
	{
		internal static StackManager<Scheduler> allSchedulers = new StackManager<Scheduler> ();

		internal static void ClearAll ()
		{
			allSchedulers.Clear ();
		}

		private Action actionTick;
		private Action actionEnd;
		private bool running = false;
		private float startTime;
		private float stepTime;
		private float interval;
		private float until;

		public Scheduler ( Action actionTick = null, Action actionEnd = null )
		{
			this.actionTick = actionTick;
			this.actionEnd = actionEnd;

			startTime = Time.now;
		}

		internal void Update ()
		{
			float localCurrent = Time.now - startTime;

			if (actionTick != null)
			{
				if (localCurrent - stepTime >= interval)
				{
					stepTime = localCurrent;
					actionTick?.Invoke ();
				}
			}

			if (localCurrent >= until)
			{
				Stop ();
				actionEnd?.Invoke ();
			}
		}

		public void Start ( float interval, float until = float.MaxValue )
		{
			if (!running)
			{
				running = true;

				this.interval = interval;
				this.until = until;

				startTime = Time.now;
				allSchedulers.Add (this);
			}
			else
			{
				Logger.Log ("[Scheduler] Attempted to start a scheduler already running...");
			}
		}

		public void Stop ()
		{
			if (running)
			{
				running = false;
				allSchedulers.Remove (this);
			}
			else
			{
				Logger.Log ("[Scheduler] Attempted to stop a scheduler whilst not running...");
			}
		}
	}
}