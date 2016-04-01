using System;
using UN_Button;
using System.Collections.Generic;
using UN_SingleMenuManager;


namespace UN_Menu
{
    public class Menu
    {
        //
        string menuName;
        Boolean selected;
        //
        int anySelected = 0;
        Boolean nooneSelected = false;
        //menu's position
        float x;
        float y;
        //list of the buttons in the menu
        List<Button> buttonList;

        //constructor used for the rendered version or aka when we need the positions
        public Menu(string MN, List<Button> BL, float X, float Y)
        {
            menuName = MN;
            buttonList = BL;
            x = X;
            y = Y;
        }
        //constructor used for the console versions
        public Menu(string MN, List<Button> BL)
        {
            menuName = MN;
            buttonList = BL;
        }
        public void PauseTime()
        {
            //stop time steps so the game is paused
        }
        //deselects all buttons
        public void DeSelectAllB()
        {
            //
            foreach (Button e in buttonList)
            {
                e.DeSelect();
            }
        }
        // selects the first button in
        public void SelectButtonZeroOrActiveB()
        {
            //checks if empty before running
            if (buttonList != null)
            {
                foreach (Button e in buttonList)
                {
                    if (!e.IsSelected())
                    {
                        anySelected++;
                        if (anySelected==buttonList.Count)
                        {
                            nooneSelected = true;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                //selects button
                if (nooneSelected)
                {
                    DeSelectAllB();
                    buttonList[0].Select();
                }
                //resets 
                anySelected = 0;
                nooneSelected = false;
            }
            else
            {
                //error message for if no buttons are found in the button list
                Console.WriteLine("error: no buttons were found in the button list");
            }
        }
        public void DeSelect()
        {
            selected = false;
            //no using atm because its a console project at that this would spam it
            //Console.WriteLine("button: " + buttonName + " is NOT selected");
        }
        //sets this menu to be active
        public void Select()
        {            
            selected = true;
        }
        //used to check if this menu is active
        public bool IsSelected()
        {
            //checks in this menu is selected
            if (selected)
            {
                return true;
            }
            else
            {
                return false;
            }
        }   
        public void DisplayM()
        {
            //insert here what the menu looks like aka texture or text or w/e we want in the rendered version of the game
        }
        public void DisplayButtons()
        {
            if (buttonList != null)
            {
                //display all buttons(only display for console here)
                foreach (Button e in buttonList)
                {
                    e.Display();
                }
            }
            else
            {
                //error message for if no buttons are found in the button list
                Console.WriteLine("error: no buttons were found in the button list");
            }
        }
        public void DisplayConsole()
        {
            Console.WriteLine(menuName);
        }
        public void DisplayConsoleButtons()
        {
            if (buttonList != null)
            {
                //display all buttons(only display for console here)
                foreach (Button e in buttonList)
                {
                    e.DisplayConsole();
                }
            }
            else
            {
                //error message for if no buttons are found in the button list
                Console.WriteLine("error: no buttons were found in the button list");
            }
        }
        // used this menues the currently selected buttons action 
        public void UseButton()
        {
            foreach (Button e in buttonList)
            {
                if (e.IsSelected() && selected)
                {
                    e.DoAction();
                    //this is the first ideal place for the function below, but i cant be run from here without double trouble
                    //SingleMenuManager.Instance.SelectFirstButtonInMenu();
                }
            }
        }
        public void SelectUp()
        {
            if (buttonList != null)
            {
                //first position in the list
                if (buttonList[0].IsSelected())
                {
                    buttonList[0].DeSelect();
                    buttonList[buttonList.Count - 1].Select();
                }
                //last postition in the list
                else if (buttonList[buttonList.Count - 1].IsSelected())
                {
                    buttonList[buttonList.Count - 1].DeSelect();
                    buttonList[buttonList.Count - 2].Select();
                }
                else
                {
                    //middle posititions in the list
                    for (int i = 1; i < buttonList.Count - 1; i++)
                    {
                        if (buttonList[i].IsSelected())
                        {
                            buttonList[i].DeSelect();
                            buttonList[i - 1].Select();
                            break;
                        }
                    }
                }
            }
            else
            {
                //error message for if no buttons are found in the button list
                Console.WriteLine("error: no buttons were found in the button list");
            } 
        }
        public void SelectDown()
        {
            if (buttonList != null)
            {
                //first position in the list
                if (buttonList[0].IsSelected() && buttonList.Count > 1)
                {
                    buttonList[0].DeSelect();
                    buttonList[1].Select();
                }
                //last postition in the list
                else if (buttonList[buttonList.Count - 1].IsSelected())
                {
                    buttonList[buttonList.Count - 1].DeSelect();
                    buttonList[0].Select();
                }
                else
                {
                    //middle posititions in the list
                    for (int i = 1; i < buttonList.Count - 1; i++)
                    {
                        if (buttonList[i].IsSelected())
                        {
                            buttonList[i].DeSelect();
                            buttonList[i + 1].Select();
                            break;
                        }
                    }
                }
            }
            else
            {
                //error message for if no buttons are found in the button list
                Console.WriteLine("error: no buttons were found in the button list");
            }  
        }
    }
}
