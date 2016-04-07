using System.Collections.Generic;
using System.Linq;

namespace PMGF
{
	namespace PMGCore
	{

		public enum ValueType{ INT, ACTOR };

		public class PMGValueStack
		{
			GenericStack<PMGActor> ActorStack = new GenericStack<PMGActor>();
			GenericStack<int> IntStack = new GenericStack<int>();

			public PMGValueStack()
			{

			}

            public void Clear()
            {
                // clear the stack
                ActorStack = new GenericStack<PMGActor>();
                IntStack = new GenericStack<int>();
            }

            public object GetValueOfType(ValueType t)
            {
                // Return value of the specified type, or throw error
                switch(t)
                {
                    case ValueType.INT:
                        return (object) IntStack.Pop();
                    case ValueType.ACTOR:
                        return (object) ActorStack.Pop();

                    default:
                        throw new System.ArgumentException("Non-existant valuetype","ValueType t");
                }

            }

            public void PushValueOfType(object val, ValueType t)
            {
                // Push a value of type or return error
                switch(t)

                {
					case ValueType.INT:
						IntStack.Push ((int)val);
						break;
					case ValueType.ACTOR:
						ActorStack.Push ((PMGActor)val);
						break;

                }

            }

            // Overloading Push functions
            // INT
            public void Push(int val)
            {
                IntStack.Push(val);
            }

            // ACTOR
            public void Push(PMGActor val)
            {
                ActorStack.Push(val);
            }


		}

	}
}
