namespace PMGF
{

	namespace PMGCore
	{

		public class PMGEventFixed : PMGEvent
		{
			PMGMethod Method; // or method id

			public PMGEventFixed()
			{
			}

			public override void Trigger()
			{
				Method.Call();
			}

		}

	}

}
