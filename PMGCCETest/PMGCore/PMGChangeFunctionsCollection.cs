using System;
using System.Collections.Generic;
using PMGF.PMGGameInstance;
//using UnityEngine;
using System.Collections;
//using PMGF.PMGUnity.Managers;


namespace PMGF
{
    namespace PMGCore
    {
        // Delegate for valuefunctions
        public delegate void ChangeFunction(PMGActor actor, PMGValueStack localStack);

        public class PMGChangeFunctionsCollection //: MonoBehaviour
        {

            // All the manually created value functions
            public List<ChangeFunction> Collection = new List<ChangeFunction>();

            //map
            PMGMap Map = new PMGMap();

            public void Initialize()
            {
                // Add all the functions you want
                // f# */
                /*  X *///Collection.Add(CF_DebugWriteToConsole); // Write a msg to console
                /*  0 */Collection.Add(CF_DoNothing);           // Do nothing
                /*  1 */Collection.Add(CF_PushLeetToActor);     // Push 1337 to actor stack
                /*  2 */Collection.Add(CF_Movement_MoveUpOnce); 
                /*  3 */Collection.Add(CF_Movement_MoveLeftOnce);
                /*  4 */Collection.Add(CF_Movement_MoveDownOnce);
                /*  5 */Collection.Add(CF_Movement_MoveRightOnce);
                /*  6 */Collection.Add(CF_Movement_MoveRandomOnce);  
                /*  7 */Collection.Add(CF_GameOver);
                /*  8 */Collection.Add(CF_Action_AttachKeyToActor);
                /*  9 */Collection.Add(CF_GameWin);
                /* 10 */Collection.Add(CF_Talk_RemoveKeyMessage);
                /* 11 */Collection.Add(CF_Talk_RemoveNoKeyMessage);
           
            }



            /*
             * USER DEFINED FUNCTIONS
             */
            /*public void CF_DebugWriteToConsole(PMGActor actor, PMGValueStack localStack)
            {
                // Write a message to console for testing purposes.
                int fromStack = System.Convert.ToInt32(localStack.PopValueOfType(ValueType.INT));

                string s = string.Format("Change function called. Got {0} from stack!",fromStack);

                Console.WriteLine(s);
            }*/

