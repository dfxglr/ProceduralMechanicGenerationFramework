namespace PMGF
{
	namespace PMGCore
	{

		public class PMGFunction
		{

            public int _whichFunction;
            public PMGValueStack _localStack,_actorStack;

            public PMGFunction(int whichFunction)
            {
                _whichFunction = whichFunction;
            }


	        public void Do(PMGValueStack localStack, PMGValueStack actorStack)
	        {
                // Local stack is the stack of the calling event or method
                // actor stach is the stack of the actor to which the calling
                //      method or event belongs.
                //
                _localStack = localStack;
                _actorStack = actorStack;
	        }
		}

	}
}
