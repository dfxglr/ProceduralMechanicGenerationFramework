namespace PMGF
{
	namespace PMGCore
	{

		public sealed class PMGConditionFunction : PMGFunction
		{
			// Condition functions can get values from the valustacks

			public PMGConditionFunction(int whichFunction) : base(whichFunction)
			{
                Type = FunctionType.CONDITION;
			}

            public override bool Do(PMGActor actor, PMGValueStack localStack)
            {
                return actor.Core.ConditionFunctions.Collection[_whichFunction](actor, localStack);
            }
		}


	}
}
