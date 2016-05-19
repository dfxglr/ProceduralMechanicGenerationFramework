namespace PMGF
{
	namespace PMGCore
	{

		public sealed class PMGChangeFunction : PMGFunction
		{
			public PMGChangeFunction(int whichFunction) : base(whichFunction)
			{
                Type = FunctionType.CHANGE;
			}

            public override bool Do(PMGActor actor, PMGValueStack localStack)
            {
                // call correct change function
                actor.Core.ChangeFunctions.Collection[_whichFunction](actor, localStack);

                //dummy return that's not used
                return true;
            }
		}


	}
}
