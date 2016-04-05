using System.Collections.Generic;

namespace PMGF
{

	namespace PMGCore
	{
		public class PMGMethod
		{

            public List<PMGExecuteList> _steps;
            public bool _running;
            public int _stepIter;

            public PMGValueStack _valueStack;

            // Delegate for when the method is done (for calling event, perhaps);
            public delegate void DoneDelegate();

            public DoneDelegate _onDone;



			public void Call()
			{
			}

            public void ReachedEnd()
            {
                // Reached end of the list of time_steps.
                _running = false;
                _stepIter = 0;
            }


            public virtual void TimeStep()
            {
                // Go to next time step, unless done
                _stepIter++;
                if(_stepIter == _steps.Count)
                {
                    // We are at last step (+1) - end it
                    _stepIter = 0;
                    _running = false;
                    if(_onDone != null)
                        _onDone();
                }
                else
                {
                    // Execute next step
                    _steps[_stepIter].Execute();
                }
            }

            public virtual void ExecuteListStep()
            {
                // Execute next action in execute list
                // Not used now. Needed if we do not want one agent to execute an entire timeframe before another (e.g. make smaller steps for each, before switching timestep
            }
		}
	}

}
