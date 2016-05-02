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
            
                
            //actors in map
            List<PMGActor> mapActors = new List<PMGActor>();


            


            public PMGSingleGameInstance()
            {
                
            }
            
           
            
            // builds the instance
            public void BuildInstance()
            {
                //uses map, decoded genome lists, mimicks setup of coretest
                //core first
                //actors 
                //methods
                //events
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
