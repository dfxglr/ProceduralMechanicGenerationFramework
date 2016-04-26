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
            PMGMap Map = new PMGMap();


            public PMGGenomeParse ParsedSet = new PMGGenomeParse();
            
                
            //actors in map
            List<PMGActor> actors = new List<PMGActor>();


            


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
        }
    }
}
