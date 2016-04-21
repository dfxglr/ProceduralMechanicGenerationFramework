using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMGF.PMGCore;

namespace PMGF
{
    namespace PMGGameInstance
    {
        class PMGSingleGameInstance
        {
            //creating a map
            PMGMap Map = new PMGMap();

            // creating a test genome
            PMGGenomeSet _genomeSet; 

            //lists to hold decoded genomes
            
            //actors types
            List<List<int>> actorTypes = new List<List<int>>();
            //actor type positions and amounts sorta
            List<List<int>> actorTypePosition3Split = new List<List<int>>();
            List<List<List<int>>> actorTypePositions = new List<List<List<int>>>();
            //lists for instantiation
            //actors in map
            List<PMGActor> actors = new List<PMGActor>();


            //lists to hold errors

            //actor genome
            //indexInGenome
            List<int> actorWithoutStats = new List<int>();
            //(indexInGenome,value)
            List<Vector> statsWithoutActors = new List<Vector>();

            //actor position genome

            //event genome
            
            //method genome


            public PMGSingleGameInstance()
            {
                
            }
            private void DecodeActorG()
            {
                int mainIndex = 0;
                int actorTypeIndex = 0;
                bool untilFirstActor = true;
                //runs through the actor genome
                for (int e = 0; e < _genomeSet.actorGenome.Count; e++)
                {
                    //checks for new actor type
                    if (_genomeSet.actorGenome[e] == -1)
                    {
                        //check that list dosent end with an actor
                        if (mainIndex + 1 != _genomeSet.actorGenome.Count)
                        {
                            //check that a new actor dosentstart right after the current one
                            if (_genomeSet.actorGenome[e + 1] != -1)
                            {
                                //add new type to type list
                                actorTypes.Add(new List<int>());

                                //begins at the current actor type in the genome list
                                for (int subIndex = mainIndex + 1; subIndex < _genomeSet.actorGenome.Count; subIndex++)
                                //foreach(int e2 in _genomeSet.actorGenome)
                                {
                                    //break out when new actor is met
                                    if (_genomeSet.actorGenome[subIndex] == -1)
                                    {
                                        break;
                                    }
                                    //adds to the value to the actor type list
                                    else
                                    {
                                        actorTypes[actorTypeIndex].Add(_genomeSet.actorGenome[subIndex]);
                                    }
                                }
                                //counts up the actor type index
                                actorTypeIndex++;
                            }
                            else
                            {
                                //Error: genome has 1 actor with no stats at position: genome[e]
                                actorWithoutStats.Add(e);
                            }
                            //untilFirstActor = false;
                        }
                        else
                        {
                            //error: genome has one actor with no stats at position: genome.count-1
                            actorWithoutStats.Add(e);
                        }
                        //happens at every time we hit an actor in the genome, tho only needs to happen once
                        untilFirstActor = false;
                    }
                    else if (untilFirstActor)
                    {
                        //error: genome has a stat with no parrent actor in the start of the genome
                        statsWithoutActors.Add(new Vector(e, _genomeSet.actorGenome[e]));
                    }
                    //counts up main index
                    mainIndex++;
                }
            }
            //decode actor position
            public void DecodeActorPos()
            {
                int mainIndex = 0;
                //runs through the genome
                for (int e = 0; e < _genomeSet.actorPositionsGenome.Count; e++)
                {

                    for (int subIndex = mainIndex; subIndex < 3; subIndex++)
                    {

                    }
                }


                    /*/run for each type of actor present in the actor type list
                   for(int e = 0; e < actorTypes.Count; e++)
                   {
                       int mainIndex = 0;
                       //runs through the actor position genome for that actor type
                       for (int e2 = 0; e2 < _genomeSet.actorPositionsGenome.Count; e2++)
                       {
                           //check if the elemnet of the genome is equal to the type in the type list
                           if (e2==e)
                           {
                               for(int subIndex = mainIndex; subIndex < 3)
                           }
                           mainIndex++;
                       }    
                   }//*/
                }
            //decode entire genome
            public void DecodeGenome(PMGGenomeSet TobeDecoded)
            {
                _genomeSet = TobeDecoded;
                DecodeActorG();
                DecodeActorPos();
                //decode actor positions
                //decode event
                //decode methods
            }
            // builds the instance
            public void BuildInstance()
            {
                //uses map, decoded genome lists, mimicks setup of coretest
                //core first
                //actors then and then so on
            }
            //debug displey function for checking all things have been put into actor list correctly
            public void DisplayActorTypeList()
            {
                Console.WriteLine("ActorTypes:");
                for (int i = 0;i < actorTypes.Count;i++)
                {
                    Console.Write("["+i+"] with stats: ");
                    for (int j = 0; j< actorTypes[i].Count;j++)
                    {
                        Console.Write(actorTypes[i][j]);
                    }
                    Console.WriteLine("");
                }
            }
            public void DisplayGenomeSetErrors()
            {
                //--------------------------------------------------------------------------------------//
                Console.WriteLine("GENOME SET ERROR REPORT:");

                //actors genome
                Console.WriteLine("ACTOR GENOME:");
                //
                Console.WriteLine("Actors with no stats:");
                Console.WriteLine(" in total: " + actorWithoutStats.Count);
                foreach (int e in actorWithoutStats)
                {
                    Console.WriteLine(" at index: "+e);
                }
                //
                Console.WriteLine("Stats with no actor in the start of genome:");
                Console.WriteLine(" in total: " + statsWithoutActors.Count);
                foreach (Vector e in statsWithoutActors)
                {
                    Console.WriteLine(" at index: " + e.X + ", with value: "+e.Y);
                }

                //events
                //methods
                //--------------------------------------------------------------------------------------//
            }
        }
    }
}
