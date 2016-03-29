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

            SingleMenuManager.Instance.Setup();
            SingleMenuManager.Instance.DisplayCurrent();
            Console.WriteLine("__________________");



            SingleMenuManager.Instance.SelectMenu(0);
            //SingleMenuManager.Instance.DisplayCurrent();

            SingleMenuManager.Instance.MoveUp();
            SingleMenuManager.Instance.MoveUp();
            //SingleMenuManager.Instance.MoveUp();


            //SingleMenuManager.Instance.WhoIsSelected();

            SingleMenuManager.Instance.ActivateButton();
            //SingleMenuManager.Instance.WhoIsSelected();

            /*
            SingleMenuManager.Instance.DisplayCurrent();
            Console.WriteLine("__________________");
            SingleMenuManager.Instance.ActivateButton();
            SingleMenuManager.Instance.DisplayCurrent();
            Console.WriteLine("__________________");
            SingleMenuManager.Instance.ActivateButton();
            SingleMenuManager.Instance.DisplayCurrent();
            */
            /*
            SingleMenuManager.Instance.SelectMenu(1);
            SingleMenuManager.Instance.DisplayCurrent();
            Console.WriteLine("__________________");
            SingleMenuManager.Instance.SelectMenu(2);
            SingleMenuManager.Instance.DisplayCurrent();
            Console.WriteLine("__________________");
            SingleMenuManager.Instance.SelectMenu(3);
            SingleMenuManager.Instance.DisplayCurrent();
            Console.WriteLine("__________________");
            SingleMenuManager.Instance.SelectMenu(4);
            SingleMenuManager.Instance.DisplayCurrent();
            Console.WriteLine("__________________");
            */
            //SingleMenuManager.Instance.test();
            //StartMenu.TestButton();


            //testing end
            Console.WriteLine("###################### Program end, hit key");
            Console.ReadKey();
        }
    }
}
