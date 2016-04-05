namespace PMGF
{

	namespace PMGCore
	{

		public class PMGEventFixed : PMGEvent
		{
			PMGMethod Method; // or method id

			public PMGEventFixed(PMGMethod method, EventTriggerBehavior behavior) : base(method, behavior)
			{
			}

			public override void Trigger()
			{
				Method.Call();
			}

		}

	}

}
