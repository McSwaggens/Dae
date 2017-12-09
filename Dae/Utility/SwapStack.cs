using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dae
{
	/// <summary>
	/// A swap stack is a List that has some extra functions primarily for stacks
	/// </summary>
	/// <typeparam name="T">Type</typeparam>
	public class SwapStack<T> : List<T>
	{
		public void MoveToBack ( T t )
		{
			Remove (t);
			Add (t);
		}

		public void MoveToFront ( T t )
		{
			Remove (t);
			Insert (0, t);
		}

		public void Swap ( T from, T to )
		{
			int indexFrom = IndexOf (from);
			int indexTo = IndexOf (to);

			if (indexFrom > indexTo)
			{
				RemoveAt (indexFrom);
				Insert (indexFrom, to);

				RemoveAt (indexTo);
				Insert (indexTo, from);
			}
			else
			{
				RemoveAt (indexTo);
				Insert (indexTo, from);

				RemoveAt (indexFrom);
				Insert (indexFrom, to);
			}
		}
	}
}