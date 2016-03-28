using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UN_Menu;
using UN_Button;

namespace UN_SingleMenuManager
{
    public sealed class SingleMenuManager
    {
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


        List<Menu> menuList;
        int selectCount = 0;
        //-------------------------------------------------// My added functions
        //acts as a manuel constuctor and feeds the list of menues into this instance
        public void Feed(List<Menu> M)
        {
            menuList = M;
        }
        //private function to deselect the other menues
        private void DeSelectRest(Menu ActiveMenu)
        {            
            //
            foreach (Menu e in menuList)
            {
                if (e != ActiveMenu)
                {
                    e.DeSelect();
                }
            }           
        }
        //
        public void Begin()
        {
            //checks to secure that function can be run if menulist is empty
            if (menuList != null)
            {
                //for startup to make sure that one is selected and only one
                foreach (Menu e in menuList)
                {
                    if (!e.IsSelected())
                    {
                        selectCount++;
                    }
                }
                if (selectCount >= menuList.Count - 1)
                {
                    menuList[0].Select();
                    DeSelectRest(menuList[0]);
                }
            }
            else
            {
                //error message here
                Console.WriteLine("The list of menues have not been added to the single menu manager, as such the function cannot be run, please use the function: SingleMenuManager.Instance.Feed(List<Menu> menuList), before executing other menu manager functions");
            }
        }
        //
        public void DisplayCurrent()
        {

            //checks to secure that function can be run if menulist is empty
            if (menuList != null)
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
            else
            {
                //error message here
                Console.WriteLine("The list of menues have not been added to the single menu manager, as such the function cannot be run, please use the function: SingleMenuManager.Instance.Feed(menuList of your choice), before executing other menu manager functions");
            }
        }
        //
        public void SelectMenu(Menu ChosenMenu)
        {
            //checks to secure that function can be run if menulist is empty
            if (menuList != null)
            {
                ChosenMenu.Select();
                DeSelectRest(ChosenMenu);
            }
            else
            {
                //error message here
                Console.WriteLine("The list of menues have not been added to the single menu manager, as such the function cannot be run, please use the function: SingleMenuManager.Instance.Feed(menuList of your choice), before executing other menu manager functions");
            }
        }

        //-------------------------------------------------//
    }
}

