using System;
using PMGF.PMGCore;

namespace PMGFTest
{
	class MainClass
	{
		public static void Main (string[] args)
		{
            Console.WriteLine("Test program starting.");


            // Create a new core (one is needed per game)
            PMGCore core = new PMGCore();

            // Create actor
            PMGActor actor = new PMGActor(core);        // actor used for test

            // Create methods
            // ...
            PMGMethod testMethod = new PMGMethod();     // method with no event
            PMGMethod eventMethod = new PMGMethod();    // method for event

            // Create functions for the methods
            PMGValueFunction testValF = new PMGValueFunction(0);
            PMGConditionFunction testCondF = new PMGConditionFunction(0);
            PMGUtilityFunction testUtilF = new PMGUtilityFunction(0);
            PMGChangeFunction testChgF = new PMGChangeFunction(0);



            // Create events
            PMGEvent testEvent = new PMGEvent(eventMethod,actor);

            // Create execution lists
            PMGExecuteList methList1 = new PMGExecuteList(testMethod as object, FunctionOwnerType.METHOD, actor);
            PMGExecuteList methList1_2 = new PMGExecuteList(testMethod as object, FunctionOwnerType.METHOD, actor);
            PMGExecuteList methList2 = new PMGExecuteList(eventMethod as object, FunctionOwnerType.METHOD, actor);
            PMGExecuteList evList = new PMGExecuteList(testEvent as object, FunctionOwnerType.EVENT, actor);

            // Add functions to lists
            evList._functions.Add(testCondF);           // Conditions for event
            methList1._functions.Add(testValF);
            methList1._functions.Add(testChgF);

            methList1_2._functions.Add(testValF);
            methList1_2._functions.Add(testChgF);


            methList2._functions.Add(testValF);
            methList2._functions.Add(testUtilF);
            methList2._functions.Add(testChgF);


            // Add execution lists to events/methods
            testEvent._conditions = evList;
            testMethod._steps.Add(methList1);
            eventMethod._steps.Add(methList2);

            // Add event to actor (as dynamic for now)
            actor.Events.Add(testEvent);


            // Run some timesteps in the TODO GAMEMANAGER or whatever
            //
            // In each timestep we: test conditions for events. Run timesteps in methods
            testMethod.Call(); // we want to call method 1 from the beginniing
            for(int i = 0; i < 15; i++)
            {
                core.WorldTimeSteps = i;

                Console.WriteLine("-- T = {0} --", i);

                // test method 1
                testMethod.TimeStep();

                // Check events on actor (testEvent with eventMethod)
                foreach(PMGEvent E in actor.Events)
                {
                    E.EvaluateConditions();
                    E._method.TimeStep();
                }

            }

            Console.ReadKey();


		}
	}
}
