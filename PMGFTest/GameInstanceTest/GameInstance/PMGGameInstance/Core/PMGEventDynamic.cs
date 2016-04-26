
namespace PMGF
{
	namespace PMGCore
	{

		public class PMGEventDynamic : PMGEvent
		{

			public PMGEventDynamic(PMGMethod method, PMGActor calling,
                                    EventTriggerBehavior behavior
                                    = EventTriggerBehavior.ALWAYS)
                                    : base(method, calling, behavior)
			{


			}

		}

	}
}
