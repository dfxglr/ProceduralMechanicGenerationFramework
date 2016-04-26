using System;

namespace PMGF
{
	namespace PMGGenerator
	{
		public class PMGCCEHandler
		{
			public PMGCCEHandler ()
			{

			}

            public void RunGeneration()
            {
                // Run through an entire generation
                //


                // For each species
                //      if (generation 0, i.e. first run)
                //          make random genes
                //          return
                //
                //      Select parents (stochastic uniform)
                //
                //      Crossover (SVLC)
                //
                //      Mutate children
                //
                //      Repair functions on children (discard entirely broken too?)
                //
                //

                // Fitness calculation
                //      For each species
                //          For each individual
                //             make set with random from other species
                //
                //             if (not first generation)
                //                  make set with previously highest fitness individuals from other species
                //
                //              Calculate fitness for 2 (or 1) generated sets (fitness is best of the two)
                //
                //              Update fitness of individual
                //

                // Update best performing set
                //


            }
		}
	}
}

