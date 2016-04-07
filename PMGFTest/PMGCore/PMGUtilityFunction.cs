namespace PMGF
{
	namespace PMGCore
	{

		public class PMGUtilityFunction : PMGFunction
		{
            // Utility functions can change the method or event calling


			public PMGUtilityFunction(int whichFunction) : base(whichFunction)
			{
                Type = FunctionType.UTILITY;
			}

            public override void Do(PMGActor actor, object owner, FunctionOwnerType ownerType)
            {
                // call correct utility function
                actor.Core.UtilityFunctions.Collection[_whichFunction](actor, owner, ownerType);

            }
		}


	}
}
