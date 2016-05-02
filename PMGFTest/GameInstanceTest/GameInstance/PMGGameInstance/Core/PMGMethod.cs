using System.Collections.Generic;

namespace PMGF
{

	namespace PMGCore
	{
		public class PMGMethod
		{

            public List<PMGExecuteList> _steps = new List<PMGExecuteList>();

            public bool _running;
            public int _stepIter;

            public PMGExecuteList CurrentStep = null;

            public PMGValueStack _valueStack = new PMGValueStack();

            // Delegate for when the method is done (for calling event, perhaps);
            public delegate void DoneDelegate();

            public DoneDelegate _onDone;


            public PMGMethod()
            {
                _running = false;
                _stepIter = 0;
            }

            public void Call()
            {
                // Start the method
                _running = true;
                _stepIter = 0;
            }

            public void Pause()
            {
                // Stop the method (but stay at current timestep
                _running = false;
            }

            public void Reset()
            {
                // Stop and reset the method (e.g. timestep 0)
                _running = false;
                _stepIter = 0;

                // stop potential execution in list
                CurrentStep.StopExecution();

                _valueStack = new PMGValueStack();
            }

            public void Stop()
            {
                // same as above)
                Reset();
            }


            public virtual void TimeStep()
            {
                if(!_running)
                    return;

                // Go to next time step, unless done
                if(_stepIter == _steps.Count)
                {
                    // We are at last step (+1) - end it
                    ReachedEnd();
                }
                else
                {
                    // Execute next step
                    CurrentStep = _steps[_stepIter];
                    if (CurrentStep != null)
                    {
                        _steps[_stepIter].Execute();
                    }
                    _stepIter++;
                }
            }

            public void ReachedEnd()

            {
                // Reached end of the list of time_steps.
                Reset();

                if(_onDone != null)
                    _onDone();
            }

		}
	}

}
