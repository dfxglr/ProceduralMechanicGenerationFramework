using System;
using PMGF.PMGCore;

namespace PMGFTest
{
	class MainClass
	{
		public static void Main (string[] args)
		{
            Console.WriteLine("Test program starting.");


            // Test Functions
            //
            PMGFunction basef = new PMGFunction();
            PMGValueFunction varf = new PMGValueFunction();
            PMGConditionFunction condf = new PMGConditionFunction();
            PMGUtilityFunction utilf = new PMGUtilityFunction();
            PMGChangeFunction chanf = new PMGChangeFunction();

            // Test Methods
            //
            PMGMethod meth = new PMGMethod();

            // Test Events
            //
            PMGEvent basee = new PMGEvent(meth,EventTriggerBehavior.ONE_TIME);
            PMGEventFixed fixe = new PMGEventFixed(meth,EventTriggerBehavior.ONE_TIME);
            PMGEventDynamic dyne = new PMGEventDynamic(meth,EventTriggerBehavior.ONE_TIME);

            // Test with "foreach(EventTriggerBehavior b in Enum.EventTriggerBehavior.GetValues(typeof(EventTriggerBehavior))) { skldjfklasjfls}



            // Test Actors
            //
            PMGActor a = new PMGActor();

		}
	}
}
