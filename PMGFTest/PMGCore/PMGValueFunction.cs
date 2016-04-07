
namespace PMGF
{
	namespace PMGCore
	{



		public class PMGValueFunction : PMGFunction
		{
			// Value functions add to valuestacks


			public PMGValueFunction(int whichFunction) : base(whichFunction)
			{
                Type = FunctionType.VALUE;
			}

            public override void Do(PMGActor actor, PMGValueStack localStack)
            {
                // call correct value function
                actor.Core.ValueFunctions.Collection[_whichFunction](actor, localStack);
            }

		}


	}
}
