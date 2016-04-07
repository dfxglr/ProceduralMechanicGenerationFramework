using System.Collections.Generic;

namespace PMGF
{
	namespace PMGCore
	{

		public class PMGExecuteList
		{

            public List<PMGFunction> _functions = new List<PMGFunction>();

            private object _owner;
            private FunctionOwnerType _ownerType;
            private PMGActor _actor;


            public PMGExecuteList(object owner, FunctionOwnerType ownerT, PMGActor actor)
            {
                // The owner of the execution list (event or method)
                _owner = owner;
                _ownerType = ownerT;

                // The actor that owns the owner (yaya too meta shut up)
                _actor = actor;
            }

            // Run all functions in list (bool because condition functions)
            public bool Execute()
            {
                // Get the local stack ready (need proper casting)
                PMGValueStack localStack;

                switch(_ownerType)
                {
                    case FunctionOwnerType.EVENT:
                        localStack = ((PMGEvent) _owner)._valueStack;
                        break;
                    case FunctionOwnerType.METHOD:
                        localStack = ((PMGMethod) _owner)._valueStack;
                        break;
                }

                // Prepare bool for condition functions
                bool AllTrue = true;

                // Execute all functions in list at once
                foreach(PMGFunction f in _functions)
                {
                    switch(f.Type)
                    {
                        case FunctionType.VALUE:
                            ((PMGValueFunction) f).Do(_actor, localStack);
                            break;

                        case FunctionType.UTILITY:
                            ((PMGUtilityFunction) f).Do(_actor, _owner, _ownerType);
                            break;

                        case FunctionType.CONDITION:
                             AllTrue &= ((PMGConditionFunction) f).Do(_actor, localStack);
                             break;
                        case FunctionType.CHANGE:
                             ((PMGChangeFunction) f).Do(_actor, localStack);
                             break;
                    }
                }

                return AllTrue;
            }


		}

	}
}
