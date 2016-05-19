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


            private bool _exing = true;


            public PMGExecuteList(object owner, FunctionOwnerType ownerT, PMGActor actor)
            {
                // The owner of the execution list (event or method)
                if(owner == null)
                    throw new System.ArgumentNullException("owner");
                _owner = owner;
                _ownerType = ownerT;

                // The actor that owns the owner (yaya too meta shut up)
                _actor = actor;
            }

            public void StopExecution()
            {
                _exing = false;
            }

            // Run all functions in list (bool because condition functions)
            public bool Execute()
            {
                // Get the local stack ready (need proper casting)
                PMGValueStack localStack = null;

                _exing = true;

                switch (_ownerType)
                {
                    case FunctionOwnerType.EVENT:
                        PMGEvent E = _owner as PMGEvent;

                        if(E == null)
                            throw new System.InvalidCastException("Casting of owner as PMGEvent failed.");

                        localStack = E._valueStack;
                        break;


                    case FunctionOwnerType.METHOD:
                        PMGMethod M = _owner as PMGMethod;

                        if(M == null)
                            throw new System.InvalidCastException("Casting of owner as PMGMethod failed.");

                        localStack = M._valueStack;
                        break;
                }

                if(localStack == null)
                    throw new System.InvalidOperationException("localStack is still null. Could not be set to event or method stack.");

                // Prepare bool for condition functions
                bool AllTrue = true;

                // Execute all functions in list at once
                for(int i = 0; i < _functions.Count; i++)
                {
                    if(!_exing)
                    {
                        // something stopped the execution of the list
                        _exing = true;
                        return false;
                    }

                    if(i < 0 || i > _functions.Count)
                    {
                        throw new System.InvalidOperationException("Tried to execute functions outside of list. Did a utility function change it?");
                    }

                    PMGFunction f = _functions[i];

                    switch(f.Type)
                    {
                        case FunctionType.VALUE:
                            PMGValueFunction vf = f as PMGValueFunction;

                            if(vf == null)
                                throw new System.InvalidCastException("Casting of function as 2PMGValueFunction failed.");

                            vf.Do(_actor, localStack);
                            break;

                        case FunctionType.UTILITY:
                            PMGUtilityFunction uf = f as PMGUtilityFunction;

                            if(uf == null)
                                throw new System.InvalidCastException("Casting of function as PMGUtilityFunction failed.");

                            uf.Do(_actor, _owner, _ownerType);
                            break;

                        case FunctionType.CONDITION:
                            PMGConditionFunction cf = f as PMGConditionFunction;

                            if(cf == null)
                                throw new System.InvalidCastException("Casting of function as PMGConditionFunction failed.");

                            AllTrue &= cf.Do(_actor, localStack);
                             break;

                        case FunctionType.CHANGE:
                            PMGChangeFunction chf = f as PMGChangeFunction;

                            if(chf == null)
                                throw new System.InvalidCastException("Casting of function as PMGChangeFunction failed.");

                            chf.Do(_actor, localStack);
                             break;
                    }
                }

                return AllTrue;
            }


		}

	}
}
