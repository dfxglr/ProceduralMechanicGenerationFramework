using System;
using System.Linq;
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
			private const int extrinsicWeight = 0;
			private const int intrinsicWeight = 1;

			//Intrinsic weights
			enum IW {NumActors, NumActorTypes, NumEvents, NumMethods};
			List<int> IntrinsicWeights = new List<int> ();

			// Extrinsic weights


			public PMGFitness() : base()
			{
				// Intrinsic weights
				IntrinsicWeights.Add(1);
				IntrinsicWeights.Add(1);
				IntrinsicWeights.Add(1);
				IntrinsicWeights.Add(1);
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
				finalFitness = realWeight(intrinsicWeight) * IntrinsicFitness(GInstance) 
								+ realWeight(extrinsicWeight) * ExtrinsicFitness(GInstance);

				//return finalFitness;
				return finalFitness;
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



				// num methods & event
				int numMethods = 0;
				int numMethodTypes = 0;
				List<int> methodTypesFound = new List<int> ();

				int numEvents = 0;
				int numEventTypes = 0;
				List<int> eventTypesFound = new List<int> ();

				foreach (PMGActor actor in GInstance.SpawnedActors) {
					numMethods += actor.Events.Count;

					foreach (PMGEvent e in actor.Events) {
						numEvents++;

						if (!eventTypesFound.Contains (e.Type))
							eventTypesFound.Add (e.Type);

						if (!methodTypesFound.Contains (e._method.Type))
							methodTypesFound.Add (e._method.Type);
					}
				}

				numMethodTypes = methodTypesFound.Count;
				numEventTypes = eventTypesFound.Count;


				// num end events

				// Weight and sum up
				ifit = realWeight (IntrinsicWeights [(int)IW.NumActors]) * pdfLogNormScaled (numActors)
				+ realWeight (IntrinsicWeights [(int)IW.NumEvents]) * pdfLogNormScaled (numEvents)
				+ realWeight (IntrinsicWeights [(int)IW.NumMethods]) * pdfLogNormScaled (numMethods);

				return ifit;

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

			private double realWeight(int relativeWeight, List<int> allRelativeWeights)
			{
				return relativeWeight / allRelativeWeights.Sum ();
			}

			private double pdfLogNormScaled(int x, double sigma=1, double theta=2, double median=8)
			{
				
				// See paper for documentation
				return (Math.Pow(Math.E,	-((Math.Pow(Math.Log((x-theta)/median),2))
													/
											Math.Pow(2*sigma,2)))
								/
						((x - theta)*sigma*Math.Sqrt(2*Math.PI)))

					* (1 / ((1/median) * Math.Sqrt(Math.E/(2*Math.PI))));		// y = [0;1]
			}
        }

    }
}
