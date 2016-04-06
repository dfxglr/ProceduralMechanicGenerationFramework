using System;
using System.Collections.Generic;

namespace PMGF
{
	namespace PMGCore
	{



		public class PMGValueFunction : PMGFunction
		{

			// Value functions add to valuestacks
			public PMGValueFunction(int whichFunction) : base(whichFunction)
			{


                // Make sure specific functions has been added, before instantiation
                if(PMGValueFunctionsCollection.Instance.Collection.Count == 0)
                {
                    throw new
                        InvalidOperationException("No value functions have been added");
                }


                // which function is the specific value function this instantiation is.
                if(whichFunction < 0 || whichFunction >= PMGValueFunctionsCollection.Instance.Collection.Count)
                {
                    throw new
                        ArgumentOutOfRangeException("ValueFunction number out of range",
                                                    "whichFunction");
                }
			}

            public void Do(PMGValueStack localStack, PMGValueStack actorStack)
            {
                // Store stacks, as defined in base
                base.Do(localStack,actorStack);

                // call correct value function
                PMGValueFunctionsCollection.Instance.Collection[_whichFunction](localStack, actorStack);
            }

		}


	}
}
