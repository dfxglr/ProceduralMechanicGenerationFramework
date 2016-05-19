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


			public PMGFitness() : base()
			{
			}


            public double Evaluate(List<IChromosome> chromosomeSet)
            {
				PMGGenomeSet GenomeSet = new PMGGenomeSet ();
				double finalFitness = 0.0;


				// Convert list of chromosomes to genome set
				GenomeSet = ConvertChromosomeSetToGenomeSet(chromosomeSet);


				PMGSingleGameInstance GInstance = new PMGSingleGameInstance ();
				// Parse the genome set
				GInstance.GameSet.DecodeGenomeSet(GenomeSet);

				// Weigh intrinsic/extrinsic
				finalFitness = intrinsicWeight * IntrinsicFitness() + extrinsicWeight * ExtrinsicFitness(GInstance);

				//return finalFitness;
				return RandomizationProvider.Current.GetFloat();
            }

			private double IntrinsicFitness()
			{
				// Intrinsic fitnesses //
				return 0f;

			}

			private double ExtrinsicFitness(PMGSingleGameInstance GInstance)
			{
				// Extrinsic fitnesses //
				// Create a game instance

				GInstance.BuildInstance(false);

				// Run with various players and get extrinsic fitness
				return 0f;
			}


			private PMGGenomeSet ConvertChromosomeSetToGenomeSet(List<IChromosome> chromoSet)
			{
				PMGGenomeSet result = new PMGGenomeSet ();

				//actors
				List<int> actorGenome = new List<int>();
				List<int> actorPositionsGenome = new List<int>();
				//events
				List<List<int>> eventGenome = new List<List<int>>();
				//methods
				List<List<int>> methodGenome = new List<List<int>>();

				foreach (Gene g in chromoSet[0].GetGenes())
				{
					actorGenome.Add ((int)g.Value);
				}
				foreach (Gene g in chromoSet[1].GetGenes())
				{
					actorPositionsGenome.Add ((int)g.Value);
				}
				foreach (Gene g in chromoSet[2].GetGenes())
				{
					eventGenome.Add (g.Value as List<int>);
				}
				foreach (Gene g in chromoSet[3].GetGenes())
				{
					methodGenome.Add (g.Value as List<int>);
				}

				result.actorGenome = actorGenome;
				result.actorPositionsGenome = actorPositionsGenome;
				result.eventGenome = eventGenome;
				result.methodGenome = methodGenome;

				return result;
			}
        }

    }
}
