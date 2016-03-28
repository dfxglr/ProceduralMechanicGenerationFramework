using System;
using System.Collections.Generic;
using UN_Button;
using UN_Menu;

//make a singleton version of this class

namespace UN_MenuManager
{
    public class MenuMananger
    {
        List<Menu> menuList;
        int selectcount = 0;

        //the function from the constructor need to be used here, as static classes does not allow constuctor input
       public MenuMananger(List<Menu> M)
        {
            menuList = M;
        }

        //
        private void DeSelectRest(Menu ActiveMenu)
        {
            foreach (Menu e in menuList)
            {
                if (e != ActiveMenu)
                {
                    e.DeSelect();
                }
            }
        }
        public void Begin()
        {
            //for startup to make sure that one is selected and only one
            foreach (Menu e in menuList)
            {
                if (!e.IsSelected())
                {
                    selectcount++;
                }
            }
            if (selectcount >= menuList.Count - 1)
            {
                menuList[0].Select();
                DeSelectRest(menuList[0]);
            }
        }

        public void DisplayCurrent()
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
        public void SelectMenu(Menu ChosenMenu)
        {
            ChosenMenu.Select();
            DeSelectRest(ChosenMenu);
        }
    }
}

