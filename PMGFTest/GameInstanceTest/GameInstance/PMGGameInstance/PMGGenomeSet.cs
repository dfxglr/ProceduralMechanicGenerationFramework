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
            public List<int> actorPositions = new List<int>();
            //events

            //methods
            public PMGGenomeSet()
            {

            }

            public void TestPMGGenomeSet()
            {
                //test genome 
                actorGenome.Add(-1);//A
                actorGenome.Add(3);
                actorGenome.Add(3);
                actorGenome.Add(0);
                actorGenome.Add(4);
                actorGenome.Add(1);
                actorGenome.Add(-1);//A
                actorGenome.Add(2);
                actorGenome.Add(-1);//A
                actorGenome.Add(0);
                actorGenome.Add(0);
                actorGenome.Add(8);
                actorGenome.Add(4);
                actorGenome.Add(3);

                //test positions

                //first pos available in map
                actorGenome.Add(0);//type
                actorGenome.Add(1);//x
                actorGenome.Add(1);//y

                //inside wall
                actorGenome.Add(1);
                actorGenome.Add(7);
                actorGenome.Add(7);

                //close to map center
                actorGenome.Add(1);
                actorGenome.Add(24);
                actorGenome.Add(17);
                
                //
                actorGenome.Add(2);
                actorGenome.Add(1);
                actorGenome.Add(8);
                
                //
                actorGenome.Add(2);
                actorGenome.Add(3);
                actorGenome.Add(3);
                
                //
                actorGenome.Add(3);
                actorGenome.Add(3);
                actorGenome.Add(3);

            }
        }
    }
}
