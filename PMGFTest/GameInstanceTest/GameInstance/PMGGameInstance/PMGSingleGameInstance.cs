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
            //new game core
            PMGGameCore MainCore = new PMGGameCore();

            //creating a map
            PMGMap _Map = new PMGMap();

            //game set
            public PMGGenomeParse GameSet = new PMGGenomeParse();

            //semi-static player genome
            public PMGGenomeSet OutputGenome = new PMGGenomeSet();
            public PMGGenomeParse PlayerSet = new PMGGenomeParse();
            List<PMGActor> Player = new List<PMGActor>();                      
                
            //actors
            //created from parsed set
            List<PMGActor> CreatedActors = new List<PMGActor>();
            //removed incomeplete actors from created actors, and made lsit of spawnable types
            List<PMGActor> SpawnAbleActors = new List<PMGActor>();
            //actors spawned in map
            List<PMGActor> SpawnedActors = new List<PMGActor>();
            
            

            //player actor
            //PMGActor PLayer;


            
            //instance build erros
            //actors in walls
            //undefined methods
            //undefined events


            public PMGSingleGameInstance()
            {
                Player.Add(new PMGActor(MainCore));               
            }



            // builds the instance
            public void BuildInstance(PMGGenomeSet InputSet,bool debug)
            {
                //decode genome set
                GameSet.DecodeGenomeSet(InputSet);            

                // build actor types from parsed set
                BuildActorTypes(ref CreatedActors,GameSet,debug);

                // removes incomplete actor types before creating spawn list
                RemoveIncompleteActortypes(ref CreatedActors,debug);

                //create spawn list
                CreateSpawnTypeList(ref CreatedActors,ref SpawnAbleActors,debug);

                //make player genome
                MakePlayerGenome(ref OutputGenome);
                //decome it
                PlayerSet.DecodeGenomeSet(OutputGenome);
                //build player
                if (debug)
                {
                    Console.WriteLine("Player actor: ");
                }
                BuildActorTypes(ref Player, PlayerSet, debug);


                //createspawnlist, actorID's
                SpawnActors(ref GameSet, ref SpawnAbleActors ,ref SpawnedActors,1000,debug);


                _Map.DisplayConsole();

                
                

            }

            // creates actors types from parsed set, counts up errors from missing methods and events, as well as missing value, utility, condition, and change functions
            //dont stare too long into the abyss... 
            //Downsides: 
            //      the longer genomes, the longer time to finish becomes
            //      missing functions, methods and events errors, are counted multiple times(does not check for if their are already missing)
            //Upsides:
            //      removes the need for a potato index system, which would otherwise be needed, as loop index accounts
            //      does it all in one go, aka, when its done, its done, and no new actor types needs to be created
            public void BuildActorTypes(ref List<PMGActor>OutputActorList, PMGGenomeParse InputSet, bool debug)
            {
                //create actors, methods and event and add them to the actors
                //mainlist of actor types
                for (int i = 0; i < InputSet.actorTypes.Count; i++)
                {

                    //add new actor to created actors
                    OutputActorList.Add(new PMGActor(MainCore));
                    if (debug)
                    {
                        Console.WriteLine("Actor type : " + i);
                    }

                    //gets overwritten by how ever many method and event pairs the actor types has 
                    PMGMethod CurrentMethod = new PMGMethod();
                    PMGEvent CurrentEvent;

                    bool CurrentMethodComeplete = false;
                    //sublist of actor stats
                    for (int j = 0; j < InputSet.actorTypes[i].Count; j++)
                    {
                        //is it even, time for method--------------------------------------------------------------------------//
                        if (j % 2 == 0)
                        {
                            if (debug)
                            {
                                Console.WriteLine("    method type: " + InputSet.actorTypes[i][j]);
                            }
                            //check the index list to see if 
                            for (int e = 0; e < InputSet.methodIndexList.Count; e++)
                            {


                                bool methodTypeFound = false;

                                //if the value is an index in the method index list
                                if (InputSet.actorTypes[i][j] == e)
                                {
                                    //type found is set to true
                                    methodTypeFound = true;
                                    if (debug)
                                    {
                                        Console.WriteLine("        type: " + InputSet.actorTypes[i][j] + " detected at genome index: " + InputSet.methodIndexList[e]);
                                        Console.WriteLine("            list of executelists created for method type: " + InputSet.actorTypes[i][j]);
                                    }
                                    //new list of executelist for current method
                                    List<PMGExecuteList> CurrentMethodExecuteLists = new List<PMGExecuteList>();

                                    //
                                    int Findex = 0;
                                    List<List<int>> functionIndex = new List<List<int>>();


                                    //goes through method genome to make execute lists
                                    for (int ex = InputSet.methodIndexList[e] + 1; ex < InputSet._genomeSet.methodGenome.Count; ex++)
                                    {
                                        //check for new method or new timestep or the end of the genome
                                        if (InputSet._genomeSet.methodGenome[ex][0] == (int)GenomeKeys.MethodKey || InputSet._genomeSet.methodGenome[ex][0] == (int)GenomeKeys.TimeStep || ex == InputSet._genomeSet.methodGenome.Count - 1)
                                        {
                                            //add last function in the genome only if end is hit
                                            if (ex == InputSet._genomeSet.methodGenome.Count - 1)
                                            {
                                                functionIndex.Add(new List<int>());
                                                functionIndex[Findex].Add(InputSet._genomeSet.methodGenome[ex][0]);
                                                functionIndex[Findex].Add(InputSet._genomeSet.methodGenome[ex][1]);
                                                Findex++;
                                            }
                                            //make sure there are functions to add to a new executelist
                                            if (functionIndex.Count > 0)
                                            {
                                                //create new execution list
                                                CurrentMethodExecuteLists.Add(new PMGExecuteList(CurrentMethod as object, FunctionOwnerType.METHOD, OutputActorList[i]));
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
                                                            //add function to currentmethodexecutelits[timeindex] if 
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

                                                            //
                                                            bool functionFound = false;                                                            
                                                            if (InputSet.UnknownValueFunctionType.Count == 0)
                                                            {
                                                                InputSet.UnknownValueFunctionType.Add(functionIndex[f][1]);                                                               
                                                            }
                                                            else
                                                            {                                                                
                                                                //checks through the list to ensure we dont add the same type if its already unknown
                                                                for (int q = 0; q < InputSet.UnknownValueFunctionType.Count; q++)
                                                                {
                                                                    //if its not found in the list add it
                                                                    if (functionIndex[f][1] == InputSet.UnknownValueFunctionType[q])
                                                                    {
                                                                        //set to true
                                                                        functionFound = true;
                                                                    }
                                                                }
                                                                // found in list = false then add
                                                                if (!functionFound)
                                                                {
                                                                    InputSet.UnknownValueFunctionType.Add(functionIndex[f][1]);                                                                    
                                                                }                             
                                                            }                                                                                                                                                                                                                                      
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
                                                            //
                                                            bool functionFound = false;
                                                            if (InputSet.UnknownChangeFunctionType.Count == 0)
                                                            {
                                                                InputSet.UnknownChangeFunctionType.Add(functionIndex[f][1]);
                                                            }
                                                            else
                                                            {
                                                                //checks through the list to ensure we dont add the same type if its already unknown
                                                                for (int q = 0; q < InputSet.UnknownChangeFunctionType.Count; q++)
                                                                {
                                                                    //if its not found in the list add it
                                                                    if (functionIndex[f][1] == InputSet.UnknownChangeFunctionType[q])
                                                                    {
                                                                        //set to true
                                                                        functionFound = true;
                                                                    }
                                                                }
                                                                // found in list = false then add
                                                                if (!functionFound)
                                                                {
                                                                    InputSet.UnknownChangeFunctionType.Add(functionIndex[f][1]);
                                                                }
                                                            }
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
                                                            //
                                                            bool functionFound = false;
                                                            if (InputSet.UnknownUtilityFunctionType.Count == 0)
                                                            {
                                                                InputSet.UnknownUtilityFunctionType.Add(functionIndex[f][1]);
                                                            }
                                                            else
                                                            {
                                                                //checks through the list to ensure we dont add the same type if its already unknown
                                                                for (int q = 0; q < InputSet.UnknownUtilityFunctionType.Count; q++)
                                                                {
                                                                    //if its not found in the list add it
                                                                    if (functionIndex[f][1] == InputSet.UnknownUtilityFunctionType[q])
                                                                    {
                                                                        //set to true
                                                                        functionFound = true;
                                                                    }
                                                                }
                                                                // found in list = false then add
                                                                if (!functionFound)
                                                                {
                                                                    InputSet.UnknownUtilityFunctionType.Add(functionIndex[f][1]);
                                                                }
                                                            }
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
                                            if (InputSet._genomeSet.methodGenome[ex][0] == (int)GenomeKeys.MethodKey)
                                            {
                                                break;
                                            }
                                            //add empty executelists corrospoding to how many timesteps are needed
                                            if (InputSet._genomeSet.methodGenome[ex][0] == (int)GenomeKeys.TimeStep)
                                            {
                                                //adds null lists for how many timesteps needs to be added
                                                for (int t = 0; t < InputSet._genomeSet.methodGenome[ex][1]; t++)
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
                                            functionIndex[Findex].Add(InputSet._genomeSet.methodGenome[ex][0]);
                                            functionIndex[Findex].Add(InputSet._genomeSet.methodGenome[ex][1]);
                                            Findex++;
                                        }
                                    }

                                    //
                                    if (debug)
                                    {
                                        Console.WriteLine("            list of executelists for method: " + InputSet.actorTypes[i][j] + " finished");
                                    }
                                    //add every item in list of executelists to _steps in current method
                                    for (int ms = 0; ms < CurrentMethodExecuteLists.Count; ms++)
                                    {
                                        CurrentMethod._steps.Add(CurrentMethodExecuteLists[ms]);
                                        if (debug)
                                        {
                                            Console.WriteLine("                executelist: " + ms + " added to method: " + InputSet.actorTypes[i][j]);
                                        }
                                    }
                                    //
                                    CurrentMethodComeplete = true;
                                    if (debug)
                                    {
                                        Console.WriteLine("    method type: " + InputSet.actorTypes[i][j] + " comeplete");
                                    }
                                    //break when a type is found to avoid some uneeded loops
                                    break;

                                }
                                else if (!methodTypeFound)
                                {
                                    // error: Method not found in method list
                                    if (debug)
                                    {
                                        Console.WriteLine("        type: " + InputSet.actorTypes[i][j] + " not detected at genome index: " + InputSet.methodIndexList[e]);
                                    }
                                    //NOTE: adds every time the same number(does not take into account if the number is already there)
                                    InputSet.MissingMethodTypes.Add(InputSet.actorTypes[i][j]);
                                }
                            }
                        }
                        //if its odd, time for event--------------------------------------------------------------------------//
                        if (j % 2 != 0)
                        {
                            if (debug)
                            {
                                Console.WriteLine("     event type: " + InputSet.actorTypes[i][j]);
                            }
                            //check that this events method is found as current method
                            if (CurrentMethodComeplete)
                            {
                                //resets
                                CurrentMethodComeplete = false;
                                bool CurrentEventFound = false;
                                
                                //check through the event index list
                                for (int e = 0; e < InputSet.eventIndexList.Count; e++)
                                {
                                    //if the value is an index in the event index list
                                    if (InputSet.actorTypes[i][j] == e)
                                    {
                                        CurrentEventFound = true;
                                        if (debug)
                                        {
                                            Console.WriteLine("        type: " + InputSet.actorTypes[i][j] + " detected at genome index: " + InputSet.eventIndexList[e]);
                                        }
                                        //assigns current event with current actor and current method for the event 
                                        CurrentEvent = new PMGEvent(CurrentMethod, OutputActorList[i], (EventTriggerBehavior)InputSet._genomeSet.eventGenome[InputSet.eventIndexList[e]][1]);
                                        if (debug)
                                        {
                                            Console.WriteLine("            event has (method: " + InputSet.actorTypes[i][j - 1] + ", actor: " + i + ", behavior: " + (EventTriggerBehavior)InputSet._genomeSet.eventGenome[InputSet.eventIndexList[e]][1] + ")");
                                        }
                                        //create executelist for current event 
                                        PMGExecuteList CurrentEventExecutelist = new PMGExecuteList(CurrentEvent as object, FunctionOwnerType.EVENT, OutputActorList[i]);
                                        if (debug)
                                        {
                                            Console.WriteLine("                added new executelist");
                                        }
                                        //goes through event genome to add functions to current event executelist
                                        for (int ex = InputSet.eventIndexList[e] + 1; ex < InputSet._genomeSet.eventGenome.Count; ex++)
                                        {
                                            //break was here;
                                            //break when hitting next event or end
                                            if (InputSet._genomeSet.eventGenome[ex][0] == (int)GenomeKeys.EventKey)
                                            {
                                                break;
                                            }
                                            //value
                                            if (InputSet._genomeSet.eventGenome[ex][0] == (int)GenomeKeys.ValueFunc)
                                            {
                                                if (debug)
                                                {
                                                    Console.WriteLine("                    value function detected");
                                                }
                                                
                                                //check for if the type is in collection
                                                if (InputSet._genomeSet.eventGenome[ex][1] < MainCore.ValueFunctions.Collection.Count)
                                                {
                                                    //adds value function to current event's conditions executelist
                                                    CurrentEventExecutelist._functions.Add(new PMGValueFunction(InputSet._genomeSet.eventGenome[ex][1]));
                                                    if (debug)
                                                    {
                                                        Console.WriteLine("                        value function type: " + InputSet._genomeSet.eventGenome[ex][1] + " added to executelist");
                                                    }
                                                }
                                                else
                                                {
                                                    //error: value function type not found in collections
                                                    
                                                    if (debug)
                                                    {
                                                        Console.WriteLine("                        value function type: " + InputSet._genomeSet.eventGenome[ex][1] + " was not found");
                                                    }
                                                    //
                                                    bool functionFound = false;
                                                    if (InputSet.UnknownValueFunctionType.Count == 0)
                                                    {
                                                        InputSet.UnknownValueFunctionType.Add(InputSet._genomeSet.eventGenome[ex][1]);
                                                    }
                                                    else
                                                    {
                                                        //checks through the list to ensure we dont add the same type if its already unknown
                                                        for (int q = 0; q < InputSet.UnknownValueFunctionType.Count; q++)
                                                        {
                                                            //if its not found in the list add it
                                                            if (InputSet._genomeSet.eventGenome[ex][1] == InputSet.UnknownValueFunctionType[q])
                                                            {
                                                                //set to true
                                                                functionFound = true;
                                                            }
                                                        }
                                                        // found in list = false then add
                                                        if (!functionFound)
                                                        {
                                                            InputSet.UnknownValueFunctionType.Add(InputSet._genomeSet.eventGenome[ex][1]);
                                                        }
                                                    }
                                                }
                                            }
                                            //condition
                                            if (InputSet._genomeSet.eventGenome[ex][0] == (int)GenomeKeys.CondFunc)
                                            {
                                                if (debug)
                                                {
                                                    Console.WriteLine("                    condition function detected");
                                                }
                                                //check for if the type is in collection
                                                if (InputSet._genomeSet.eventGenome[ex][1] < MainCore.ConditionFunctions.Collection.Count)
                                                {
                                                    //adds condition function to current event's conditions executelist
                                                    CurrentEventExecutelist._functions.Add(new PMGConditionFunction(InputSet._genomeSet.eventGenome[ex][1]));
                                                    if (debug)
                                                    {
                                                        Console.WriteLine("                        condition function type: " + InputSet._genomeSet.eventGenome[ex][1] + " added to executelist");
                                                    }
                                                }
                                                else
                                                {
                                                    //error: conditions function type not found in collections
                                                    
                                                    if (debug)
                                                    {
                                                        Console.WriteLine("                        condition function type: " + InputSet._genomeSet.eventGenome[ex][1] + " was not found");
                                                    }
                                                    //
                                                    bool functionFound = false;
                                                    if (InputSet.UnknownConditionFunctionType.Count == 0)
                                                    {
                                                        InputSet.UnknownConditionFunctionType.Add(InputSet._genomeSet.eventGenome[ex][1]);
                                                    }
                                                    else
                                                    {
                                                        //checks through the list to ensure we dont add the same type if its already unknown
                                                        for (int q = 0; q < InputSet.UnknownConditionFunctionType.Count; q++)
                                                        {
                                                            //if its not found in the list add it
                                                            if (InputSet._genomeSet.eventGenome[ex][1] == InputSet.UnknownConditionFunctionType[q])
                                                            {
                                                                //set to true
                                                                functionFound = true;
                                                            }
                                                        }
                                                        // found in list = false then add
                                                        if (!functionFound)
                                                        {
                                                            InputSet.UnknownConditionFunctionType.Add(InputSet._genomeSet.eventGenome[ex][1]);
                                                        }
                                                    }
                                                }
                                            }
                                            //utility
                                            if (InputSet._genomeSet.eventGenome[ex][0] == (int)GenomeKeys.UtilFunc)
                                            {
                                                if (debug)
                                                {
                                                    Console.WriteLine("                    utility function detected");
                                                }
                                                //check for if the type is in collection
                                                if (InputSet._genomeSet.eventGenome[ex][1] < MainCore.UtilityFunctions.Collection.Count)
                                                {
                                                    //adds utility function to current event's conditions executelist
                                                    CurrentEventExecutelist._functions.Add(new PMGUtilityFunction(InputSet._genomeSet.eventGenome[ex][1]));
                                                    if (debug)
                                                    {
                                                        Console.WriteLine("                        utility function type: " + InputSet._genomeSet.eventGenome[ex][1] + " added to executelist");
                                                    }
                                                }
                                                else
                                                {
                                                    //error: utility function type not found in collections
                                                    
                                                    if (debug)
                                                    {
                                                        Console.WriteLine("                        utility function type: " + InputSet._genomeSet.eventGenome[ex][1] + " was not found");
                                                    }
                                                    //
                                                    bool functionFound = false;
                                                    if (InputSet.UnknownUtilityFunctionType.Count == 0)
                                                    {
                                                        InputSet.UnknownUtilityFunctionType.Add(InputSet._genomeSet.eventGenome[ex][1]);
                                                    }
                                                    else
                                                    {
                                                        //checks through the list to ensure we dont add the same type if its already unknown
                                                        for (int q = 0; q < InputSet.UnknownUtilityFunctionType.Count; q++)
                                                        {
                                                            //if its not found in the list add it
                                                            if (InputSet._genomeSet.eventGenome[ex][1] == InputSet.UnknownUtilityFunctionType[q])
                                                            {
                                                                //set to true
                                                                functionFound = true;
                                                            }
                                                        }
                                                        // found in list = false then add
                                                        if (!functionFound)
                                                        {
                                                            InputSet.UnknownUtilityFunctionType.Add(InputSet._genomeSet.eventGenome[ex][1]);
                                                        }
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
                                            OutputActorList[i].Events.Add(CurrentEvent);
                                            if (debug)
                                            {

                                                Console.WriteLine("     event type: " + InputSet.actorTypes[i][j] + " comeplete, and added to actor: " + i);
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("     event type: " + InputSet.actorTypes[i][j] + " Incomeplete");
                                        }
                                        //break when a type is found to avoid some uneeded loops
                                        break;
                                    }
                                    //no event found
                                    else
                                    {
                                        // error: event tye not found in even type index
                                        InputSet.MissingEventTypes.Add(InputSet.actorTypes[i][j]);
                                        if (debug)
                                        {
                                            Console.WriteLine("        type: " + InputSet.actorTypes[i][j] + " not detected at index: " + InputSet.eventIndexList[e]);
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
                    if (OutputActorList[i].Events.Count > 0)
                    {                        
                        if (debug)
                        {
                            Console.WriteLine("Actor type: " + i + " comeplete");
                        }
                    }
                    else
                    {                        
                        Console.WriteLine("Actor type: " + i + " Incomeplete");
                    }
                    
                }///
            }

            //removes incomplete actors
            public void RemoveIncompleteActortypes(ref List<PMGActor> OutputActorList,bool debug)
            {
               
                //runs through newly created actor types
                for (int i =0; i< OutputActorList.Count;i++)
                {
                    //check for incomplete
                    if (OutputActorList[i].Events.Count==0)
                    {
                        OutputActorList[i] = null;
                        
                        if (debug)
                        {
                            Console.WriteLine("Created-Actor type: "+i+" was set to null, due to being incomplete");
                        }
                    }
                }               
            }

            //creates the actually spawnable types
            public void CreateSpawnTypeList(ref List<PMGActor>InputActorList, ref List<PMGActor> OutputActorList,bool debug)
            {                              
                for (int i = 0;i < InputActorList.Count;i++)
                {
                    if (InputActorList[i] != null)
                    {
                        OutputActorList.Add(InputActorList[i]);
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

            //spawns in the actors if they are found in the spawnlist, and if their position is not within a wall
            public void SpawnActors(ref PMGGenomeParse InputSet, ref List<PMGActor> InputActorList, ref List<PMGActor> OutputActorList,int IDBase,bool debug)
            {

                //the first 0 in the actor type positions list will be the player, any 0 found after will not be spawned, to ofc only have 1 player
                bool playerActorWasFound = false;

                if (debug)
                {
                    Console.WriteLine("Spawning Actors:");
                }

                //create spawn list 
                for (int i = 0; i < InputSet.actorTypePositions.Count; i++)
                {
                    //checks if the type is in spawnable types
                    if (InputSet.actorTypePositions[i][0] < InputActorList.Count)
                    {
                        //checks for posisiton in map
                        if (_Map.Map[InputSet.actorTypePositions[i][1], InputSet.actorTypePositions[i][2]] ==0)
                        {
                            //checks for first 0 and set it to the player, any 0 found there after is well fuck em
                            if (InputSet.actorTypePositions[i][0] == 0)
                            {
                                //if player not found spawn the other actor
                                if (!playerActorWasFound)
                                {
                                    //give player 0 old guys method 0
                                    Player[0].Events[0]._method = OutputActorList[0].Events[0]._method;

                                    //spawns player
                                    OutputActorList.Add(Player[0]);
                                                                                                           
                                    //places its ID in the map
                                    _Map.Map[InputSet.actorTypePositions[i][1], InputSet.actorTypePositions[i][2]] = (OutputActorList.Count - 1 + IDBase);
                                    

                                    if (debug)
                                    {
                                        Console.WriteLine("     player actor  spawned at position[ "+ InputSet.actorTypePositions[i][1]+" , "+ InputSet.actorTypePositions[i][2]+" ] with ID: "+(SpawnedActors.Count-1+IDBase));
                                    }
                                }
                                else
                                {
                                    //error more player actors found
                                    InputSet.DuplicatePlayersInATPList.Add(i);
                                    if (debug)
                                    {
                                        Console.WriteLine("     actor type: " + InputSet.actorTypePositions[i][0] + " Not spawned, due to already existing player actor");
                                    }
                                }
                                playerActorWasFound = true;

                            }
                            else
                            {
                                //spawns the actor
                                OutputActorList.Add(InputActorList[InputSet.actorTypePositions[i][0]]);
                                //places its ID in the map
                                _Map.Map[InputSet.actorTypePositions[i][1], InputSet.actorTypePositions[i][2]] = (OutputActorList.Count - 1 + IDBase);
                                if (debug)
                                {
                                    Console.WriteLine("     actor type: " + InputSet.actorTypePositions[i][0] + " spawned at position[ "+ InputSet.actorTypePositions[i][1]+" , "+ InputSet.actorTypePositions[i][2]+" ] with ID: "+ (OutputActorList.Count - 1 + IDBase));
                                }
                            }
                        }
                        else
                        {
                            //error: faulty position

                            //however special case for 0
                            if (InputSet.actorTypePositions[i][0] == 0) {
                                InputSet.DuplicatePlayersInATPList.Add(i);
                                if (debug)
                                {
                                    Console.WriteLine("     actor type: " + InputSet.actorTypePositions[i][0] + " Not spawned, due to position[ " + InputSet.actorTypePositions[i][1] + " , " + InputSet.actorTypePositions[i][2] + " ] not being free, counts existing player actor error");
                                }
                            }
                            else
                            {
                                //add position error
                                InputSet.TypePlusFaultyStartPositions.Add(new List<int>());
                                InputSet.TypePlusFaultyStartPositions[InputSet.TypePlusFaultyStartPositions.Count - 1].Add(InputSet.actorTypePositions[i][0]);
                                InputSet.TypePlusFaultyStartPositions[InputSet.TypePlusFaultyStartPositions.Count - 1].Add(InputSet.actorTypePositions[i][1]);
                                InputSet.TypePlusFaultyStartPositions[InputSet.TypePlusFaultyStartPositions.Count - 1].Add(InputSet.actorTypePositions[i][2]);
                                if (debug)
                                {
                                    Console.WriteLine("     actor type: " + InputSet.actorTypePositions[i][0] + " Not spawned, due to position[ " + InputSet.actorTypePositions[i][1] + " , " + InputSet.actorTypePositions[i][2] + " ] not being free");
                                }
                            }   
                        }
                    }
                    else
                    {
                        // error: requested actor type not found in spawnable actors actors

                        if (debug)
                        {
                            Console.WriteLine("     actor type: " + InputSet.actorTypePositions[i][0] + " Not spawned, due to unfound type");
                        }
                        //
                        bool actorFound = false;
                        if (InputSet.ActorTypeNotFoundInSpawnedActorList.Count == 0)
                        {
                            InputSet.ActorTypeNotFoundInSpawnedActorList.Add(InputSet.actorTypePositions[i][0]);
                        }
                        else
                        {
                            //checks through the list to ensure we dont add the same type if its already unknown
                            for (int q = 0; q < InputSet.ActorTypeNotFoundInSpawnedActorList.Count; q++)
                            {
                                //if its not found in the list add it
                                if (InputSet.actorTypePositions[i][0] == InputSet.ActorTypeNotFoundInSpawnedActorList[q])
                                {
                                    //set to true
                                    actorFound = true;
                                }
                            }
                            // found in list = false then add
                            if (!actorFound)
                            {
                                InputSet.ActorTypeNotFoundInSpawnedActorList.Add(InputSet.actorTypePositions[i][0]);
                            }
                        }

                    }                    
                }                
                //should player still not be found, first actor in the spawnedactor gets replaced with the player 
                if (!playerActorWasFound)
                {
                    //count up error, as we know the genome does not contain one or more players
                    InputSet.NoActorZeroFound = true;

                    //give player 0 old guys method 0
                    Player[0].Events[0]._method = OutputActorList[0].Events[0]._method;
                    
                    //replaces 0 with the player
                    OutputActorList[0] = Player[0];
                    if (debug)
                    {
                        Console.WriteLine("     player actor replaced SpawnedActor[0], and had method: "+ Player[0].Events[0]._method.GetType());
                    }
                }
                if (debug)
                {
                    Console.WriteLine("Spawning comeplete, and following actors(by ID) are in the map");
                    for(int e = 0; e< SpawnedActors.Count;e++)
                    {
                        Console.WriteLine("     actor: "+(e+IDBase));
                    }
                }
            }

            //adde stuff to player genome
            public void MakePlayerGenome(ref PMGGenomeSet OutputGenome)
            {
                //actor genome                
                OutputGenome.actorGenome.Add((int)GenomeKeys.ActorKey); //Le Player
                //first set of method and event
                OutputGenome.actorGenome.Add(0); // default action is same as UP
                OutputGenome.actorGenome.Add(0); // event for action button pressed
                //second set of method and event
                OutputGenome.actorGenome.Add(0); // method for UP
                OutputGenome.actorGenome.Add(1); // event for UP button pressed
                //third set of method and event
                OutputGenome.actorGenome.Add(1); // method for LEFT
                OutputGenome.actorGenome.Add(2); // event for LEFT button pressed
                //fourth set of method and event
                OutputGenome.actorGenome.Add(2); // method for DOWN
                OutputGenome.actorGenome.Add(3); // event for DOWN button pressed
                //fifth set of method and event
                OutputGenome.actorGenome.Add(3); // method for RIGHT
                OutputGenome.actorGenome.Add(4); // event for RIGHT button pressed

                //method genome - could need some value or utility functions, future decides
                OutputGenome.methodGenome.Add(new List<int>()); // method 0
                OutputGenome.methodGenome[0].Add((int)GenomeKeys.MethodKey);
                OutputGenome.methodGenome[0].Add(0); // is this even needed
                OutputGenome.methodGenome.Add(new List<int>()); // change function for method 0
                OutputGenome.methodGenome[1].Add((int)GenomeKeys.ChangeFunc);
                OutputGenome.methodGenome[1].Add(0); // function that moves actor Up
                OutputGenome.methodGenome.Add(new List<int>()); // method 1
                OutputGenome.methodGenome[2].Add((int)GenomeKeys.MethodKey);
                OutputGenome.methodGenome[2].Add(0); // is this even needed
                OutputGenome.methodGenome.Add(new List<int>()); // change function for method 1
                OutputGenome.methodGenome[3].Add((int)GenomeKeys.ChangeFunc);
                OutputGenome.methodGenome[3].Add(0); // function that moves actor Left
                OutputGenome.methodGenome.Add(new List<int>()); // method 2
                OutputGenome.methodGenome[4].Add((int)GenomeKeys.MethodKey);
                OutputGenome.methodGenome[4].Add(0); // is this even needed
                OutputGenome.methodGenome.Add(new List<int>()); // change function for method 2
                OutputGenome.methodGenome[5].Add((int)GenomeKeys.ChangeFunc);
                OutputGenome.methodGenome[5].Add(0); // function that moves actor Down
                OutputGenome.methodGenome.Add(new List<int>()); // method 3
                OutputGenome.methodGenome[6].Add((int)GenomeKeys.MethodKey);
                OutputGenome.methodGenome[6].Add(0); // is this even needed
                OutputGenome.methodGenome.Add(new List<int>()); // change function for method 3
                OutputGenome.methodGenome[7].Add((int)GenomeKeys.ChangeFunc);
                OutputGenome.methodGenome[7].Add(0); // function that moves actor Right

                //event genome - could need some value or utility functions, future decides
                OutputGenome.eventGenome.Add(new List<int>()); // event 0
                OutputGenome.eventGenome[0].Add((int)GenomeKeys.EventKey);
                OutputGenome.eventGenome[0].Add(4); //behavior 4 is ONE_TIME... might need to be always not sure
                OutputGenome.eventGenome.Add(new List<int>()); //condition for event 0
                OutputGenome.eventGenome[1].Add((int)GenomeKeys.CondFunc);
                OutputGenome.eventGenome[1].Add(0); //condition X is button press w or arrowUp
                OutputGenome.eventGenome.Add(new List<int>()); // event 1
                OutputGenome.eventGenome[2].Add((int)GenomeKeys.EventKey);
                OutputGenome.eventGenome[2].Add(4); //behavior 4 is ONE_TIME... might need to be always not sure
                OutputGenome.eventGenome.Add(new List<int>()); //condition for event 1
                OutputGenome.eventGenome[3].Add((int)GenomeKeys.CondFunc);
                OutputGenome.eventGenome[3].Add(0); //condition X is button press a or arrowLeft
                OutputGenome.eventGenome.Add(new List<int>()); // event 2
                OutputGenome.eventGenome[4].Add((int)GenomeKeys.EventKey);
                OutputGenome.eventGenome[4].Add(4); //behavior 4 is ONE_TIME... might need to be always not sure
                OutputGenome.eventGenome.Add(new List<int>()); //condition for event 2
                OutputGenome.eventGenome[5].Add((int)GenomeKeys.CondFunc);
                OutputGenome.eventGenome[5].Add(0); //condition X is button press s or arrowDown
                OutputGenome.eventGenome.Add(new List<int>()); // event 3
                OutputGenome.eventGenome[6].Add((int)GenomeKeys.EventKey);
                OutputGenome.eventGenome[6].Add(4); //behavior 4 is ONE_TIME... might need to be always not sure
                OutputGenome.eventGenome.Add(new List<int>()); //condition for event 3
                OutputGenome.eventGenome[7].Add((int)GenomeKeys.CondFunc);
                OutputGenome.eventGenome[7].Add(0); //condition X is button press d or arrowRight
                OutputGenome.eventGenome.Add(new List<int>()); // event 4
                OutputGenome.eventGenome[8].Add((int)GenomeKeys.EventKey);
                OutputGenome.eventGenome[8].Add(4); //behavior 4 is ONE_TIME... might need to be always not sure
                OutputGenome.eventGenome.Add(new List<int>()); //condition for event 4
                OutputGenome.eventGenome[9].Add((int)GenomeKeys.CondFunc);
                OutputGenome.eventGenome[9].Add(0); //condition X is button press d or arrowUp

                //dosent need a positions genome as we get that from the 



            }
            //
        }
    }
}
