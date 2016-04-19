using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using PMFG.PMGUnity.MenuParts;
using PMFG.PMGUnity.Managers;
using UnityEngine;
using System.Collections;

namespace PMFG
{
    namespace PMGUnity
    {
        namespace Managers
        {
            public sealed class SingleMenuManager : MonoBehaviour
            {
                //variables 
                List<Menu> menuList = new List<Menu>();
                //(selectCount isnt used much other than in void Begin and even there is only used a few times, potential to change this) 
                //Boolean selectMenu = true;
                public GameObject parrent;
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
                    //parrent - make sure its in the scene ofc
                    parrent = GameObject.Find("Main Camera");
                    //color
                    DefaultColor = new Vector4(0.0f, 1.0f, 0.0f, 1.0f);
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
                    Menu PauseMenu;
                    //lists for buttons and menues
                    List<Button> MainMenuButtons = new List<Button>();
                    List<Button> StartMenuButtons = new List<Button>();
                    List<Button> RestartMenuButtons = new List<Button>();
                    List<Button> OptionsMenuButtons = new List<Button>();
                    List<Button> ControlsMenuButtons = new List<Button>();
                    List<Button> CreditsMenuButtons = new List<Button>();
                    List<Button> EndMenuButtons = new List<Button>();
                    List<Button> PauseMenuButtons = new List<Button>();
                    //Main Menu
                    MainMenuButtons.Add(new Button("Start", 1, new Vector3(-6, 4, 15)));
                    MainMenuButtons.Add(new Button("Options", 3, new Vector3(-6, 1, 15)));
                    MainMenuButtons.Add(new Button("Exit", new Vector3(-6, -2, 15)));
                    MainMenu = new Menu("Welcome to PMGF", MainMenuButtons, new Vector3(0, 0, 16));
                    //start menu 
                    StartMenuButtons.Add(new Button("New Game", new Vector3(-6, 4, 15)));
                    StartMenuButtons.Add(new Button("Previous Game", new Vector3(-6, 1, 15)));
                    StartMenuButtons.Add(new Button("Back", 0, new Vector3(-6, -2, 15)));
                    StartMenu = new Menu("START", StartMenuButtons, new Vector3(0, 0, 16));
                    //restart menu 
                    RestartMenuButtons.Add(new Button("New Game", new Vector3(-6, 4, 15)));
                    RestartMenuButtons.Add(new Button("Previous Game", new Vector3(-6, 1, 15)));
                    RestartMenuButtons.Add(new Button("Exit", new Vector3(-6, -2, 15)));
                    if (runInDebug)
                    {
                        RestartMenuButtons.Add(new Button("Back", 6, new Vector3(-6, -5, 15)));
                    }
                    RestartMenu = new Menu("RESTART", RestartMenuButtons, new Vector3(0, 0, 16));
                    //options menu
                    OptionsMenuButtons.Add(new Button("Controls", 4, new Vector3(-6, 4, 15)));
                    OptionsMenuButtons.Add(new Button("Credits", 5, new Vector3(-6, 1, 15)));
                    if (runInDebug)
                    {
                        OptionsMenuButtons.Add(new Button("Debug", 8, new Vector3(-6, -2, 15)));
                        OptionsMenuButtons.Add(new Button("Back", 0, new Vector3(-6, -5, 15)));
                    }
                    else
                    {
                        OptionsMenuButtons.Add(new Button("Back", 0, new Vector3(-6, -2, 15)));
                    }
                    OptionsMenu = new Menu("OPTIONS", OptionsMenuButtons, new Vector3(0, 0, 16));
                    //
                    //controls menu
                    ControlsMenuButtons.Add(new Button("Back", 3, new Vector3(-6, 4, 15)));
                    ControlsMenu = new Menu("CONTROLS", ControlsMenuButtons, new Vector3(0, 0, 16));
                    //credits menu
                    CreditsMenuButtons.Add(new Button("Back", 3, new Vector3(-6, 4, 15)));
                    CreditsMenu = new Menu("CREDITS", CreditsMenuButtons, new Vector3(0, 0, 16));
                    //End Menu
                    EndMenuButtons.Add(new Button("Restart", 2, new Vector3(-6, 4, 15)));
                    EndMenuButtons.Add(new Button("Exit", new Vector3(-6, 1, 15)));
                    if (runInDebug)
                    {
                        EndMenuButtons.Add(new Button("Back", 8, new Vector3(-6, -2, 15)));
                    }
                    EndMenu = new Menu("GAME OVER", EndMenuButtons, new Vector3(0, 0, 16));
                    //Pause Menu
                    PauseMenuButtons.Add(new Button("Continue", new Vector3(-6, 4, 15)));
                    PauseMenuButtons.Add(new Button("Exit To Main", 0, new Vector3(-6, 1, 15)));
                    PauseMenuButtons.Add(new Button("Exit", new Vector3(-6, -2, 15)));
                    PauseMenu = new Menu("PAUSE", PauseMenuButtons, new Vector3(0, 0, 16));
                    //adds to the list of menues - have main menu in the beginning always plz, dont be dick 
                    menuList.Add(MainMenu);
                    menuList.Add(StartMenu);
                    menuList.Add(RestartMenu);
                    menuList.Add(OptionsMenu);
                    menuList.Add(ControlsMenu);
                    menuList.Add(CreditsMenu);
                    menuList.Add(EndMenu);
                    menuList.Add(PauseMenu);

