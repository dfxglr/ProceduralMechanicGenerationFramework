﻿using System;
using System.Linq;
using System.Collections.Generic;
using PMGF.PMGCore;
using PMGF.PMGGenerator;
using PMGF.PMGGameInstance;
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
				static List<IChromosome> gset = new List<IChromosome> ();

				public static void Main (string[] args)
				{
					for (int i = 0; i < 10; i++)
					{
						//Console.ReadKey ();
						RunTest (i); 
					}

				}
				public static void RunTest(int run_nr)
				{

					PMGExporter exporter = new PMGExporter ();
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
					ga.Termination = pgc.CreateTermination();

					ga.GenerationRan += delegate {
						//System.Threading.Thread.Sleep(1000);
						gset = ga.BestChromosomeSet;
						var uset = ga.UberBestSet;
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
							Console.WriteLine("{0} ({3} - {1}, |{2}|):",gstr[i], gset[i].Fitness, ga.Species[i].Population.CurrentGeneration.Chromosomes.Sum(t => t.Fitness)/ga.Species[i].Population.CurrentGeneration.Chromosomes.Count, uset[i].Fitness);
							exporter.ExportSetToFile (new PMGGenomeSet (uset), string.Format("./OutputGenomes/kRun_nr_{0:00}_gen{1:00000}",run_nr, ga.GenerationsNumber));
							/*foreach(Gene _g in gg)
							{
								List<int> _gl = _g.Value as List<int>;
								if(_gl == null)
									Console.Write("{0} ", _g.Value);
								else
									_gl.ForEach(h => Console.Write("|{0}",h));
							}
							Console.WriteLine("--");*/
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
