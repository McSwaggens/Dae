using System;
using System.Collections;
using System.Collections.Generic;

namespace Dae
{
	public class StackManager<T> : IList<T>, IUpdate
	{
		public List<T> actual = new List<T> ();
		private List<T> add = new List<T> ();
		private List<T> remove = new List<T> ();

		internal List<T> newItems = new List<T> ();
		internal List<T> removedItems = new List<T> ();

		public T this[int index] { get => actual[index]; set => actual[index] = value; }

		public int Count => actual.Count;

		public bool IsReadOnly => false;

		public void Add ( T item )
		{
			add.Add (item);
		}

		public void Clear ()
		{
			actual.Clear ();
		}

		public bool Contains ( T item )
		{
			return actual.Contains (item);
		}

		public void CopyTo ( T[] array, int arrayIndex )
		{
			actual.CopyTo (array, arrayIndex);
		}

		public IEnumerator<T> GetEnumerator ()
		{
			return actual.GetEnumerator ();
		}

		public int IndexOf ( T item )
		{
			return actual.IndexOf (item);
		}

		public void Insert ( int index, T item )
		{
			throw new NotSupportedException ();
		}

		public bool Remove ( T item )
		{
			remove.Add (item);

			return true;
		}

		public void RemoveAt ( int index )
		{
			throw new NotSupportedException ();
		}

		public void ForceAdd ( T item )
		{
			actual.Add (item);
		}

		public void Update ()
		{
			// Remove all entities from actual that are in the remove list
			remove.ForEach (t => actual.Remove (t));
			remove.ForEach (t => removedItems.Add (t));
			remove.Clear ();

			// Add all entities from the add list into the actual list
			add.ForEach (t => actual.Add (t));
			add.ForEach (t => newItems.Add (t));
			add.Clear ();
		}

		IEnumerator IEnumerable.GetEnumerator ()
		{
			return actual.GetEnumerator ();
		}
	}
}