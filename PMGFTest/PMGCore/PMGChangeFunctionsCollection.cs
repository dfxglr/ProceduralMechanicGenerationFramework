using System;
using System.Collections.Generic;


namespace PMGF
{
    namespace PMGCore
    {
        // Delegate for valuefunctions
        public delegate void ChangeFunction(PMGActor actor, PMGValueStack localStack);

        public class PMGChangeFunctionsCollection
        {

            // All the manually created value functions
            public List<ChangeFunction> Collection = new List<ChangeFunction>();

            public void Initialize()
            {
                // Add all the functions you want
                // f# */
                /*  0 */Collection.Add(CF_DebugWriteToConsole); // Write a msg to console
                /*  1 */Collection.Add(CF_DoNothing);           // Do nothing
                /*  2 */Collection.Add(CF_PushLeetToActor);     // Push 1337 to actor stack
            }



            /*
             * USER DEFINED FUNCTIONS
             */
            public void CF_DebugWriteToConsole(PMGActor actor, PMGValueStack localStack)
            {
                // Write a message to console for testing purposes.
                Console.WriteLine("VF_DebugWriteToConsole Called!");
            }

            public void CF_DoNothing(PMGActor actor, PMGValueStack localStack)
            {
                // literally do nothing
            }

            public void CF_PushLeetToActor(PMGActor actor, PMGValueStack localStack)
            {
                // push 1337 to actor stack
                actor.ValueStack.Push(1337);
            }
        }
    }
}
