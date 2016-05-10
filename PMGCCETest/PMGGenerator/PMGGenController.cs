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

			private const int ActorMinPopSize = 50;
			private const int ActorMaxPopSize = 100;

			private const int MethodMinPopSize = 50;
			private const int MethodMaxPopSize = 100;

			private const int ActorLocMinPopSize = 50;
			private const int ActorLocMaxPopSize = 100;

			private const int EventMinPopSize = 50;
			private const int EventMaxPopSize = 100;


			private int UtilityFunctionsCount;
			private int ValueFunctionsCount;
			private int ChangeFunctionsCount;
			private int ConditionFunctionsCount;

			private int MapW;
			private int MapH;


			public List<CCESpecies> CreateSpecies()
			{
				// Get counts
				PMGCore pmgc = new PMGCore();
				UtilityFunctionsCount = pmgc.UtilityFunctions.Count;
				ValueFunctionsCount = pmgc.ValueFunctions.Count;
				ChangeFunctionsCount = pmgc.ChangeFunctions.Count;
				ConditionFunctionsCount = pmgc.Conditionfunctions.Count;

				PMGMap map = new PMGMap ();
				MapH = map.Map.GetLength (0);
				MapW = map.Map.GetLength (1);

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
				s.Crossover = new SVLC ();


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

				PMGActorGenome g = new PMGActorGenome (
					                   RandomizationProvider.Current.GetInt (
						                   2, PMGGenomeConfigurations.ActorMaxInitLen), PMGGenomeConfigurations.ActorMaxValue);

				// Add a population of actor genomes
				s.Population = new Population(ActorMinPopSize, ActorMaxPopSize, g.CreateNew());
				return s;
			}

			private CCESpecies CreateMethodSpecies()
			{
				// Add specific actor features to species
				CCESpecies s = CommonFeatsSpecies ();
				s.Name = "Methods";

				PMGMethodGenome g = new PMGMethodGenome (
					                    RandomizationProvider.Current.GetInt (2, PMGGenomeConfigurations.MethodMaxInitLen),
					                    Math.Max (UtilityFunctionsCount, Math.Max (ValueFunctionsCount, ChangeFunctionsCount)));

				s.Population = new Population (MethodMinPopSize, MethodMaxPopSize, g.CreateNew ());
				return s;
			}

			private CCESpecies CreateActorLocSpecies()
			{
				// Add specific actor features to species
				CCESpecies s = CommonFeatsSpecies ();
				s.Name = "Actor Locations";

				PMGActorLocationGenome g = new PMGActorLocationGenome (
					                           RandomizationProvider.Current.GetInt (2, PMGGenomeConfigurations.ActorLocMaxInitLen),
					                           Math.Max (MapH, MapW));

				s.Population = new Population (ActorLocMinPopSize, ActorLocMaxPopSize, g.CreateNew ());
				return s;
			}

			private CCESpecies CreateEventSpecies()
			{
				// Add specific actor features to species
				CCESpecies s = CommonFeatsSpecies ();
				s.Name = "Events";

				PMGDynamicEventGenome g = new PMGDynamicEventGenome (
					                          RandomizationProvider.Current.GetInt (2, PMGGenomeConfigurations.EventMaxInitLen),
					                          Math.Max (ValueFunctionsCount, Math.Max (UtilityFunctionsCount, ConditionFunctionsCount)));

				s.Population = new Population (EventMinPopSize, EventMaxPopSize, g.CreateNew ());
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
		}
	}
}