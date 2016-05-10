using System;
using System.Collections.Generic;


namespace PMGF
{
    namespace PMGCore
    {
        // Delegate for valuefunctions
        public delegate bool ConditionFunction(PMGActor actor, PMGValueStack localStack);

        public class PMGConditionFunctionsCollection
        {

            // All the manually created value functions
            public List<ConditionFunction> Collection = new List<ConditionFunction>();

            public void Initialize()
            {
                // Add all the functions you want
                // f# */
                /*  0 */Collection.Add(CF_DebugWriteToConsole); // Write a msg to console
                /*  1 */Collection.Add(CF_DoNothing);           // Do nothing
                /*  2 */Collection.Add(CF_PushLeetToActor);     // Push 1337 to actor stack
                /*  3 */    //action (space or enter button pressed)
                /*  4 */    //Up     (w or arrowUp button pressed)
                /*  5 */    //Left   (a or arrowLeft button pressed)
                /*  6 */    //Down   (s or arrowDown button pressed)
                /*  7 */    //Right  (d or arrowRight button pressed)
            }



            /*
             * USER DEFINED FUNCTIONS
             */
            public bool CF_DebugWriteToConsole(PMGActor actor, PMGValueStack localStack)
            {
                System.Console.WriteLine("Cond. function checking if timestep > 5");
                if(actor.Core.WorldTimeSteps > 5)
                {
                    Console.WriteLine("It did!");
                    return true;
                }
                else
                {
                    Console.WriteLine("It did not!");
                    return false;
                }
            }

            public bool CF_DoNothing(PMGActor actor, PMGValueStack localStack)
            {
                // literally do nothing
                return true;
            }

            public bool CF_PushLeetToActor(PMGActor actor, PMGValueStack localStack)
            {
                // push 1337 to actor stack
                actor.ValueStack.Push(1337);
                return true;
            }
        }
    }
}
