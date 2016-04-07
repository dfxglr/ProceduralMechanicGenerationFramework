namespace PMGF
{
	namespace PMGCore
	{

		public class PMGConditionFunction : PMGFunction
		{
			// Condition functions can get values from the valustacks

			public PMGConditionFunction(int whichFunction) : base(whichFunction)
			{
                Type = FunctionType.CONDITION;
			}

            public bool Do(PMGActor actor, PMGValueStack localStack)
            {
                actor.Core.ConditionFunctions.PMGConditionFunctionsCollection.Collection[_whichFunction](actor, localStack);
            }
		}


	}
}
