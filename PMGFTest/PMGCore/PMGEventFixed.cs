namespace PMGF
{

	namespace PMGCore
	{

		public class PMGEventFixed : PMGEvent
		{
			PMGMethod Method; // or method id

			public PMGEventFixed(PMGMethod method, PMGActor actor,
                                 EventTriggerBehavior behavior = EventTriggerBehavior.ALWAYS)
                                : base(method, actor, behavior)
			{
			}

			public override void Trigger()
			{
				Method.Call();
			}

		}

	}

}
