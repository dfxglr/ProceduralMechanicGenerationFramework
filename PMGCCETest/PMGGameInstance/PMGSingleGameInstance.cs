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

            
            public PMGGenomeParse ParsedSet = new PMGGenomeParse();
            
                
            //actors
            List<PMGActor> CreatedActors = new List<PMGActor>();
            List<PMGActor> SpawnedActors = new List<PMGActor>();
            //methods
            List<PMGMethod> Methods = new List<PMGMethod>();
            List<List<PMGExecuteList>> MethodsExecutelists = new List<List<PMGExecuteList>>(); 
            //events
            List<PMGEvent> Events = new List<PMGEvent>();
            //actors new iD - shouldhave the lenght of the actor type positions list, and contain the id for each actor there, starting from 1000
            List<int> ActorIDs = new List<int>();



            
            //instance build erros
            //actors in walls
            //undefined methods
            //undefined events


            public PMGSingleGameInstance()
            {
                
            }



            // builds the instance
            public void BuildInstance(PMGGenomeSet TobeParsed)
            {
                //decode genome set
                ParsedSet.DecodeGenomeSet(TobeParsed);

                //make core
                PMGGameCore MainCore = new PMGGameCore();

            
                //--------------------------------// testing stuff, this is only here because our collections are empty atm
                // Create functions for the methods
                PMGValueFunction testValF = new PMGValueFunction(0);//works for event too
                PMGUtilityFunction testUtilF = new PMGUtilityFunction(0);
                PMGChangeFunction testChgF = new PMGChangeFunction(0);
                //create functions for the events
                PMGConditionFunction testCondF = new PMGConditionFunction(0);
                ////--------------------------------//

                //-----//
                bool debug = false;
                //-----//

                //create actors, methods and event and add them to the actors
                //mainlist of actor types
                for (int i = 0; i< ParsedSet.actorTypes.Count;i++)
                {
                    
                    //add new actor to created actors
                    CreatedActors.Add(new PMGActor(MainCore));
                    if (debug)
                    {
                        Console.WriteLine("actor : " + i);
                    }
                    //sublist of actor stats
                    for(int j = 0; j< ParsedSet.actorTypes[i].Count; j++)
                    {
                        
                        
                        PMGMethod CurrentMethod = new PMGMethod();
                        PMGEvent CurrentEvent;

                        //is it even, time for method
                        if (j%2 == 0)
                        {
                            if (debug) {
                                Console.WriteLine("    method type: " + ParsedSet.actorTypes[i][j]);
                            }
                            //check the index list to see if 
                            for (int e = 0; e < ParsedSet.methodIndexList.Count;e++) {

                                bool TypeFound = false;
                                //if the value is an index in the method index list
                                if (ParsedSet.actorTypes[i][j] == ParsedSet.methodIndexList[e])
                                {
                                    //type found is set to true
                                    TypeFound = true;
                                    if (debug) {
                                        Console.WriteLine("        type: " + ParsedSet.actorTypes[i][j] + " detected at index: " + e);
                                        Console.WriteLine("            list of executelists created for method type: " + ParsedSet.actorTypes[i][j]);
                                    }
                                    //new list of executelist for current method
                                    List<PMGExecuteList> CurrentMethodExecuteLists = new List<PMGExecuteList>(); // = new PMGExecuteList(CurrentMethod as object, FunctionOwnerType.METHOD, CreatedActors[i]);

                                    //
                                    int Findex = 0;
                                    List<List<int>> functionIndex = new List<List<int>>();
                                    int timeIndex = 0;
                                    

                                    //goes through method genome to make execute lists
                                    for (int ex = ParsedSet.methodIndexList[e]+1; ex<ParsedSet._genomeSet.methodGenome.Count;ex++)
                                    {
                                        //check for new method
                                        if (ParsedSet._genomeSet.methodGenome[ex][0] == (int)GenomeKeys.MethodKey)
                                        {
											if(debug)
                                            	Console.WriteLine("                added new executelist");
                                            //create new execution list
                                            CurrentMethodExecuteLists.Add(new PMGExecuteList(CurrentMethod as object, FunctionOwnerType.METHOD, CreatedActors[i]));


                                            for (int f = 0; f < functionIndex.Count; f++)
                                            {
                                                
                                                //add functions to the last execute list
                                                //if value function
                                                if (functionIndex[f][0] == (int)GenomeKeys.ValueFunc)
                                                {
													if(debug)
														Console.WriteLine("                    value function detected");
                                                    //check if its in the collections                                                    
                                                    if (functionIndex[f][1] < MainCore.ValueFunctions.Collection.Count)
                                                    {
														if(debug)
															Console.WriteLine("                        value function type: "+ functionIndex[f][1]+ " added to executelist: "+ (CurrentMethodExecuteLists.Count - 1));
                                                        //add function to currentmethodexecutelits[timeindex]
                                                        CurrentMethodExecuteLists[CurrentMethodExecuteLists.Count - 1]._functions.Add(new PMGValueFunction(functionIndex[f][1]));

                                                    }
                                                    else
                                                    {
                                                        //error: value function requested not found in the collections
														if(debug)
															Console.WriteLine("                        value function type: " + functionIndex[f][1]+" was not found");
                                                    }
                                                }
                                                // else if change function
                                                else if (functionIndex[f][0] == (int)GenomeKeys.ChangeFunc)
                                                {
													if(debug)
														Console.WriteLine("                    change function detected");
                                                    //check if its in the collections                                                    
                                                    if (functionIndex[f][1] < MainCore.ChangeFunctions.Collection.Count)
                                                    {
														if(debug)
															Console.WriteLine("                        change function type: " + functionIndex[f][1] + " added to executelist: " + (CurrentMethodExecuteLists.Count - 1));
                                                        CurrentMethodExecuteLists[CurrentMethodExecuteLists.Count - 1]._functions.Add(new PMGChangeFunction(functionIndex[f][1]));
                                                    }
                                                    else
                                                    {
                                                        //error: change function requested not found in the collections
														if(debug)
															Console.WriteLine("                        change function type: " + functionIndex[f][1] + " was not found");
                                                    }
                                                }
                                                //else utility function
                                                else
                                                {
													if(debug)
														Console.WriteLine("                    utility function detected");
                                                    //check if its in the collections                                                    
                                                    if (functionIndex[f][1] < MainCore.UtilityFunctions.Collection.Count)
                                                    {
														if(debug)
															Console.WriteLine("                        utility function type: " + functionIndex[f][1] + " added to executelist: " + (CurrentMethodExecuteLists.Count - 1));
                                                        CurrentMethodExecuteLists[CurrentMethodExecuteLists.Count - 1]._functions.Add(new PMGUtilityFunction(functionIndex[f][1]));
                                                    }
                                                    else
                                                    {
                                                        //error: utility function requested not found in the collections
														if(debug)
															Console.WriteLine("                        utility function type: " + functionIndex[f][1] + " was not found");

                                                    }
                                                }
                                            }
                                            //breaks out at the next method found
                                            break;
                                        }
                                        //check for timesteps between current and new method
                                        else if (ParsedSet._genomeSet.methodGenome[ex][0] == (int)GenomeKeys.TimeStep)
                                        {
                                            
                                            //checks for timestep right after first method
                                            if (functionIndex.Count > 0)
                                            {
                                                
                                                //create new execution list
                                                CurrentMethodExecuteLists.Add(new PMGExecuteList(CurrentMethod as object, FunctionOwnerType.METHOD, CreatedActors[i]));
												if(debug)
													Console.WriteLine("                added executelist: " + (CurrentMethodExecuteLists.Count - 1));

                                                //add functions to current methods execute list before time step
                                                for (int f = 0; f < functionIndex.Count; f++)
                                                {
                                                    //add functions to the last execute list
                                                    //if value function
                                                    if (functionIndex[f][0] == (int)GenomeKeys.ValueFunc)
                                                    {
														if(debug)
															Console.WriteLine("                    value function detected");
                                                        //check if its in the collections                                                    
                                                        if (functionIndex[f][1] < MainCore.ValueFunctions.Collection.Count)
                                                        {
															if(debug)
																Console.WriteLine("                        value function type: " + functionIndex[f][1] + " added to executelist: " + (CurrentMethodExecuteLists.Count - 1));
                                                            //add function to currentmethodexecutelits[timeindex]
                                                            CurrentMethodExecuteLists[CurrentMethodExecuteLists.Count - 1]._functions.Add(new PMGValueFunction(functionIndex[f][1]));

                                                        }
                                                        else
                                                        {
                                                            //error: value function requested not found in the collections
															if(debug)
																Console.WriteLine("                        value function type: " + functionIndex[f][1] + " was not found");
                                                        }
                                                    }
                                                    // else if change function
                                                    else if (functionIndex[f][0] == (int)GenomeKeys.ChangeFunc)
                                                    {
														if(debug)
															Console.WriteLine("                    change function detected");
                                                        //check if its in the collections                                                    
                                                        if (functionIndex[f][1] < MainCore.ChangeFunctions.Collection.Count)
                                                        {
															if(debug)
																Console.WriteLine("                        change function type: " + functionIndex[f][1] + " added to executelist: " + (CurrentMethodExecuteLists.Count - 1));
                                                            CurrentMethodExecuteLists[CurrentMethodExecuteLists.Count - 1]._functions.Add(new PMGChangeFunction(functionIndex[f][1]));
                                                        }
                                                        else
                                                        {
                                                            //error: change function requested not found in the collections
															if(debug)
																Console.WriteLine("                        change function type: " + functionIndex[f][1] + " was not found");
                                                        }
                                                    }
                                                    //else utility function
                                                    else
                                                    {
														if(debug)
															Console.WriteLine("                    utility function detected");
                                                        //check if its in the collections                                                    
                                                        if (functionIndex[f][1] < MainCore.UtilityFunctions.Collection.Count)
                                                        {
															if(debug)
																Console.WriteLine("                        utility function type: " + functionIndex[f][1] + " added to executelist: " + (CurrentMethodExecuteLists.Count - 1));
                                                            CurrentMethodExecuteLists[CurrentMethodExecuteLists.Count - 1]._functions.Add(new PMGUtilityFunction(functionIndex[f][1]));
                                                        }
                                                        else
                                                        {
                                                            //error: utility function requested not found in the collections
															if(debug)
																Console.WriteLine("                        utility function type: " + functionIndex[f][1] + " was not found");

                                                        }
                                                    }
                                                }
                                                //adds null lists for how many timesteps needs to be added
                                                for (int t = 0; t < ParsedSet._genomeSet.methodGenome[ex][1]; t++)
                                                {
													if(debug)
														Console.WriteLine("                added timestep as executelist: " + (CurrentMethodExecuteLists.Count - 1));
                                                    CurrentMethodExecuteLists.Add(null);
                                                }
                                            }
                                            

                                            timeIndex++;

                                            //resets function index list
                                            Findex = 0;
                                            functionIndex = new List<List<int>>();                                            
                                        }
                                        //check for anything thats not a method key or timestep
                                        else
                                        {
                                            //counts up the index
                                            functionIndex.Add(new List<int>());
                                            functionIndex[Findex].Add(ParsedSet._genomeSet.methodGenome[ex][0]);
                                            functionIndex[Findex].Add(ParsedSet._genomeSet.methodGenome[ex][1]);
                                            Findex++;
                                        }
                                    }
                                    if (true)
                                    {

                                    }
                                    else
                                    {
                                        //error: 
                                    }
                                    //add functions to those lists
                                    //add all lists to current method
                                    //CurrentMethod._steps.add(CurrentExList)
                                }
                                else if(!TypeFound)
                                {
                                    // error: Method not found in method list
									if(debug)
										Console.WriteLine("        type: "+ ParsedSet.actorTypes[i][j]+" not detected at index: "+e);
                                    

                                }//*/
                            }///
                        }
                        //if its odd, time for event
                        else if(j % 2 != 0)
                        {
							if(debug)
								Console.WriteLine("     event type: " + j);
                            //add check for no method, in case when method does not match any on the list
                            //add event
                            //CurrentEvent = new PMGEvent(CurrentMethod, CreatedActors[i]/, behavior///);

                            //at the end, add the event to the actor
                            //CreatedActors[i].Events.Add(CurrentEvent);
                        }///   
                    }///
                }///
            }
        }
    }
}
