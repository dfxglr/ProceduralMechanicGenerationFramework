using System;
using PMGF.PMGCore;
using PMGF.PMGGenerator;
using GeneticSharp.Domain;

namespace PMGF
{
	namespace PMGGenerator
	{
		namespace PMGCCETest
		{
			class MainClass
			{
				public static void Main (string[] args)
				{
					// Use PMGGenController to control GeneticAlgorithmCCE
					// so that it can create Lists of Chromosomes (Genome Sets)
					// Lead that to the parser
					Console.WriteLine("Initiating PMG CCE Test");

					// Controller instance
					PMGGenController pgc = new PMGGenController ();

					// GA instance
					GeneticAlgorithmCCE ga = new GeneticAlgorithmCCE (pgc.CreateSpecies ());

					// Set the fitness function
					//ga.Fitness = new pmgfitness

					// Set the termination
					//ga.Termination = new ...
					Console.WriteLine("PMG CCE Test Done");
				}
			}

		}
	}
}
