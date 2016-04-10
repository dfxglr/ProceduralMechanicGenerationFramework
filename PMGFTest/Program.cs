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
            PMGActor actor = new PMGActor(core);

            // Create methods
            // ...

            // Create functions for the methods


            // Create events
            //

            // Create functions for events


            // Run some timesteps in the TODO GAMEMANAGER or whatever




		}
	}
}
