using System.Collections.Generic;

namespace PMGF
{
	namespace PMGCore
	{

		public class PMGExecuteList
		{

            public List<PMGFunction> Functions;

			public PMGExecuteList()
			{

			}

            public void Execute()
            {
                foreach(PMGFunction f in Functions)
                {
                    f.Do();
                }
            }


		}

	}
}
