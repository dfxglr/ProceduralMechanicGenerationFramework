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
                PMGMap map = new PMGMap();
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

                    GameMap = new GameObject[map.chart.GetLength(1),map.chart.GetLength(0)];
                    //find the anchor
                    GameObject Anchor = GameObject.Find("MapAnchor");

                    //y
                    for (int y = 0; y < map.chart.GetLength(0); y++)
                    {
                        //x
                        for (int x = 0; x < map.chart.GetLength(1); x++)
                        {
                            //check for 0 or 1 in txtmap
                            //Debug.Log("derp");
                            if (map.chart[y,x] == 1)
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
                                GameMap[x, y].name = "EmptySpace[" + x + ", " +y+ "]";
                            }
                        }
                    }

                    //test
                    GameObject bob =  Instantiate(Resources.Load("Prefabs/Wall"), GameMap[6,2].transform.position+new Vector3(0,0,3), Quaternion.identity) as GameObject;
                    bob.transform.parent = Anchor.transform;
                }
            }
        }
    }
}
