using System;
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
            PMGMap Map = new PMGMap();

            // creating a test genome
            PMGGenomeSet _genomeSet; 

            //actors
            public int actorGenCount = 0;
            //list of types of actors
            List<List<int>> actorTypes = new List<List<int>>();
            //list of instantiated actors in map
            List<PMGActor> actors = new List<PMGActor>();

            public PMGSingleGameInstance()
            {

            }
            //decode genome
            public void DecodeGenome(PMGGenomeSet TobeDecoded)
            {
                _genomeSet = TobeDecoded;

                //decoding actors
                foreach(int e in _genomeSet.actorGenome)
                {
                    //check for new actor
                    if(e == -1)
                    {
                        //add new type to type list
                        actorTypes.Add(new List<int>());
                       
                        
                        //
                        /*foreach ()
                        {

                        }//*/
                    }
                }



            }
            // builds the instance
            public void BuildInstance()
            {
                //uses map, decoded genome lists, mimicks setup of coretest



            }
        }
    }
}
