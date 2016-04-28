using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMGF
{
    namespace PMGGameInstance
    { 
        class PMGGenomeSet
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


                actorGenome.Add(2);
                actorGenome.Add(6);
                actorGenome.Add(0);
                //actorGenome.Add(-1);
                actorGenome.Add(0);
                actorGenome.Add(2);
                /*actorGenome.Add(6);
                actorGenome.Add(0);
                actorGenome.Add(-1);
                actorGenome.Add(0);
                actorGenome.Add(2);
                actorGenome.Add(-1);
                actorGenome.Add(-1);
                actorGenome.Add(6);
                actorGenome.Add(0);
                actorGenome.Add(-1);
                actorGenome.Add(0);
                actorGenome.Add(2);
                actorGenome.Add(6);
                actorGenome.Add(0);
                actorGenome.Add(-1);
                actorGenome.Add(0);
                actorGenome.Add(2);
                actorGenome.Add(6);
                actorGenome.Add(0);
                actorGenome.Add(-1);
                actorGenome.Add(0);//*/





                //test positions

                //type 0
                actorPositionsGenome.Add(0);//type
                actorPositionsGenome.Add(1);//x
                actorPositionsGenome.Add(1);//y

                /*/type 1
                actorPositionsGenome.Add(1);
                actorPositionsGenome.Add(2);
                actorPositionsGenome.Add(2);

                //type 2
                actorPositionsGenome.Add(2);
                actorPositionsGenome.Add(3);
                actorPositionsGenome.Add(3);

                //jump to type 4
                actorPositionsGenome.Add(4);
                actorPositionsGenome.Add(6);
                actorPositionsGenome.Add(6);

                //type 0 again
                actorPositionsGenome.Add(0);
                actorPositionsGenome.Add(7);
                actorPositionsGenome.Add(7);

                //type 11, which dosent exist in the type genome
                actorPositionsGenome.Add(11);
                actorPositionsGenome.Add(12);
                actorPositionsGenome.Add(12);
            
                //type 3
                actorPositionsGenome.Add(3);
                actorPositionsGenome.Add(4);
                actorPositionsGenome.Add(4);

                //type 11, which dosent exist in the type genome, again
                actorPositionsGenome.Add(12);
                actorPositionsGenome.Add(14);
                actorPositionsGenome.Add(14);

                //type 4 again, but with error of being too short
                actorPositionsGenome.Add(4);
                //actorPositionsGenome.Add(10);
                //actorPositionsGenome.Add(3);//*/

                //events



            }
        }
    }
}
