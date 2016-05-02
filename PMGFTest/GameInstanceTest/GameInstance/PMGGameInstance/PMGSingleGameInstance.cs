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
            List<PMGActor> Actors = new List<PMGActor>();
            //methods
            //events
            //actors new iD


            
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

                //create actors
                foreach (int e in ParsedSet.actorTypes[0])
                {
                    //add to created actors
                    Actors.Add(new PMGActor(MainCore));
                }
                //create methods
                foreach(int e in ParsedSet.eventIndexList)
                {
                    //create from event genome
                    //put into event
                }



                //give actors unique ID

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
