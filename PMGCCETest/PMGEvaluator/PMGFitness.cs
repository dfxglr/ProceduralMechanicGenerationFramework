using System.Collections.Generic;
using GeneticSharp.Domain.Fitnesses;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Randomizations;

using PMGF.PMGGameInstance;
using PMGF.PMGGenerator;
using PMGF.PMGCore;

namespace PMGF
{
    namespace PMGEvaluator
    {


        public class PMGFitness : ICCEFitness
        {


			/* Weights for parts of fitness */
			private const double extrinsicWeight = 0.5;
			private const double intrinsicWeight = 0.5;

			//Intrinsic weights
			private const double iwNumActors = 0.5;
			private const double iwNumMethods = 0.5;
			private const double iwNumEvents = 0.5;
			private const double iwNumEndEvents = 0.5;

			private const double iwComplexity = 0.5;
			private const double iwMovementType = 0.5;
			private const double iwGoalType = 0.5;

			// Extrinsic weights
			private const double ewCompletion = 0.5;
			private const double ewTimeout = 0.5;
			private const double ewResilience = 0.5;
			private const double ewBoardCoverage = 0.5;


			public PMGFitness() : base()
			{
			}


            public double Evaluate(List<IChromosome> chromosomeSet)
            {
				PMGGenomeSet GenomeSet = new PMGGenomeSet ();
				GenomeSet.ChromosomeSetToGenomeSet (chromosomeSet);
				double finalFitness = 0.0;


				// Convert list of chromosomes to genome set


				PMGSingleGameInstance GInstance = new PMGSingleGameInstance ();


				// Parse and build the genome set
				GInstance.GameSet.DecodeGenomeSet(GenomeSet);
				GInstance.BuildInstance(false);

				// Weigh intrinsic/extrinsic
				finalFitness = intrinsicWeight * IntrinsicFitness(GInstance) + extrinsicWeight * ExtrinsicFitness(GInstance);

				//return finalFitness;
				return RandomizationProvider.Current.GetFloat();
            }

			private double IntrinsicFitness(PMGSingleGameInstance GInstance)
			{
				// Intrinsic fitnesses //

				double ifit = 0;


				// Complexity

				// Movement Type

				// Goal Type


				// Number of things

				// num actors
				int numActorTypes = GInstance.SpawnAbleActors.Count;
				int numActors = GInstance.SpawnedActors.Count;


				// num methods

				// num events

				// num end events

				// Weight and sum up

				return 0f;

			}

			private double ExtrinsicFitness(PMGSingleGameInstance GInstance)
			{
				// Extrinsic fitnesses //


				// Create a game instance

				// Run with various players and get extrinsic fitness

				// Completion

				// Timeout

				// Resilience

				//Board Coverage


				return 0f;
			}
        }

    }
}
