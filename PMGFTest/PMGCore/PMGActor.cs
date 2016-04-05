using System.Collections.Generic;

namespace PMGF
{
	namespace PMGCore
	{

		public class PMGActor
		{
			// List<PMGMethod> Methods;
            // Actors don't have events. Misleading in article
            //  - the events have methods.

			List<PMGEventFixed> FixedEvents;
			List<PMGEventDynamic> DynamicEvent;

            PMGValueStack ValueStack;



		    public PMGActor()
		    {

		    }



		}

	}
}
