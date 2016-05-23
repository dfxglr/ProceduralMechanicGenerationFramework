using System;
using System.Collections.Generic;
using System.Linq;

namespace PMGF
{
	namespace PMGCore
	{

		public class GenericStack <T>
		{
			public List<T> Values = new List<T>();

			public GenericStack ()
			{
			}


			public void Push(T item)
			{
				// Add item to end of correct stack

				if (item == null)
					throw new ArgumentNullException ("item", "Pushing null to stack");

				Values.Add (item);
			}

			public T Pop()
			{
				if (Values.Count > 0)
                {
					T tmp = Values.Last ();
					Values.Remove (Values.Last ());

					return tmp;
				}
                else
                {
					throw new System.InvalidOperationException ("Popping empty stack");
				}

			}

            public T PopButNoPop()
            {
                // Pop without removing (so don't actually pop)
				if (Values.Count > 0) {
					T tmp = Values.Last ();
					return tmp;
				} else {
					throw new System.InvalidOperationException ("Popping empty stack");
				}

            }

            public T Read(int pos)
            {
                // Read from pos, without removing
                if(Values.Count > 0)
                {
                    if(Values.Count < pos)
                    {
                        return Values[pos];
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException("Reading from outside stack", "int pos");
                    }
                }
                else
                {
                    throw new System.InvalidOperationException("Reading from empty stack");
                }
            }
		}
	}
}



