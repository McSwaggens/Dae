using System.Collections.Generic;

namespace Dae
{
	public class Stack<T> where T : class
	{
		protected List<T> stack = new List<T> ();
		protected bool canPopLast = true;

		public T Top
		{
			get
			{
				return stack.Count > 0 ? stack[stack.Count - 1] : null;
			}
		}

		public uint Size
		{
			get
			{
				return (uint)stack.Count;
			}
		}

		public Stack ( List<T> stack = null, bool canPopLast = true )
		{
			if (stack != null)
			{
				this.stack = stack;
			}

			this.canPopLast = canPopLast;
		}

		public Stack ( bool canPopLast = true )
		{
			this.canPopLast = canPopLast;
		}

		public void Push ( T item )
		{
			T oldTop = Top;

			stack.Add (item);

			OnStackPushed (oldTop);
		}

		public void Pop ( uint amount = 1 )
		{
			if (amount >= 1)
			{
				for (int i = 0; i < amount; i++)
				{
					PopTop ();
				}
			}

			void PopTop ()
			{
				if (
					( stack.Count <= 1 && !canPopLast ) // Check canPopLast
					|| ( stack.Count == 0 ) // Make sure there is something to actually pop
					)
				{
					return;
				}

				int index = ( stack.Count - 1 );

				T item = stack[index];

				// Maybe do something with the item

				stack.RemoveAt (index);
			}
		}

		protected virtual void OnStackPopped ( T item )
		{
		}

		protected virtual void OnStackPushed ( T oldTop )
		{
		}
	}
}