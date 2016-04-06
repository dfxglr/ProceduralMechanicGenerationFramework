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

			List<PMGEventFixed> FixedEvents = new List<PMGEventFixed>();
			List<PMGEventDynamic> DynamicEvent = new List<PMGEventDynamic>();

            public PMGValueStack ValueStack = new PMGValueStack();

            public PMGCore Core;


		    public PMGActor(PMGCore _Core)
		    {
                Core = _Core;

		    }



		}

	}
}
