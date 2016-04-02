

namespace PMGF
{

	namespace PMGCore
	{


        public enum EventTriggerBehavior { ALWAYS, REQUIRE_FALSE, ONE_TIME, VICTORY, DEFEAT, NEUTRAL };



		public class PMGEvent
		{

		    public PMGEvent()
		    {
		    }

			public virtual void Trigger()
			{
                // Trigger the event (start from first item in execute list at timestep 0
			}


		}

	}
}
