using System.Collections.Generic;

namespace PMGF
{
	namespace PMGCore
	{

		public class PMGExecuteList
		{

            public List<PMGFunction> _functions;


            public void Execute()
            {
                // Execute all functions in list at once
                foreach(PMGFunction f in _functions)
                {
                    f.Do();
                }
            }


		}

	}
}
