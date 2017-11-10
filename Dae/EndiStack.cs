using System.Collections.Generic;

namespace Dae
{
	public class EndiStack<T> : Stack<T> where T : class, IEndi
	{
		public EndiStack (List<T> stack = null, bool canPopLast = true) : base (stack, canPopLast)
		{
		}

		public EndiStack (bool canPopLast = true) : base (canPopLast)
		{
		}

		protected override void OnStackPopped (T item)
		{
			item?.Disable ();
			Top?.Enable ();
		}

		protected override void OnStackPushed (T oldItem)
		{
			oldItem?.Disable ();
			Top?.Enable ();
		}
	}

	public class EnableStack<T> : Stack<T> where T : class, IEnable
	{
		public EnableStack (List<T> stack = null, bool canPopLast = true) : base (stack, canPopLast)
		{
		}

		public EnableStack (bool canPopLast = true) : base (canPopLast)
		{
		}

		protected override void OnStackPopped (T item)
		{
			Top?.Enable ();
		}

		protected override void OnStackPushed (T oldItem)
		{
			Top?.Enable ();
		}
	}

	public class DisableStack<T> : Stack<T> where T : class, IDisable
	{
		public DisableStack (List<T> stack = null, bool canPopLast = true) : base (stack, canPopLast)
		{
		}

		public DisableStack (bool canPopLast = true) : base (canPopLast)
		{
		}

		protected override void OnStackPopped (T item)
		{
			item?.Disable ();
		}

		protected override void OnStackPushed (T oldItem)
		{
			oldItem?.Disable ();
		}
	}
}