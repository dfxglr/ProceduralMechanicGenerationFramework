using System.Collections.Generic;


namespace PMGF
{
    namespace PMGCore
    {
        // Delegate for valuefunctions
        public delegate void ValueFunction(PMGValueStack localStack,
                                           PMGValueStack actorStack);

        public sealed partial class PMGValueFunctionsCollection
        {

            // All the manually created value functions
            public List<ValueFunction> Collection = new List<ValueFunction>();

            public void Initialize()
            {
                // Add all the functions you want
                Collection.Add(VF_DoNothing);           // Do nothing
                Collection.Add(VF_PushLeetToActor);     // Push 1337 to actor stack
            }



            /*
             * USER DEFINED FUNCTIONS
             */

            public void VF_DoNothing(PMGValueStack localStack, PMGValueStack actorStack)
            {
                // literally do nothing
            }

            public void VF_PushLeetToActor(PMGValueStack localStack, PMGValueStack actorStack)
            {
                // push 1337 to actorstack
                actorStack.Push(1337);
            }
        }
    }
}
