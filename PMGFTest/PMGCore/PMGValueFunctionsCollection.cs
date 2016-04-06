using System;
using System.Collections.Generic;


namespace PMGF
{
    namespace PMGCore
    {
        // Delegate for valuefunctions
        public delegate void ValueFunction(PMGValueStack localStack,
                                           PMGActor actor);

        public class PMGValueFunctionsCollection
        {

            // All the manually created value functions
            public List<ValueFunction> Collection = new List<ValueFunction>();

            public void Initialize()
            {
                // Add all the functions you want
                // f# */
                /*  0 */Collection.Add(VF_DebugWriteToConsole); // Write a msg to console
                /*  1 */Collection.Add(VF_DoNothing);           // Do nothing
                /*  2 */Collection.Add(VF_PushLeetToActor);     // Push 1337 to actor stack
            }



            /*
             * USER DEFINED FUNCTIONS
             */
            public void VF_DebugWriteToConsole(PMGValueStack localStack, PMGActor actor)
            {
                // Write a message to console for testing purposes.
                System.Console.WriteLine("VF_DebugWriteToConsole Called!");
            }

            public void VF_DoNothing(PMGValueStack localStack, PMGActor actor)
            {
                // literally do nothing
            }

            public void VF_PushLeetToActor(PMGValueStack localStack, PMGActor actor)
            {
                // push 1337 to actor stack
                actor.ValueStack.Push(1337);
            }
        }
    }
}
