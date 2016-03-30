using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UN_SingleMenuManager;
using UN_Button;
using UN_Menu;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("###################### Program begin");
            //testing begin

            //these two are a force pair
            //  SingleMenuManager.Instance.ActivateButton();
            //  SingleMenuManager.Instance.SelectFirstOrActiveButton();

            //----------------------------------------------------------------------------------// simulation of a player operating the menu using the 3 otions: moveup, movedown, and activate

            //starts up and shows start menu
            SingleMenuManager.Instance.Setup();
            SingleMenuManager.Instance.DisplayCurrent();
            Console.WriteLine("__________________");

            //moves up in startmenu which selects >exit< and activates it
            SingleMenuManager.Instance.MoveUp();
            SingleMenuManager.Instance.ActivateButton();
            SingleMenuManager.Instance.SelectFirstOrActiveButton(); //this thing is ok but still annoying
            Console.WriteLine("__________________");
            SingleMenuManager.Instance.DisplayCurrent();
            Console.WriteLine("__________________");

            //Moves up in startmenu again which selects >options< and activates it
            SingleMenuManager.Instance.MoveUp();
            SingleMenuManager.Instance.ActivateButton();
            SingleMenuManager.Instance.SelectFirstOrActiveButton();
            Console.WriteLine("__________________");
            SingleMenuManager.Instance.DisplayCurrent();
            Console.WriteLine("__________________");

            //moves down in optionsmenu which selects >tryagain< and activates it
            SingleMenuManager.Instance.MoveDown();
            SingleMenuManager.Instance.ActivateButton();
            SingleMenuManager.Instance.SelectFirstOrActiveButton();
            Console.WriteLine("__________________");
            SingleMenuManager.Instance.DisplayCurrent();
            Console.WriteLine("__________________");

            //dosent move in optionsmenu, >tryagain< is still selected, activates it again
            SingleMenuManager.Instance.ActivateButton();
            SingleMenuManager.Instance.SelectFirstOrActiveButton();
            Console.WriteLine("__________________");
            SingleMenuManager.Instance.DisplayCurrent();
            Console.WriteLine("__________________");

            //moves down in options menu which selects >controls< and activates it
            SingleMenuManager.Instance.MoveDown();
            SingleMenuManager.Instance.ActivateButton();
            SingleMenuManager.Instance.SelectFirstOrActiveButton();
            Console.WriteLine("__________________");
            SingleMenuManager.Instance.DisplayCurrent();
            Console.WriteLine("__________________");

            //tries to moves down in credits menu but only >back< can be selected, proceeds to activates it
            SingleMenuManager.Instance.MoveDown();
            SingleMenuManager.Instance.ActivateButton();
            SingleMenuManager.Instance.SelectFirstOrActiveButton();
            Console.WriteLine("__________________");
            SingleMenuManager.Instance.DisplayCurrent();
            Console.WriteLine("__________________");

            //moves down 3 times in options menu which selects >credits< and activates it
            SingleMenuManager.Instance.MoveDown();
            SingleMenuManager.Instance.MoveDown();
            SingleMenuManager.Instance.MoveDown();
            SingleMenuManager.Instance.ActivateButton();
            SingleMenuManager.Instance.SelectFirstOrActiveButton();
            Console.WriteLine("__________________");
            SingleMenuManager.Instance.DisplayCurrent();
            Console.WriteLine("__________________");

            // in creditsmenu, activates >back<
            SingleMenuManager.Instance.ActivateButton();
            SingleMenuManager.Instance.SelectFirstOrActiveButton();
            Console.WriteLine("__________________");
            SingleMenuManager.Instance.DisplayCurrent();
            Console.WriteLine("__________________");

            //moves up in optionsmenu which selects >back< and activates it
            SingleMenuManager.Instance.MoveUp();
            SingleMenuManager.Instance.ActivateButton();
            SingleMenuManager.Instance.SelectFirstOrActiveButton();
            Console.WriteLine("__________________");
            SingleMenuManager.Instance.DisplayCurrent();
            Console.WriteLine("__________________");

            //congratz on going full circle, menu system ready for more button action, input interaction, and nice display, all the rest is done

            //----------------------------------------------------------------------------------// end of simulation
            //testing end
            Console.WriteLine("###################### Program end, hit key");
            Console.ReadKey();
        }
    }
}
