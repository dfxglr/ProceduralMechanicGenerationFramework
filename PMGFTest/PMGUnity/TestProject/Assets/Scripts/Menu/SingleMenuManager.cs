using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using UN_Menu;
using UN_Button;
using UnityEngine;
using System.Collections;

namespace UN_SingleMenuManager
{
    public sealed class SingleMenuManager : MonoBehaviour
    {
        //variables 
        List<Menu> menuList;
        //(selectCount isnt used much other than in void Begin and even there is only used a few times, potential to change this) 
        //Boolean selectMenu = true;

       


        //-------------------------------------------------// singleton thread safe with no lock (type 4 from http://csharpindepth.com/Articles/General/Singleton.aspx)
        private static readonly SingleMenuManager instance = new SingleMenuManager();
        // Explicit static constructor to tell C# compiler not to mark type as 'beforefieldinit'
        static SingleMenuManager()
        {
        }
        private SingleMenuManager()
        {
        }
        public static SingleMenuManager Instance
        {
            get
            {
                return instance;
            }
        }
        //-------------------------------------------------//
        //the following are My added functions

        //
        public void SetupGame(Boolean runInDebug)
        {
            //-------------------------------------------------// setup begin (change buttons and menue and shit here)
            //menues
            Menu MainMenu;
            Menu StartMenu;
            Menu RestartMenu;
            Menu OptionsMenu;
            Menu ControlsMenu;
            Menu CreditsMenu;
            Menu EndMenu;
            //lists for buttons and menues
            List<Button> MainMenuButtons = new List<Button>();
            List<Button> StartMenuButtons = new List<Button>();
            List<Button> RestartMenuButtons = new List<Button>();
            List<Button> OptionsMenuButtons = new List<Button>();
            List<Button> ControlsMenuButtons = new List<Button>();
            List<Button> CreditsMenuButtons = new List<Button>();
            List<Button> EndMenuButtons = new List<Button>();
            List<Menu> Menues = new List<Menu>();
            //Main Menu
            MainMenuButtons.Add(new Button("Start", 1, 10, 10));
            MainMenuButtons.Add(new Button("Options", 3, 10, 15));
            MainMenuButtons.Add(new Button("Exit", 10, 20));
            MainMenu = new Menu("Welcome to PMGF", MainMenuButtons, 20, 50);
            //start menu 
            StartMenuButtons.Add(new Button("New Game", 10, 10));
            StartMenuButtons.Add(new Button("Previous Game", 10, 15));
            StartMenuButtons.Add(new Button("Back", 0, 10, 20));
            StartMenu = new Menu("START:", StartMenuButtons, 20, 50);
            //restart menu 
            RestartMenuButtons.Add(new Button("New Game", 10, 10));
            RestartMenuButtons.Add(new Button("Previous Game", 10, 15));
            RestartMenuButtons.Add(new Button("Exit", 10, 20));
            RestartMenu = new Menu("RESTART:", RestartMenuButtons, 20, 50);
            //options menu
            OptionsMenuButtons.Add(new Button("Controls", 4, 10, 20));
            OptionsMenuButtons.Add(new Button("Credits", 5, 10, 25));
            if (runInDebug)
            {
                OptionsMenuButtons.Add(new Button("Debug", 7, 10, 25));
            }
            OptionsMenuButtons.Add(new Button("Back", 0, 10, 30));
            OptionsMenu = new Menu("OPTIONS:", OptionsMenuButtons, 20, 50);
            //
            //controls menu
            ControlsMenuButtons.Add(new Button("Back", 3, 10, 10));
            ControlsMenu = new Menu("CONTROLS:", ControlsMenuButtons, 20, 50);
            //credits menu
            CreditsMenuButtons.Add(new Button("Back", 3, 10, 10));
            CreditsMenu = new Menu("CREDITS", CreditsMenuButtons, 20, 50);
            //End Menu
            EndMenuButtons.Add(new Button("Restart", 2, 10, 10));
            EndMenuButtons.Add(new Button("Exit", 10, 20));
            EndMenu = new Menu("GAME OVER:", EndMenuButtons, 20, 50);
            //adds to the list of menues - have main menu in the beginning always plz, dont be dick 
            Menues.Add(MainMenu);
            Menues.Add(StartMenu);
            Menues.Add(RestartMenu);
            Menues.Add(OptionsMenu);
            Menues.Add(ControlsMenu);
            Menues.Add(CreditsMenu);
            Menues.Add(EndMenu);
            //debug menu
            if (runInDebug)
            {
                Menu DebugMenu;
                //
                List<Button> DebugMenuButtons = new List<Button>();
                //Add degub buttons here
                DebugMenuButtons.Add(new Button("Game Over", 6, 10, 10));
                DebugMenuButtons.Add(new Button("Back", 3, 10, 10));
                DebugMenu = new Menu("DEBUG:", DebugMenuButtons, 20, 50);
                Menues.Add(DebugMenu);
            }
            //-------------------------------------------------// setup end
            SingleMenuManager.Instance.Feed(Menues);
            SingleMenuManager.Instance.Begin();

        }
        //debug console setup
        public void SetupConsole(Boolean runInDebug)
        {
            //-------------------------------------------------// setup begin (change buttons and menue and shit here)
            //menues
            Menu MainMenu;
            Menu StartMenu;
            Menu RestartMenu;
            Menu OptionsMenu;
            Menu ControlsMenu;
            Menu CreditsMenu;
            Menu EndMenu;
            //lists for buttons and menues
            List<Button> MainMenuButtons = new List<Button>();
            List<Button> StartMenuButtons = new List<Button>();
            List<Button> RestartMenuButtons = new List<Button>();
            List<Button> OptionsMenuButtons = new List<Button>();
            List<Button> ControlsMenuButtons = new List<Button>();
            List<Button> CreditsMenuButtons = new List<Button>();
            List<Button> EndMenuButtons = new List<Button>();
            List<Menu> Menues = new List<Menu>();
            //Main Menu
            MainMenuButtons.Add(new Button("Start", 1));
            MainMenuButtons.Add(new Button("Options", 3));
            MainMenuButtons.Add(new Button("Exit"));
            MainMenu = new Menu("Welcome to PMGF", MainMenuButtons);
            //start menu 
            StartMenuButtons.Add(new Button("New Game"));
            StartMenuButtons.Add(new Button("Previous Game"));
            StartMenuButtons.Add(new Button("Back", 0));
            StartMenu = new Menu("START:", StartMenuButtons);
            //restart menu 
            RestartMenuButtons.Add(new Button("New Game"));
            RestartMenuButtons.Add(new Button("Previous Game"));
            RestartMenuButtons.Add(new Button("Exit"));
            RestartMenu = new Menu("RESTART:", RestartMenuButtons);
            //options menu
            OptionsMenuButtons.Add(new Button("Controls", 4));
            OptionsMenuButtons.Add(new Button("Credits", 5));
            if (runInDebug)
            {
                OptionsMenuButtons.Add(new Button("Debug", 7));
            }
            OptionsMenuButtons.Add(new Button("Back", 0));
            OptionsMenu = new Menu("OPTIONS:", OptionsMenuButtons);
            //
            //controls menu
            ControlsMenuButtons.Add(new Button("Back", 3));
            ControlsMenu = new Menu("CONTROLS:", ControlsMenuButtons);
            //credits menu
            CreditsMenuButtons.Add(new Button("Back", 3));
            CreditsMenu = new Menu("CREDITS", CreditsMenuButtons);
            //End Menu
            EndMenuButtons.Add(new Button("Restart", 2));
            EndMenuButtons.Add(new Button("Exit"));
            EndMenu = new Menu("GAME OVER:", EndMenuButtons);
            //adds to the list of menues - have main menu in the beginning always plz, dont be dick 
            Menues.Add(MainMenu);
            Menues.Add(StartMenu);
            Menues.Add(RestartMenu);
            Menues.Add(OptionsMenu);
            Menues.Add(ControlsMenu);
            Menues.Add(CreditsMenu);
            Menues.Add(EndMenu);
            //debug menu
            if (runInDebug)
            {
                Menu DebugMenu;
                //
                List<Button> DebugMenuButtons = new List<Button>();
                //Add degub buttons here
                DebugMenuButtons.Add(new Button("Game Over", 6, 10, 10));
                DebugMenuButtons.Add(new Button("Back", 3, 10, 10));
                DebugMenu = new Menu("DEBUG:", DebugMenuButtons, 20, 50);
                Menues.Add(DebugMenu);
            }

            //-------------------------------------------------// setup end
            SingleMenuManager.Instance.Feed(Menues);
            SingleMenuManager.Instance.Begin();
            SingleMenuManager.instance.DisplayConsoleCurrent();
        }

