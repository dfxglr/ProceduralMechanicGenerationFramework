using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMGF.PMGCore;
using PMGF.PMGGenerator;


namespace PMGF
{
    namespace PMGGameInstance
    {
        class PMGSingleGameInstance
        {
            //creating a map
            PMGMap _Map = new PMGMap();

            //core
            PMGGameCore MainCore = new PMGGameCore();


            public PMGGenomeParse ParsedSet = new PMGGenomeParse();
            
                
            //actors
            //created from parsed set
            List<PMGActor> CreatedActors = new List<PMGActor>();
            //removed incomeplete actors from created actors, and made lsit of spawnable types
            List<PMGActor> SpawnAbleActors = new List<PMGActor>();
            //actors spawned in map
            List<PMGActor> SpawnedActors = new List<PMGActor>();
            
            //actors new iD - shouldhave the lenght of the spawned, and contain the id for each actor there, starting from 1000
            List<int> ActorIDs = new List<int>();
            //methods
            List<PMGMethod> Methods = new List<PMGMethod>();
            List<List<PMGExecuteList>> MethodsExecutelists = new List<List<PMGExecuteList>>(); 
            //events
            List<PMGEvent> Events = new List<PMGEvent>();
            



            
            //instance build erros
            //actors in walls
            //undefined methods
            //undefined events


            public PMGSingleGameInstance()
            {
                
            }



            // builds the instance
            public void BuildInstance(PMGGenomeSet TobeParsed,bool debug)
            {
                //decode genome set
                ParsedSet.DecodeGenomeSet(TobeParsed);            
                

            
                //--------------------------------// testing stuff, this is only here because our collections are empty atm
                // Create functions for the methods
                PMGValueFunction testValF = new PMGValueFunction(0);//works for event too
                PMGUtilityFunction testUtilF = new PMGUtilityFunction(0);
                PMGChangeFunction testChgF = new PMGChangeFunction(0);
                //create functions for the events
                PMGConditionFunction testCondF = new PMGConditionFunction(0);
                ////--------------------------------//

                // build actor types from parsed set
                BuildActorTypes(debug);

                // removes incomplete actor types before creating spawn list
                RemoveIncompleteActortypes(debug);

                //create spawn list
                CreateSpawnTypeList(debug);

                //replace first actor with player actor, but keep first method for action event

                //createspawnlist, actorID's, and spawn actors into map
                SpawnActors(debug);

                
                
            }

