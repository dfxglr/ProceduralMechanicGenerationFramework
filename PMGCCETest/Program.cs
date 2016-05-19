using System;
using System.Linq;
using System.Collections.Generic;
using PMGF.PMGCore;
using PMGF.PMGGenerator;
using GeneticSharp.Domain;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Terminations;

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
					for (int i = 0; i < 1000; i++)
					{
						//Console.ReadKey ();
						RunTest ();
					}

				}
				public static void RunTest()
				{
			
					// Use PMGGenController to control GeneticAlgorithmCCE
					// so that it can create Lists of Chromosomes (Genome Sets)
					// Lead that to the parser
					Console.WriteLine("Initiating PMG CCE Test");

					// Controller instance
					PMGGenController pgc = new PMGGenController ();

					// GA instance
					GeneticAlgorithmCCE ga = new GeneticAlgorithmCCE (pgc.CreateSpecies ());

					ga.Fitness = 	pgc.CreateFitness ();

					// Configure the GA (what should this do?..)
					pgc.ConfigGA(ga);

					// Set the termination
					//ga.Termination = new ...		// 1 minute
					ga.Termination = new GenerationNumberTermination(100);

					ga.GenerationRan += delegate {
						//System.Threading.Thread.Sleep(1000);
						List<IChromosome> gset = ga.BestChromosomeSet;
						Console.Clear ();
						Console.WriteLine ("Generation # {0}", ga.GenerationsNumber);
						Console.WriteLine("Time: {0}", ga.TimeEvolving);
						Console.WriteLine("Population sizes: {0}\t{1}\t{2}\t{3}", ga.Species[0].Population.CurrentGeneration.Chromosomes.Count,
																					ga.Species[1].Population.CurrentGeneration.Chromosomes.Count,
																					ga.Species[2].Population.CurrentGeneration.Chromosomes.Count,
																					ga.Species[3].Population.CurrentGeneration.Chromosomes.Count);

						// Hackish way to display genomes...
						string [] gstr = {"Actor","ActorLoc","Event","Method"};

						for(int i = 0; i < 4; i++)
						{
							Gene [] gg;
							gg = gset[i].GetGenes();
							Console.WriteLine("{0} ({1}, |{2}|):",gstr[i], gset[i].Fitness, ga.Species[i].Population.CurrentGeneration.Chromosomes.Sum(t => t.Fitness)/ga.Species[i].Population.CurrentGeneration.Chromosomes.Count);
							foreach(Gene _g in gg)
							{
								List<int> _gl = _g.Value as List<int>;
								if(_gl == null)
									Console.Write("{0} ", _g.Value);
								else
									_gl.ForEach(h => Console.Write("|{0}",h));
							}
							Console.WriteLine("--");
						}

						//System.Threading.Thread.Sleep(1000);
							
					};


					try{
						ga.Start();
					}
					catch(Exception ex) {
						Console.ForegroundColor = ConsoleColor.DarkRed;                 
						Console.WriteLine();                                            
						Console.WriteLine("Error: {0}", ex.Message);                    
						Console.ResetColor();                                           
						Console.ReadKey();                                              
						return;           
					}
					Console.WriteLine("PMG CCE Test Done");
				}
			}

		}
	}
}
