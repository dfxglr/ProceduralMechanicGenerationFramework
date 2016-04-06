namespace PMGF
{
	namespace PMGCore
	{

        // Generic delegate to use (we have specific ones too)
        public delegate void FunctionDelegate(PMGValueStack localStack, PMGActor actor);


		public class PMGFunction
		{

            protected int _whichFunction;


            public PMGFunction(int whichFunction)
            {
                _whichFunction = whichFunction;
            }

            public virtual void Do(PMGValueStack localStack, PMGActor actor)
            {
            }
		}

	}
}
