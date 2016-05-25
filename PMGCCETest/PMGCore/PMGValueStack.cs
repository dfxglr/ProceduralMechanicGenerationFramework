using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace PMGF
{
	namespace PMGCore
	{

		public enum ValueType{ INT, ACTOR, TIMER };

		public class PMGValueStack
		{
			public GenericStack<PMGActor> ActorStack = new GenericStack<PMGActor>();
			public GenericStack<int> IntStack = new GenericStack<int>();
            public GenericStack<Stopwatch> TimerStack = new GenericStack<Stopwatch>();

			public PMGValueStack()
			{

			}

            public void Clear()
            {
                // clear the stack
                ActorStack = new GenericStack<PMGActor>();
                IntStack = new GenericStack<int>();
                TimerStack = new GenericStack<Stopwatch>();
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

            public Stopwatch GetTimer()
            {
                Stopwatch tA = GetValueOfType(ValueType.TIMER) as Stopwatch;

                if (tA == null)
                    throw new System.InvalidCastException("Getting stopwatch value and casting as actor failed and returned null.");

                return tA;
            }

            public Stopwatch PopTimer()
            {
                Stopwatch tA = PopValueOfType(ValueType.TIMER) as Stopwatch;

                if (tA == null)
                    throw new System.InvalidCastException("Getting timer value and casting as actor failed and returned null.");

                return tA;
            }

            public void Push(Stopwatch val)
            {
                TimerStack.Push(val);
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
                    case ValueType.TIMER:
                        return TimerStack.PopButNoPop() as object;
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
                    case ValueType.TIMER:
                        return TimerStack.Pop() as object;
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
                    throw new System.ArgumentNullException("val", "Pushing null to valuestack");

                switch(t)

                {
					case ValueType.INT:
						IntStack.Push(System.Convert.ToInt32(val));
						break;
					case ValueType.ACTOR:
						ActorStack.Push(val as PMGActor);
						break;
                    case ValueType.TIMER:
                        TimerStack.Push(val as Stopwatch);
                        break;

                }

            }



		}

	}
}
