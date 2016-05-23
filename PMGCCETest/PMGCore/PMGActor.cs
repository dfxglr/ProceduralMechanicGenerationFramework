using System.Collections.Generic;
using System.Linq;

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
            //type
            public int Type;

            public PMGGameCore Core;

            public PMGActor(PMGGameCore _Core)
            {
                Core = _Core;

            }

            public PMGActor GetActorCopy()
            {
                PMGActor copy = new PMGActor(Core);

                copy.IsPlayer = IsPlayer;
                copy.position = new List<int>(position);

                foreach (PMGEvent e in Events)
                {
                    // Make method copy
                    PMGMethod mcopy = new PMGMethod();

                    List<PMGExecuteList> ml = new List<PMGExecuteList>();

                    // Make execute list copies
                    foreach (PMGExecuteList l in e._method._steps)
                    {
                        if (l == null)
                            ml.Add(null);
                        else
                        {
                            ml.Add(new PMGExecuteList(mcopy, FunctionOwnerType.METHOD, copy));
                            ml.Last()._functions = l._functions;
                        }
                    }

                    mcopy._steps = ml;

                    PMGEvent ecopy = new PMGEvent(mcopy, copy, e._behavior);

                    PMGExecuteList elist = new PMGExecuteList(ecopy, FunctionOwnerType.EVENT, copy);
                    elist._functions = e._conditions._functions;
                    ecopy._conditions = elist;

                    copy.Events.Add(ecopy);
                }

                return copy;
            }
		}
	}
}
