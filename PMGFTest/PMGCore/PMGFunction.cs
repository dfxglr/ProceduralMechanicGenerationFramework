namespace PMGF
{
	namespace PMGCore
	{

        // Generic delegate to use (we have specific ones too)
        public delegate void FunctionDelegate(PMGValueStack localStack, PMGActor actor);

        // types of functions
        public enum FunctionType {VALUE, UTILITY, CHANGE, CONDITION};

        // types of objects that can own (call) functions
        public enum FunctionOwnerType { METHOD, EVENT };


		public class PMGFunction
		{

            public FunctionType Type;
            protected int _whichFunction;


            public PMGFunction(int whichFunction)
            {
                _whichFunction = whichFunction;
            }

            public virtual void Do(PMGActor actor)
            {
                // Every function needs the actor (to access Core, where function
                //  collections are
            }
		}

	}
}
