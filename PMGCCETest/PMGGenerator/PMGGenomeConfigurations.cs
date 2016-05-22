namespace PMGF
{
    namespace PMGGenerator
    {


		public sealed class PMGGenomeConfigurations
		{
			private static readonly PMGGenomeConfigurations instance = new PMGGenomeConfigurations();

			static PMGGenomeConfigurations()
			{
			}

			private PMGGenomeConfigurations()
			{
			}

			public static PMGGenomeConfigurations Instance {
				get {
					return instance;
				}
			}
	        // All 2 layer genomes need to add up to 1
	        //
	        //
	        // Method probabilities add up to 1
	        public const float MethodProb_Key = 0.2f;
	        public const float MethodProb_ValFunc = 0.3f;
	        public const float MethodProb_ChgFunc = 0.2f;
	        public const float MethodProb_UtilFunc = 0.05f;
	        public const float MethodProb_Timestep = 0.25f;

			public const int   MethodMaxInitLen = 100;


			public const int MethodMaxGenomeLength = 100;

	        //
	        //
	        // Event probabilities add up to 1
	        public const float EventProb_Key = 0.2f;
	        public const float EventProb_UtilFunc = 0.05f;
	        public const float EventProb_ValFunc = 0.35f;
	        public const float EventProb_CondFunc = 0.4f;

			public const int   EventMaxInitLen = 100;

			public const int EventMaxGenomeLength = 100;



	        // Actor key probability
	        public const float ActorProb_Key = 0.1f;
			public const int   ActorMaxValue = 50;
			public const int   ActorMaxInitLen = 100;


			public const int ActorMaxGenomeLength = 100;


			public const int   ActorLocMaxInitLen = 100;


			public const int ActorLocMaxGenomeLength = 100;


		}
    }
}





