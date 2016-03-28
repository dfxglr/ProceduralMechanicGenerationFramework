using System;
using UN_Button;
using System.Collections.Generic;


namespace UN_Menu
{
    public class Menu
    {
        string menuName;
        Boolean selected;
        //menu's position
        float x;
        float y;
        //list of the buttons in the menu
        List<Button> buttonList;

        public Menu(string MN, List<Button> BL, float X, float Y)
        {
            menuName = MN;
            buttonList = BL;
            x = X;
            y = Y;



            //checks if empty before running
            if (buttonList.Count != 0)
            {
                //sets first button to be active
                buttonList[0].IsSelected();
            }
            else
            {
                //error message for if no buttons are found in the button list
                Console.WriteLine("error: no buttons were found in the button list");
            }
        }
        public void PauseTime()
        {
            //stop time steps so the game is paused
        }
        public void DeSelect()
        {
            selected = false;
            //no using atm because its a console project at that this would spam it
            //Console.WriteLine("button: " + buttonName + " is NOT selected");
        }
        public void Select()
        {
            //sets this menu to be active
            selected = true;
        }
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

        public void Display()
        {
            //name and/or textures
            Console.WriteLine(menuName);
        }
        public void DisplayButtons()
        {
            if (buttonList.Count != 0)
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

        public void MoveUp()
        {
            //buttonlist-1 into .Select()
            //plus begin/end conditions for list[0 and N]
        }
        public void MoveDown()
        {
            //buttonlist+1 into .Select()
            //plus begin/end conditions for list[0 and N]
        }
    }
}
