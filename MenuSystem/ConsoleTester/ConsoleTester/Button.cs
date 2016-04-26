using System;
using System.Collections.Generic;
using System.Windows.Input;
using UN_SingleMenuManager;
using UN_SingleInputManager;


//choose and assing a namespace
namespace UN_Button
{
    public class Button
    {

        //public
        //buttonName must fit case names!!!
        String buttonName;
        //the base 100 is the default value, when there is not input, and is used for error message in buttons Back and Options atm
        int relatedMenu = 100;
        float x;
        float y;
        Boolean selected;
        //private
        //protected


        //compiler 1, base
        public Button(string BN, float X, float Y)
        {
            buttonName = BN;
            x = X;
            y = Y;
        }
        //complier 2, used for moving between menues
        public Button(string BN, int R, float X, float Y)
        {
            buttonName = BN;
            relatedMenu = R;
            x = X;
            y = Y;
        }
        //compiler 3, base in console
        public Button(string BN)
        {
            buttonName = BN;
        }
        //complier 4, used for moving between menues in console
        public Button(string BN, int R)
        {
            buttonName = BN;
            relatedMenu = R;
        }


        public void DeSelect()
        {
            //deselects this button
            selected = false;
        }
        public void Select()
        {
            //deselects this button
            selected = true;
        }
        public bool IsSelected()
        {
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
            //insert here what the button looks like aka texture or text or w/e we want in the rendered version of the game
        }
        public void DisplayConsole()
        {
            //displays the button itself
            //for this example its console.write(buttonName);
            if (selected)
            {
                Console.WriteLine(">" + buttonName + "<");
            }
            else
            {
                Console.WriteLine(buttonName);
            }
        }      
        public void DoAction()
        {
            //make switch case for different atcions,a nd then we just add any new action here nice nice nice
            //buttonName must fit case names!!!
            switch (buttonName)
            {
                case "Start":
                    //insert start button function here and remove the writeline below - console need only
                    Console.WriteLine("button [" + buttonName + "] does its action(which is nothing atm)");
                    //selects the related menu
                    MoveToRelated();
                    break;
                case "Restart":
                    //insert start button function here and remove the writeline below - console need only
                    Console.WriteLine("button [" + buttonName + "] does its action");
                    //selects the related menu
                    MoveToRelated();
                    break;
                case "Exit":
                    //insert exit button function here and remove the writeline below - console need only
                    Console.WriteLine("button [" + buttonName + "] does its action(which is nothing atm)");
                    SingleInputManager.Instance.GameQuit();
                    break;
                case "Back":
                    //insert back button function here and remove the writeline below - console need only
                    Console.WriteLine("button [" + buttonName + "] does its action");
                    //selects the related menu
                    MoveToRelated();
                    break;
                case "New Game":
                    //insert try again button function here and remove the writeline below - console need only
                    Console.WriteLine("button [" + buttonName + "] does its action(which is nothing atm)");
                    break;
                case "Previous Game":
                    //insert start menu function here and remove the writeline below - console need only
                    Console.WriteLine("button [" + buttonName + "] does its action(which is nothing atm)");  
                    break;
                case "Controls":
                    //insert start menu function here and remove the writeline below - console need only
                    Console.WriteLine("button [" + buttonName + "] does its action");
                    //selects the related menu
                    MoveToRelated();
                    break;
                case "Credits":
                    //insert start menu function here and remove the writeline below - console need only
                    Console.WriteLine("button [" + buttonName + "] does its action");
                    //selects the related menu
                    MoveToRelated();
                    break;
                case "Options":
                    //insert start menu function here and remove the writeline below - console need only
                    Console.WriteLine("button [" + buttonName + "] does its action");
                    //selects the related menu
                    MoveToRelated();
                    break;
                case "Debug":
                    //insert start menu function here and remove the writeline below - console need only
                    Console.WriteLine("button [" + buttonName + "] does its action");
                    //selects the related menu
                    MoveToRelated();
                    break;
                case "Game Over":
                    //insert start menu function here and remove the writeline below - console need only
                    Console.WriteLine("button [" + buttonName + "] does its action");
                    //selects the related menu
                    MoveToRelated();
                    break;
                default:
                    //error message for if a button is none of the premade buttons have been selected and activated
                    Console.WriteLine("Default button, tried to execute a button action, however none of the premade button options were selected or activated");
                    break;
            }
        }
        //---------------------------------------------------------------// button functions below
        private void MoveToRelated()
        {
            //selects the related menu
            if (relatedMenu != 100 && SingleMenuManager.Instance.MenuCount() != 0 && relatedMenu <= SingleMenuManager.Instance.MenuCount())
            {
                SingleMenuManager.Instance.SelectMenu(relatedMenu);
            }
            else
            {
                //error for when a person forgets to add the related menu, when having made the button
                Console.WriteLine("Error: you have tried to move to the previously selected menu, but this button has not received a related menu or the related menu number given is higher than the number of menues in the menulist, please check the Setup function in the Single Menu Manager, at the creating of the button: " + buttonName);
            }
        }
    }
}
