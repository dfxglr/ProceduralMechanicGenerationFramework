using System;
using System.Collections.Generic;
//using System.Windows.Input;
using PMGF.PMGGameInstance;
using UnityEngine;


namespace PMGF
{
    namespace PMGCore
    {
        // Delegate for valuefunctions
        public delegate bool ConditionFunction(PMGActor actor, PMGValueStack localStack);

        public class PMGConditionFunctionsCollection
        {

            // All the manually created value functions
            public List<ConditionFunction> Collection = new List<ConditionFunction>();

            //---------------------------------------------------------------//
            // input key bools
            bool actionPressed = false;
            bool upPressed = false;
            bool leftPressed = false;
            bool downPressed = false;
            bool rightPressed = false;
            // the static map
            PMGMap Map = new PMGMap();             
            //---------------------------------------------------------------//
            public void Initialize()
            {
                // Add all the functions you want
                // f# */
                /*  0 */Collection.Add(CF_DebugWriteToConsole); // Write a msg to console
                /*  1 */Collection.Add(CF_DoNothing);           // Do nothing
                /*  2 */Collection.Add(CF_PushLeetToActor);     // Push 1337 to actor stack
                /*  3 */Collection.Add(CF_Input_IsActionPressedOnce);    //action (space or enter button pressed)
                /*  4 */Collection.Add(CF_Input_IsUpPressedOnce);     // Up     (w or arrowUp button pressed)
                /*  5 */Collection.Add(CF_Input_IsLeftPressedOnce);   // Left   (a or arrowLeft button pressed)
                /*  6 */Collection.Add(CF_Input_IsDownPressedOnce);   // Down   (s or arrowDown button pressed)
                /*  7 */Collection.Add(CF_Input_IsRightPressedOnce);  // Right  (d or arrowRight button pressed)
                /*  8 */Collection.Add(CF_Dectection_IsUpASpace);     
                /*  9 */Collection.Add(CF_Dectection_IsLeftASpace);   
                /* 10 */Collection.Add(CF_Dectection_IsDownASpace);  
                /* 11 */Collection.Add(CF_Dectection_IsRightASpace);
            }
            

            /*
             * USER DEFINED FUNCTIONS
             */
            public bool CF_DebugWriteToConsole(PMGActor actor, PMGValueStack localStack)
            {
                System.Console.WriteLine("Cond. function checking if timestep > 5");
                if(actor.Core.WorldTimeSteps > 5)
                {
                    Console.WriteLine("It did!");
                    return true;
                }
                else
                {
                    Console.WriteLine("It did not!");
                    return false;
                }
            }

            public bool CF_DoNothing(PMGActor actor, PMGValueStack localStack)
            {
                // literally do nothing
                return true;
            }

            public bool CF_PushLeetToActor(PMGActor actor, PMGValueStack localStack)
            {
                // push 1337 to actor stack
                actor.ValueStack.Push(420);
                return true;
            }
            public bool CF_Input_IsActionPressedOnce(PMGActor actor, PMGValueStack localStack)
            {
                Console.WriteLine("     key action is pressed");
                return true;

                if ((Input.GetKeyDown(KeyCode.Return) && !actionPressed) || (Input.GetKeyDown(KeyCode.Space) && !actionPressed))
                {
                    actionPressed = true;        
                    return true;
                }                
                if (Input.GetKeyUp(KeyCode.Return) && Input.GetKeyUp(KeyCode.Space) && actionPressed)
                {
                    actionPressed = false;
                }
                return false;  //*/
            }
            public bool CF_Input_IsUpPressedOnce(PMGActor actor, PMGValueStack localStack)
            {
                Console.WriteLine("     key up is pressed");

                return true;
                if ((Input.GetKeyDown(KeyCode.W) && !upPressed) || (Input.GetKeyDown(KeyCode.UpArrow) && !upPressed))
                {
                    upPressed = true;
                    return true;
                }
                if (Input.GetKeyUp(KeyCode.W) && Input.GetKeyUp(KeyCode.UpArrow) && upPressed)
                {
                    upPressed = false;
                }
                return false;//
            }
            public bool CF_Input_IsLeftPressedOnce(PMGActor actor, PMGValueStack localStack)
            {
                Console.WriteLine("     key left is pressed");
                return true;
                if ((Input.GetKeyDown(KeyCode.A) && !leftPressed) || (Input.GetKeyDown(KeyCode.LeftArrow) && !leftPressed))
                {
                    leftPressed = true;
                    return true;
                }
                if (Input.GetKeyUp(KeyCode.A) && Input.GetKeyUp(KeyCode.LeftArrow) && leftPressed)
                {
                    leftPressed = false;
                }
                return false;
            }
            public bool CF_Input_IsDownPressedOnce(PMGActor actor, PMGValueStack localStack)
            {
                Console.WriteLine("     key down is pressed");
                return true;
                if ((Input.GetKeyDown(KeyCode.S) && !downPressed) || (Input.GetKeyDown(KeyCode.DownArrow) && !downPressed))
                {
                    downPressed = true;
                    return true;
                }
                if (Input.GetKeyUp(KeyCode.S) && Input.GetKeyUp(KeyCode.DownArrow) && downPressed)
                {
                    downPressed = false;
                }
                return false;//*/
            }
            public bool CF_Input_IsRightPressedOnce(PMGActor actor, PMGValueStack localStack)
            {
                Console.WriteLine("     key right is  pressed");
                return true;
                if ((Input.GetKeyDown(KeyCode.D) && !rightPressed) || (Input.GetKeyDown(KeyCode.RightArrow) && !rightPressed))
                {
                    rightPressed = true;
                    return true;
                }
                if (Input.GetKeyUp(KeyCode.D) && Input.GetKeyUp(KeyCode.RightArrow) && rightPressed)
                {
                    rightPressed = false;
                }
                return false;
            }
            public bool CF_Dectection_IsUpASpace(PMGActor actor, PMGValueStack localStack)
            {
                if (Map.chart[actor.position[0],actor.position[1]-1]== 0)
                {
                    Console.WriteLine("     up is a space");
                    return true;
                }
                else
                {
                    Console.WriteLine("     up is a wall");
                    return false;
                }
            }
            public bool CF_Dectection_IsLeftASpace(PMGActor actor, PMGValueStack localStack)
            {
                if (Map.chart[actor.position[0]-1, actor.position[1]] == 0)
                {
                    Console.WriteLine("     left is a space");
                    return true;
                }
                else
                {
                    Console.WriteLine("     left is a wall");
                    return false;
                }
            }
            public bool CF_Dectection_IsDownASpace(PMGActor actor, PMGValueStack localStack)
            {
                if (Map.chart[actor.position[0], actor.position[1] + 1] == 0)
                {
                    Console.WriteLine("     down is a space");
                    return true;
                }
                else
                {
                    Console.WriteLine("     down is a wall");
                    return false;
                }
            }
            public bool CF_Dectection_IsRightASpace(PMGActor actor, PMGValueStack localStack)
            {
                if (Map.chart[actor.position[0]+1, actor.position[1]] == 0)
                {
                    Console.WriteLine("     right is a space");
                    return true;
                }
                else
                {
                    Console.WriteLine("     right is a wall");
                    return false;
                }
            }
        }
    }
}
