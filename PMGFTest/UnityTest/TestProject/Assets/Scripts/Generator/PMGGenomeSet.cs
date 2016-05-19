using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using PMGF.PMGGenerator;

namespace PMGF
{
    namespace PMGGameInstance
    { 
        public class PMGGenomeSet
        {
            //actors
            public List<int> actorGenome = new List<int>();
            public List<int> actorPositionsGenome = new List<int>();
            //events
            public List<List<int>> eventGenome = new List<List<int>>();
            //methods
            public List<List<int>> methodGenome = new List<List<int>>();

            public PMGGenomeSet()
            {
            }

            public void TestPMGGenomeSet()
            {
                //test genome
                
                //---------------------------------------------------------//
                //actors
                actorGenome.Add(2);
                actorGenome.Add(6);
                actorGenome.Add(0);
                //new actor
                actorGenome.Add((int)GenomeKeys.ActorKey);
                actorGenome.Add(0);
                actorGenome.Add(2);
                actorGenome.Add(2);
                actorGenome.Add(0);
                actorGenome.Add(10);
                actorGenome.Add(0);
                //new actor
                actorGenome.Add((int)GenomeKeys.ActorKey);
                actorGenome.Add(2);
                actorGenome.Add(2);
                //new actor
                actorGenome.Add((int)GenomeKeys.ActorKey);
                actorGenome.Add((int)GenomeKeys.ActorKey);
                actorGenome.Add(2);

                //new actor
                actorGenome.Add((int)GenomeKeys.ActorKey);
                actorGenome.Add(6);
                actorGenome.Add(0);
                //actorGenome.Add(11);
                //new actor
                actorGenome.Add((int)GenomeKeys.ActorKey);
                actorGenome.Add(0);
                actorGenome.Add(2);
                actorGenome.Add(6);
                actorGenome.Add(0);
                //new actor
                actorGenome.Add((int)GenomeKeys.ActorKey);
                actorGenome.Add(2);
                actorGenome.Add(2);
                actorGenome.Add(6);
                actorGenome.Add(0);
                //actorGenome.Add(11);
                //new actor
                actorGenome.Add((int)GenomeKeys.ActorKey);
                actorGenome.Add(0);//*/
                //actorGenome.Add(0);//*/




                //---------------------------------------------------------//
                //positions

                //type 0
                //y

                //type 1
                actorPositionsGenome.Add(2);
                actorPositionsGenome.Add(1);
                actorPositionsGenome.Add(1);

                //type 2
                actorPositionsGenome.Add(0);
                actorPositionsGenome.Add(2);
                actorPositionsGenome.Add(3);

                //jump to type 4
                actorPositionsGenome.Add(1);
                actorPositionsGenome.Add(1);
                actorPositionsGenome.Add(1);

                //type 0 again
                actorPositionsGenome.Add(4);
                actorPositionsGenome.Add(7);
                actorPositionsGenome.Add(7);

                //type 11, which dosent exist in the type genome
                actorPositionsGenome.Add(11);
                actorPositionsGenome.Add(12);
                actorPositionsGenome.Add(12);
            
                //type 3
                actorPositionsGenome.Add(3);
                actorPositionsGenome.Add(3);
                actorPositionsGenome.Add(2);

                //type 11, which dosent exist in the type genome, again
                actorPositionsGenome.Add(12);
                actorPositionsGenome.Add(14);
                actorPositionsGenome.Add(14);

                //type 4 again, but with error of being too short
                actorPositionsGenome.Add(4);
                actorPositionsGenome.Add(10);
                //actorPositionsGenome.Add(3);//*/

                //---------------------------------------------------------//
                //events
                eventGenome.Add(new List<int>());
                //new event
                eventGenome[0].Add((int)GenomeKeys.ValueFunc);
                eventGenome[0].Add(1);
                //
                eventGenome.Add(new List<int>());
                //new value function
                eventGenome[1].Add((int)GenomeKeys.EventKey);
                eventGenome[1].Add(1);
                //
                eventGenome.Add(new List<int>());
                //new value function
                eventGenome[2].Add((int)GenomeKeys.ValueFunc);
                eventGenome[2].Add(2);
                //
                eventGenome.Add(new List<int>());
                //new value function
                eventGenome[3].Add((int)GenomeKeys.EventKey);
                eventGenome[3].Add(4);
                //
                eventGenome.Add(new List<int>());
                //new condition function
                eventGenome[4].Add((int)GenomeKeys.EventKey);
                eventGenome[4].Add(0);
                //
                eventGenome.Add(new List<int>());
                //new condition function
                eventGenome[5].Add((int)GenomeKeys.ValueFunc);
                eventGenome[5].Add(2);
                //
                eventGenome.Add(new List<int>());
                //new value function
                eventGenome[6].Add((int)GenomeKeys.EventKey);
                eventGenome[6].Add(3);
                //
                eventGenome.Add(new List<int>());
                //new event
                eventGenome[7].Add((int)GenomeKeys.CondFunc);
                eventGenome[7].Add(2);
                //
                eventGenome.Add(new List<int>());
                //new condition function
                eventGenome[8].Add((int)GenomeKeys.EventKey);
                eventGenome[8].Add(6);
                //
                eventGenome.Add(new List<int>());
                //new value function
                eventGenome[9].Add((int)GenomeKeys.ValueFunc);
                eventGenome[9].Add(2);
                //
                eventGenome.Add(new List<int>());
                //new value function
                eventGenome[10].Add((int)GenomeKeys.EventKey);
                eventGenome[10].Add(1);
                //
                eventGenome.Add(new List<int>());
                //new event
                eventGenome[11].Add((int)GenomeKeys.ValueFunc);
                eventGenome[11].Add(2);
                //
                eventGenome.Add(new List<int>());
                //new condition fucntion
                eventGenome[12].Add((int)GenomeKeys.EventKey);
                eventGenome[12].Add(1);
                //
                eventGenome.Add(new List<int>());
                //new condition function
                eventGenome[13].Add((int)GenomeKeys.CondFunc);
                eventGenome[13].Add(5);
                
                //---------------------------------------------------------//
                //methods
                methodGenome.Add(new List<int>());
                //new 
                methodGenome[0].Add((int)GenomeKeys.MethodKey);
                methodGenome[0].Add(11);

                methodGenome.Add(new List<int>());
                //new 
                methodGenome[1].Add((int)GenomeKeys.UtilFunc);
                methodGenome[1].Add(1);

                methodGenome.Add(new List<int>());
                //new 
                methodGenome[2].Add((int)GenomeKeys.ValueFunc);
                methodGenome[2].Add(1);

                methodGenome.Add(new List<int>());
                //new 
                methodGenome[3].Add((int)GenomeKeys.TimeStep);
                methodGenome[3].Add(1);

                methodGenome.Add(new List<int>());
                //new 
                methodGenome[4].Add((int)GenomeKeys.ValueFunc);
                methodGenome[4].Add(0);

                methodGenome.Add(new List<int>());
                //new 
                methodGenome[5].Add((int)GenomeKeys.MethodKey);
                methodGenome[5].Add(1);

                methodGenome.Add(new List<int>());
                //new 
                methodGenome[6].Add((int)GenomeKeys.MethodKey);
                methodGenome[6].Add(0);

                methodGenome.Add(new List<int>());
                //new 
                methodGenome[7].Add((int)GenomeKeys.UtilFunc);
                methodGenome[7].Add(0);

                methodGenome.Add(new List<int>());
                //new 
                methodGenome[8].Add((int)GenomeKeys.MethodKey);
                methodGenome[8].Add(0);

                methodGenome.Add(new List<int>());
                //new 
                methodGenome[9].Add((int)GenomeKeys.UtilFunc);
                methodGenome[9].Add(1);

                methodGenome.Add(new List<int>());
                //new 
                methodGenome[10].Add((int)GenomeKeys.MethodKey);
                methodGenome[10].Add(0);

                methodGenome.Add(new List<int>());
                //new 
                methodGenome[11].Add((int)GenomeKeys.UtilFunc);
                methodGenome[11].Add(0);

                methodGenome.Add(new List<int>());
                //new 
                methodGenome[12].Add((int)GenomeKeys.ChangeFunc);
                methodGenome[12].Add(1);

                methodGenome.Add(new List<int>());
                //new 
                methodGenome[13].Add((int)GenomeKeys.TimeStep);
                methodGenome[13].Add(1);//*/
            }
        }
    }
}
