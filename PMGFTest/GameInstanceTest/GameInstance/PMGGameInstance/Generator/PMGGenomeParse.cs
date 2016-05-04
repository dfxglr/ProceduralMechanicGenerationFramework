using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMGF.PMGGenerator;

namespace PMGF
{
    namespace PMGGameInstance
    {
        class PMGGenomeParse
        {
            //local genome set tho it needs to be public
            public PMGGenomeSet _genomeSet;
            //----------------------------------------------------------------//parsed lists
            //actors types
            public List<List<int>> actorTypes = new List<List<int>>();
            //actor type positions and amounts sorta
            public List<List<int>> actorTypePositions = new List<List<int>>();
            //index list for event genome
            public List<int> eventIndexList = new List<int>();
            //index list for mehtod genome
            public List<int> methodIndexList = new List<int>();
            //-------------------------------------------------------------------------------//error counting
            //-----------------------//
            //actor genome
            //indexInGenome
            List<int> actorWithoutStats = new List<int>();
            //(indexInGenome,value)
            List<List<int>> statsWithoutActors = new List<List<int>>();
            //
            //List<List<int>> 
            //
            bool ActorTypesGenomeLengthLessThanTwo = false;
            bool anyActorFound = false;
            //-----------------------//
            //actor position genome
            //IndexInGenome, ActorType
            List<List<int>> UndefinedActorType = new List<List<int>>();
            //(indexInGenome,value)
            List<List<int>> DefinedActorTypeWithFlawedPosition = new List<List<int>>();
            //this can only occur if the genome is too short
            List<List<int>> UndefinedActorTypeWithFlawedPosition = new List<List<int>>();
            //
            bool ActorTypePositionsGenomeLengthLessThanThree = false;
            //-----------------------//
            //event genome
            List<int> IncompleteEvents = new List<int>();
            //
            bool EventGenomeLengthLessThanThree = false;
            bool anyEventFound = false;
            //-----------------------//
            //method genome
            List<int> IncompleteMethods = new List<int>();

            //
            bool MethodGenomeLengthLessThanTwo = false;
            bool anyMethodFound = false;
            //-------------------------------------------------------------------------------//


