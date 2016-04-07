using System;
using System.Collections.Generic;
//using System.Windows.Input;
using UN_SingleMenuManager;
using UN_SingleInputManager;
using UnityEngine;
using UnityEditor;
using System.Collections;


//choose and assing a namespace
namespace UN_Button
{
    
    public class Button : MonoBehaviour
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

        //-------------------------------------------// unity editor stuff
        //private GameObject button;
        //public PrefabUtility button;
        //public Texture[] textureLists;
        //-------------------------------------------//

        //compiler 1, base
        public Button(string BN, float X, float Y)
        {
            buttonName = BN;
            x = X;
            y = Y;
            
            //Instantiate(button, new Vector3(x, y, 0), Quaternion.identity);
        }
        //complier 2, used for moving between menues
        public Button(string BN, int R, float X, float Y)
        {
            buttonName = BN;
            relatedMenu = R;
            x = X;
            y = Y;
            //Instantiate(button, new Vector3(x, y, 0), Quaternion.identity);
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
        public void InstantiateButton()
        {
            GameObject button =  Instantiate(Resources.Load("Prefabs/button"), new Vector3(x, y, 0), Quaternion.identity) as GameObject;
            //----------------------------------//add what the object should do on instantiation here
            //tags the instance with its name
            button.tag = buttonName;
            //Texture
            button.GetComponent<Renderer>().material.mainTexture = (Texture2D)Resources.Load("Textures/Buttons/" + buttonName) as Texture2D;
            //Rotation
            button.transform.eulerAngles = SingleMenuManager.Instance.LeRotation();
            //----------------------------------//
        }
        //might need to destroy them again
        public void DestroyButton()
        {
            //lets hope that if there is no tag that it does nothing
            Destroy(GameObject.FindGameObjectWithTag(buttonName));
        }
        public string ButtonName()
        {
            return buttonName;
        }
        public void DeSelect()
        {
            //deselects this button
            selected = false;
            //display on not selected but is set here

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
        public void DisplayConsole()
        {
            //displays the button itself
            //for this example its console.write(buttonName);
            if (selected)
            {
                Console.WriteLine(">" + buttonName + "<");
                Debug.Log(">" + buttonName + "<");
            }
            else
            {
                Console.WriteLine(buttonName);
                Debug.Log(buttonName);
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
                    Debug.Log("button [" + buttonName + "] does its action(which is nothing atm)");
                    //selects the related menu
                    MoveToRelated();
                    break;
                case "Restart":
                    //insert start button function here and remove the writeline below - console need only
                    Console.WriteLine("button [" + buttonName + "] does its action");
                    Debug.Log("button [" + buttonName + "] does its action");
                    //selects the related menu
                    MoveToRelated();
                    break;
                case "Exit":
                    //insert exit button function here and remove the writeline below - console need only
                    Console.WriteLine("button [" + buttonName + "] does its action(which is nothing atm)");
                    Debug.Log("button [" + buttonName + "] does its action(which is nothing atm)");
                    SingleInputManager.Instance.GameQuit();
                    break;
                case "Back":
                    //insert back button function here and remove the writeline below - console need only
                    Console.WriteLine("button [" + buttonName + "] does its action");
                    Debug.Log("button [" + buttonName + "] does its action");
                    //selects the related menu
                    MoveToRelated();
                    break;
                case "New Game":
                    //insert try again button function here and remove the writeline below - console need only
                    Console.WriteLine("button [" + buttonName + "] does its action(which is nothing atm)");
                    Debug.Log("button [" + buttonName + "] does its action(which is nothing atm)");
                    break;
                case "Previous Game":
                    //insert start menu function here and remove the writeline below - console need only
                    Console.WriteLine("button [" + buttonName + "] does its action(which is nothing atm)");
                    Debug.Log("button [" + buttonName + "] does its action(which is nothing atm)");
                    break;
                case "Controls":
                    //insert start menu function here and remove the writeline below - console need only
                    Console.WriteLine("button [" + buttonName + "] does its action");
                    Debug.Log("button [" + buttonName + "] does its action");
                    //selects the related menu
                    MoveToRelated();
                    break;
                case "Credits":
                    //insert start menu function here and remove the writeline below - console need only
                    Console.WriteLine("button [" + buttonName + "] does its action");
                    Debug.Log("button [" + buttonName + "] does its action");
                    //selects the related menu
                    MoveToRelated();
                    break;
                case "Options":
                    //insert start menu function here and remove the writeline below - console need only
                    Console.WriteLine("button [" + buttonName + "] does its action");
                    Debug.Log("button [" + buttonName + "] does its action");
                    //selects the related menu
                    MoveToRelated();
                    break;
                case "Debug":
                    //insert start menu function here and remove the writeline below - console need only
                    Console.WriteLine("button [" + buttonName + "] does its action");
                    Debug.Log("button [" + buttonName + "] does its action");
                    //selects the related menu
                    MoveToRelated();
                    break;
                case "Game Over":
                    //insert start menu function here and remove the writeline below - console need only
                    Console.WriteLine("button [" + buttonName + "] does its action");
                    Debug.Log("button [" + buttonName + "] does its action");
                    //selects the related menu
                    MoveToRelated();
                    break;
                default:
                    //error message for if a button is none of the premade buttons have been selected and activated
                    Console.WriteLine("Default button, tried to execute a button action, however none of the premade button options were selected or activated");
                    Debug.Log("Default button, tried to execute button action, however none of the premade button options were selelcted or activated");
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
                //unity objects
                SingleMenuManager.Instance.DestroyAll();
                SingleMenuManager.Instance.InstantiateCurrent();
            }
            else
            {
                //error for when a person forgets to add the related menu, when having made the button
                Console.WriteLine("Error: you have tried to move to the previously selected menu, but this button has not received a related menu or the related menu number given is higher than the number of menues in the menulist, please check the Setup function in the Single Menu Manager, at the creating of the button: " + buttonName);
                Debug.Log("Error: you have tried to move to the previously selected menu, but this button has not received a related menu or the related menu number given is higher than the number of menues in the menulist, please check the Setup function in the Single Menu Manager, at the creating of the button: " + buttonName);
            }
        }
    }
}