            public void CF_DoNothing(PMGActor actor, PMGValueStack localStack)
            {
                // literally do nothing
                //Debug.Log("Actor: " + actor.Type + " yells Faggit!");
            }
            public void CF_PushLeetToActor(PMGActor actor, PMGValueStack localStack)
            {
                // push 1337 to actor stack               
                actor.ValueStack.Push(9001);
            }
            public void CF_Movement_MoveUpOnce(PMGActor actor, PMGValueStack localStack)
            {
                //map boundary check
                if (actor.position[1] - 1 > 0 && !actor.HasMoved)
                {
                    actor.position[1] = actor.position[1] - 1;
                    //Console.WriteLine("         moved up 1 step");
                    //Debug.Log("Actor: "+actor.Type+ " moved one step up");
                    actor.HasMoved = true;
                    
                }
                else
                {
                    //Console.WriteLine("         cant moved up 1 step");
                    //Debug.Log("Cant move up");
                }
            }
            public void CF_Movement_MoveLeftOnce(PMGActor actor, PMGValueStack localStack)
            {                
                //map boundary check
                if (actor.position[0] - 1 > 0 && !actor.HasMoved)
                {
                    actor.position[0] = actor.position[0] - 1;
                    //Console.WriteLine("         moved left 1 step");
                    //Debug.Log("Actor: " + actor.Type + " moved one step left");
                    actor.HasMoved = true;
                }
                else
                {
                    //Console.WriteLine("         cant moved left 1 step");
                    //Debug.Log("Cant move left");
                }
            }
            public void CF_Movement_MoveDownOnce(PMGActor actor, PMGValueStack localStack)
            {               
                //map boundary check
                if (actor.position[1] + 1 < Map.chart.GetLength(1) && !actor.HasMoved)
                {
                    actor.position[1] = actor.position[1] + 1;
                    //Console.WriteLine("         moved down 1 step");
                    //Debug.Log("Actor: " + actor.Type + " moved one step down");
                    actor.HasMoved = true;
                }
                else
                {
                    //Console.WriteLine("         cant moved down 1 step");
                    //Debug.Log("Cant move down");
                }
            }
            public void CF_Movement_MoveRightOnce(PMGActor actor, PMGValueStack localStack)
            {
                //map boundary check
                if (actor.position[0] + 1 < Map.chart.GetLength(0) && !actor.HasMoved)
                {
                    actor.position[0] = actor.position[0] + 1;
                    //Console.WriteLine("         moved right 1 step");
                    //Debug.Log("Actor: " + actor.Type + " moved one step right");
                    actor.HasMoved = true;
                }
                else
                {
                    //Console.WriteLine("         cant moved right 1 step");
                    //Debug.Log("Cant move right");
                }
            }            
            public void CF_Movement_MoveRandomOnce(PMGActor actor, PMGValueStack localStack)
            {
                //moves in a random direction
                System.Random Range = new System.Random();
                int WASD = Range.Next(0, 3);
                switch (WASD)
                {
                    case 0: // move up
                            //map boundary check
                        if (actor.position[1] - 1 > 0 && !actor.HasMoved)
                        {
                            actor.position[1] = actor.position[1] - 1;
                            //Console.WriteLine("         moved up 1 step");
                            //Debug.Log("Actor: "+actor.Type+ " moved one step up");
                            actor.HasMoved = true;

                        }
                        else
                        {
                            //Console.WriteLine("         cant moved up 1 step");
                            //Debug.Log("Cant move up");
                        }
                        break;
                    case 1: // move left
                            //map boundary check
                        if (actor.position[0] - 1 > 0 && !actor.HasMoved)
                        {
                            actor.position[0] = actor.position[0] - 1;
                            //Console.WriteLine("         moved left 1 step");
                            //Debug.Log("Actor: " + actor.Type + " moved one step left");
                            actor.HasMoved = true;
                        }
                        else
                        {
                            //Console.WriteLine("         cant moved left 1 step");
                            //Debug.Log("Cant move left");
                        }
                        break;
                    case 2: // move down
                            //map boundary check
                        if (actor.position[1] + 1 < Map.chart.GetLength(1) && !actor.HasMoved)
                        {
                            actor.position[1] = actor.position[1] + 1;
                            //Console.WriteLine("         moved down 1 step");
                            //Debug.Log("Actor: " + actor.Type + " moved one step down");
                            actor.HasMoved = true;
                        }
                        else
                        {
                            //Console.WriteLine("         cant moved down 1 step");
                            //Debug.Log("Cant move down");
                        }
                        break;
                    case 3: // move right
                            //map boundary check
                        if (actor.position[0] + 1 < Map.chart.GetLength(0) && !actor.HasMoved)
                        {
                            actor.position[0] = actor.position[0] + 1;
                            //Console.WriteLine("         moved right 1 step");
                            //Debug.Log("Actor: " + actor.Type + " moved one step right");
                            actor.HasMoved = true;
                        }
                        else
                        {
                            //Console.WriteLine("         cant moved right 1 step");
                            //Debug.Log("Cant move right");
                        }
                        break;
                    default:
                        //Debug.Log("Tried to move in a random direction but didnt get any of the four: up,left,down,right");
                        break;
                }
            }           
            public void CF_GameOver(PMGActor actor, PMGValueStack localStack)
            {
                /*/hack job stuff
                SingleMenuManager.Instance.DeSelectAllM();
                SingleMenuManager.Instance.DestroyAll();
                SingleMenuManager.Instance.SelectMenu(6);
                SingleMenuManager.Instance.SelectFirstOrActiveButton();
                //SingleInputManager.Instance.inMenu = true;
                SingleMenuManager.Instance.InstantiateCurrent();
                //diplay the selected button in unity and throwing default color on the rest
                SingleMenuManager.Instance.DisplayUnitySelected();
                SingleInputManager.Instance.inMenu = true;//*/
                //Environment.Exit(0);
                //Console.WriteLine("YOU LOST LOL");
                //System.Threading.Thread.Sleep(10000);


            }           
            public void CF_Action_AttachKeyToActor(PMGActor actor, PMGValueStack localStack)
            {
                //disables/removes other events and adds parrents this actor under the other, or follows it constantly 

                //add key to players "inventory"
                if (localStack.GetActor().HasKey == false)
                {
                    localStack.GetActor().HasKey = true;
                    //Debug.Log("got the key");


                    /*GameObject Anchor = GameObject.Find("ActorAnchor");
                    //if()
                    GameObject talk = Instantiate(Resources.Load("Prefabs/TalkBlock"), SingleMapManager.Instance.GameMap[actor.position[0], actor.position[1]].transform.position + new Vector3(2f, 2f, -1), Quaternion.identity) as GameObject;
                    talk.transform.eulerAngles = SingleMenuManager.Instance.LeRotation();
                    talk.GetComponent<Renderer>().material.mainTexture = (Texture2D)Resources.Load("Textures/GettingKey") as Texture2D;
                    talk.transform.parent = Anchor.transform;
                    talk.name = "Here have the key";//*/
                }
                


                //turn display off maybe
                //SingleInstanceManager.Instance.unityActors
            }
            public void CF_GameWin(PMGActor actor, PMGValueStack localStack)
            {
                //Debug.Log("you won lolololol");
                if (localStack.GetActor().HasKey == false)
                {
                    
                    //Debug.Log("got the key");


                    /*GameObject Anchor = GameObject.Find("ActorAnchor");
                    //if()
                    GameObject talk = Instantiate(Resources.Load("Prefabs/TalkBlock"), SingleMapManager.Instance.GameMap[actor.position[0], actor.position[1]].transform.position + new Vector3(2f, 2f, -1), Quaternion.identity) as GameObject;
                    talk.transform.eulerAngles = SingleMenuManager.Instance.LeRotation();
                    talk.GetComponent<Renderer>().material.mainTexture = (Texture2D)Resources.Load("Textures/GoGetKey") as Texture2D;
                    talk.transform.parent = Anchor.transform;
                    talk.name = "Go get the key";//*/
                }
                else
                {
                    //Debug.Log("you won lolololol");
                    //hack job stuff
                    /*SingleMenuManager.Instance.DeSelectAllM();
                    SingleMenuManager.Instance.DestroyAll();
                    SingleMenuManager.Instance.SelectMenu(8);
                    SingleMenuManager.Instance.SelectFirstOrActiveButton();
                    //SingleInputManager.Instance.inMenu = true;
                    SingleMenuManager.Instance.InstantiateCurrent();
                    //diplay the selected button in unity and throwing default color on the rest
                    SingleMenuManager.Instance.DisplayUnitySelected();
                    SingleInputManager.Instance.inMenu = true;*/

                    //Console.Write("YOU WIN LOLOLOL");
                    //System.Threading.Thread.Sleep(10000);

                }

            }

            public void CF_Talk_RemoveKeyMessage(PMGActor actor, PMGValueStack localStack)
            {
                /*GameObject message = GameObject.Find("Here have the key");
                Destroy(message);*/
            }

            public void CF_Talk_RemoveNoKeyMessage(PMGActor actor, PMGValueStack localStack)
            {
                /*GameObject message = GameObject.Find("Go get the key");
                Destroy(message);*/
            }
        }
    }
}
