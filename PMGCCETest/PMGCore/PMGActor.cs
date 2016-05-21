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

            //player tag
            public bool IsPlayer = false;
            //position
            public List<int> position = new List<int>();

            public PMGGameCore Core;

		    public PMGActor(PMGGameCore _Core)
		    {
                Core = _Core;

		    }
		}
	}
}
