using System.Collections.Generic;

namespace PMGF
{

	namespace PMGCore
	{
<<<<<<< HEAD
		
		public class PMGMethod
		{

=======

		public class PMGMethod
		{

            public List<PMGExecuteList> Steps;
            public bool Running;
            public int StepIter;

            // Delegate for when the method is done (for calling event, perhaps);
            public delegate void DoneDelegate();

            public DoneDelegate OnDone = null;

>>>>>>> pvt_working_branch


		    public PMGMethod()
		    {
		    }

<<<<<<< HEAD
		}
	}

}
=======
			public void Call()
			{
			}

            public void ReachedEnd()
            {
                // Reached end of the list of timesteps.
                Running = false;
                StepIter = 0;
            }


            public virtual void TimeStep()
            {
                // Go to next time step, unless done
                StepIter++;
                if(StepIter == Steps.Count)
                {
                    StepIter = 0;
                    Running = false;
                }
                else
                {
                    Steps[StepIter].Execute();

                    if(OnDone != null)
                        OnDone();
                }
            }

            public virtual void ExecuteListStep()
            {
                // Execute next action in execute list
            }
		}
	}

}
>>>>>>> pvt_working_branch
