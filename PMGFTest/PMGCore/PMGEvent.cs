

namespace PMGF
{

	namespace PMGCore
	{


        public enum EventTriggerBehavior { ALWAYS, REQUIRE_FALSE, ONE_TIME, VICTORY, DEFEAT, NEUTRAL };
        // Default behavior is ALWAYS



		public class PMGEvent
		{

            public PMGExecuteList _conditions;
            private bool _lastConditionCheck;
            private bool _thisConditionCheck;
            private bool _canTrigger;
            public EventTriggerBehavior _behavior;
            public PMGMethod _method;
            public PMGValueStack _valueStack;

            public PMGActor _callingActor;


		    public PMGEvent(PMGMethod method, PMGActor calling,
                            EventTriggerBehavior behavior= EventTriggerBehavior.ALWAYS)
		    {
                this._method = method;
                this._behavior = behavior;
                this._callingActor = calling;
                _lastConditionCheck = false;
                _thisConditionCheck = false;
                _canTrigger = false;
		    }

			public void Trigger()
			{
                // Trigger the event (start from first item in execute list at timestep 0
                if(_canTrigger)
                    _method.Call();

                if(_behavior == EventTriggerBehavior.ONE_TIME)
                {
                    // Remove this event  TODO
                }
			}

            public void EvaluateConditions()
            {
                // Check conditions
                _lastConditionCheck = _thisConditionCheck;
                _thisConditionCheck = _conditions.Execute();


                _canTrigger = _thisConditionCheck;

                _canTrigger &=  ! _method._running;

                _canTrigger &= TypeCheck();


                Trigger();

                // clear stack
                _valueStack.Clear();
            }

            public bool TypeCheck()
            {


            }

            public void OnMethodDone()
            {
                // To call when the method is done
                switch(_behavior)
                {
                    default:
                        break;
                }
            }


		}

	}
}
