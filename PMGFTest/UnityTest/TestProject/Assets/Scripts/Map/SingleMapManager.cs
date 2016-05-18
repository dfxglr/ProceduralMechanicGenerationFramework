using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PMGF.PMGGameInstance;

namespace PMGF
{
    namespace PMGUnity
    {
        namespace Managers
        {
            public sealed class SingleMapManager : MonoBehaviour
            {
                //the map
                PMGMap Map = new PMGMap();

                public GameObject[,] GameMap;

                //-------------------------------------------------// singleton thread safe with no lock (type 4 from http://csharpindepth.com/Articles/General/Singleton.aspx)
                private static readonly SingleMapManager instance = new SingleMapManager();
                // Explicit static constructor to tell C# compiler not to mark type as 'beforefieldinit'
                static SingleMapManager()
                {
                }
                private SingleMapManager()
                {
                }
                public static SingleMapManager Instance
                {
                    get
                    {
                        return instance;
                    }
                }
                //-------------------------------------------------//

                //generates the static map
                public void Setup()
                {
                    GameMap = new GameObject[Map.chart.GetLength(0),Map.chart.GetLength(1)];
       
                    //find the anchor
                    GameObject Anchor = GameObject.Find("MapAnchor");

                    //y
                    for (int x = 0; x < Map.chart.GetLength(0); x++)
                    {                        //x
                        for (int y = 0; y < Map.chart.GetLength(1); y++)
                        {
                            //check for 0 or 1 in txtmap
                            //Debug.Log("derp");
                            if (Map.chart[x,y] == 1)
                            {
                                //insert "wall"
                                GameMap[x,y] = Instantiate(Resources.Load("Prefabs/Wall"), new Vector3(x,-y, 0)+Anchor.transform.position, Quaternion.identity)as GameObject;
                                GameMap[x,y].transform.parent = Anchor.transform;
                                GameMap[x, y].GetComponent<Renderer>().material.mainTexture = (Texture2D)Resources.Load("Textures/Map/Wall") as Texture2D;
                                GameMap[x, y].name = "Wall[" + x + ", " + y + "]";
                            }
                            else
                            {
                                //insert "space"
                                GameMap[x, y] = Instantiate(Resources.Load("Prefabs/EmptySpace"), new Vector3(x, -y, 0) + Anchor.transform.position, Quaternion.identity) as GameObject;
                                GameMap[x, y].transform.parent = Anchor.transform;
                                GameMap[x, y].GetComponentInChildren<Renderer>().material.mainTexture = (Texture2D)Resources.Load("Textures/Map/Floor") as Texture2D;
                                GameMap[x, y].name = "EmptySpace[" + x + ", " + y + "]";
                            }
                        }
                    }
                }
            }
        }
    }
}
