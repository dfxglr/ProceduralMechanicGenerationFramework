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


		public abstract class PMGFunction
		{

            public FunctionType Type;
            protected int _whichFunction;


            public PMGFunction(int whichFunction)
            {
                _whichFunction = whichFunction;
            }



            // used for value and change function
            // and used for condition function
            public virtual bool Do(PMGActor actor, PMGValueStack localStack)
            {
                throw new System.InvalidOperationException("Virtual method Do(PMGActor actor) called when it shouldn't be (wrong arguments or casting when calling derived overriden function?)");
            }

            // used for utility function
            public virtual void Do(PMGActor actor, object owner, FunctionOwnerType ownerType)
            {
                throw new System.InvalidOperationException("Virtual method Do(PMGActor actor) called when it shouldn't be (wrong arguments or casting when calling derived overriden function?)");
            }
		}

	}
}
