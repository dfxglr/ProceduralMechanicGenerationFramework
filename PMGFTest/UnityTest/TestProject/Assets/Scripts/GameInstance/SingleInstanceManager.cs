using UnityEngine;
using System.Collections;
using PMGF.PMGGameInstance;
using System.Collections.Generic;



namespace PMGF
{
    namespace PMGUnity
    {
        namespace Managers
        {
            public class SingleInstanceManager : MonoBehaviour
            {

                //variables
                PMGGenomeSet TestSet = new PMGGenomeSet();

                PMGGenomeParse ParsedSet = new PMGGenomeParse();

                public  PMGSingleGameInstance ThisGameForNow = new PMGSingleGameInstance();

                public List<GameObject> unityActors = new List<GameObject>();
                //-------------------------------------------------// singleton thread safe with no lock (type 4 from http://csharpindepth.com/Articles/General/Singleton.aspx)
                private static readonly SingleInstanceManager instance = new SingleInstanceManager();
                // Explicit static constructor to tell C# compiler not to mark type as 'beforefieldinit'
                static SingleInstanceManager()
                {
                }
                private SingleInstanceManager()
                {
                }
                public static SingleInstanceManager Instance
                {
                    get
                    {
                        return instance;
                    }
                }
                //-------------------------------------------------//

                
                public void setup()
                {

                    //generate genome
                    TestSet.TestPMGGenomeSet();

                    //decode genome
                    ParsedSet.DecodeGenomeSet(TestSet);

                    //set the gameset
                    ThisGameForNow.SetInternalParsedSet(ParsedSet);
                }
                

                //MakeUnityActors
                public void MakeUnityActors()
                {
                    GameObject Anchor = GameObject.Find("ActorAnchor");
                    for (int i =0; i < ThisGameForNow.SpawnedActors.Count;i++)
                    {
                        unityActors.Add(Instantiate(Resources.Load("Prefabs/Actor"), SingleMapManager.Instance.GameMap[ThisGameForNow.SpawnedActors[i].position[0], ThisGameForNow.SpawnedActors[i].position[1]].transform.position, Quaternion.identity) as GameObject);
                        unityActors[unityActors.Count - 1].transform.parent = Anchor.transform;

                    }
                }
                //UpdateUnityActors
                public void UpdateUnityActors()
                {
                    for (int i = 0; i < ThisGameForNow.SpawnedActors.Count; i++)
                    {
                        unityActors[i].transform.position = SingleMapManager.Instance.GameMap[ThisGameForNow.SpawnedActors[i].position[0], ThisGameForNow.SpawnedActors[i].position[1]].transform.position;

                    }
                }
            }
        }
    }
}
