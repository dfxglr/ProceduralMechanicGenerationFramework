

namespace PMGF
{

	namespace PMGCore
	{


        public enum EventTriggerBehavior { ALWAYS, REQUIRE_FALSE, ONE_TIME, VICTORY, DEFEAT, NEUTRAL };



		public class PMGEvent
		{

            public EventTriggerBehavior Behavior;
            public PMGMethod Method;
            public PMGValueStack _valueStack;


		    public PMGEvent(PMGMethod method, EventTriggerBehavior behavior)
		    {
                this.Method = method;
                this.Behavior = behavior;
		    }

			public virtual void Trigger()
			{
                // Trigger the event (start from first item in execute list at timestep 0
			}


            public virtual void OnMethodDone()
            {
                // To call when the method is done
                switch(Behavior)
                {

                }
            }


		}

	}
}
