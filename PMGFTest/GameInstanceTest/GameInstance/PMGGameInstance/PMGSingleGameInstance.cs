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


                //create actors, methods and event and add them to the actors
                //mainlist
                for (int i = 0; i< ParsedSet.actorTypes.Count;i++) {
                    //add new actor
                    CreatedActors.Add(new PMGActor(MainCore));
                    //sublist
                    for(int j = 0; j< ParsedSet.actorTypes[i].Count; j++)
                    {
                        //is it even, time for method
                        if (j%2 == 0)
                        {
                            //check the index list to see if 
                            for (int e =0; e < ParsedSet.methodIndexList.Count;e++) {
                                //if the value is an index in the method index list
                                if (ParsedSet.actorTypes[i][j] == e)
                                {
                                    //save index in actortypes
                                    //save amount of methods
                                    //add method
                                }
                                else
                                {
                                    // error: Method not found in 
                                }
                            }
                            //add method
                        }
                        //if its odd, time for event
                        else
                        {
                            //add event 
                        }   
                    }
                }


                //add executelists to methods

                foreach (int j in ParsedSet.methodIndexList) {
                    //check through method genome
                    for (int i = ParsedSet.methodIndexList[j] + 1; i < ParsedSet._genomeSet.methodGenome.Count; i++)
                    {
                            
                    }
                }
          
                //--------------------------------// testing stuff, this is only here because our collections are empty atm
                // Create functions for the methods
                PMGValueFunction testValF = new PMGValueFunction(0);//works for event too
                PMGUtilityFunction testUtilF = new PMGUtilityFunction(0);
                PMGChangeFunction testChgF = new PMGChangeFunction(0);
                //create functions for the events
                PMGConditionFunction testCondF = new PMGConditionFunction(0);
                //--------------------------------//
                

                //add execute lists for mehtods
                
                //add functions to executelists


                //create events
                foreach (int e in ParsedSet.eventIndexList)
                {
                    //create from method genome
                    //put into method list
                    //Events.Add(new PMGEvent(which method, which actor, which behavior));
                }


                //add functions to methods
                //




                //give actors unique ID very last

                ////build events 



                //uses map, decoded genome lists, mimicks setup of coretest
                //core first
                //actors 
                //methods
                //events
            }

            public void RunTimeSteps(int timeSteps)
            {


            }

            /*//add new list for new event to event type list
                                    eventTypes.Add(new List<int>());
                                    //new event
                                    eventTypes[mainIndex].Add(_genomeSet.eventGenome[mainIndex][0]);
                                    //and its behavior
                                    eventTypes[mainIndex].Add(_genomeSet.eventGenome[mainIndex][1]);

                                    //adds the following condition and value functions for that event, untill new event or end of genome is reached
                                    for (int subIndex = mainIndex+1; subIndex < _genomeSet.eventGenome.Count; subIndex++)
                                    {
                                        //break out when new event is met
                                        if (_genomeSet.eventGenome[subIndex][0] == -2)
                                        {
                                            break;
                                        }
                                        //adds to the value to the actor type list
                                        else
                                        {
                                            //eventTypes[eventTypeIndex].Add(_genomeSet.eventGenome[subIndex]);
                                            eventTypes.Add(new List<int>());
                                            //new event
                                            eventTypes[subIndex].Add(_genomeSet.eventGenome[subIndex][0]);
                                            //and its behavior
                                            eventTypes[subIndex].Add(_genomeSet.eventGenome[subIndex][1]);
                                        }
                                    }*/
        }
    }
}
