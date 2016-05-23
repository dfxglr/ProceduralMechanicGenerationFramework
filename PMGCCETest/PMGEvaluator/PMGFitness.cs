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
			private const int NumTrialGames = 1;
			private const int InGameRunTime = 100; // 100 ingame time seconds 100 * (1 / seconds_per_timestep) timesteps
			private const int InGameRunSteps = 1000; 	// currently assuming 0.1s per timestep
			enum PlayerType {Passive, Random};
			int TotalCells = 0;
			/* Weights for parts of fitness */
			private const int extrinsicWeight = 1;
			private const int intrinsicWeight = 1;

			//Intrinsic weights
			enum IW {NumActors, NumActorTypes, NumEvents, NumMethods};
			List<int> IntrinsicWeights = new List<int> ();

			// Extrinsic weights
			enum EW {BoardCoverage}
			List<int> ExtrinsicWeights = new List<int>();
			List<int> ExtrinsicExceptionWeights = new List<int> ();



			public PMGFitness() : base()
			{
				// Intrinsic weights
				IntrinsicWeights.Add(1);	// NumActors
				IntrinsicWeights.Add(1);	// NumActorTypes
				IntrinsicWeights.Add(1);	// NumEvents
				IntrinsicWeights.Add(1);	// NumMethods

				ExtrinsicWeights.Add (1);	// BoardCoverage
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
				//finalFitness = ((double)intrinsicWeight / 2.0) * IntrinsicFitness(GInstance) 
					//+ ((double)extrinsicWeight / 2.0) * ExtrinsicFitness(GInstance);

				//return finalFitness;
				return finalFitness;
            }

			private double IntrinsicFitness(PMGSingleGameInstance GInstance)
			{
				// Intrinsic fitnesses //

				double ifit = 0.0;


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
				ifit = realWeight (IntrinsicWeights [(int)IW.NumActors], IntrinsicWeights) * pdfLogNormScaled (numActors);
				ifit += realWeight (IntrinsicWeights [(int)IW.NumEvents], IntrinsicWeights) * pdfLogNormScaled (numEvents);
				ifit += realWeight (IntrinsicWeights [(int)IW.NumActorTypes], IntrinsicWeights) * pdfLogNormScaled (numActorTypes);
				ifit += realWeight (IntrinsicWeights [(int)IW.NumMethods], IntrinsicWeights) * pdfLogNormScaled (numMethods);

				return ifit;

			}

			private double ExtrinsicFitness(PMGSingleGameInstance GInstance)
			{
				// Extrinsic fitnesses //
				double efit = 0.0;

				// Run with various players and get extrinsic fitness


				bool GameTimedOut = false;
				bool TrialsTimedOut = false;

				PlayerType PType = PlayerType.Passive;

				List<double> VisitedTilesRatio = new List<double> ();
				List<int> PoppingEmptyStack = new List<int> ();
				List<int> ReadEmptyStack = new List<int> ();
				List<int> ReadOutsideStack = new List<int> ();
				List<int> PushNullToStack = new List<int> ();
				List<int> ExecuteListOwnerNull = new List<int> ();

				timer.Start ();

				for (int trials = 0; trials < NumTrialGames; trials++) {

					PoppingEmptyStack.Add (0);
					ReadEmptyStack.Add (0);
					ReadOutsideStack.Add (0);
					PushNullToStack.Add (0);
					ExecuteListOwnerNull.Add (0);
					List<List<int>> VisitedTiles = new List<List<int>>();

					for (int timestep = 0; timestep < InGameRunSteps; timestep++) {
						// Get some diagnostics?
						if (timer.Elapsed.Seconds > extrinsicMaxRunPerGame) {
							// timeout
							GameTimedOut = true;
						}

						// Try to do a step. Catch any exceptions and keep running
						try
						{
							//GInstance.UpdateActors ();
						}
						catch(Exception e) {
							// Yes this is ugly and messy and hacky
							switch (e.Message) {
							case "Popping empty stack":
								PoppingEmptyStack[trials]++;
								break;
							case "Reading from empty stack":
								ReadEmptyStack[trials]++;
								break;
							case "Reading from outside stack":
								ReadOutsideStack[trials]++;
								break;
							case "Pushing null to stack":
								PushNullToStack[trials]++;
								break;
							case "ExecuteList owner is null":
								ExecuteListOwnerNull[trials]++;
								break;
							case "Casting of owner as PMGEvent failed.":
								break;
							case "Casting of owner as PMGMethod failed.":
								break;
							case "Tried to execute functions outside of list.":
								break;
							case "localStack is null":
								break;
							case "Casting of function as PMGValueFunction failed.":
								break;
							case "Casting of function as PMGUtilityFunction failed.":
								break;
							case "Casting of function as PMGConditionFunction failed.":
								break;
							case "Casting of function as PMGChangeFunction failed.":
								break;
							case "Pushing null to valuestack":
								break;
							default:
								Console.WriteLine ("Some other exception from inside the game:");
								Console.WriteLine (e.Message);
								Console.WriteLine ("Full Genome:");
								PMGExporter exporter = new PMGExporter ();
								exporter.ExportSetToFile (GInstance.GameSet._genomeSet, "./genome_for_no_method_functions");
								GInstance.GameSet._genomeSet.ExportSerializedGenomeSet ().ForEach (t => Console.WriteLine (t));
								throw new Exception (e.Message);
								break;
							}
						}
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
					VisitedTilesRatio.Add((double)VisitedTiles.Count/(double)TotalCells);

					if (timer.Elapsed.Seconds > extrinsicMaxTimeForTrials) {
						TrialsTimedOut = true;
					}

				}

				// Completion

				// Timeout

				// Resilience


				//Board Coverage
				double BoardCoverage = (double)VisitedTilesRatio.Sum() / (double)NumTrialGames;


				efit = BoardCoverage * ExtrinsicWeights [(int)EW.BoardCoverage];

				return efit;
			}

			private double realWeight(int relativeWeight, List<int> allRelativeWeights)
			{
				double result = (double)relativeWeight / (double)allRelativeWeights.Sum ();
				return result;
			}

			private double pdfLogNormScaled(int x, double sigma=1.0, double theta=2.0, double median=8.0)
			{
				
				// See paper for documentation
				if (x <= theta)
					return 0.0;
				
				double scaling = (1.0 / ((1.0/median) * Math.Sqrt(Math.E/(2.0*Math.PI))));
				double innerLog = (x-theta)/median;
				double eExponent = -1 * (Math.Pow(Math.Log (innerLog),2) / (2 * Math.Pow (sigma, 2)));
				double lowerPart = (x - theta) * sigma * Math.Sqrt (2.0 * Math.PI);
				double unscaledResult = Math.Pow (Math.E, eExponent) / lowerPart;
				double result = unscaledResult * scaling;
				
				return result;
			}
        }

    }
}
