using System;
using System.Collections.Generic;
using GeneticSharp.Domain;
using GeneticSharp.Domain.Mutations;
using GeneticSharp.Domain.Selections;
using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Crossovers;
using GeneticSharp.Domain.Fitnesses;
using GeneticSharp.Domain.Terminations;
using GeneticSharp.Domain.Reinsertions;
using GeneticSharp.Domain.Randomizations;

using PMGF.PMGEvaluator;
using PMGF.PMGCore;
using PMGF.PMGGameInstance;


namespace PMGF
{
	namespace PMGGenerator
	{

		public class PMGGenController
		{
			// Create all the species needed

			// Use genome lengths as per PMGGenomeConfigurations

			private const int ActorMinPopSize = 10;
			private const int ActorMaxPopSize = 50;

			private const int MethodMinPopSize = 10;
			private const int MethodMaxPopSize = 50;

			private const int ActorLocMinPopSize = 10;
			private const int ActorLocMaxPopSize = 50;

			private const int EventMinPopSize = 10;
			private const int EventMaxPopSize = 50;


			private int UtilityFunctionsCount;
			private int ValueFunctionsCount;
			private int ChangeFunctionsCount;
			private int ConditionFunctionsCount;

			private int MapW;
			private int MapH;


			public List<CCESpecies> CreateSpecies()
			{
				// Get counts
				PMGGameCore pmgc = new PMGGameCore();
				UtilityFunctionsCount = pmgc.UtilityFunctions.Collection.Count;
				ValueFunctionsCount = pmgc.ValueFunctions.Collection.Count;
				ChangeFunctionsCount = pmgc.ChangeFunctions.Collection.Count;
				ConditionFunctionsCount = pmgc.ConditionFunctions.Collection.Count;

				PMGMap map = new PMGMap ();
				MapH = map.chart.GetLength (0);
				MapW = map.chart.GetLength (1);

				List<CCESpecies> res = new List<CCESpecies> ();

				res.Add (CreateActorSpecies ());
				res.Add (CreateActorLocSpecies ());
				res.Add (CreateEventSpecies ());
				res.Add (CreateMethodSpecies ());

				return res;
			}

			private CCESpecies CommonFeatsSpecies()
			{
				// Create a species with all the common features
				CCESpecies s = new CCESpecies ();
				s.Selection = new StochasticUniversalSamplingSelection ();

				// Use SVLC 
				s.Crossover = new SVLC(2, 8);


				// Using mutations with relative probabilites as done in SVLC paper
				List<IMutation> muts = new List<IMutation> ();
				List<float> relprob = new List<float> ();

				muts.Add (new PointMutationSmall ());
				relprob.Add (6);
				muts.Add (new PointMutationLarge ());
				relprob.Add (4);
				muts.Add (new DeletionMutation ());
				relprob.Add (5);
				muts.Add (new InsertionMutation ());
				relprob.Add (1);
				muts.Add (new ReplicationMutation ());
				relprob.Add (1);
				muts.Add (new SlipMutation ());
				relprob.Add (1);

				s.Mutation = new MultipleMutations (muts, relprob);


				s.Reinsertion = new FitnessBasedReinsertion ();

				return s;
			}

			private CCESpecies CreateActorSpecies()
			{
				// Add specific actor features to species
				CCESpecies s = CommonFeatsSpecies ();
				s.Name = "Actors";
				s.ID = 0;

				PMGActorGenome g = new PMGActorGenome (
					                   RandomizationProvider.Current.GetInt (
						                   20, PMGGenomeConfigurations.ActorMaxInitLen), PMGGenomeConfigurations.ActorMaxValue);

				// Add a population of actor genomes
				s.Population = new Population(ActorMinPopSize, ActorMaxPopSize, g.CreateNew());
				s.Population.GenerationStrategy = new PerformanceGenerationStrategy (1);
				s.Reparation = new PMGRepairFunction (s.ID);
				return s;
			}

			private CCESpecies CreateActorLocSpecies()
			{
				// Add specific actor features to species
				CCESpecies s = CommonFeatsSpecies ();
				s.Name = "Actor Locations";
				s.ID = 1;

				PMGActorLocationGenome g = new PMGActorLocationGenome (
					                           RandomizationProvider.Current.GetInt (20, PMGGenomeConfigurations.ActorLocMaxInitLen),
					                           Math.Max (MapH, MapW));

				s.Population = new Population (ActorLocMinPopSize, ActorLocMaxPopSize, g.CreateNew ());
				s.Population.GenerationStrategy = new PerformanceGenerationStrategy (1);
				s.Reparation = new PMGRepairFunction (s.ID);
				return s;
			}

			private CCESpecies CreateEventSpecies()
			{
				// Add specific actor features to species
				CCESpecies s = CommonFeatsSpecies ();
				s.Name = "Events";
				s.ID = 2;

				PMGDynamicEventGenome g = new PMGDynamicEventGenome (
					                          RandomizationProvider.Current.GetInt (20, PMGGenomeConfigurations.EventMaxInitLen),
					                          Math.Max (ValueFunctionsCount, Math.Max (UtilityFunctionsCount, ConditionFunctionsCount)));

				s.Population = new Population (EventMinPopSize, EventMaxPopSize, g.CreateNew ());
				s.Population.GenerationStrategy = new PerformanceGenerationStrategy (1);
				s.Reparation = new PMGRepairFunction (s.ID);
				return s;
			}

			private CCESpecies CreateMethodSpecies()
			{
				// Add specific actor features to species
				CCESpecies s = CommonFeatsSpecies ();
				s.Name = "Methods";
				s.ID = 3;

				PMGMethodGenome g = new PMGMethodGenome (
					RandomizationProvider.Current.GetInt (20, PMGGenomeConfigurations.MethodMaxInitLen),
					Math.Max (UtilityFunctionsCount, Math.Max (ValueFunctionsCount, ChangeFunctionsCount)));

				s.Population = new Population (MethodMinPopSize, MethodMaxPopSize, g.CreateNew ());
				s.Population.GenerationStrategy = new PerformanceGenerationStrategy (1);
				s.Reparation = new PMGRepairFunction (s.ID);
				return s;
			}

			public ICCEFitness CreateFitness()
			{
				// Create the fitness function to be used
				return new PMGFitness();
			}

			public ITermination CreateTermination()
			{
				// Create the termination function to be used
				// ( time? )									H  M   S
				return new TimeEvolvingTermination(new TimeSpan(0, 10, 0));
			}

			public void ConfigGA(GeneticAlgorithmCCE ga)
			{
				
			}
		}
	}
}