using System;
using System.Collections.Generic;


namespace PMGF
{
    namespace PMGCore
    {
        // Delegate for valuefunctions
        public delegate void ValueFunction(PMGActor actor,
                                           PMGValueStack localStack);

        public class PMGValueFunctionsCollection
        {

            // All the manually created value functions
            public List<ValueFunction> Collection = new List<ValueFunction>();

            public void Initialize()
            {
                // Add all the functions you want
                // f# */
                /*  0 */Collection.Add(VF_DebugWriteToConsole); // Write a msg to console
                        Collection.Add(VF_Push42ToLocal);
                /*  1 */Collection.Add(VF_DoNothing);           // Do nothing
                /*  2 */Collection.Add(VF_PushLeetToActor);     // Push 1337 to actor stack
            }



            /*
             * USER DEFINED FUNCTIONS
             */
            public void VF_DebugWriteToConsole(PMGActor actor, PMGValueStack localStack)
            {
                // Write a message to console for testing purposes.
               // System.Console.WriteLine("VF_DebugWriteToConsole Called!");

                // You can also call other functions!
                VF_Push1337ToLocal(actor, localStack);

            }

            public void VF_Push42ToLocal(PMGActor actor, PMGValueStack localStack)
            {
                //System.Console.WriteLine("Pushing 42 to local stack");
                localStack.Push(42);
            }

            public void VF_Push1337ToLocal(PMGActor actor, PMGValueStack localStack)
            {
                //System.Console.WriteLine("Push 1337 to local stack");
                localStack.Push(1337);
            }

            public void VF_DoNothing(PMGActor actor, PMGValueStack localStack)
            {
                // literally do nothing
            }

            public void VF_PushLeetToActor(PMGActor actor, PMGValueStack localStack)
            {
                // push 1337 to actor stack
                actor.ValueStack.Push(1337);
            }
        }
    }
}