            //constuctor
            public PMGGenomeParse()
            {
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

            //decode actor types
            public void DecodeActorGenome()
            {
                int actorTypeIndex = 0;
                int statsWOAErrors = 0;
                
                //check genome length 
                if (_genomeSet.actorGenome.Count > 2)
                {
                    //runs through the actor genome
                    for (int mainIndex = 0; mainIndex < _genomeSet.actorGenome.Count; mainIndex++)
                    {
                        //checks for new actor type
                        if (_genomeSet.actorGenome[mainIndex] == (int)GenomeKeys.ActorKey)
                        {
                            //check that list dosent end with an actor
                            if (mainIndex + 2 < _genomeSet.actorGenome.Count)
                            {
                                //check that a new actor dosentstart right after the current one
                                if (_genomeSet.actorGenome[mainIndex + 1] != (int)GenomeKeys.ActorKey && _genomeSet.actorGenome[mainIndex + 2] != (int)GenomeKeys.ActorKey)
                                {
                                    //add new type to type list
                                    actorTypes.Add(new List<int>());

                                    //begins at the current actor type in the genome list
                                    for (int subIndex = mainIndex + 1; subIndex < _genomeSet.actorGenome.Count; subIndex++)
                                    {
                                        //break out when new actor is met
                                        if (_genomeSet.actorGenome[subIndex] == (int)GenomeKeys.ActorKey)
                                        {
                                            break;
                                        }
                                        //adds to the value to the actor type list
                                        else
                                        {
                                            actorTypes[actorTypeIndex].Add(_genomeSet.actorGenome[subIndex]);
                                        }
                                    }
                                    //check for odd length of stats list, to ensure we only get pairs
                                    if (actorTypes[actorTypeIndex].Count%2 !=0)
                                    {
                                        actorTypes[actorTypeIndex].RemoveAt(actorTypes[actorTypeIndex].Count-1);
                                        //error: stats list for actor x was odd, last item removed


                                    }

                                    //counts up the actor type index
                                    actorTypeIndex++;
                                }
                                else
                                {
                                    //Error: genome has 1 actor with no stats at position: genome[e]
                                    actorWithoutStats.Add(mainIndex);
                                }
                            }
                            else
                            {
                                //error: genome has one actor with no stats at position: genome.count-1
                                actorWithoutStats.Add(mainIndex);
                            }
                            //happens at every time we hit an actor in the genome, tho only needs to happen once, after first actor is made
                            anyActorFound = true;
                        }
                        else if (!anyActorFound)
                        {
                            //error: genome has a stat with no parrent actor in the start of the genome
                            statsWithoutActors.Add(new List<int>());
                            statsWithoutActors[statsWOAErrors].Add(mainIndex);
                            statsWithoutActors[statsWOAErrors].Add(_genomeSet.actorGenome[mainIndex]);
                            statsWOAErrors++;
                        }
                    }
                }
                else
                {
                    //error genome to short to make anything
                    ActorTypesGenomeLengthLessThanTwo = true;
                }
            }
            //decode actor position
            public void DecodeActorPosGenome()
            {
                int PosListIndex = 0;
                int DefinedActorWFPErrorC = 0;
                int UndefinedActorErrorC = 0;

                //check genome length
                if (_genomeSet.actorPositionsGenome.Count > 2)
                {
                    //check is for no actors
                    if (anyActorFound)
                    {
                        //runs through the actor positon genome and splits it into a 2d lists of lists of int 
                        for (int mainIndex = 0; mainIndex < _genomeSet.actorPositionsGenome.Count; mainIndex += 3)
                        {
                            //cross refences to see it the type declarations matches the position type
                            if (_genomeSet.actorPositionsGenome[mainIndex] < actorTypes.Count)
                            {
                                //checks for lack of coordianates
                                if (mainIndex + 2 < _genomeSet.actorPositionsGenome.Count)
                                {
                                    //adds new list 
                                    actorTypePositions.Add(new List<int>());
                                    //takes values from mainIndex and the following two
                                    for (int subIndex = mainIndex; subIndex < mainIndex + 3; subIndex++)
                                    {
                                        actorTypePositions[PosListIndex].Add(_genomeSet.actorPositionsGenome[subIndex]);
                                    }
                                    PosListIndex++;
                                }
                                else
                                {
                                    //error: not actor type found macthing the actor type position
                                    DefinedActorTypeWithFlawedPosition.Add(new List<int>());
                                    DefinedActorTypeWithFlawedPosition[DefinedActorWFPErrorC].Add(mainIndex);
                                    DefinedActorTypeWithFlawedPosition[DefinedActorWFPErrorC].Add(_genomeSet.actorPositionsGenome[mainIndex]);
                                    DefinedActorWFPErrorC++;
                                }
                            }
                            else
                            {
                                //error: actor type position does not have a corrosponding actor type in the actor type list
                                UndefinedActorType.Add(new List<int>());
                                //index
                                UndefinedActorType[UndefinedActorErrorC].Add(mainIndex);
                                //type
                                UndefinedActorType[UndefinedActorErrorC].Add(_genomeSet.actorPositionsGenome[mainIndex]);
                                //position
                                if (mainIndex + 2 < _genomeSet.actorPositionsGenome.Count)
                                {
                                    //x
                                    UndefinedActorType[UndefinedActorErrorC].Add(_genomeSet.actorPositionsGenome[mainIndex + 1]);
                                    //y
                                    UndefinedActorType[UndefinedActorErrorC].Add(_genomeSet.actorPositionsGenome[mainIndex + 2]);
                                }
                                UndefinedActorErrorC++;
                            }
                        }
                    }
                }
                else
                {
                    //error: actor positions genome too short to make anything
                    ActorTypePositionsGenomeLengthLessThanThree = true;
                }
            }
            //"decode" method genome
            public void DecodeMethodGenome()
            {
                //check for length more that 1
                if (_genomeSet.methodGenome.Count > 1)
                {
                    for (int mainIndex = 0; mainIndex < _genomeSet.methodGenome.Count; mainIndex++)
                    {
                        //check for new event
                        if (_genomeSet.methodGenome[mainIndex][0] == (int)GenomeKeys.MethodKey)
                        {
                            //check that list dosent end with an event
                            if (mainIndex + 1 < _genomeSet.methodGenome.Count)
                            {
                                //check that a new method dosentstart right after the current one
                                if (_genomeSet.methodGenome[mainIndex + 1][0] != (int)GenomeKeys.MethodKey)
                                {
                                    //count up new method beginnings
                                    methodIndexList.Add(mainIndex);

                                }
                                else
                                {
                                    //error: method with no condition and valuefunc
                                    IncompleteMethods.Add(mainIndex);
                                }
                            }
                            else
                            {
                                //error: method with no condition and valuefunc
                                IncompleteMethods.Add(mainIndex);
                            }
                            anyMethodFound = true;
                        }
                        else if (!anyMethodFound)
                        {
                            //error: genome contains no events
                            //Console.WriteLine(" no balls");
                        }//*/
                    }
                }
                else
                {
                    // error: genome is too short to make a single event(count less than 2)
                    MethodGenomeLengthLessThanTwo = true;
                }
            }
            //"decode" event genome
            public void DecodeEventGenome()
            {
                //check for length more that 1
                if (_genomeSet.eventGenome.Count > 2)
                {
                    for (int mainIndex = 0; mainIndex < _genomeSet.eventGenome.Count; mainIndex++)
                    {
                        //check for new event
                        if (_genomeSet.eventGenome[mainIndex][0] == (int)GenomeKeys.EventKey)
                        {
                            //check that list dosent end with an event
                            if (mainIndex + 2 < _genomeSet.eventGenome.Count)
                            {
                                //check that a new actor dosentstart right after the current one
                                if (_genomeSet.eventGenome[mainIndex + 1][0] != (int)GenomeKeys.EventKey && _genomeSet.eventGenome[mainIndex + 2][0] != (int)GenomeKeys.EventKey)
                                {
                                    //count up new event beginnings
                                    eventIndexList.Add(mainIndex);
                                      
                                }
                                else
                                {
                                    //error: event with no condition and valuefunc
                                    IncompleteEvents.Add(mainIndex);
                                }
                            }
                            else
                            {
                                //error: event with no condition and valuefunc
                                IncompleteEvents.Add(mainIndex);
                            }
                            anyEventFound = true;
                        }
                        else if(!anyEventFound)
                        {
                            //error: genome contains no events
                            //Console.WriteLine(" no balls");
                        }//*/
                    }
                }
                else
                {
                    // error: genome is too short to make a single event(count less than 2)
                    EventGenomeLengthLessThanThree = true;
                }       
            }
            //error report(displays in console)
            public void GenomeSetErrorReport()
            {
                //--------------------------------------------------------------------------------------//
                Console.WriteLine("GENOME SET ERROR REPORT:");

                //--------------------------------------------//
                //actors genome
                Console.WriteLine("ACTOR GENOME:");
                //checking if genome is too short to produce any result
                if (!ActorTypesGenomeLengthLessThanTwo)
                {
                    //check for in there are any actors
                    if (anyActorFound) {
                        //displays total amount of actors with not stats, and their index in the genome
                        Console.WriteLine("Actors with no stats:");
                        Console.WriteLine(" in total: " + actorWithoutStats.Count);
                        foreach (int e in actorWithoutStats)
                        {
                            Console.WriteLine(" at index: " + e);
                        }

                        //displays the stats that has not parrent actor if any, in the beginning of the genome
                        Console.WriteLine("Stats with no actor in the start of genome:");
                        Console.WriteLine(" in total: " + statsWithoutActors.Count);
                        for (int i = 0; i < statsWithoutActors.Count; i++)
                        {
                            Console.WriteLine(" at index: " + statsWithoutActors[i][0] + ", with value: " + statsWithoutActors[i][1]);
                        }
                    }
                    else
                    {
                        Console.WriteLine(" no actor types found in genome");
                    }
                }
                else
                {
                    Console.WriteLine(" genome is too short to create any actors(count less than 1)");
                }
                //--------------------------------------------//
                //actor type positions genome
                Console.WriteLine("ACTOR TYPE POSITIONS GENOME:");

                //check for genome is too short
                if (!ActorTypePositionsGenomeLengthLessThanThree)
                {
                    //check for actor genome too short
                    if (!ActorTypesGenomeLengthLessThanTwo)
                    {
                        // check for no actors founds
                        if (anyActorFound)
                        {
                            //undefined actor types in the actor type position genome
                            Console.WriteLine("undefined actor types:");
                            Console.WriteLine(" in total: " + UndefinedActorType.Count);
                            for (int i = 0; i < UndefinedActorType.Count; i++)
                            {
                                Console.Write(" at index: " + UndefinedActorType[i][0] + ", type: " + UndefinedActorType[i][1]);
                                //check for if position has both coords, by checking length of list
                                if (UndefinedActorType[i].Count > 2)
                                {
                                    Console.Write(", at position: ");
                                    Console.Write("(" + UndefinedActorType[i][2]);
                                    Console.WriteLine(" " + UndefinedActorType[i][3] + ")");
                                }
                                else
                                {
                                    Console.WriteLine(", at position: (? ?)");
                                }
                            }

                            //defined actor types with incomplete positions
                            Console.WriteLine("defined types with uncomplete position(only 1 at the end):");
                            Console.WriteLine(" in total: " + DefinedActorTypeWithFlawedPosition.Count);
                            foreach (List<int> e in DefinedActorTypeWithFlawedPosition)
                            {
                                Console.WriteLine(" at index: " + e[0] + ", type: " + e[1]);
                            }
                        }
                        else
                        {
                            Console.WriteLine(" actor type genome has not actor types");
                        }
                    }
                    else
                    {
                        Console.WriteLine(" actor position genome is of sufficient length, however actor type genome is also to short to have any complete actors");
                    }
                }
                else
                {
                    Console.WriteLine(" actor positions genome is too short to have any complete positions(count is less than 3)");
                }
                //--------------------------------------------//
                //event genome
                Console.WriteLine("EVENT GENOME");
                //check for length
                if (!EventGenomeLengthLessThanThree) {
                    //check for any events
                    if (anyEventFound) {
                        Console.WriteLine(" Incomplete events");
                        Console.WriteLine(" In total: "+IncompleteEvents.Count);

                        foreach(int e in IncompleteEvents)
                        {
                            Console.WriteLine(" at index: " + e);
                        }

                    }
                    else
                    {
                        Console.WriteLine(" event genome does not contain any event types");
                    }
                }
                else
                {
                    Console.WriteLine(" event genome too short to have any complete events");
                }
                //--------------------------------------------//
                //method genome
                Console.WriteLine("METHOD GENOME");
                //check for length
                if (!MethodGenomeLengthLessThanTwo)
                {
                    //check for any events
                    if (anyMethodFound)
                    {
                        Console.WriteLine(" Incomplete methods");
                        Console.WriteLine(" In total: " + IncompleteMethods.Count);

                        foreach (int e in IncompleteMethods)
                        {
                            Console.WriteLine(" at index: " + e);
                        }

                    }
                    else
                    {
                        Console.WriteLine(" method genome does not contain any event types");
                    }
                }
                else
                {
                    Console.WriteLine(" method genome too short to have any complete events");
                }
                //--------------------------------------------------------------------------------------//
            }
            //debug display function for checking all things have been put into actor list correctly
            public void DisplayActorTypeList()
            {
                Console.WriteLine("Parsed Actor Types:");
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
            //debug display function for checking all things have been put into actor list correctly
            public void DisplayActorTypePossplit3List()
            {
                Console.WriteLine("Which actor type, and where:");
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
            //no display methods
            //no display events
        }
    }
}
