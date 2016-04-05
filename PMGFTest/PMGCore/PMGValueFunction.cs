using System;
using System.Collections.Generic;

namespace PMGF
{
	namespace PMGCore
	{

		public class PMGValueFunction : PMGFunction
		{
            // All the manually created functions:
            public delegate void ValueFunction(PMGValueStack localStack, PMGValueStack actorStack);
            List<ValueFunction> _valueFunctions = new List<ValueFunction>();
            // Example of adding a specific function to valuefunctions:
            // void somefunction(PMGValueStack localStack, PMGValueStack actorStack)
            // {
            //      //do something
            //      // add something to a stack
            // }
            //
            // _valueFunctions.Add(somefunction)


			// Value functions add to valuestacks
			public PMGValueFunction(int whichFunction) : base(whichFunction)
			{


                // Make sure specific functions has been added, before instantiation
                if(_valueFunctions.Count == 0)
                {
                    throw new InvalidOperationException("No value functions have been added");
                }


                // which function is the specific value function this instantiation is.
                if(whichFunction < 0 || whichFunction >= _valueFunctions.Count)
                {
                    throw new ArgumentOutOfRangeException("ValueFunction number out of range", "whichFunction");
                }
			}

            public void Do(PMGValueStack localStack, PMGValueStack actorStack)
            {
                // Store stacks, as defined in base
                base.Do(localStack,actorStack);

                // call correct value function
            }

		}


	}
}
