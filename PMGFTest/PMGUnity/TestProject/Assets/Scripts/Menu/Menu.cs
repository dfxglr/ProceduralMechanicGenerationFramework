using System;
using UN_Button;
using System.Collections.Generic;
using UN_SingleMenuManager;
using UnityEngine;
using System.Collections;


namespace UN_Menu
{
    public class Menu : MonoBehaviour
    {
        //
        public string menuName;
        public GameObject menu;
        public Boolean isSelected;
        //
        int anySelected = 0;
        Boolean nooneSelected = false;
        //menu's position
        float x;
        float y;
        //list of the buttons in the menu
        public List<Button> buttonList;

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
            //checks if empty before running
            if (buttonList != null)
            {
                foreach (Button e in buttonList)
                {
                    e.isSelected = false;
                }
            }
            else
            {
                Debug.Log("Error: no buttons found in the button list");
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
                    if (!e.isSelected)
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
                    buttonList[0].isSelected = true;
                    //display here
                    //GameObject.FindGameObjectWithTag(buttonList[0].ButtonName()).GetComponent<Renderer>().material.color = new Vector4(255,0,0,0);

                }
                //resets 
                anySelected = 0;
                nooneSelected = false;
            }
            else
            {
                //error message for if no buttons are found in the button list
                Console.WriteLine("error: no buttons were found in the button list");
                Debug.Log("Error: no buttons found in the button list");
            }
        }

        //instantiates menu unity game object, set its name, texture and rotation
        public void InstantiateMenu()
        {
            menu = Instantiate(Resources.Load("Prefabs/menu"), new Vector3(x, y, 0), Quaternion.identity) as GameObject;
            //----------------------------------//add what the object should do on instantiation here
            //tag the instance with its name
            menu.name = menuName;
            //texture
            //Texture2D menuTex = Resources.Load("Prefabs/" + menuName) as Texture;
            menu.GetComponent<Renderer>().material.mainTexture = (Texture2D)Resources.Load("Textures/menues/" + menuName) as Texture2D;
            //Rotation
            menu.transform.eulerAngles = SingleMenuManager.Instance.LeRotation();
            
            //----------------------------------//
        }
        
        //
        public void InstantiateMenuButtons()
        {
            if (buttonList != null)
            {
                //display all buttons(only display for console here)
                foreach (Button e in buttonList)
                {
                    e.InstantiateButton();
                }
            }
            else
            {
                //error message for if no buttons are found in the button list
                Console.WriteLine("error: no buttons were found in the button list");
                Debug.Log("Error: no buttons found in the button list");
            }
        }

        public void DisplayConsole()
        {
            Console.WriteLine(menuName);
            Debug.Log(menuName);
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
                Debug.Log("Error: no buttons found in the button list");
            }
        }
    }
}
