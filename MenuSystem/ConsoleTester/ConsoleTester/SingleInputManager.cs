using System;
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

        //function that ends the update loop when the game need to quit
        public bool IsGameQuit()
        {
            return quit;
        }
        //function that ends the update loop when the game need to quit
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
                }
                //remeber to reset after each key and send ref to the assosiated bool
                ResetBool(Key.W, ref wIsPressed);
                // s 
                if (Keyboard.IsKeyDown(Key.S) && !sIsPressed)
                {
                    sIsPressed = true;
                    //place desired function below
                    SingleMenuManager.Instance.MoveDown();
                }
                //remeber to reset after each key and send ref to the assosiated bool
                ResetBool(Key.S, ref sIsPressed);
                //-------------------------------------------------------------------//
                //-------------------------------------------------------------------// Up, down
                // w 
                if (Keyboard.IsKeyDown(Key.Up) && !upIsPressed)
                {
                    upIsPressed = true;
                    //place desired function below
                    SingleMenuManager.Instance.MoveUp();
                }
                //remeber to reset after each key and send ref to the assosiated bool
                ResetBool(Key.Up, ref upIsPressed);
                // s 
                if (Keyboard.IsKeyDown(Key.Down) && !downIsPressed)
                {
                    downIsPressed = true;
                    //place desired function below
                    SingleMenuManager.Instance.MoveDown();
                }
                //remeber to reset after each key and send ref to the assosiated bool
                ResetBool(Key.Down, ref downIsPressed);

                // print display to console
                if (Keyboard.IsKeyDown(Key.P) && !pIsPressed)
                {
                    //
                    pIsPressed = true;
                    //
                    Console.WriteLine("__________________");
                    SingleMenuManager.Instance.DisplayCurrent();
                    Console.WriteLine("__________________");        
                }
                //remeber to reset after each key and send ref to the assosiated bool
                ResetBool(Key.P, ref pIsPressed);
                //Console.WriteLine("IN MENU");
            }
            //NOT IN MENU
            else
            {
                //Console.WriteLine("NOT IN MENU");
            }
        }



        //test just to see if single input manager can use:
        //  SingleMenuManager.Instance.ActivateButton();
        //  SingleMenuManager.Instance.SelectFirstOrActiveButton();
        //without creating creating double trouble
        //Results: its can, woohoo
        public void RunMenuFromInputTest()
        {
            //these two are a force pair, when hitting/activating a button
            //  SingleMenuManager.Instance.ActivateButton();
            //  SingleMenuManager.Instance.SelectFirstOrActiveButton();
             
            //----------------------------------------------------------------------------------// simulation of a player operating the menu using the 3 otions: moveup, movedown, and activate
            //starts up and shows start menu
            SingleMenuManager.Instance.Setup();
            SingleMenuManager.Instance.DisplayCurrent();
            Console.WriteLine("__________________");

            //moves up in startmenu which selects >exit< and activates it
            SingleMenuManager.Instance.MoveUp();
            Console.WriteLine("__________________");
            SingleMenuManager.Instance.DisplayCurrent();
            Console.WriteLine("__________________");
            SingleMenuManager.Instance.ActivateButton();
            SingleMenuManager.Instance.SelectFirstOrActiveButton(); //this thing is ok but still annoying
            Console.WriteLine("__________________");
            SingleMenuManager.Instance.DisplayCurrent();
            Console.WriteLine("__________________");

            //Moves up in startmenu again which selects >options< and activates it
            SingleMenuManager.Instance.MoveUp();
            Console.WriteLine("__________________");
            SingleMenuManager.Instance.DisplayCurrent();
            Console.WriteLine("__________________");
            SingleMenuManager.Instance.ActivateButton();
            SingleMenuManager.Instance.SelectFirstOrActiveButton();
            Console.WriteLine("__________________");
            SingleMenuManager.Instance.DisplayCurrent();
            Console.WriteLine("__________________");

            //moves down in optionsmenu which selects >tryagain< and activates it
            SingleMenuManager.Instance.MoveDown();
            Console.WriteLine("__________________");
            SingleMenuManager.Instance.DisplayCurrent();
            Console.WriteLine("__________________");
            SingleMenuManager.Instance.ActivateButton();
            SingleMenuManager.Instance.SelectFirstOrActiveButton();
            Console.WriteLine("__________________");
            SingleMenuManager.Instance.DisplayCurrent();
            Console.WriteLine("__________________");

            //dosent move in optionsmenu, >tryagain< is still selected, activates it again
            SingleMenuManager.Instance.ActivateButton();
            SingleMenuManager.Instance.SelectFirstOrActiveButton();
            Console.WriteLine("__________________");
            SingleMenuManager.Instance.DisplayCurrent();
            Console.WriteLine("__________________");

            //moves down in options menu which selects >controls< and activates it
            SingleMenuManager.Instance.MoveDown();
            Console.WriteLine("__________________");
            SingleMenuManager.Instance.DisplayCurrent();
            Console.WriteLine("__________________");
            SingleMenuManager.Instance.ActivateButton();
            SingleMenuManager.Instance.SelectFirstOrActiveButton();
            Console.WriteLine("__________________");
            SingleMenuManager.Instance.DisplayCurrent();
            Console.WriteLine("__________________");

            //tries to moves down in credits menu but only >back< can be selected, proceeds to activates it
            SingleMenuManager.Instance.MoveDown();
            Console.WriteLine("__________________");
            SingleMenuManager.Instance.DisplayCurrent();
            Console.WriteLine("__________________");
            SingleMenuManager.Instance.ActivateButton();
            SingleMenuManager.Instance.SelectFirstOrActiveButton();
            Console.WriteLine("__________________");
            SingleMenuManager.Instance.DisplayCurrent();
            Console.WriteLine("__________________");

            //moves down 3 times in options menu which selects >credits< and activates it
            SingleMenuManager.Instance.MoveDown();
            Console.WriteLine("__________________");
            SingleMenuManager.Instance.DisplayCurrent();
            Console.WriteLine("__________________");
            SingleMenuManager.Instance.MoveDown();
            Console.WriteLine("__________________");
            SingleMenuManager.Instance.DisplayCurrent();
            Console.WriteLine("__________________");
            SingleMenuManager.Instance.MoveDown();
            Console.WriteLine("__________________");
            SingleMenuManager.Instance.DisplayCurrent();
            Console.WriteLine("__________________");
            SingleMenuManager.Instance.ActivateButton();
            SingleMenuManager.Instance.SelectFirstOrActiveButton();
            Console.WriteLine("__________________");
            SingleMenuManager.Instance.DisplayCurrent();
            Console.WriteLine("__________________");

            // in creditsmenu, activates >back<
            SingleMenuManager.Instance.ActivateButton();
            SingleMenuManager.Instance.SelectFirstOrActiveButton();
            Console.WriteLine("__________________");
            SingleMenuManager.Instance.DisplayCurrent();
            Console.WriteLine("__________________");

            //moves up in optionsmenu which selects >back< and activates it
            SingleMenuManager.Instance.MoveUp();
            Console.WriteLine("__________________");
            SingleMenuManager.Instance.DisplayCurrent();
            Console.WriteLine("__________________");
            SingleMenuManager.Instance.ActivateButton();
            SingleMenuManager.Instance.SelectFirstOrActiveButton();
            Console.WriteLine("__________________");
            SingleMenuManager.Instance.DisplayCurrent();
            Console.WriteLine("__________________");

            //congratz on going full circle, menu system ready for more button action, input interaction, and nice display, all the rest is done

            //----------------------------------------------------------------------------------// end of simulation
        }
    }
}
