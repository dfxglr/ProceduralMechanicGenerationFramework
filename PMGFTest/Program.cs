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

            //Actor, method, and exe list needed to test functions
            Console.WriteLine("Creating Actor, Method, and Exe list.");
            PMGActor testActor = new PMGActor(core);
            PMGMethod testMethod = new PMGMethod();

            // Exe list needs local stack (i.e. the method stack) and actor
            PMGExecuteList testExeList = new PMGExecuteList((object) testMethod,
                                                            FunctionOwnerType.METHOD,
                                                            testActor);

            // Testing value functions
            Console.WriteLine("Creating value functions.");
            PMGValueFunction vf1 = new PMGValueFunction(0); // VF_DebugWriteToConsole
            PMGValueFunction vf2 = new PMGValueFunction(1); // VF_DoNothing
            PMGValueFunction vf3 = new PMGValueFunction(0); // VF_DebugWriteToConsole
            PMGValueFunction vf4 = new PMGValueFunction(2); // VF_PushLeetToActor

            // Add value functions to exe list
            Console.WriteLine("Adding value functions to exe list.");
            testExeList._functions.Add(vf1);
            testExeList._functions.Add(vf2);
            testExeList._functions.Add(vf3);
            testExeList._functions.Add(vf4);

            // Add exe list to method
            Console.WriteLine("Adding exe list to method.");
            testMethod._steps.Add(testExeList);


            // Call the method (usually done from event)
            Console.WriteLine("Calling Method");
            testMethod.Call();


            // Run some timesteps
            Console.WriteLine("Running timesteps");
            for(int i=0; i < 5; i++)
            {
                testMethod.TimeStep();
            }

		}
	}
}
