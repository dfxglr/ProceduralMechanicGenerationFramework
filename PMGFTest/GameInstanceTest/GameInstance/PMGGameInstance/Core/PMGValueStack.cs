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
                        return  IntStack.PopButNoPop() as object;
                    case ValueType.ACTOR:
                        return  ActorStack.PopButNoPop() as object;

                    default:
                        throw new System.ArgumentException("Non-existant valuetype","t");
                }

            }


            public object PopValueOfType(ValueType t)
            {
                // Return value of the specified type, or throw error
                switch(t)
                {
                    case ValueType.INT:
                        return  IntStack.Pop() as object;
                    case ValueType.ACTOR:
                        return  ActorStack.Pop() as object;

                    default:
                        throw new System.ArgumentException("Non-existant valuetype","ValueType t");
                }

            }


            public void PushValueOfType(object val, ValueType t)
            {
                // Push a value of type or return error
                if(val == null)
                    throw new System.ArgumentNullException("val");

                switch(t)

                {
					case ValueType.INT:
						IntStack.Push(System.Convert.ToInt32(val));
						break;
					case ValueType.ACTOR:
						ActorStack.Push(val as PMGActor);
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