                    //debug menu
                    if (runInDebug)
                    {
                        Menu DebugMenu;
                        //
                        List<Button> DebugMenuButtons = new List<Button>();
                        //Add degub buttons here
                        DebugMenuButtons.Add(new Button("Game Over", 6, new Vector3(-6, 4, 15)));
                        DebugMenuButtons.Add(new Button("Back", 3, new Vector3(-6, 1, 15)));
                        DebugMenu = new Menu("DEBUG", DebugMenuButtons, new Vector3(0, 0, 16));
                        menuList.Add(DebugMenu);
                    }
                    //-------------------------------------------------// setup end
                    //selects the first menu in the menulist
                    SingleMenuManager.Instance.SelectMenu(0);
                    SingleMenuManager.Instance.SelectFirstOrActiveButton();
                    //
                    SingleInputManager.Instance.inMenu = true;
                    //instantiates the selected menu
                    SingleMenuManager.Instance.InstantiateCurrent();
                    //diplay the selected button in unity and throwing default color on the rest
                    SingleMenuManager.Instance.DisplayUnitySelected();
                }
                //-----------------------------------------------------------------------------------------------------// can be used from outside scribt
                //activates the action of the currently selected button 
                public void ActivateButton(Boolean w_Console)
                {
                    //checks if there are any items in menulist
                    if (menuList != null)
                    {
                        // goes through menulist
                        foreach (Menu e in menuList)
                        {
                            //runs for the selected menu only
                            if (e.isSelected)
                            {
                                //checks if there are any items in the selected menu's buttonlist
                                if (e.buttonList != null)
                                {
                                    //goes through selected menu's buttonlist
                                    foreach (Button e2 in e.buttonList)
                                    {
                                        //runs for the selected button only
                                        if (e2.isSelected)
                                        {
                                            //does the action assigned to the selected button
                                            e2.DoAction();
                                            if (w_Console)
                                            {
                                                Debug.Log("button [" + e2.buttonName + "] does its action, which may or may not be set, find the button in button scribt, in DoAction(), and see it if is, if nothing happens");
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    Debug.Log("Error: no buttons found in the button list of the selected menu");
                                }
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
                        //runs for each menu
                        foreach (Menu e in menuList)
                        {
                            //deselects the menu
                            e.isSelected = false;
                        }
                    }
                    else
                    {
                        //error message here
                        Console.WriteLine("The list of menues have not been added to the single menu manager, as such this function cannot be run, please check if the function: SingleMenuManager.Instance.Feed(menuList of your choice), has been used in the single menu manager setup function");
                        Debug.Log("The list of menues have not been added to the single menu manager, as such this function cannot be run, please check if the function: SingleMenuManager.Instance.Feed(menuList of your choice), has been used in the single menu manager setup function");
                    }
                }

                //destroys any menu or button game object found, used to clear out for new instantiation
                public void DestroyAll()
                {
                    //checks to secure that function can be run if menulist is empty
                    if (menuList != null)
                    {
                        //will work fine so long as no two menues are selected at the same time
                        foreach (Menu e in menuList)
                        {
                            if (e.buttonList != null)
                            {
                                foreach (Button e2 in e.buttonList)
                                {
                                    Destroy(e2.button);
                                }
                            }
                            else
                            {
                                //error message for if no buttons are found in the button list
                                Console.WriteLine("error: no buttons were found in the button list");
                            }
                            Destroy(e.menu);
                        }
                    }
                    else
                    {
                        //error message here
                        Console.WriteLine("The list of menues have not been added to the single menu manager, as such this function cannot be run, please check if the function: SingleMenuManager.Instance.Feed(menuList of your choice), has been used in the single menu manager setup function");
                        Debug.Log("The list of menues have not been added to the single menu manager, as such this function cannot be run, please check if the function: SingleMenuManager.Instance.Feed(menuList of your choice), has been used in the single menu manager setup function");
                    }
                }

                //displays the currently active console menu
                public void DisplayConsoleCurrent()
                {
                    //checks to secure that function can be run if menulist is empty
                    if (menuList != null)
                    {
                        //display currently active menu, if there is any
                        //will work fine so long as no two menues are selected at the same time
                        foreach (Menu e in menuList)
                        {
                            if (e.isSelected)
                            {
                                //
                                Debug.Log(e.menuName);
                                //
                                foreach (Button e2 in e.buttonList)
                                {
                                    //runs if the button is selected
                                    if (e2.isSelected)
                                    {
                                        //writed out the name with markers
                                        //Console.WriteLine(">" + e2.buttonName + "<");
                                        Debug.Log(">" + e2.buttonName + "<");
                                    }
                                    else
                                    {
                                        //writed out the name with no markers
                                        //Console.WriteLine(e2.buttonName);
                                        Debug.Log(e2.buttonName);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        //error message here
                        //Console.WriteLine("The list of menues have not been added to the single menu manager, as such this function cannot be run, please check if the function: SingleMenuManager.Instance.Feed(menuList of your choice), has been used in the single menu manager setup function");
                        Debug.Log("The list of menues have not been added to the single menu manager, as such this function cannot be run, please check if the function: SingleMenuManager.Instance.Feed(menuList of your choice), has been used in the single menu manager setup function");
                    }
                }

                // displays the selected menu by tint all the buttons in it with default color and then tinting the selected button
                public void DisplayUnitySelected()
                {
                    //checks to secure that function can be run if menulist is empty
                    if (menuList != null)
                    {
                        //display currently active menu, if there is any
                        //will work fine so long as no two menues are selected at the same time
                        foreach (Menu e in menuList)
                        {
                            //runs if the menu is selected
                            if (e.isSelected)
                            {
                                //runs for each button
                                foreach (Button e2 in e.buttonList)
                                {
                                    //Tints the all the button game objects with the default color 
                                    e2.button.GetComponent<Renderer>().material.color = DefaultColor;
                                    if (e2.isSelected)
                                    {
                                        //tints the selected button game object with the tint color
                                        e2.button.GetComponent<Renderer>().material.color = TintColor;
                                    }
                                }
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

                //instatiates the selected menu's menu and button objects in unity
                public void InstantiateCurrent()
                {
                    //checks to secure that function can be run if menulist is empty
                    if (menuList != null)
                    {
                        //display currently active menu, if there is any
                        //will work fine so long as no two menues are selected at the same time
                        foreach (Menu e in menuList)
                        {
                            if (e.isSelected)
                            {
                                //instantiated menues
                                e.InstantiateMenu();
                                //
                                if (e.buttonList != null)
                                {
                                    //display all buttons(only display for console here)
                                    foreach (Button e2 in e.buttonList)
                                    {
                                        e2.InstantiateButton();
                                    }
                                }
                                else
                                {
                                    //error message for if no buttons are found in the button list
                                    Console.WriteLine("error: no buttons were found in the button list");
                                    Debug.Log("Error: no buttons found in the button list");
                                }
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

                //returns the rotation of the menu, used by buttons, in InstatiateButton()
                public Vector3 LeRotation()
                {
                    return MenuRotation;
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

                //selects the above below the the currently active button
                public void MoveUp()
                {
                    //checks if there are any items in menulist
                    if (menuList != null)
                    {
                        // goes through menulist
                        foreach (Menu e in menuList)
                        {
                            //runs for the selected menu only
                            if (e.isSelected)
                            {
                                //checks if there are any items in the selected menu's buttonlist
                                if (e.buttonList != null)
                                {
                                    //if first position in the list - move up by deselecting and then selecting the end item in the list
                                    if (e.buttonList[0].isSelected)
                                    {
                                        e.buttonList[0].isSelected = false;
                                        e.buttonList[e.buttonList.Count - 1].isSelected = true;
                                    }
                                    //if last position in the list - move up by deselecting and then selecting the previous item in the list
                                    else if (e.buttonList[e.buttonList.Count - 1].isSelected)
                                    {
                                        e.buttonList[e.buttonList.Count - 1].isSelected = false;
                                        e.buttonList[e.buttonList.Count - 2].isSelected = true;
                                    }
                                    else
                                    {
                                        //if any middle position in the list - move up by finding the selected button and deselecting it and then selecting the previous item in the list
                                        for (int i = 1; i < e.buttonList.Count - 1; i++)
                                        {
                                            if (e.buttonList[i].isSelected)
                                            {
                                                e.buttonList[i].isSelected = false;
                                                e.buttonList[i - 1].isSelected = true;
                                                break;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    //error message for if no buttons are found in the button list
                                    Console.WriteLine("error: no buttons were found in the button list");
                                    Debug.Log("Error: no buttons found in the button list of the selected menu");
                                }
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
                    //checks if there are any items in menulist
                    if (menuList != null)
                    {
                        foreach (Menu e in menuList)
                        {
                            if (e.isSelected)
                            {
                                if (e.buttonList != null)
                                {
                                    //if first position in the list - move down by deselecting and then selecting the next item in the list
                                    if (e.buttonList[0].isSelected && e.buttonList.Count > 1)
                                    {
                                        e.buttonList[0].isSelected = false;
                                        e.buttonList[1].isSelected = true;
                                    }
                                    //if last position in the list - move down by deselecting and then selecting the first item in the list
                                    else if (e.buttonList[e.buttonList.Count - 1].isSelected)
                                    {
                                        e.buttonList[e.buttonList.Count - 1].isSelected = false;
                                        e.buttonList[0].isSelected = true;
                                    }
                                    else
                                    {
                                        //if any middle position in the list - move down by finding the selected button and deselecting it and then selecting the next item in the list
                                        for (int i = 1; i < e.buttonList.Count - 1; i++)
                                        {
                                            if (e.buttonList[i].isSelected)
                                            {
                                                e.buttonList[i].isSelected = false;
                                                e.buttonList[i + 1].isSelected = true;
                                                break;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    //error message for if no buttons are found in the button list
                                    Console.WriteLine("error: no buttons were found in the button list");
                                    Debug.Log("Error: no buttons found in the button list of the selected menu");
                                }
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

                //returns true if a menu is seleected, used by single input manager, in InputUpdate() 
                public bool IsMenuSelected()
                {
                    //use a return function to get the menurownumber which is equal to the relatedmenu value
                    if (menuList != null)
                    {
                        //runs for each menu
                        foreach (Menu e in menuList)
                        {
                            //checks if a menu is selected
                            if (e.isSelected)
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

                //selects the first button in the selected menu's buttonlist, only if no other button is selected
                public void SelectFirstOrActiveButton()
                {
                    //use a return function to get the menurownumber which is equal to the relatedmenu value
                    if (menuList != null)
                    {
                        //runs for each menu
                        foreach (Menu e in menuList)
                        {
                            //checks if the menu is selected
                            if (e.isSelected)
                            {
                                //if yes, selected the first button, or the current active one by
                                //run if button list is not empty
                                if (e.buttonList != null)
                                {
                                    //runs for each button in the buttonlist
                                    foreach (Button e2 in e.buttonList)
                                    {
                                        //runs if the button is not selected
                                        if (!e2.isSelected)
                                        {
                                            //add to the count of none selected buttons
                                            e.ButtonsNotSelectedCount++;
                                            //runs if all the buttons are not selecteds
                                            if (e.ButtonsNotSelectedCount == e.buttonList.Count)
                                            {
                                                e.noButtonSelected = true;
                                            }
                                        }
                                        //runs in any single button is selected
                                        else
                                        {
                                            //breaks out of the foreach loop and moves on
                                            break;
                                        }
                                    }
                                    //runs if no button is selected
                                    if (e.noButtonSelected)
                                    {
                                        //checks if buttin list is null
                                        if (e.buttonList != null)
                                        {
                                            //runs for each button
                                            foreach (Button e2 in e.buttonList)
                                            {
                                                //deselects the button
                                                e2.isSelected = false;
                                            }
                                        }
                                        else
                                        {
                                            Debug.Log("Error: no buttons found in the button list");
                                        }
                                        // selects the first button
                                        e.buttonList[0].isSelected = true;
                                    }
                                    //resets 
                                    e.ButtonsNotSelectedCount = 0;
                                    e.noButtonSelected = false;
                                }
                                else
                                {
                                    //error message for if no buttons are found in the button list
                                    Console.WriteLine("error: no buttons were found in the button list");
                                    Debug.Log("Error: no buttons found in the button list");
                                }
                            }
                            else
                            {
                                //checks if buttin list is null
                                if (e.buttonList != null)
                                {
                                    //runs for each button
                                    foreach (Button e2 in e.buttonList)
                                    {
                                        //deselects the button
                                        e2.isSelected = false;
                                    }
                                }
                                else
                                {
                                    Debug.Log("Error: no buttons found in the button list");
                                }
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
                        menuList[MenuRowNumber].isSelected = true;
                    }
                    else
                    {
                        //error message here
                        Console.WriteLine("The list of menues have not been added to the single menu manager, as such this function cannot be run, please check if the function: SingleMenuManager.Instance.Feed(menuList of your choice), has been used in the single menu manager setup function");
                        Debug.Log("The list of menues have not been added to the single menu manager, as such this function cannot be run, please check if the function: SingleMenuManager.Instance.Feed(menuList of your choice), has been used in the single menu manager setup function");
                    }
                }

                //pauses the game
                public void PauseGame()
                {
                    // somehow pause the game when no menu is active
                }
                //-----------------------------------------------------------------------------------------------------//
            }
        }
    }
}

