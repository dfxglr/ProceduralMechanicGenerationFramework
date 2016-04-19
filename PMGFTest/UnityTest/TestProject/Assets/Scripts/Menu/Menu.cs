using System;
using PMFG.PMGUnity.MenuParts;
using System.Collections.Generic;
using PMFG.PMGUnity.Managers;
using UnityEngine;
using System.Collections;


namespace PMFG
{
    namespace PMGUnity
    {
        namespace MenuParts
        {
            public class Menu : MonoBehaviour
            {
                //base variables for this menu
                public string menuName;
                public GameObject menu;
                public Boolean isSelected;
                //int and bool for checking if any of the buttons are selected
                public int ButtonsNotSelectedCount = 0;
                public Boolean noButtonSelected = false;
                //this menu's position
                Vector3 Pos;
                //list of the buttons in this menu
                public List<Button> buttonList;

                //constructor used for the rendered version or aka when we need the positions
                public Menu(string MN, List<Button> BL, Vector3 position)
                {
                    menuName = MN;
                    buttonList = BL;
                    Pos = position;
                }

                //constructor used for the console versions
                public Menu(string MN, List<Button> BL)
                {
                    menuName = MN;
                    buttonList = BL;
                }

                //instantiates the menu in unity terms, with conditions of: position, name, texture, rotation
                public void InstantiateMenu()
                {
                    //instantiates a unity object at position of this object
                    menu = Instantiate(Resources.Load("Prefabs/menu"), Pos + SingleMenuManager.Instance.parrent.transform.position, Quaternion.identity) as GameObject;
                    //----------------------------------//add what the object should do on instantiation here
                    menu.transform.parent = SingleMenuManager.Instance.parrent.transform;
                    //tag the instance with its name
                    menu.name = menuName;
                    //texture
                    //Texture2D menuTex = Resources.Load("Prefabs/" + menuName) as Texture;
                    menu.GetComponent<Renderer>().material.mainTexture = (Texture2D)Resources.Load("Textures/menues/" + menuName) as Texture2D;
                    //Rotation
                    menu.transform.eulerAngles = SingleMenuManager.Instance.LeRotation();
                    //----------------------------------//
                }
            }
        }
    }
}