        //feed the single menu manager with stuff from the setup function
        private void Feed(List<Menu> M)
        {
            //adds the list of menues created in the setup function
            menuList = M;
        }    
        //selects the first menu in the menu list to be the active, if no other menu isset to be active
        public void Begin()
        {
            //checks to secure that function can be run if menulist is empty
            if (menuList != null)
            {
                DeSelectAllM();
                menuList[0].Select();
                
                menuList[0].SelectButtonZeroOrActiveB();
            }
            else
            {
                //error message here
                Console.WriteLine("The list of menues have not been added to the single menu manager, as such this function cannot be run, please check if the function: SingleMenuManager.Instance.Feed(menuList of your choice), has been used in the single menu manager setup function");
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------// 
        //pauses the game
        public void Pause()
        {
            // somehow pause the game in here
        }
        //displays the currently active menu
        public void DisplayCurrent()
        {
            //checks to secure that function can be run if menulist is empty
            if (menuList != null)
            {
                //display currently active menu, if there is any
                //will work fine so long as no two menues are selected at the same time
                foreach (Menu e in menuList)
                {
                    if (e.IsSelected())
                    {
                        e.DisplayM();
                        e.DisplayButtons();
                    }
                }
            }
            else
            {
                //error message here
                Console.WriteLine("The list of menues have not been added to the single menu manager, as such this function cannot be run, please check if the function: SingleMenuManager.Instance.Feed(menuList of your choice), has been used in the single menu manager setup function");
            }
        }
        // displays the currently active console menu
        public void DisplayConsoleCurrent()
        {

            //checks to secure that function can be run if menulist is empty
            if (menuList != null)
            {
                //display currently active menu, if there is any
                //will work fine so long as no two menues are selected at the same time
                foreach (Menu e in menuList)
                {
                    if (e.IsSelected())
                    {
                        Console.WriteLine("__________________");
                        e.DisplayConsole();
                        e.DisplayConsoleButtons();
                        Console.WriteLine("__________________");
                    }
                }
            }
            else
            {
                //error message here
                Console.WriteLine("The list of menues have not been added to the single menu manager, as such this function cannot be run, please check if the function: SingleMenuManager.Instance.Feed(menuList of your choice), has been used in the single menu manager setup function");
            }
        }
        //deselects all menues
        public void DeSelectAllM()
        {
            foreach (Menu e in menuList)
            {
                e.DeSelect();
            }
        }
        //returns true if no menu in the list is set to selected
        public bool NoMenuSelected()
        {
            //use a return function to get the menurownumber which is equal to the relatedmenu value
            if (menuList != null)
            {
                foreach (Menu e in menuList)
                {
                    if (e.IsSelected())
                    {
                        return true;
                    }
                }
                return false;
            }
            else
            {
                //error message here
                Console.WriteLine("The list of menues have not been added to the single menu manager, as such this function: NoMenuSelected(), in SinngleMenuManager cannot be run");
                Console.WriteLine("since no menues were found, input switches to default, which is the 'NOT IN MENU', in SingleInputManager");
                return false;
            }
        }
        //annoying this function cant be run inside the classes without dual button activation, i have no clue why this is the case, but after testing all possible location with no luck im left to leave it in the main
        public void SelectFirstOrActiveButton()
        {
            //use a return function to get the menurownumber which is equal to the relatedmenu value
            if (menuList != null)
            {
                foreach (Menu e in menuList)
                {
                    if (e.IsSelected())
                    {
                        e.SelectButtonZeroOrActiveB();
                    }
                    else
                    {
                        e.DeSelectAllB();
                    }
                }
            }
            else
            {
                //error message here
                Console.WriteLine("The list of menues have not been added to the single menu manager, as such this function cannot be run, please check if the function: SingleMenuManager.Instance.Feed(menuList of your choice), has been used in the single menu manager setup function");
            }
        }
        //selects a chosen menu using ints to refer to the position in the menulist
        public void SelectMenu(int MenuRowNumber)
        {
            //checks to secure that function can be run if menulist is empty
            if (menuList != null)
            {
                //Console.WriteLine("i hope i dont repeat");
                DeSelectAllM();
                //menuList[MenuRowNumber].SelectButtonZero();
                menuList[MenuRowNumber].Select();
                //cannot be called wihtout going full retard
                //SelectFirstOrActiveButton();
            }
            else
            {
                //error message here
                Console.WriteLine("The list of menues have not been added to the single menu manager, as such this function cannot be run, please check if the function: SingleMenuManager.Instance.Feed(menuList of your choice), has been used in the single menu manager setup function");
            }            
        }
        //returns the number of menues
        public int MenuCount()
        {
            //checks to secure that function can be run if menulist is empty
            if (menuList != null)
            {
                return menuList.Count;
            }
            else
            {
                //error message here
                Console.WriteLine("The list of menues have not been added to the single menu manager, as such this function cannot be run, please check if the function: SingleMenuManager.Instance.Feed(menuList of your choice), has been used in the single menu manager setup function");
                return 0;
            }
        }
        //activates the action of the currently selected button 
        public void ActivateButton()
        {
            //
            if (menuList != null)
            {
                foreach (Menu e in menuList)
                {
                    if (e.IsSelected())
                    {
                        e.UseButton();
                    }
                }
            }
            else
            {
                //error message here
                Console.WriteLine("The list of menues have not been added to the single menu manager, as such this function cannot be run, please check if the function: SingleMenuManager.Instance.Feed(menuList of your choice), has been used in the single menu manager setup function");
            }
        }
        //selects the above below the the currently active button
        public void MoveUp()
        {
            //
            if (menuList != null)
            {
                foreach (Menu e in menuList)
                {
                    if (e.IsSelected())
                    {
                        e.SelectUp();
                    }
                }
            }
            else
            {
                //error message here
                Console.WriteLine("The list of menues have not been added to the single menu manager, as such this function cannot be run, please check if the function: SingleMenuManager.Instance.Feed(menuList of your choice), has been used in the single menu manager setup function");
            }
        }
        //selects the button below the the currently active button
        public void MoveDown()
        {
            //
            if (menuList != null)
            {
                foreach (Menu e in menuList)
                {
                    if (e.IsSelected())
                    {
                        e.SelectDown();
                    }
                }
            }
            else
            {
                //error message here
                Console.WriteLine("The list of menues have not been added to the single menu manager, as such this function cannot be run, please check if the function: SingleMenuManager.Instance.Feed(menuList of your choice), has been used in the single menu manager setup function");
            }
        }
        //----------------------------------------------------------------------------------------------------------------------------------//
    }
}

