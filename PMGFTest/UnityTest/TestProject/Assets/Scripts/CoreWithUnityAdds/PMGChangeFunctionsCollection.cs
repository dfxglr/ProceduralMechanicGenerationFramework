using System;
using System.Collections.Generic;
using PMGF.PMGGameInstance;


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

            //map
            PMGMap Map = new PMGMap();

            public void Initialize()
            {
                // Add all the functions you want
                // f# */
                /*  X *///Collection.Add(CF_DebugWriteToConsole); // Write a msg to console
                /*  0 */Collection.Add(CF_DoNothing);           // Do nothing
                /*  1 */Collection.Add(CF_PushLeetToActor);     // Push 1337 to actor stack
                /*  2 */Collection.Add(CF_Movement_MoveUpOnce); 
                /*  3 */Collection.Add(CF_Movement_MoveLeftOnce);
                /*  4 */Collection.Add(CF_Movement_MoveDownOnce);
                /*  5 */Collection.Add(CF_Movement_MoveRightOnce);  
            }



            /*
             * USER DEFINED FUNCTIONS
             */
            /*public void CF_DebugWriteToConsole(PMGActor actor, PMGValueStack localStack)
            {
                // Write a message to console for testing purposes.
                int fromStack = System.Convert.ToInt32(localStack.PopValueOfType(ValueType.INT));

                string s = string.Format("Change function called. Got {0} from stack!",fromStack);

                Console.WriteLine(s);
            }*/

            public void CF_DoNothing(PMGActor actor, PMGValueStack localStack)
            {
                // literally do nothing
            }

            public void CF_PushLeetToActor(PMGActor actor, PMGValueStack localStack)
            {
                // push 1337 to actor stack
                Console.WriteLine("fagit");
                actor.ValueStack.Push(9001);
            }
            public void CF_Movement_MoveUpOnce(PMGActor actor, PMGValueStack localStack)
            {
                //y
                
                //map boundary check
                if (actor.position[1] - 1 > 0)
                {
                    actor.position[1] = actor.position[1] - 1;
                    Console.WriteLine("         moved up 1 step");
                }
                else
                {
                    Console.WriteLine("         cant moved up 1 step");
                }
            }
            public void CF_Movement_MoveLeftOnce(PMGActor actor, PMGValueStack localStack)
            {
                //x
                //map boundary check
                if (actor.position[0] - 1 > 0)
                {
                    actor.position[0] = actor.position[0] - 1;
                    Console.WriteLine("         moved left 1 step");
                }
                else
                {
                    Console.WriteLine("         cant moved left 1 step");
                }
            }
            public void CF_Movement_MoveDownOnce(PMGActor actor, PMGValueStack localStack)
            {               
                //map boundary check
                if (actor.position[1] + 1 < Map.chart.GetLength(1))
                {
                    actor.position[1] = actor.position[1] + 1;
                    Console.WriteLine("         moved down 1 step");
                }
                else
                {
                    Console.WriteLine("         cant moved down 1 step");
                }
            }
            public void CF_Movement_MoveRightOnce(PMGActor actor, PMGValueStack localStack)
            {
                //map boundary check
                if (actor.position[0] + 1 < Map.chart.GetLength(0))
                {
                    actor.position[0] = actor.position[0] + 1;
                    Console.WriteLine("         moved right 1 step");
                }
                else
                {
                    Console.WriteLine("         cant moved right 1 step");
                }
            }
        }
    }
}
