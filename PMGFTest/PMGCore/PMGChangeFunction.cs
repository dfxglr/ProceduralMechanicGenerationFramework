namespace PMGF
{
	namespace PMGCore
	{

		public class PMGChangeFunction : PMGFunction
		{
			public PMGChangeFunction(int whichFunction) : base(whichFunction)
			{
                Type = FunctionType.CHANGE;
			}

            public override void Do(PMGActor actor, PMGValueStack localStack)
            {
                // call correct change function
                actor.Core.ChangeFunctions.Collection[_whichFunction](actor, localStack);
            }
		}


	}
}
