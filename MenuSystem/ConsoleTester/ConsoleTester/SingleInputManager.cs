﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UN_SingleMenuManager;
using System.Windows.Input;
using System.Windows.Forms;


namespace UN_SingleInputManager
{
    class SingleInputManager
    {
        //variales
        Boolean quit = false;
        Boolean isConsole = true;
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
        private void ResetBool(Key k, ref bool booleanRelatedToKey)
        {
            //---------------------------------------------------//resetter for all key
            //
            if (Keyboard.IsKeyUp(k) && booleanRelatedToKey)
            {
                //Console.WriteLine("fagit");
                booleanRelatedToKey = false;
            }
            //---------------------------------------------------//
        }
        //allows for user to set program to either be console or not at the beginning - works regardless of console or not
        public void Setup(Boolean runInConsole)
        {
            isConsole = runInConsole;
        }
        //update function 
        public void InputUpdate()
        {          
            //add keys here

            //Exits the program
            if (Keyboard.IsKeyDown(Key.Escape))
            {
                Console.WriteLine("quit was hit");
                GameQuit();
            }
            //IN MENU
            if (SingleMenuManager.Instance.NoMenuSelected())
            {
                //-------------------------------------------------------------------// w, s
                // w 
                if (Keyboard.IsKeyDown(Key.W) && !wIsPressed)
                {
                    wIsPressed = true;                    
                    //place desired function below
                    SingleMenuManager.Instance.MoveUp();
                    //displays the current console menu, if isConsole is true
                    if (isConsole)
                    {
                        SingleMenuManager.Instance.DisplayConsoleCurrent();
                    }
                }
                // s 
                if (Keyboard.IsKeyDown(Key.S) && !sIsPressed)
                {
                    sIsPressed = true;
                    //place desired function below
                    SingleMenuManager.Instance.MoveDown();
                    //displays the current console menu, if isConsole is true
                    if (isConsole)
                    {
                        SingleMenuManager.Instance.DisplayConsoleCurrent();
                    }
                }
                //-------------------------------------------------------------------//

                //-------------------------------------------------------------------// Up, down
                // up
                if (Keyboard.IsKeyDown(Key.Up) && !upIsPressed)
                {
                    upIsPressed = true;
                    //place desired function below
                    SingleMenuManager.Instance.MoveUp();
                    //displays the current console menu, if isConsole is true
                    if (isConsole)
                    {
                        SingleMenuManager.Instance.DisplayConsoleCurrent();
                    }
                }
                // down 
                if (Keyboard.IsKeyDown(Key.Down) && !downIsPressed)
                {
                    downIsPressed = true;
                    //place desired function below
                    SingleMenuManager.Instance.MoveDown();
                    //displays the current console menu, if isConsole is true
                    if (isConsole)
                    {
                        SingleMenuManager.Instance.DisplayConsoleCurrent();
                    }
                }
                //-------------------------------------------------------------------//

                //-------------------------------------------------------------------// enter, space
                // enter 
                if (Keyboard.IsKeyDown(Key.Enter) && !enterIsPressed)
                {
                    enterIsPressed = true;
                    //place desired function below
                    SingleMenuManager.Instance.ActivateButton();
                    SingleMenuManager.Instance.SelectFirstOrActiveButton();
                    //displays the current console menu, if isConsole is true
                    if (isConsole)
                    {
                        SingleMenuManager.Instance.DisplayConsoleCurrent();
                    }
                }
                // space 
                if (Keyboard.IsKeyDown(Key.Space) && !spaceIsPressed)
                {
                    spaceIsPressed = true;
                    //place desired function below
                    SingleMenuManager.Instance.ActivateButton();
                    SingleMenuManager.Instance.SelectFirstOrActiveButton();
                    //displays the current console menu, if isConsole is true
                    if (isConsole)
                    {
                        SingleMenuManager.Instance.DisplayConsoleCurrent();
                    }
                }
                //-------------------------------------------------------------------//

                //-------------------------------------------------------------------// reseting the bools for each key, this used for only pressing once
                ResetBool(Key.W, ref wIsPressed);
                ResetBool(Key.S, ref sIsPressed);
                ResetBool(Key.Up, ref upIsPressed);
                ResetBool(Key.Down, ref downIsPressed);
                ResetBool(Key.Enter, ref enterIsPressed);
                ResetBool(Key.Space, ref spaceIsPressed);
                //-------------------------------------------------------------------//
            }
            //NOT IN MENU
            else
            {
                //insert code containing the controls needed for game aka : w,a,s,d - up,left,down,right - enter,space - escape

                //escape key should bring you to options menu and pause the game using SingleMenuManager.Instance.Pause();


            }
        }    
    }
}
