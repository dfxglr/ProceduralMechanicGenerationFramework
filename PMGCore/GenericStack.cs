using System;
using System.Collections.Generic;

namespace PMGF
{
	namespace PMGCore
	{

		public class GenericStack <T>
		{
			public List<T> Values;

			public GenericStack ()
			{
			}


			public void Push(T item)
			{
				// Add item to end of correct stack

				Values.Add (item);
			}

			public T Pop()
			{
				if (Values.Count > 0) {
					T tmp = Values.Last ();
					Values.Remove (Values.Last ());

					return tmp;
				} else {
					throw new System.InvalidOperationException ("Popping empty stack");
				}

			}
		}
	}
}