            // creates actors types from parsed set, counts up errors from missing methods and events, as well as missing value, utility, condition, and change functions
            //dont stare too long into the abyss... 
            //Downsides: 
            //      the longer genomes, the longer time to finish becomes
            //      missing functions, methods and events errors, are counted multiple times(does not check for if their are already missing)
            //Upsides:
            //      removes the need for a potato index system, which would otherwise be needed, as loop index accounts
            //      does it all in one go, aka, when its done, its done, and no new actor types needs to be created
            public void BuildActorTypes(bool debug)
            {
                //create actors, methods and event and add them to the actors
                //mainlist of actor types
                for (int i = 0; i < ParsedSet.actorTypes.Count; i++)
                {

                    //add new actor to created actors
                    CreatedActors.Add(new PMGActor(MainCore));
                    if (debug)
                    {
                        Console.WriteLine("Created-Actor : " + i);
                    }

                    //gets overwritten by how ever many method and event pairs the actor types has 
                    PMGMethod CurrentMethod = new PMGMethod();
                    PMGEvent CurrentEvent;

                    bool CurrentMethodComeplete = false;
                    //sublist of actor stats
                    for (int j = 0; j < ParsedSet.actorTypes[i].Count; j++)
                    {
                        //is it even, time for method--------------------------------------------------------------------------//
                        if (j % 2 == 0)
                        {
                            if (debug)
                            {
                                Console.WriteLine("    method type: " + ParsedSet.actorTypes[i][j]);
                            }
                            //check the index list to see if 
                            for (int e = 0; e < ParsedSet.methodIndexList.Count; e++)
                            {


                                bool methodTypeFound = false;

                                //if the value is an index in the method index list
                                if (ParsedSet.actorTypes[i][j] == e)
                                {
                                    //type found is set to true
                                    methodTypeFound = true;
                                    if (debug)
                                    {
                                        Console.WriteLine("        type: " + ParsedSet.actorTypes[i][j] + " detected at genome index: " + ParsedSet.methodIndexList[e]);
                                        Console.WriteLine("            list of executelists created for method type: " + ParsedSet.actorTypes[i][j]);
                                    }
                                    //new list of executelist for current method
                                    List<PMGExecuteList> CurrentMethodExecuteLists = new List<PMGExecuteList>();

                                    //
                                    int Findex = 0;
                                    List<List<int>> functionIndex = new List<List<int>>();


                                    //goes through method genome to make execute lists
                                    for (int ex = ParsedSet.methodIndexList[e] + 1; ex < ParsedSet._genomeSet.methodGenome.Count; ex++)
                                    {
                                        //check for new method or new timestep or the end of the genome
                                        if (ParsedSet._genomeSet.methodGenome[ex][0] == (int)GenomeKeys.MethodKey || ParsedSet._genomeSet.methodGenome[ex][0] == (int)GenomeKeys.TimeStep || ex == ParsedSet._genomeSet.methodGenome.Count - 1)
                                        {
                                            //add last function in the genome only if end is hit
                                            if (ex == ParsedSet._genomeSet.methodGenome.Count - 1)
                                            {
                                                functionIndex.Add(new List<int>());
                                                functionIndex[Findex].Add(ParsedSet._genomeSet.methodGenome[ex][0]);
                                                functionIndex[Findex].Add(ParsedSet._genomeSet.methodGenome[ex][1]);
                                                Findex++;
                                            }
                                            //make sure there are functions to add to a new executelist
                                            if (functionIndex.Count > 0)
                                            {
                                                //create new execution list
                                                CurrentMethodExecuteLists.Add(new PMGExecuteList(CurrentMethod as object, FunctionOwnerType.METHOD, CreatedActors[i]));
                                                if (debug)
                                                {
                                                    Console.WriteLine("                added new executelist: " + (CurrentMethodExecuteLists.Count - 1));
                                                }
                                                //
                                                for (int f = 0; f < functionIndex.Count; f++)
                                                {
                                                    //add functions to the last execute list
                                                    //if value function
                                                    if (functionIndex[f][0] == (int)GenomeKeys.ValueFunc)
                                                    {
                                                        if (debug)
                                                        {
                                                            Console.WriteLine("                    value function detected");
                                                        }
                                                        //check if its in the collections                                                    
                                                        if (functionIndex[f][1] < MainCore.ValueFunctions.Collection.Count)
                                                        {
                                                            //add function to currentmethodexecutelits[timeindex]
                                                            CurrentMethodExecuteLists[CurrentMethodExecuteLists.Count - 1]._functions.Add(new PMGValueFunction(functionIndex[f][1]));
                                                            if (debug)
                                                            {
                                                                Console.WriteLine("                        value function type: " + functionIndex[f][1] + " added to executelist: " + (CurrentMethodExecuteLists.Count - 1));
                                                            }
                                                        }
                                                        else
                                                        {
                                                            //error: value function requested not found in the collections
                                                            if (debug)
                                                            {
                                                                Console.WriteLine("                        value function type: " + functionIndex[f][1] + " was not found");
                                                            }
                                                            //NOTE: adds every time the same number which is annoying but need fix later
                                                            ParsedSet.UnknownValueFunctionType.Add(functionIndex[f][1]);
                                                        }
                                                    }
                                                    // else if change function
                                                    else if (functionIndex[f][0] == (int)GenomeKeys.ChangeFunc)
                                                    {
                                                        if (debug)
                                                        {
                                                            Console.WriteLine("                    change function detected");
                                                        }
                                                        //check if its in the collections                                                    
                                                        if (functionIndex[f][1] < MainCore.ChangeFunctions.Collection.Count)
                                                        {
                                                            CurrentMethodExecuteLists[CurrentMethodExecuteLists.Count - 1]._functions.Add(new PMGChangeFunction(functionIndex[f][1]));
                                                            if (debug)
                                                            {
                                                                Console.WriteLine("                        change function type: " + functionIndex[f][1] + " added to executelist: " + (CurrentMethodExecuteLists.Count - 1));
                                                            }
                                                        }
                                                        else
                                                        {
                                                            //error: change function requested not found in the collections

                                                            if (debug)
                                                            {
                                                                Console.WriteLine("                        change function type: " + functionIndex[f][1] + " was not found");
                                                            }
                                                            //NOTE: adds every time the same number which is annoying but need fix later
                                                            ParsedSet.UnknownChangeFunctionType.Add(functionIndex[f][1]);
                                                        }
                                                    }
                                                    //else utility function
                                                    else
                                                    {
                                                        if (debug)
                                                        {
                                                            Console.WriteLine("                    utility function detected");
                                                        }
                                                        //check if its in the collections                                                    
                                                        if (functionIndex[f][1] < MainCore.UtilityFunctions.Collection.Count)
                                                        {
                                                            CurrentMethodExecuteLists[CurrentMethodExecuteLists.Count - 1]._functions.Add(new PMGUtilityFunction(functionIndex[f][1]));
                                                            if (debug)
                                                            {
                                                                Console.WriteLine("                        utility function type: " + functionIndex[f][1] + " added to executelist: " + (CurrentMethodExecuteLists.Count - 1));
                                                            }
                                                        }
                                                        else
                                                        {
                                                            //error: utility function requested not found in the collections
                                                            if (debug)
                                                            {
                                                                Console.WriteLine("                        utility function type: " + functionIndex[f][1] + " was not found");
                                                            }
                                                            //NOTE: adds every time the same number which is annoying but need fix later
                                                            ParsedSet.UnknownUtilityFunctionType.Add(functionIndex[f][1]);
                                                        }
                                                    }                                                    
                                                }
                                                //checks for execute lists with no functions in, if found, turns then to null, aka makes it a timestep
                                                if (CurrentMethodExecuteLists[CurrentMethodExecuteLists.Count - 1]._functions.Count > 0)
                                                {
                                                    //has functions and we do nothing
                                                    if (debug)
                                                    {
                                                        Console.WriteLine("                executelist: " + (CurrentMethodExecuteLists.Count - 1) + " comeplete");
                                                    }
                                                }
                                                else
                                                {
                                                    //has no functions and we turn it into a time step
                                                    //remove recently made list
                                                    CurrentMethodExecuteLists.RemoveAt(CurrentMethodExecuteLists.Count - 1);
                                                    //add timestep in its place
                                                    CurrentMethodExecuteLists.Add(null);
                                                    if (debug)
                                                    {
                                                        Console.WriteLine("                executelist: " + (CurrentMethodExecuteLists.Count - 1) + " incomeplete, was replaced by a timestep");
                                                    }
                                                }
                                            }
                                            //breaks out when new method is found
                                            if (ParsedSet._genomeSet.methodGenome[ex][0] == (int)GenomeKeys.MethodKey)
                                            {
                                                break;
                                            }
                                            //add empty executelists corrospoding to how many timesteps are needed
                                            if (ParsedSet._genomeSet.methodGenome[ex][0] == (int)GenomeKeys.TimeStep)
                                            {
                                                //adds null lists for how many timesteps needs to be added
                                                for (int t = 0; t < ParsedSet._genomeSet.methodGenome[ex][1]; t++)
                                                {
                                                    CurrentMethodExecuteLists.Add(null);
                                                    Console.WriteLine("                added timestep as executelist: " + (CurrentMethodExecuteLists.Count - 1));
                                                }
                                                //resets function index list
                                                Findex = 0;
                                                functionIndex = new List<List<int>>();
                                            }
                                        }
                                        //check for anything thats not a method key or timestep or the end of the genome
                                        else
                                        {
                                            //counts up the function index
                                            functionIndex.Add(new List<int>());
                                            functionIndex[Findex].Add(ParsedSet._genomeSet.methodGenome[ex][0]);
                                            functionIndex[Findex].Add(ParsedSet._genomeSet.methodGenome[ex][1]);
                                            Findex++;
                                        }
                                    }

                                    //
                                    if (debug)
                                    {
                                        Console.WriteLine("            list of executelists for method: " + ParsedSet.actorTypes[i][j] + " finished");
                                    }
                                    //add every item in list of executelists to _steps in current method
                                    for (int ms = 0; ms < CurrentMethodExecuteLists.Count; ms++)
                                    {
                                        CurrentMethod._steps.Add(CurrentMethodExecuteLists[ms]);
                                        if (debug)
                                        {
                                            Console.WriteLine("                executelist: " + ms + " added to method: " + ParsedSet.actorTypes[i][j]);
                                        }
                                    }
                                    //
                                    CurrentMethodComeplete = true;
                                    if (debug)
                                    {
                                        Console.WriteLine("    method type: " + ParsedSet.actorTypes[i][j] + " comeplete");
                                    }
                                    //break when a type is found to avoid some uneeded loops
                                    break;

                                }
                                else if (!methodTypeFound)
                                {
                                    // error: Method not found in method list
                                    if (debug)
                                    {
                                        Console.WriteLine("        type: " + ParsedSet.actorTypes[i][j] + " not detected at genome index: " + ParsedSet.methodIndexList[e]);
                                    }
                                    //NOTE: adds every time the same number(does not take into account if the number is already there)
                                    ParsedSet.MissingMethodTypes.Add(ParsedSet.actorTypes[i][j]);
                                }
                            }
                        }
                        //if its odd, time for event--------------------------------------------------------------------------//
                        if (j % 2 != 0)
                        {
                            if (debug)
                            {
                                Console.WriteLine("     event type: " + ParsedSet.actorTypes[i][j]);
                            }
                            //check that this events method is found as current method
                            if (CurrentMethodComeplete)
                            {
                                //resets
                                CurrentMethodComeplete = false;
                                bool CurrentEventFound = false;

                                //check through the event index list
                                for (int e = 0; e < ParsedSet.eventIndexList.Count; e++)
                                {
                                    //if the value is an index in the event index list
                                    if (ParsedSet.actorTypes[i][j] == e)
                                    {
                                        CurrentEventFound = true;
                                        if (debug)
                                        {
                                            Console.WriteLine("        type: " + ParsedSet.actorTypes[i][j] + " detected at genome index: " + ParsedSet.eventIndexList[e]);
                                        }
                                        //assigns current event with current actor and current method for the event 
                                        CurrentEvent = new PMGEvent(CurrentMethod, CreatedActors[i], (EventTriggerBehavior)ParsedSet._genomeSet.eventGenome[ParsedSet.eventIndexList[e]][1]);
                                        if (debug)
                                        {
                                            Console.WriteLine("            event has (method: " + ParsedSet.actorTypes[i][j - 1] + ", actor: " + i + ", behavior: " + (EventTriggerBehavior)ParsedSet._genomeSet.eventGenome[ParsedSet.eventIndexList[e]][1] + ")");
                                        }
                                        //create executelist for current event 
                                        PMGExecuteList CurrentEventExecutelist = new PMGExecuteList(CurrentEvent as object, FunctionOwnerType.EVENT, CreatedActors[i]);
                                        if (debug)
                                        {
                                            Console.WriteLine("                added new executelist");
                                        }
                                        //goes through event genome to add functions to current event executelist
                                        for (int ex = ParsedSet.eventIndexList[e] + 1; ex < ParsedSet._genomeSet.eventGenome.Count; ex++)
                                        {
                                            //break when hitting next event or end
                                            if (ParsedSet._genomeSet.eventGenome[ex][0] == (int)GenomeKeys.EventKey)
                                            {
                                                break;
                                            }
                                            //value
                                            if (ParsedSet._genomeSet.eventGenome[ex][0] == (int)GenomeKeys.ValueFunc)
                                            {
                                                if (debug)
                                                {
                                                    Console.WriteLine("                    value function detected");
                                                }
                                                //check for if the type is in collection
                                                if (ParsedSet._genomeSet.eventGenome[ex][1] < MainCore.ValueFunctions.Collection.Count)
                                                {
                                                    //adds value function to current event's conditions executelist
                                                    CurrentEventExecutelist._functions.Add(new PMGValueFunction(ParsedSet._genomeSet.eventGenome[ex][1]));
                                                    if (debug)
                                                    {
                                                        Console.WriteLine("                        value function type: " + ParsedSet._genomeSet.eventGenome[ex][1] + " added to executelist");
                                                    }
                                                }
                                                else
                                                {
                                                    //error: value function type not found in collections
                                                    ParsedSet.UnknownValueFunctionType.Add(ParsedSet._genomeSet.eventGenome[ex][1]);
                                                    if (debug)
                                                    {
                                                        Console.WriteLine("                        value function type: " + ParsedSet._genomeSet.eventGenome[ex][1] + " was not found");
                                                    }
                                                }

                                            }
                                            //condition
                                            if (ParsedSet._genomeSet.eventGenome[ex][0] == (int)GenomeKeys.CondFunc)
                                            {
                                                if (debug)
                                                {
                                                    Console.WriteLine("                    condition function detected");
                                                }
                                                //check for if the type is in collection
                                                if (ParsedSet._genomeSet.eventGenome[ex][1] < MainCore.ConditionFunctions.Collection.Count)
                                                {
                                                    //adds condition function to current event's conditions executelist
                                                    CurrentEventExecutelist._functions.Add(new PMGConditionFunction(ParsedSet._genomeSet.eventGenome[ex][1]));
                                                    if (debug)
                                                    {
                                                        Console.WriteLine("                        condition function type: " + ParsedSet._genomeSet.eventGenome[ex][1] + " added to executelist");
                                                    }
                                                }
                                                else
                                                {
                                                    //error: conditions function type not found in collections
                                                    ParsedSet.UnknownConditionFunctionType.Add(ParsedSet._genomeSet.eventGenome[ex][1]);
                                                    if (debug)
                                                    {
                                                        Console.WriteLine("                        condition function type: " + ParsedSet._genomeSet.eventGenome[ex][1] + " was not found");
                                                    }
                                                }
                                            }
                                            //utility
                                            if (ParsedSet._genomeSet.eventGenome[ex][0] == (int)GenomeKeys.UtilFunc)
                                            {
                                                if (debug)
                                                {
                                                    Console.WriteLine("                    utility function detected");
                                                }
                                                //check for if the type is in collection
                                                if (ParsedSet._genomeSet.eventGenome[ex][1] < MainCore.UtilityFunctions.Collection.Count)
                                                {
                                                    //adds utility function to current event's conditions executelist
                                                    CurrentEventExecutelist._functions.Add(new PMGUtilityFunction(ParsedSet._genomeSet.eventGenome[ex][1]));
                                                    if (debug)
                                                    {
                                                        Console.WriteLine("                        utility function type: " + ParsedSet._genomeSet.eventGenome[ex][1] + " added to executelist");
                                                    }
                                                }
                                                else
                                                {
                                                    //error: utility function type not found in collections
                                                    ParsedSet.UnknownUtilityFunctionType.Add(ParsedSet._genomeSet.eventGenome[ex][1]);
                                                    if (debug)
                                                    {
                                                        Console.WriteLine("                        utility function type: " + ParsedSet._genomeSet.eventGenome[ex][1] + " was not found");
                                                    }
                                                }
                                            }
                                        }
                                        //check if any fucntions was added to the executelist
                                        if (CurrentEventExecutelist._functions.Count > 0)
                                        {
                                            //current event's conditions executelist gets filled with the current executelist
                                            CurrentEvent._conditions = CurrentEventExecutelist;
                                            
                                            if (debug)
                                            {
                                                Console.WriteLine("                executelist complete, and was added to event conditions");                                                
                                            }
                                        }
                                        else
                                        {
                                            //error event executelsit has no functions and event found is set to false
                                            CurrentEventFound = false;
                                            if (debug)
                                            {
                                                Console.WriteLine("                executelist Incomplete");
                                            }
                                        }
                                        //check for complete even
                                        if (CurrentEventFound)
                                        {
                                            //add event to current actor
                                            CreatedActors[i].Events.Add(CurrentEvent);
                                            if (debug)
                                            {

                                                Console.WriteLine("     event type: " + ParsedSet.actorTypes[i][j] + " comeplete, and added to actor: " + i);
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("     event type: " + ParsedSet.actorTypes[i][j] + " Incomeplete");
                                        }

                                    }
                                    //no event found
                                    else
                                    {
                                        // error: event tye not found in even type index
                                        ParsedSet.MissingEventTypes.Add(ParsedSet.actorTypes[i][j]);
                                        if (debug)
                                        {
                                            Console.WriteLine("        type: " + ParsedSet.actorTypes[i][j] + " not detected at index: " + ParsedSet.eventIndexList[e]);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                //error: no method for this event 
                                if (debug)
                                {
                                    Console.WriteLine("        has NO method");
                                }
                            }


                        }///                        
                    }///
                    //creates list of bools for use, in remove incomplete actors
                    if (CreatedActors[i].Events.Count > 0)
                    {                        
                        if (debug)
                        {
                            Console.WriteLine("Created-Actor: " + i + " comeplete");
                        }
                    }
                    else
                    {                        
                        Console.WriteLine("Created-Actor: " + i + " Icomeplete");
                    }
                    
                }///
            }

            //removes incomplete actors
            public void RemoveIncompleteActortypes(bool debug)
            {
               
                //runs through newly created actor types
                for (int i =0; i< CreatedActors.Count;i++)
                {
                    //check for incomplete
                    if (CreatedActors[i].Events.Count==0)
                    {
                        CreatedActors[i] = null;
                        
                        if (debug)
                        {
                            Console.WriteLine("Created-Actor type: "+i+" was set to null, due to being incomplete");
                        }
                    }
                }               
            }

            //creates the actually spawnable types
            public void CreateSpawnTypeList(bool debug)
            {
                
                for (int i = 0;i < CreatedActors.Count;i++)
                {
                    if (CreatedActors[i] != null)
                    {
                        SpawnAbleActors.Add(CreatedActors[i]);
                    }
                    
                }
                if (debug)
                {
                    Console.WriteLine("Total new Spawnable Actor types: "+SpawnAbleActors.Count+" and the new types are:");
                    for(int i = 0; i < SpawnAbleActors.Count;i++ )
                    {
                        Console.WriteLine("     Actor type: " + i);
                    }
                }
                

            }

            //add player to spawnlist

            //spawns in the actors if they are found in the spawnlist, and if their position is not within a wall
            public void SpawnActors(bool debug)
            {
                //the first 0 in the actor type positions list will be the player, any 0 found after will not be spawned, to ofc only have 1 player
                bool playerActorFound = false;

                //if types there, 
                for (int i = 0; i < ParsedSet.actorTypePositions.Count; i++)
                {
                    //checks if the type is in created types
                    if (ParsedSet.actorTypePositions[i][0]<CreatedActors.Count)
                    {

                    }
                    else
                    {
                        //error: actor type not found in created actors
                    }
                }
                

                //checks through map, finds wall/space, inserts actorID if space
            }
        }
    }
}
