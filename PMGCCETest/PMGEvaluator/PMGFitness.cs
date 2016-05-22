using System;
using System.Linq;
using System.Diagnostics;
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


			Stopwatch timer = new Stopwatch();
			private const int extrinsicMaxRunPerGame = 5; // seconds
			private const int extrinsicMaxTimeForTrials = 30;
			private const int NumTrialGames = 5;
			private const int InGameRunTime = 100; // 100 ingame time seconds 100 * (1 / seconds_per_timestep) timesteps
			private const int InGameRunSteps = 1000; 	// currently assuming 0.1s per timestep
			enum PlayerType {Passive, Random};
			int TotalCells = 0;
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
				IntrinsicWeights.Add(1);	// NumActors
				IntrinsicWeights.Add(1);	// NumActorTypes
				IntrinsicWeights.Add(1);	// NumEvents
				IntrinsicWeights.Add(1);	// NumMethods
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

				// Get number of tiles with 0 in map

				for (int x = 0; x < GInstance.GameSet.Map.chart.GetLength (0); x++) {
					for (int y = 0; y < GInstance.GameSet.Map.chart.GetLength (1); y++) {
						if (GInstance.GameSet.Map.chart [x, y] == 0)
							TotalCells++;
					}
				}

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


				// Genome breakage from parsing/building

				// Weight and sum up
				ifit = realWeight (IntrinsicWeights [(int)IW.NumActors]) * pdfLogNormScaled (numActors)
				+ realWeight (IntrinsicWeights [(int)IW.NumEvents]) * pdfLogNormScaled (numEvents)
				+ realWeight (IntrinsicWeights [(int)IW.NumMethods]) * pdfLogNormScaled (numMethods);

				return ifit;

			}

			private double ExtrinsicFitness(PMGSingleGameInstance GInstance)
			{
				// Extrinsic fitnesses //


				// Run with various players and get extrinsic fitness


				bool GameTimedOut = false;
				bool TrialsTimedOut = false;

				PlayerType PType = PlayerType.Passive;

				List<double> VisitedTilesRatio = new List<double> ();


				timer.Start ();

				for (int trials = 0; trials < NumTrialGames; trials++) {

					List<List<int>> VisitedTiles = new List<List<int>>();

					for (int timestep = 0; timestep < InGameRunSteps; timestep++) {
						// Get some diagnostics?
						if (timer.Elapsed.Seconds > extrinsicMaxRunPerGame) {
							// timeout
							GameTimedOut = true;
						}
						GInstance.UpdateActors ();

						// Loop through all the actors
						foreach (PMGActor actor in GInstance.SpawnedActors) {

							// Check if any new tiles have been visited
							bool TileHasBeenCounted = false;
							foreach (List<int> visitedTile in VisitedTiles) {
								if (Enumerable.SequenceEqual (visitedTile, actor.position))
									TileHasBeenCounted = true;
							}
							// Add them if they have not yet been visited
							if (!TileHasBeenCounted)
								VisitedTiles.Add (new List<int> (actor.position.ToArray ()));
						}
						// if game ended - how many timesteps did it take?
					}

					// Add number of visited tiles to list
					VisitedTilesRatio.Add(VisitedTiles.Count/TotalCells);

					if (timer.Elapsed.Seconds > extrinsicMaxTimeForTrials) {
						TrialsTimedOut = true;
					}

				}

				// Completion

				// Timeout

				// Resilience


				//Board Coverage
				double BoardCoverage = VisitedTilesRatio.Sum() / NumTrialGames;


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
