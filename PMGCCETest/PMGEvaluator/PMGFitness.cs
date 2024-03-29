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
			private const int extrinsicMaxRunPerGame = 4; // seconds
			private const int extrinsicMaxTimeForTrials = 16;
			private const int NumTrialGames = 1;
			private const int InGameRunTime = 100; // 100 ingame time seconds 100 * (1 / seconds_per_timestep) timesteps
			private const int InGameRunSteps = 200; 	// currently assuming 0.1s per timestep
			enum PlayerType {Passive, Random};
			int TotalCells = 0;
			/* Weights for parts of fitness */
			private const int extrinsicWeight = 1;
			private const int intrinsicWeight = 1;

			//Intrinsic weights
			enum IW {NumActors, NumActorTypes, NumEvents, NumMethods};
			List<int> IntrinsicWeights = new List<int> ();

			// Extrinsic weights
			enum EW {BoardCoverage, Exceptions}
			List<int> ExtrinsicWeights = new List<int>();
			List<int> ExtrinsicExceptionWeights = new List<int> ();



			public PMGFitness() : base()
			{
				// Intrinsic weights
				IntrinsicWeights.Add(4);	// NumActors
				IntrinsicWeights.Add(1);	// NumActorTypes
				IntrinsicWeights.Add(2);	// NumEvents
				IntrinsicWeights.Add(2);	// NumMethods

				ExtrinsicWeights.Add (3);	// BoardCoverage
				ExtrinsicWeights.Add (1);	// Exceptions

				PMGSingleGameInstance GInstance = new PMGSingleGameInstance ();
				// Get number of tiles with 0 in map

				for (int x = 0; x < GInstance.GameSet.Map.chart.GetLength (0); x++) {
					for (int y = 0; y < GInstance.GameSet.Map.chart.GetLength (1); y++) {
						if (GInstance.GameSet.Map.chart [x, y] == 0)
							TotalCells++;
					}
				}
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
				finalFitness = (Convert.ToDouble(intrinsicWeight) / 2.0) * IntrinsicFitness(GInstance) 
					+ (Convert.ToDouble(extrinsicWeight) / 2.0) * ExtrinsicFitness(GInstance);

				return finalFitness;
//				return RandomizationProvider.Current.GetFloat();
            }

			private double IntrinsicFitness(PMGSingleGameInstance GInstance)
			{
				// Intrinsic fitnesses //

				double ifit = 0.0;
				return ifit;

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

				// Exceptions
				List<double> VisitedTilesRatio = new List<double> ();

				List<int> PoppingEmptyStack = new List<int> ();
				List<int> ReadEmptyStack = new List<int> ();
				List<int> ReadOutsideStack = new List<int> ();
				List<int> PushNullToStack = new List<int> ();
				List<int> ExecuteListOwnerNull = new List<int> ();
				List<int> CastingOfFunctionFailed = new List<int> ();
				List<int> ExecutingFunctionsOutsideList = new List<int> ();
				List<int> StackIsNull = new List<int> ();
				List<int> NoStepsInMethod = new List<int> ();
				List<int> CastingOfOwnerFailed = new List<int> ();
				List<int> UndefinedError = new List<int> ();


				//ExceptionWeights
				ExtrinsicExceptionWeights.Add(1);	//PoppingEmptyStack
				ExtrinsicExceptionWeights.Add(1);	//ReadEmptyStack
				ExtrinsicExceptionWeights.Add(1);	//ReadOutsideStack
				ExtrinsicExceptionWeights.Add(1);	//PushNullToStack
				ExtrinsicExceptionWeights.Add(1);	//ExecuteListOwnerNull
				ExtrinsicExceptionWeights.Add(1);	//CastingOfFunctionFailed
				ExtrinsicExceptionWeights.Add(1);	//ExecutingFunctionsOutsideList
				ExtrinsicExceptionWeights.Add(1);	//StackIsNull
				ExtrinsicExceptionWeights.Add(2);	//NoStepsInMethod
				ExtrinsicExceptionWeights.Add(1);	//CastingOfOwnerFailed
				ExtrinsicExceptionWeights.Add(1);	//UndefinedError


				timer.Start ();

				for (int trials = 0; trials < NumTrialGames; trials++) {

					PoppingEmptyStack.Add (0);
					ReadEmptyStack.Add (0);
					ReadOutsideStack.Add (0);
					PushNullToStack.Add (0);
					ExecuteListOwnerNull.Add (0);
					CastingOfOwnerFailed.Add (0);
					ExecutingFunctionsOutsideList.Add (0);
					StackIsNull.Add (0);
					CastingOfFunctionFailed.Add (0);
					PushNullToStack.Add (0);
					NoStepsInMethod.Add (0);
					UndefinedError.Add (0);
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
							GInstance.UpdateActors ();
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
								CastingOfOwnerFailed[trials]++;
								break;
							case "Casting of owner as PMGMethod failed.":
								CastingOfOwnerFailed[trials]++;
								break;
							case "Tried to execute functions outside of list.":
								ExecutingFunctionsOutsideList[trials]++;
								break;
							case "localStack is null":
								StackIsNull[trials]++;
								break;
							case "Casting of function as PMGValueFunction failed.":
								CastingOfFunctionFailed[trials]++;
								break;
							case "Casting of function as PMGUtilityFunction failed.":
								CastingOfFunctionFailed[trials]++;
								break;
							case "Casting of function as PMGConditionFunction failed.":
								CastingOfFunctionFailed[trials]++;
								break;
							case "Casting of function as PMGChangeFunction failed.":
								CastingOfFunctionFailed[trials]++;
								break;
							case "Pushing null to valuestack":
								PushNullToStack[trials]++;
								break;
							case "Method has no steps.":
								NoStepsInMethod[trials]++;
								break;
							default:
								UndefinedError [trials]++;
//								Console.WriteLine ("Some other exception from inside the game:");
//								Console.WriteLine (e.Message);
//								Console.WriteLine ("Full Genome:");
//								PMGExporter exporter = new PMGExporter ();
//								exporter.ExportSetToFile (GInstance.GameSet._genomeSet, "./genome_for_no_method_functions");
//								GInstance.GameSet._genomeSet.ExportSerializedGenomeSet ().ForEach (t => Console.WriteLine (t));
//								throw new Exception (e.Message);
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
					VisitedTilesRatio.Add(Convert.ToDouble(VisitedTiles.Count)/Convert.ToDouble(TotalCells));

					if (timer.Elapsed.Seconds > extrinsicMaxTimeForTrials) {
						TrialsTimedOut = true;
					}

				}

				// Completion

				// Timeout

				// Resilience


				//Board Coverage
				double BoardCoverage = Convert.ToDouble(VisitedTilesRatio.Sum()) / Convert.ToDouble(NumTrialGames);

				double Exceptions = LogFunction (PoppingEmptyStack.Average ()) * realWeight(ExtrinsicExceptionWeights [0], ExtrinsicExceptionWeights)
				                    + LogFunction (ReadEmptyStack.Average ()) * realWeight(ExtrinsicExceptionWeights [0], ExtrinsicExceptionWeights)
				                    + LogFunction (ReadOutsideStack.Average ()) * realWeight(ExtrinsicExceptionWeights [1], ExtrinsicExceptionWeights)
				                    + LogFunction (PushNullToStack.Average ()) * realWeight(ExtrinsicExceptionWeights [2], ExtrinsicExceptionWeights)
				                    + LogFunction (ExecuteListOwnerNull.Average ()) * realWeight(ExtrinsicExceptionWeights [3], ExtrinsicExceptionWeights)
				                    + LogFunction (CastingOfOwnerFailed.Average ()) * realWeight(ExtrinsicExceptionWeights [4], ExtrinsicExceptionWeights)
				                    + LogFunction (ExecutingFunctionsOutsideList.Average ()) * realWeight(ExtrinsicExceptionWeights [5], ExtrinsicExceptionWeights)
				                    + LogFunction (StackIsNull.Average ()) * realWeight(ExtrinsicExceptionWeights [6], ExtrinsicExceptionWeights)
				                    + LogFunction (CastingOfFunctionFailed.Average ()) * realWeight(ExtrinsicExceptionWeights [7], ExtrinsicExceptionWeights)
				                    + LogFunction (PushNullToStack.Average ()) * realWeight(ExtrinsicExceptionWeights [8], ExtrinsicExceptionWeights)
				                    + LogFunction (NoStepsInMethod.Average ()) * realWeight(ExtrinsicExceptionWeights [9], ExtrinsicExceptionWeights)
				                    + LogFunction (UndefinedError.Average ()) * realWeight(ExtrinsicExceptionWeights [10], ExtrinsicExceptionWeights);


				efit = BoardCoverage * realWeight(ExtrinsicWeights [(int)EW.BoardCoverage], ExtrinsicWeights)
					+ Exceptions * realWeight(ExtrinsicWeights[(int)EW.Exceptions],ExtrinsicWeights);

				return efit;
			}

			private double realWeight(int relativeWeight, List<int> allRelativeWeights)
			{
				double result = Convert.ToDouble(relativeWeight) / Convert.ToDouble(allRelativeWeights.Sum ());
				return result;
			}

			private double pdfLogNormScaled(int x, double sigma=1.0, double theta=2.0, double median=8.0)
			{
				
				// See paper for documentation
				double dx = Convert.ToDouble(x);
				if (dx <= theta)
					return 0.0;
				
				double scaling = (1.0 / ((1.0/median) * Math.Sqrt(Math.E/(2.0*Math.PI))));
				double innerLog = (dx-theta)/median;
				double eExponent = -1 * (Math.Pow(Math.Log (innerLog),2) / (2 * Math.Pow (sigma, 2)));
				double lowerPart = (dx - theta) * sigma * Math.Sqrt (2.0 * Math.PI);
				double unscaledResult = Math.Pow (Math.E, eExponent) / lowerPart;
				double result = unscaledResult * scaling;
				
				return result;
			}

			private double LogFunction(double x, double a=0.75)
			{
				double result = Math.Min(a * Math.Log10 (x),1.0);
				return Math.Max(result,0.0);
			}
        }

    }
}
