namespace PMGF
{
	namespace PMGCore
	{

		public class PMGConditionFunction : PMGFunction
		{
			// Condition functions can get values from the valustacks

			public PMGConditionFunction()
			{
			}

			public virtual bool Evaluate()
			{
				return false;
			}
		}


	}
}
