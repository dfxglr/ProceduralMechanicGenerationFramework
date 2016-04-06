using System.Collections.Generic;

namespace PMGF
{
	namespace PMGCore
	{

		public class PMGExecuteList
		{

            public List<PMGFunction> _functions = new List<PMGFunction>();

            private PMGValueStack _localStack;
            private PMGActor _actor;


            public PMGExecuteList(PMGValueStack localStack, PMGActor actor)
            {
                _localStack = localStack;
                _actor = actor;
            }

            public void Execute()
            {
                // Execute all functions in list at once
                foreach(PMGFunction f in _functions)
                {
                    f.Do(_localStack,_actor);
                }
            }


		}

	}
}
