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

			public List<PMGEventFixed> FixedEvents = new List<PMGEventFixed>();
			public List<PMGEventDynamic> DynamicEvents = new List<PMGEventDynamic>();
            public List<PMGEvent> Events = new List<PMGEvent>();

            public PMGValueStack ValueStack = new PMGValueStack();


            public PMGCore Core;

		    public PMGActor(PMGCore _Core)
		    {
                Core = _Core;

		    }



		}

	}
}
