
namespace PMGF
{
	namespace PMGCore
	{



		public class PMGValueFunction : PMGFunction
		{

			// Value functions add to valuestacks
			public PMGValueFunction(int whichFunction) : base(whichFunction)
			{
			}

            public override void Do(PMGValueStack localStack, PMGActor  actor)
            {
                // call correct value function
                actor.Core.ValueFunctions.Collection[_whichFunction](localStack, actor);
            }

		}


	}
}
