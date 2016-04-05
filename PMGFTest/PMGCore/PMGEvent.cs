

namespace PMGF
{

	namespace PMGCore
	{


        public enum EventTriggerBehavior { ALWAYS, REQUIRE_FALSE, ONE_TIME, VICTORY, DEFEAT, NEUTRAL };



		public class PMGEvent
		{

            public EventTriggerBehavior _behavior;
            public PMGMethod _method;
            public PMGValueStack _valueStack;

            public PMGActor _callingActor;


		    public PMGEvent(PMGMethod method, EventTriggerBehavior behavior)
		    {
                this._method = method;
                this._behavior = behavior;
                this._callingActor = calling;
		    }

			public virtual void Trigger()
			{
                // Trigger the event (start from first item in execute list at timestep 0
			}


            public virtual void OnMethodDone()
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
