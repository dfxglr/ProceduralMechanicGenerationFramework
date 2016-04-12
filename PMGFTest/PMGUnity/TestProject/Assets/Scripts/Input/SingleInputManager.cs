using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using UN_SingleMenuManager;
//using System.Windows.Input;
//using System.Windows.Forms;
using UnityEngine;
using System.Collections;


namespace UN_SingleInputManager
{
    class SingleInputManager : MonoBehaviour
    {
        //variales
        Boolean quit = false;
        Boolean withConsole = true;
        //Boolean inMenu = true;
        //booleans use for single press action with buttons, keyEventargs class could obsolete the use of these, but is being a dick for now reason so fuck it
        
        //print display to console
        Boolean pIsPressed = false;
        //keys w,a,s,d
        Boolean wIsPressed = false;
        Boolean aIsPressed = false;
        Boolean sIsPressed = false;
        Boolean dIsPressed = false;
        //arrow keys , up, down, left, right
        Boolean upIsPressed = false;
        Boolean downIsPressed = false;
        Boolean leftIsPressed = false;
        Boolean rightIsPressed = false;
        // keys enter and space
        Boolean enterIsPressed = false;
        Boolean spaceIsPressed = false;
        // key escape
        Boolean escapeIsPressed = false;

        //-------------------------------------------------// singleton thread safe with no lock (type 4 from http://csharpindepth.com/Articles/General/Singleton.aspx)
        private static readonly SingleInputManager instance = new SingleInputManager();
        // Explicit static constructor to tell C# compiler not to mark type as 'beforefieldinit'
        static SingleInputManager()
        {
        }
        private SingleInputManager()
        {
        }
        public static SingleInputManager Instance
        {
            get
            {
                return instance;
            }
        }
        //-------------------------------------------------//
        //the following are My added functions

