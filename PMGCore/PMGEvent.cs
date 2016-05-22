

namespace PMGF
{

	namespace PMGCore
	{


        // Need to change this. End events are not behaviors. Actually they are more methods or change-functions than anything...
        //public enum EventTriggerBehavior { ALWAYS, REQUIRE_FALSE, ONE_TIME, VICTORY, DEFEAT, NEUTRAL };
        public enum EventTriggerBehavior { ALWAYS, REQUIRE_METHOD_DONE, REQUIRE_FALSE, ONE_TIME };
        // TODO Discuss event trigger/end behaviors and naming

        // Default behavior is REQUIRE_METHOD_DONE

        // TODO find out which parts should be in fixed/dyn events


		public class PMGEvent
		{

            public PMGExecuteList _conditions;
            public EventTriggerBehavior _behavior;
            public PMGMethod _method;
            public PMGValueStack _valueStack = new PMGValueStack();

            public bool _methodRunning;

            private bool _hasBeenFalse;

            public PMGActor _callingActor;

            public int Type;


		    public PMGEvent(PMGMethod method, PMGActor calling,
                            EventTriggerBehavior behavior= EventTriggerBehavior.ALWAYS)
		    {
                _method = method;
                _behavior = behavior;
                _callingActor = calling;

                _hasBeenFalse = false;
                _methodRunning = false;
		    }

			public void Trigger()
			{
                _hasBeenFalse = false;
                _methodRunning = true;


                _method._onDone = OnMethodDone;
                _method.Call();
			}

            public void EvaluateConditions()
            {
                bool CanTrigger = true;

                // Make sure method isn't already running

                CanTrigger &= (! _method._running);

                // Check conditions
                bool ConditionsCheck = _conditions.Execute();

                // Only trigger if they check out
                CanTrigger &= ConditionsCheck;

                // Check if it has been false since last Trigger
                _hasBeenFalse |= (! ConditionsCheck);

                // If required by behavior, factor that in to whether we can trigger
                if(_behavior == EventTriggerBehavior.REQUIRE_FALSE)
                {
                    CanTrigger &= _hasBeenFalse;
                }

                // yep
                if(CanTrigger)
                    Trigger();

                // clear stack
                _valueStack.Clear();
            }


            public void OnMethodDone()
            {
                // TODO Fix behavior when method is done
                // To call when the method is done
                switch(_behavior)
                {
                    case EventTriggerBehavior.ONE_TIME:
                        // Destroy this event
                        break;
                    default:

                        break;
                }
            }


		}

	}
}
