using System;
using System.Collections.Generic;
using System.Windows.Input;
using UN_SingleMenuManager;
//using System.Windows.Input.Keyboard;


//choose and assing a namespace
namespace UN_Button
{
    public class Button
    {

        //public
        //buttonName must fit case names!!!
        String buttonName;
        float x;
        float y;
        Boolean selected;
        //private
        //protected

        //compiler
        public Button(string BN, float X, float Y)
        {
            buttonName = BN;
            x = X;
            y = Y;
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
            //displays the button itself
            //for this example its console.write(buttonName);
            Console.WriteLine(buttonName);
        }
        public void DoAction()
        {
            //make switch case for different atcions,a nd then we just add any new action here nice nice nice
            //buttonName must fit case names!!!
            switch (buttonName)
            {
                case "Start":
                    //insert start button function here and remove the writeline below
                    Console.WriteLine("button: " + buttonName + " does its action(which is nothing atm)");
                    SingleMenuManager.Instance.test();
                    break;
                case "Exit":
                    //insert exit button function here and remove the writeline below
                    Console.WriteLine("button: " + buttonName + " does its action(which is nothing atm)");
                    break;
                case "Back":
                    //insert back button function here and remove the writeline below
                    Console.WriteLine("button: " + buttonName + " does its action(which is nothing atm)");
                    break;
                case "TryAgain":
                    //insert try again button function here and remove the writeline below
                    Console.WriteLine("button: " + buttonName + " does its action(which is nothing atm)");
                    break;
                case "Restart":
                    //insert start menu function here and remove the writeline below
                    Console.WriteLine("button: " + buttonName + " does its action(which is nothing atm)");
                    break;
                case "Controls":
                    //insert start menu function here and remove the writeline below
                    Console.WriteLine("button: " + buttonName + " does its action(which is nothing atm)");
                    break;
                case "Credits":
                    //insert start menu function here and remove the writeline below
                    Console.WriteLine("button: " + buttonName + " does its action(which is nothing atm)");
                    break;
                case "Options":
                    //insert start menu function here and remove the writeline below
                    Console.WriteLine("button: " + buttonName + " does its action(which is nothing atm)");
                    break;
                default:
                    //error message for if a button is none of the premade buttons have been selected and activated
                    Console.WriteLine("Default button, tried to execute a button action, however none of the premade button options were selected or activated");
                    break;
            }

           
        }


    }
}
