using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PMGF.PMGCore;

namespace PMGF
{
    namespace PMGUnity
    {
        namespace Managers
        {
            public sealed class SingleGameGenerationManager : MonoBehaviour
            {
                //game core
                PMGGameCore core = new PMGGameCore();

                //actors
                int actorParamenterCount = 0;
                string actorGenome = "A33041A2A00843";
                //string or array or list actorPositions = 3,4 , 8,8 , 13,15
                List<List<char>> actorInfo = new List<List<char>>();
                List<PMGActor> actors = new List<PMGActor>();


                //-------------------------------------------------// singleton thread safe with no lock (type 4 from http://csharpindepth.com/Articles/General/Singleton.aspx)
                private static readonly SingleGameGenerationManager instance = new SingleGameGenerationManager();
                // Explicit static constructor to tell C# compiler not to mark type as 'beforefieldinit'
                static SingleGameGenerationManager()
                {
                }
                private SingleGameGenerationManager()
                {
                }
                public static SingleGameGenerationManager Instance
                {
                    get
                    {
                        return instance;
                    }
                }
                //-------------------------------------------------//

                public void GenerateGameFromGenome()
                {


                    //actors
                    foreach(char e in actorGenome)
                    {
                        //check for A
                        if(e == 'A')
                        {
                            //adds new actor type
                            actorInfo.Add(new List<char>());
                            //
                            foreach(char e2 in actorGenome)
                            {

                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}
