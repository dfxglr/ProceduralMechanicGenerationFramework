using System;
using System.Collections.Generic;
//using UnityEngine;


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
                /*  1 */Collection.Add(VF_Push42ToLocal);
                /*  2 */Collection.Add(VF_DoNothing);           // Do nothing
                /*  3 */Collection.Add(VF_PushLeetToActor);     // Push 1337 to actor stack
                /*  4 */Collection.Add(VF_PushToStack_ActorType_0_1);   
                /*  5 */Collection.Add(VF_PushToStack_ActorType_0);  
  
            }



            /*
             * USER DEFINED FUNCTIONS
             */
            public void VF_DebugWriteToConsole(PMGActor actor, PMGValueStack localStack)
            {
                // Write a message to console for testing purposes.
                //System.Console.WriteLine("VF_DebugWriteToConsole Called!");

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
            public void VF_PushToStack_ActorType_0_1(PMGActor actor, PMGValueStack localStack)
            {
                //hack job stuff
                for (int i = 0; i < actor.Core.WorldActors.Count;i++)
                {
                    //checks for type 0 aka the player
                    if(actor.Core.WorldActors[i].Type == 0 || actor.Core.WorldActors[i].Type == 1)
                    {
                        //adds the player to the local stack
                        localStack.Push(actor.Core.WorldActors[i]);
                        //Debug.Log("actor: "+ actor.Core.WorldActors[i]+" has positionx: "+ actor.Core.WorldActors[i].position[0]);
                    }
                }
            }
            public void VF_PushToStack_ActorType_0(PMGActor actor, PMGValueStack localStack)
            {
                //hack job stuff
                for (int i = 0; i < actor.Core.WorldActors.Count; i++)
                {
                    //checks for type 0 aka the player
                    if (actor.Core.WorldActors[i].Type == 0)
                    {
                        //adds the player to the local stack
                        localStack.Push(actor.Core.WorldActors[i]);
                        //Debug.Log("actor: "+ actor.Core.WorldActors[i]+" has positionx: "+ actor.Core.WorldActors[i].position[0]);
                    }
                }
            }

        }
    }
}
