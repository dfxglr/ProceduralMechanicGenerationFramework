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



            /*
             * Get, Pop, and Push functions for all ValueTypes
             */

            // Get, Pop, and Push for ValueType.INT
            public int GetInt()
            {
                return System.Convert.ToInt32(GetValueOfType(ValueType.INT));
            }

            public int PopInt()
            {
                return System.Convert.ToInt32(PopValueOfType(ValueType.INT));
            }

            public void Push(int val)
            {
                IntStack.Push(val);
            }

            // Get, Pop, and Push for ValueType.ACTOR
            public PMGActor GetActor()
            {
                PMGActor tA = GetValueOfType(ValueType.ACTOR) as PMGActor;

                if(tA == null)
                    throw new System.InvalidCastException("Getting actor value and casting as actor failed and returned null.");

                return tA;
            }

            public PMGActor PopActor()
            {
                PMGActor tA = PopValueOfType(ValueType.ACTOR) as PMGActor;

                if(tA == null)
                    throw new System.InvalidCastException("Getting actor value and casting as actor failed and returned null.");

                return tA;
            }

            public void Push(PMGActor val)
            {
                ActorStack.Push(val);
            }



            /*
             * Generic get/pop values of type t
             */
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


            /*
             * Generic Push value of type t
             */
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



		}

	}
}
