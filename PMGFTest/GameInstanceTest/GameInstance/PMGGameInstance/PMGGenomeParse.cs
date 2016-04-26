using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMGF
{
    namespace PMGGameInstance
    {
        class PMGGenomeParse
        {
            //local genome set
            PMGGenomeSet _genomeSet;


            //----------------------------------------------------------------//parsed lists

            //actors types
            List<List<int>> actorTypes = new List<List<int>>();
            //actor type positions and amounts sorta
            List<List<int>> actorTypePositions = new List<List<int>>();

            
            //----------------------------------------------------------------//

            //-------------------------------------------------------------------------------//error counting

            //actor genome
            //indexInGenome
            List<int> actorWithoutStats = new List<int>();
            //(indexInGenome,value)
            List<List<int>> statsWithoutActors = new List<List<int>>();
            //less than 2
            bool NoActorTypesInGenome = false;

            //actor position genome
            //IndexInGenome, ActorType
            List<List<int>> UndefinedActorType = new List<List<int>>();
            //(indexInGenome,value)
            List<List<int>> DefinedActorTypeWithFlawedPosition = new List<List<int>>();
            //this can only occur if the genome is too short
            List<List<int>> UndefinedActorTypeWithFlawedPosition = new List<List<int>>();
            //less than 3
            bool NoActorTypePositionsInGenome = false;

            //event genome

            //method genome

            //-------------------------------------------------------------------------------//


            public PMGGenomeParse()
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
                        //happens at every time we hit an actor in the genome, tho only needs to happen once, after first actor is made
                        untilFirstActor = false;
                    }
                    else if (untilFirstActor)
                    {
                        //error: genome has a stat with no parrent actor in the start of the genome
                        statsWithoutActors.Add(new List<int>());
                        statsWithoutActors[0].Add(mainIndex);
                        statsWithoutActors[0].Add(_genomeSet.actorGenome[mainIndex]);
                    }
                }
                //checks if any actor types has been added to the actor types list
                if (actorTypes.Count < 1)
                {
                    NoActorTypesInGenome = true;
                }
            }
            //decode actor position
            public void DecodeActorPosGenome()
            {
                int PosListIndex = 0;
                int errorCount = 0;
                //runs through the actor positon genome and splits it into a 2d lists of lists of int 
                for (int mainIndex = 0; mainIndex < _genomeSet.actorPositionsGenome.Count; mainIndex += 3)
                {
                    //cross refences to see it the type declarations matches the position type
                    if (_genomeSet.actorPositionsGenome[mainIndex] < actorTypes.Count)
                    {
                        //checks for lack of coordianates
                        if (mainIndex + 2 < _genomeSet.actorPositionsGenome.Count)
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
                            DefinedActorTypeWithFlawedPosition.Add(new List<int>());
                            DefinedActorTypeWithFlawedPosition[0].Add(mainIndex);
                            DefinedActorTypeWithFlawedPosition[1].Add(_genomeSet.actorGenome[mainIndex]);
                        }
                    }
                    else
                    {
                        //check if 
                        if (actorTypes.Count > 0)
                        {
                            //error: actor type position does not ahve a corrosponding actor type in the actor type list
                            UndefinedActorType.Add(new List<int>());
                            UndefinedActorType[errorCount].Add(mainIndex);
                            UndefinedActorType[errorCount].Add(_genomeSet.actorPositionsGenome[mainIndex]);
                            UndefinedActorType[errorCount].Add(_genomeSet.actorPositionsGenome[mainIndex + 1]);
                            UndefinedActorType[errorCount].Add(_genomeSet.actorPositionsGenome[mainIndex + 2]);
                            errorCount++;
                        }
                        else
                        {
                            Console.WriteLine("i am run apparrently");
                            //error: 
                            UndefinedActorTypeWithFlawedPosition.Add(new List<int>());
                            UndefinedActorTypeWithFlawedPosition[0].Add(mainIndex);
                            UndefinedActorTypeWithFlawedPosition[0].Add(_genomeSet.actorPositionsGenome[mainIndex]);
                            //*/
                        }
                    }
                }
                //Console.WriteLine(actorTypePositions[0].Count);
                //check if any possition has been added to the actor type positions list
                if (actorTypePositions.Count < 1)
                {
                    NoActorTypePositionsInGenome = true;
                }
            }
            //"decode" the methods genome
            public void DecodeMethodGenome()
            {
                //check for: null, lack of event start in the beginning of genome, having event with a lack of  
            }
            public void DecodeEventGenome()
            {
                //check for: null, 
            }

            //decode entire genome
            public void DecodeGenomeSet(PMGGenomeSet TobeDecoded)
            {
                _genomeSet = TobeDecoded;
                DecodeActorGenome();
                DecodeActorPosGenome();
                DecodeMethodGenome();
                DecodeEventGenome();
            }
            public void GenomeSetErrorReport()
            {
                //--------------------------------------------------------------------------------------//
                Console.WriteLine("GENOME SET ERROR REPORT:");

                //actors genome
                Console.WriteLine("ACTOR GENOME:");
                //
                if (NoActorTypesInGenome)
                {
                    Console.WriteLine(" genome, is either too short, has a single type with to few parameters, or has no types at all");
                }

                //
                Console.WriteLine("Actors with no stats:");
                Console.WriteLine(" in total: " + actorWithoutStats.Count);
                foreach (int e in actorWithoutStats)
                {
                    Console.WriteLine(" at index: " + e);
                }
                //
                Console.WriteLine("Stats with no actor in the start of genome:");
                Console.WriteLine(" in total: " + statsWithoutActors.Count);
                foreach (List<int> e in statsWithoutActors)
                {
                    Console.WriteLine(" at index: " + e[0] + ", with value: " + e[1]);
                }

                //actor type positions genome
                Console.WriteLine("ACTOR TYPE POSITIONS GENOME:");
                //
                if (NoActorTypePositionsInGenome)
                {
                    Console.WriteLine(" genome, is either too short, or has a single type which lacks 1 or all coords");
                }
                //
                Console.WriteLine("undefined actor types:");
                Console.WriteLine(" in total: " + UndefinedActorType.Count);
                for (int i = 0; i < UndefinedActorType.Count; i++)
                {
                    Console.Write(" type: " + UndefinedActorType[i][1] + ",");
                    Console.WriteLine(" at index: " + UndefinedActorType[i][0] + ",");

                    //Console.WriteLine(" at position (" + UndefinedActorType[i][2]+ " " + UndefinedActorType[i][3]+")");
                }
                //
                Console.WriteLine("defined types with uncomplete position:");
                Console.WriteLine(" in total: " + DefinedActorTypeWithFlawedPosition.Count);
                foreach (List<int> e in DefinedActorTypeWithFlawedPosition)
                {
                    Console.WriteLine(" type: " + e[0] + ", at index: " + e[1]);
                }
                Console.WriteLine("undefined types with uncomplete position(only 1):");
                for (int i = 0; i < UndefinedActorTypeWithFlawedPosition.Count; i++)
                {

                }
                //events
                //methods
                //--------------------------------------------------------------------------------------//
            }
            //debug displey function for checking all things have been put into actor list correctly
            public void DisplayActorTypeList()
            {
                Console.WriteLine("ActorTypes:");
                for (int i = 0; i < actorTypes.Count; i++)
                {
                    Console.Write("[" + i + "] with stats: ");
                    for (int j = 0; j < actorTypes[i].Count; j++)
                    {
                        Console.Write(actorTypes[i][j] + " ");
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

        }
    }
}
