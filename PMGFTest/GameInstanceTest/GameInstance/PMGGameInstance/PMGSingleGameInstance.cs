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
            List<List<int>> actorTypePositions = new List<List<int>>();
            //List<List<List<int>>> actorTypePositions = new List<List<List<int>>>();
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
            //IndexInGenome, ActorType
            List<List<int>> UndefinedActorType = new List<List<int>>();
            List<Vector> DefinedActorTypeWithFlawedPosition = new List<Vector>();

            //event genome
            
            //method genome


            public PMGSingleGameInstance()
            {
                
            }
            //decode actor types
            private void DecodeActorGenome()
            {
                int actorTypeIndex = 0;
                bool untilFirstActor = true;
                //runs through the actor genome
                for (int mainIndex = 0; mainIndex < _genomeSet.actorGenome.Count; mainIndex++)
                {
                    //checks for new actor type
                    if (_genomeSet.actorGenome[mainIndex] == -1)
                    {
                        //check that list dosent end with an actor
                        if (mainIndex + 1 != _genomeSet.actorGenome.Count)
                        {
                            //check that a new actor dosentstart right after the current one
                            if (_genomeSet.actorGenome[mainIndex + 1] != -1)
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
                                actorWithoutStats.Add(mainIndex);
                            }
                            //untilFirstActor = false;
                        }
                        else
                        {
                            //error: genome has one actor with no stats at position: genome.count-1
                            actorWithoutStats.Add(mainIndex);
                        }
                        //happens at every time we hit an actor in the genome, tho only needs to happen once
                        untilFirstActor = false;
                    }
                    else if (untilFirstActor)
                    {
                        //error: genome has a stat with no parrent actor in the start of the genome
                        statsWithoutActors.Add(new Vector(mainIndex, _genomeSet.actorGenome[mainIndex]));
                    }
                }
                //check for null also
            }
            //decode actor position
            public void DecodeActorPosGenome()
            { 
                int PosListIndex = 0;
                int errorCount = 0;
                //runs through the actor positon genome and splits it into a 2d lists of lists of int 
                for (int mainIndex = 0; mainIndex < _genomeSet.actorPositionsGenome.Count; mainIndex+=3)
                {
                    //cross refences to see it the type declarations matches the position type
                    if (_genomeSet.actorPositionsGenome[mainIndex] < actorTypes.Count)
                    {
                        //checks for lack of coordianates
                        if (mainIndex+2 < _genomeSet.actorPositionsGenome.Count)
                        {
                            //adds a new type and position to the split 3 list
                            actorTypePositions.Add(new List<int>());
                            //split the genome into sections 2d list of lists of 3
                            for (int subIndex = mainIndex; subIndex < mainIndex + 3; subIndex++)
                            {
                                //checks for out of bounce at end of genome
                                actorTypePositions[PosListIndex].Add(_genomeSet.actorPositionsGenome[subIndex]);
                            }
                            PosListIndex++;
                        }
                        else
                        {
                            //error: not actor type found macthing the actor type position
                            DefinedActorTypeWithFlawedPosition.Add(new Vector(mainIndex, _genomeSet.actorPositionsGenome[mainIndex]));
                        }
                    }
                    else
                    {
                        //error
                        UndefinedActorType.Add(new List<int>());
                        UndefinedActorType[errorCount].Add(mainIndex);
                        UndefinedActorType[errorCount].Add(_genomeSet.actorPositionsGenome[mainIndex]);
                        UndefinedActorType[errorCount].Add(_genomeSet.actorPositionsGenome[mainIndex + 1]);
                        UndefinedActorType[errorCount].Add(_genomeSet.actorPositionsGenome[mainIndex + 2]);
                        errorCount++;
                    }
                }
                //check for null also
            }
            //decode entire genome
            public void DecodeGenomeSet(PMGGenomeSet TobeDecoded)
            {
                _genomeSet = TobeDecoded;
                DecodeActorGenome();
                DecodeActorPosGenome();
                //DecodeMethodGenome();
                //DecodeEventGenome();


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
            //debug displey function for checking all things have been put into actor list correctly
            public void DisplayActorTypePossplit3List()
            {
                Console.WriteLine("Which, and where:");
                for (int i = 0; i < actorTypePositions.Count; i++)
                {

                    Console.Write("[" + actorTypePositions[i][0] + "] at position (");
                    for (int j = 1; j < actorTypePositions[i].Count; j++)
                    {
                        Console.Write(actorTypePositions[i][j] + " ");
                    }
                    Console.WriteLine(")");
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

                //actor type positions genome
                Console.WriteLine("ACTOR TYPE POSITIONS GENOME:");
                //
                Console.WriteLine("undefined actor types:");
                Console.WriteLine(" in total: " + UndefinedActorType.Count);
                for(int i = 0; i < UndefinedActorType.Count;i++)
                {
                    Console.Write(" type: " + UndefinedActorType[i][1]);
                    Console.Write(" at index: " + UndefinedActorType[i][0]);
                    Console.WriteLine(" at position (" + UndefinedActorType[i][2]+ " " + UndefinedActorType[i][3]+")");
                }
                //
                Console.WriteLine("defined types with uncomplete position:");
                Console.WriteLine(" in total: " + DefinedActorTypeWithFlawedPosition.Count);
                foreach (Vector e in DefinedActorTypeWithFlawedPosition)
                {
                    Console.WriteLine(" at index: " + e.X + ", with value: " + e.Y);
                }
                //events
                //methods
                //--------------------------------------------------------------------------------------//
            }
        }
    }
}