        //gets if game/program is quitting
        public bool IsGameQuit()
        {
            return quit;
        }
        //exits the program
        public void GameQuit()
        {
            quit = true;
        }
        //reset Boolean to false
        private void ResetBool(KeyCode k, ref bool booleanRelatedToKey)
        {
            //---------------------------------------------------//resetter for all key
            //code changed here to fit unity shit face programs
            if (Input.GetKeyUp(k) && booleanRelatedToKey)
            {
                //Console.WriteLine("fagit");
                booleanRelatedToKey = false;
                
            }
            //---------------------------------------------------//
        }
        //allows for user to set program to either be console or not at the beginning - works regardless of console or not
        public void Setup(Boolean runWithConsole)
        {
            withConsole = runWithConsole;
        }
        //update function 
        public void InputUpdate()
        {          
            //add keys here

            //Exits the program
            
            //IN MENU
            if (SingleMenuManager.Instance.IsMenuSelected())
            {
                //-------------------------------------------------------------------// w, s
                // w 
                if (Input.GetKeyDown(KeyCode.W) && !wIsPressed)
                {
                    wIsPressed = true;                    
                    //place desired function below
                    SingleMenuManager.Instance.MoveUp();
                    //unity selector update
                    SingleMenuManager.Instance.DisplayUnitySelected();
                    //displays the current console menu, if isConsole is true
                    if (withConsole)
                    {
                        SingleMenuManager.Instance.DisplayConsoleCurrent();
                    }
                }
                // s 
                if (Input.GetKeyDown(KeyCode.S) && !sIsPressed)
                {
                    sIsPressed = true;
                    //place desired function below
                    SingleMenuManager.Instance.MoveDown();
                    //unity selector update
                    SingleMenuManager.Instance.DisplayUnitySelected();
                    //displays the current console menu, if isConsole is true
                    if (withConsole)
                    {
                        SingleMenuManager.Instance.DisplayConsoleCurrent();
                    }
                }
                //-------------------------------------------------------------------//

                //-------------------------------------------------------------------// Up, down
                // up
                if (Input.GetKeyDown(KeyCode.UpArrow) && !upIsPressed)
                {
                    upIsPressed = true;
                    //place desired function below
                    SingleMenuManager.Instance.MoveUp();
                    //unity selector update
                    SingleMenuManager.Instance.DisplayUnitySelected();
                    //displays the current console menu, if isConsole is true
                    if (withConsole)
                    {
                        SingleMenuManager.Instance.DisplayConsoleCurrent();
                    }
                }
                // down 
                if (Input.GetKeyDown(KeyCode.DownArrow) && !downIsPressed)
                {
                    downIsPressed = true;
                    //place desired function below
                    SingleMenuManager.Instance.MoveDown();
                    //unity selector update
                    SingleMenuManager.Instance.DisplayUnitySelected();
                    //displays the current console menu, if isConsole is true
                    if (withConsole)
                    {
                        SingleMenuManager.Instance.DisplayConsoleCurrent();
                    }
                }
                //-------------------------------------------------------------------//

                //-------------------------------------------------------------------// enter, space
                // enter 
                // return key = enter, because unity is retarded
                if (Input.GetKeyDown(KeyCode.Return) && !enterIsPressed)
                {
                    enterIsPressed = true;
                    //place desired function below
                    SingleMenuManager.Instance.ActivateButton(withConsole);
                    //in case the button swaps to new menu
                    SingleMenuManager.Instance.SelectFirstOrActiveButton();
                    //unity selector update
                    SingleMenuManager.Instance.DisplayUnitySelected();
                    //displays the current console menu, if isConsole is true
                    if (withConsole)
                    {
                        SingleMenuManager.Instance.DisplayConsoleCurrent();
                    }
                }
                // space 
                if (Input.GetKeyDown(KeyCode.Space) && !spaceIsPressed)
                {
                    spaceIsPressed = true;
                    //place desired function below
                    SingleMenuManager.Instance.ActivateButton(withConsole);
                    //in case the button swaps to new menu
                    SingleMenuManager.Instance.SelectFirstOrActiveButton();
                    //unity selector update
                    SingleMenuManager.Instance.DisplayUnitySelected();
                    //displays the current console menu, if isConsole is true
                    if (withConsole)
                    {
                        SingleMenuManager.Instance.DisplayConsoleCurrent();

                    }
                }
                //-------------------------------------------------------------------//

                //-------------------------------------------------------------------// reseting the bools for each key, this used for only pressing once
                ResetBool(KeyCode.W, ref wIsPressed);
                ResetBool(KeyCode.S, ref sIsPressed);
                ResetBool(KeyCode.UpArrow, ref upIsPressed);
                ResetBool(KeyCode.DownArrow, ref downIsPressed);
                ResetBool(KeyCode.Return, ref enterIsPressed);
                ResetBool(KeyCode.Space, ref spaceIsPressed);
                //-------------------------------------------------------------------//
            }
            //NOT IN MENU
            else
            {
                //insert code containing the controls needed for game aka : w,a,s,d - up,left,down,right - enter,space - escape

                //escape key should bring you to options menu and pause the game using SingleMenuManager.Instance.Pause();

                //basically place what is needed as game input here
                //-------------------------------------------------------------------// escape for options menu
                if (Input.GetKeyDown(KeyCode.Escape) && !escapeIsPressed)
                {
                    escapeIsPressed = true;
                    //place desired function below
                    SingleMenuManager.Instance.SelectMenu(3);
                    //in case the button swaps to new menu
                    SingleMenuManager.Instance.SelectFirstOrActiveButton();
                    //instantiates the selected menu
                    SingleMenuManager.Instance.InstantiateCurrent();
                    //unity selector update
                    SingleMenuManager.Instance.DisplayUnitySelected();
                    //displays the current console menu, if isConsole is true
                    if (withConsole)
                    {
                        SingleMenuManager.Instance.DisplayConsoleCurrent();

                    }
                }
                //-------------------------------------------------------------------//



                //-------------------------------------------------------------------//reseting the bools for each key, this used for only pressing once
                ResetBool(KeyCode.Space, ref spaceIsPressed);
                //-------------------------------------------------------------------//
            }
        }    
    }
}
