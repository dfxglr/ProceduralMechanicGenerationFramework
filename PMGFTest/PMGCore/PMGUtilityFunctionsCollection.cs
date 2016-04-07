using System;
using System.Collections.Generic;


namespace PMGF
{
    namespace PMGCore
    {
        // Delegate for valuefunctions
        public delegate void UtilityFunction(PMGActor actor, object owner, FunctionOwnerType ownerType);

        public class PMGUtilityFunctionsCollection
        {

            // All the manually created value functions
            public List<UtilityFunction> Collection = new List<UtilityFunction>();

            public void Initialize()
            {
                // Add all the functions you want
                // f# */
                /*  0 */Collection.Add(UF_DebugWriteToConsole); // Write a msg to console
                /*  1 */Collection.Add(UF_DoNothing);           // Do nothing
                /*  2 */Collection.Add(UF_PushLeetToActor);     // Push 1337 to actor stack
            }



            /*
             * USER DEFINED FUNCTIONS
             */
            public void UF_DebugWriteToConsole(PMGActor actor, object owner, FunctionOwnerType ownerType)
            {
                // Write a message to console for testing purposes.
                Console.WriteLine("VF_DebugWriteToConsole Called!");
            }

            public void UF_DoNothing(PMGActor actor, object owner, FunctionOwnerType ownerType)
            {
                // literally do nothing
            }

            public void UF_PushLeetToActor(PMGActor actor, object owner, FunctionOwnerType ownerType)
            {
                // push 1337 to actor stack
                actor.ValueStack.Push(1337);
            }
        }
    }
}
