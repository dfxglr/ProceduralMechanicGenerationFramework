using System;
using GeneticSharp.Domain.Chromosomes;
using PMGF.PMGGenerator;

namespace PMGF
{
	namespace PMGGenerator
	{
		public sealed class PMGRepairFunction : ReparationBase
		{
			public PMGRepairFunction (int SpeciesID)
			{
				// Set specifics for genomes
				switch (SpeciesID) {
					case 0:
						MaxGenomeLength = PMGGenomeConfigurations.ActorMaxGenomeLength;
						break;
					case 1:
						MaxGenomeLength = PMGGenomeConfigurations.ActorLocMaxGenomeLength;
						break;
					case 2:
						MaxGenomeLength = PMGGenomeConfigurations.EventMaxGenomeLength;
						break;
					case 3:
						MaxGenomeLength = PMGGenomeConfigurations.MethodMaxGenomeLength;
						break;
				}
			}

			protected int MaxGenomeLength;


			protected override void PerformRepair(IChromosome chromosome)
			{
				// if the gene is longer than allowed, cut it down, harshly
				if (chromosome.Length > MaxGenomeLength) {
					chromosome.Resize (MaxGenomeLength);
				}
			}
		}
	}
}

