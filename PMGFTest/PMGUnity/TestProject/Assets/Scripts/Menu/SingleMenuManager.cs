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

        //rotation vector for turning the menu ins a specific direction 
        Vector3 MenuRotation;
        Vector4 DefaultColor;
        Vector4 TintColor;

       


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
        public void Setup(Boolean runInDebug)
        {
            //-------------------------------------------------// setup begin (change buttons and menue and shit here)
            //-------//rotation of buttons and menues display surfaces(unity stuff)
            MenuRotation = new Vector3(90, 180, 0);

            //color
            DefaultColor = new Vector4(0.0f,1.0f,0.0f,1.0f);
            TintColor = new Vector4(1.0f, 0.0f, 0.0f, 1.0f);
            //-------//
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
            MainMenuButtons.Add(new Button("Start", 1, 0, -3));
            MainMenuButtons.Add(new Button("Options", 3, 0, -6));
            MainMenuButtons.Add(new Button("Exit", 0, -9));
            MainMenu = new Menu("Welcome to PMGF", MainMenuButtons, 0, 0);
            //start menu 
            StartMenuButtons.Add(new Button("New Game", 0, -3));
            StartMenuButtons.Add(new Button("Previous Game", 0, -6));
            StartMenuButtons.Add(new Button("Back", 0, 0, -9));
            StartMenu = new Menu("START", StartMenuButtons, 0, 0);
            //restart menu 
            RestartMenuButtons.Add(new Button("New Game", 0, -3));
            RestartMenuButtons.Add(new Button("Previous Game", 0, -6));
            RestartMenuButtons.Add(new Button("Exit", 0, -9));
            if (runInDebug)
            {
                RestartMenuButtons.Add(new Button("Back", 6, 0, -12));
            }
            RestartMenu = new Menu("RESTART", RestartMenuButtons, 0, 0);
            //options menu
            OptionsMenuButtons.Add(new Button("Controls", 4, 0, -3));
            OptionsMenuButtons.Add(new Button("Credits", 5, 0, -6));
            if (runInDebug)
            {
                OptionsMenuButtons.Add(new Button("Debug", 7, 0, -9));
                OptionsMenuButtons.Add(new Button("Back", 0, 0, -12));
            }
            else
            {
                OptionsMenuButtons.Add(new Button("Back", 0, 0, -9));
            }
            OptionsMenu = new Menu("OPTIONS", OptionsMenuButtons, 0, 0);
            //
            //controls menu
            ControlsMenuButtons.Add(new Button("Back", 3, 0, -3));
            ControlsMenu = new Menu("CONTROLS", ControlsMenuButtons, 0, 0);
            //credits menu
            CreditsMenuButtons.Add(new Button("Back", 3, 0, -3));
            CreditsMenu = new Menu("CREDITS", CreditsMenuButtons, 0, 0);
            //End Menu
            EndMenuButtons.Add(new Button("Restart", 2, 0, -3));
            EndMenuButtons.Add(new Button("Exit", 0, -6));
            if (runInDebug)
            {
                EndMenuButtons.Add(new Button("Back", 7, 0, -9));
            }
            EndMenu = new Menu("GAME OVER", EndMenuButtons, 0, 0);
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
                DebugMenuButtons.Add(new Button("Game Over", 6, 0, -3));
                DebugMenuButtons.Add(new Button("Back", 3, 0, -6));
                DebugMenu = new Menu("DEBUG", DebugMenuButtons, 0, 0);
                Menues.Add(DebugMenu);
            }
            //-------------------------------------------------// setup end
            SingleMenuManager.Instance.Feed(Menues);
            SingleMenuManager.Instance.Begin();
            SingleMenuManager.Instance.InstantiateCurrent();
            //SingleMenuManager.Instance.DisplayUnitySelected();
        }
        public Vector3 LeRotation()
        {
            return MenuRotation;
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
                Debug.Log("The list of menues have not been added to the single menu manager, as such this function cannot be run, please check if the function: SingleMenuManager.Instance.Feed(menuList of your choice), has been used in the single menu manager setup function");
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------// 
        //pauses the game
        public void Pause()
        {
            // somehow pause the game in here
        }
        //
        public void DisplayUnitySelected()
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
                        Debug.Log(e.menuName);
                        //---------------------------------------// unity find and show selected button
                         foreach (Button e2 in e.GetButtonList())
                         {
                             Debug.Log(e2.buttonName);
                             GameObject.Find(e2.buttonName).GetComponent<Renderer>().material.color = DefaultColor;
                             if (e2.IsSelected())
                             {
                                Debug.Log("got colored");
                                 //tints the object for the selected button
                                 GameObject.FindGameObjectWithTag(e2.buttonName).GetComponent<Renderer>().material.color = TintColor;
                             }
                         }
                         //---------------------------------------//*/
                    }
                }
            }
            else
            {
                //error message here
                Console.WriteLine("The list of menues have not been added to the single menu manager, as such this function cannot be run, please check if the function: SingleMenuManager.Instance.Feed(menuList of your choice), has been used in the single menu manager setup function");
                Debug.Log("The list of menues have not been added to the single menu manager, as such this function cannot be run, please check if the function: SingleMenuManager.Instance.Feed(menuList of your choice), has been used in the single menu manager setup function");
            }
        }
        //
        public void InstantiateCurrent()
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
                        e.InstantiateMenu();
                        e.InstantiateMenuButtons();
                    }
                }
            }
            else
            {
                //error message here
                Console.WriteLine("The list of menues have not been added to the single menu manager, as such this function cannot be run, please check if the function: SingleMenuManager.Instance.Feed(menuList of your choice), has been used in the single menu manager setup function");
                Debug.Log("The list of menues have not been added to the single menu manager, as such this function cannot be run, please check if the function: SingleMenuManager.Instance.Feed(menuList of your choice), has been used in the single menu manager setup function");
            }
        }
        //destroys any menu object found, to clear out for new instantiation
        public void DestroyAll()
        {
            //checks to secure that function can be run if menulist is empty
            if (menuList != null)
            {
                //display currently active menu, if there is any
                //will work fine so long as no two menues are selected at the same time
                foreach (Menu e in menuList)
                {
                    if (true)
                    {
                        e.DestroyMenu();
                        e.DestroyButtons();
                    }
                    else
                    {
                        Debug.Log("unity is a fucking fagit");
                    }
                    
                }
            }
            else
            {
                //error message here
                Console.WriteLine("The list of menues have not been added to the single menu manager, as such this function cannot be run, please check if the function: SingleMenuManager.Instance.Feed(menuList of your choice), has been used in the single menu manager setup function");
                Debug.Log("The list of menues have not been added to the single menu manager, as such this function cannot be run, please check if the function: SingleMenuManager.Instance.Feed(menuList of your choice), has been used in the single menu manager setup function");
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
                Debug.Log("The list of menues have not been added to the single menu manager, as such this function cannot be run, please check if the function: SingleMenuManager.Instance.Feed(menuList of your choice), has been used in the single menu manager setup function");
            }
        }
        //deselects all menues
        public void DeSelectAllM()
        {
            //checks to secure that function can be run if menulist is empty
            if (menuList != null)
            {
                foreach (Menu e in menuList)
                {
                    e.DeSelect();
                }
            }
            else
            {
                //error message here
                Console.WriteLine("The list of menues have not been added to the single menu manager, as such this function cannot be run, please check if the function: SingleMenuManager.Instance.Feed(menuList of your choice), has been used in the single menu manager setup function");
                Debug.Log("The list of menues have not been added to the single menu manager, as such this function cannot be run, please check if the function: SingleMenuManager.Instance.Feed(menuList of your choice), has been used in the single menu manager setup function");
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
                Debug.Log("The list of menues have not been added to the single menu manager, as such this function cannot be run, please check if the function: SingleMenuManager.Instance.Feed(menuList of your choice), has been used in the single menu manager setup function");
                return false;
            }
        }
        //annoying this function cant be run inside the classes without dual button activation, i have no clue why this is the case, but after testing all possible location with no luck im left to leave it in the main
        //needed for when activating a button from input to set the selected in the new menu
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
                Debug.Log("The list of menues have not been added to the single menu manager, as such this function cannot be run, please check if the function: SingleMenuManager.Instance.Feed(menuList of your choice), has been used in the single menu manager setup function");
            }
            //unity selector
            //SingleMenuManager.Instance.DisplayUnitySelected();
        }
        //selects a chosen menu using ints to refer to the position in the menulist
        public void SelectMenu(int MenuRowNumber)
        {
            //checks to secure that function can be run if menulist is empty
            if (menuList != null)
            {
                DeSelectAllM();
                menuList[MenuRowNumber].Select();
            }
            else
            {
                //error message here
                Console.WriteLine("The list of menues have not been added to the single menu manager, as such this function cannot be run, please check if the function: SingleMenuManager.Instance.Feed(menuList of your choice), has been used in the single menu manager setup function");
                Debug.Log("The list of menues have not been added to the single menu manager, as such this function cannot be run, please check if the function: SingleMenuManager.Instance.Feed(menuList of your choice), has been used in the single menu manager setup function");
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
                Debug.Log("The list of menues have not been added to the single menu manager, as such this function cannot be run, please check if the function: SingleMenuManager.Instance.Feed(menuList of your choice), has been used in the single menu manager setup function");
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
                Debug.Log("The list of menues have not been added to the single menu manager, as such this function cannot be run, please check if the function: SingleMenuManager.Instance.Feed(menuList of your choice), has been used in the single menu manager setup function");
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
                Debug.Log("The list of menues have not been added to the single menu manager, as such this function cannot be run, please check if the function: SingleMenuManager.Instance.Feed(menuList of your choice), has been used in the single menu manager setup function");
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
                Debug.Log("The list of menues have not been added to the single menu manager, as such this function cannot be run, please check if the function: SingleMenuManager.Instance.Feed(menuList of your choice), has been used in the single menu manager setup function");
            }
        }
        //----------------------------------------------------------------------------------------------------------------------------------//
    }
}

