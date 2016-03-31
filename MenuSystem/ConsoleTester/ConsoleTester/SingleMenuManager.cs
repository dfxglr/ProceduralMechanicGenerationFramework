using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UN_Menu;
using UN_Button;

namespace UN_SingleMenuManager
{
    public sealed class SingleMenuManager
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

        //setup function to be used and edited for desired menues and buttons
        public void Setup()
        {
            //-------------------------------------------------// setup begin (change buttons and menue and shit here)
            //menues
            Menu StartMenu;
            Menu OptionsMenu;
            Menu ControlsMenu;
            Menu CreditsMenu;
            Menu EndMenu;
            //lists for buttons and menues
            List<Button> StartMenuButtons = new List<Button>();
            List<Button> OptionsMenuButtons = new List<Button>();
            List<Button> ControlsMenuButtons = new List<Button>();
            List<Button> CreditsMenuButtons = new List<Button>();
            List<Button> EndMenuButtons = new List<Button>();
            List<Menu> Menues = new List<Menu>();
            //start menu 
            StartMenuButtons.Add(new Button("Start",  10, 10));
            StartMenuButtons.Add(new Button("Options", 1, 10, 15));
            StartMenuButtons.Add(new Button("Exit", 10, 20));
            StartMenu = new Menu("Welcome to PMGF", StartMenuButtons, 20, 50);
            //options menu
            OptionsMenuButtons.Add(new Button("Restart", 10, 10));
            OptionsMenuButtons.Add(new Button("TryAgain", 10, 15));
            OptionsMenuButtons.Add(new Button("Controls", 2, 10, 20));
            OptionsMenuButtons.Add(new Button("Credits", 3, 10, 20));
            OptionsMenuButtons.Add(new Button("Back", 0, 10, 20));
            OptionsMenu = new Menu("OPTIONS:", OptionsMenuButtons, 20, 50);
            //controls menu
            ControlsMenuButtons.Add(new Button("Back", 1, 10, 10));
            ControlsMenu = new Menu("CONTROLS:", ControlsMenuButtons, 20, 50);
            //credits menu
            CreditsMenuButtons.Add(new Button("Back", 1, 10, 10));
            CreditsMenu = new Menu("CREDITS", CreditsMenuButtons, 20, 50);
            //End Menu
            EndMenuButtons.Add(new Button("Restart", 10, 10));
            EndMenuButtons.Add(new Button("TryAgain", 10, 15));
            EndMenuButtons.Add(new Button("Exit", 10, 20));
            EndMenu = new Menu("GAME OVER:", EndMenuButtons, 20, 50);
            //adds to the list of menues
            Menues.Add(StartMenu);
            Menues.Add(OptionsMenu);
            Menues.Add(ControlsMenu);
            Menues.Add(CreditsMenu);
            Menues.Add(EndMenu);
            //-------------------------------------------------// setup end
            SingleMenuManager.Instance.Feed(Menues);
            SingleMenuManager.Instance.Begin();

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

        //----------------------------------------------------------------------------------------------------------------------------------// following are public functions that may be called from other scribts
        // displays he currently active menu
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
                        e.Display();
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

