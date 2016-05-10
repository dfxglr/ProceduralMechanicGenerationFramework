using System.Collections.Generic;
using GeneticSharp.Domain.Fitnesses;
using GeneticSharp.Domain.Chromosomes;

using PMGF.PMGGameInstance;
using PMGF.PMGGenerator;
using PMGF.PMGCore;

namespace PMGF
{
    namespace PMGEvaluator
    {


        public class PMGFitness : ICCEFitness
        {

			PMGGenomeSet GenomeSet;
			PMGGenomeParse GenomeParser;

			PMGSingleGameInstance GInstance;

			/* Weights for parts of fitness */
			private const double extrinsicWeight = 0.5;
			private const double intrinsicWeight = 0.5;


			public PMGFitness() : base()
			{
				GenomeParser = new PMGGenomeParse ();
				GInstance = new PMGSingleGameInstance ();
			}


            public override double Evaluate(List<IChromosome> chromosomeSet)
            {
				double finalFitness = 0.0;


				// Convert list of chromosomes to genome set
				GenomeSet = ConvertChromosomeSetToGenomeSet(chromosomeSet);

				// Parse the genome set
				GenomeParser.DecodeGenomeSet(GenomeSet);


				// Weigh intrinsic/extrinsic
				finalFitness = intrinsicWeight * IntrinsicFitness() + extrinsicWeight * ExtrinsicFitness();

				return finalFitness;
            }

			private double IntrinsicFitness()
			{
				// Intrinsic fitnesses //

			}

			private double ExtrinsicFitness()
			{
				// Extrinsic fitnesses //
				// Create a game instance
				GInstance.BuildInstance(GenomeSet);

				// Run with various players and get extrinsic fitness
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
					actorGenome.Add (g.Value as int);
				}
				foreach (Gene g in chromoSet[1].GetGenes())
				{
					actorPositionsGenome.Add (g.Value as int);
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
