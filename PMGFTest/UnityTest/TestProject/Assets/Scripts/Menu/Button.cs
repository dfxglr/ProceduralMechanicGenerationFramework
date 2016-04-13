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
        public String buttonName;
        public GameObject button;
        //the base 100 is the default value so that 0 isnt, when there is not input, and is used for error message in buttons Back and Options atm
        int relatedMenu = 100;
        Vector3 Pos;
        public Boolean isSelected;
        //private
        //protected

        //-------------------------------------------// unity editor stuff
        //private GameObject button;
        //public PrefabUtility button;
        //public Texture[] textureLists;
        //-------------------------------------------//

        //compiler 1, base
        public Button(string BN, Vector3 position)
        {
            buttonName = BN;
            Pos = position;
        }

        //complier 2, used for moving between menues
        public Button(string BN, int R, Vector3 position)
        {
            buttonName = BN;
            relatedMenu = R;
            Pos = position;
        }

        /*//compiler 3, base in console
        public Button(string BN)
        {
            buttonName = BN;
        }

        //complier 4, used for moving between menues in console
        public Button(string BN, int R)
        {
            buttonName = BN;
            relatedMenu = R;
        }//*/

        //instantiates the button in unity terms, with conditions of: position, name, texture, rotation
        public void InstantiateButton()
        {
            button =  Instantiate(Resources.Load("Prefabs/button"), Pos+SingleMenuManager.Instance.parrent.transform.position, Quaternion.identity) as GameObject;
            //----------------------------------//add what the object should do on instantiation here
            //makes it a child of the main cam
            button.transform.parent = SingleMenuManager.Instance.parrent.transform;
            //name
            button.name = buttonName;
            //Texture
            button.GetComponent<Renderer>().material.mainTexture = (Texture2D)Resources.Load("Textures/Buttons/" + buttonName) as Texture2D;
            //Rotation
            button.transform.eulerAngles = SingleMenuManager.Instance.LeRotation();
            //----------------------------------//
        }

        //---------------------------------------------------------------// insert new buttons in the switch case below
        public void DoAction()
        {
            //make switch case for different atcions,a nd then we just add any new action here nice nice nice
            //buttonName must fit case names!!!
            switch (buttonName)
            {
                case "Back":
                    //selects the related menu
                    MoveToRelated();
                    break;
                case "Continue":
                    //
                case "Controls":
                    //selects the related menu
                    MoveToRelated();
                    break;
                case "Credits":
                    //selects the related menu
                    MoveToRelated();
                    break;
                case "Debug":
                    //selects the related menu
                    MoveToRelated();
                    break;
                case "Exit":
                    //quits the unity game
                    //Application.Quit();//is ignored in the editor and web player
                    break;
                case "Exit To Main":
                    //closes the game instance but keeps the application running and goes to main menu
                    MoveToRelated();
                    break;
                case "Game Over":
                    //selects the related menu
                    MoveToRelated();
                    break;
                case "New Game":
                    //needed because button press instantiates the current menu
                    SingleMenuManager.Instance.DeSelectAllM();
                    //changes input to be not be in menu
                    SingleInputManager.Instance.inMenu = false;
                    // insert start new game function here below
                    break;
                case "Options":
                    //selects the related menu
                    MoveToRelated();
                    break;
                case "Previous Game":
                    break;
                case "Restart":
                    //selects the related menu
                    MoveToRelated();
                    break;
                case "Start":
                    //selects the related menu
                    MoveToRelated();
                    break; 
                default:
                    //error message for if a button is none of the premade buttons have been selected and activated
                    Console.WriteLine("Default button, tried to execute a button action, however none of the premade button options were selected or activated");
                    Debug.Log("Default button, tried to execute button action for "+buttonName+", however none of the premade button options were selelcted or activated");
                    break;
            }
        }
        
        //---------------------------------------------------------------// user-made button-functions below
        private void MoveToRelated()
        {
            //selects the related menu
            if (relatedMenu != 100 && SingleMenuManager.Instance.MenuCount() != 0 && relatedMenu <= SingleMenuManager.Instance.MenuCount())
            {
                //unity objects
                SingleMenuManager.Instance.SelectMenu(relatedMenu);
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
