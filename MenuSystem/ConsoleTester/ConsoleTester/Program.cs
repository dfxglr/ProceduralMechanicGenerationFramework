using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using UN_SingleMenuManager;
using UN_SingleInputManager;
using UN_Button;
using UN_Menu;

namespace ConsoleApplication1
{
    class Program
    {
        [STAThread] // sta thread needed for inputs to work lol because console derp
        static void Main(string[] args)
        {
            Console.WriteLine("###################### Program begin");
            //testing begin


            //--------------------------------------------------//pretend setup function
            //SingleInputManager.Instance.RunMenuFromInputTest();
            SingleMenuManager.Instance.Setup();
            //--------------------------------------------------//


            //--------------------------------------------------//pretend update function
            while(!SingleInputManager.Instance.IsGameQuit())
            {
                SingleInputManager.Instance.InputUpdate();
            }
            //--------------------------------------------------//          

            //testing end
            Console.WriteLine("###################### Program end, hit key");
            //Console.ReadKey();
        }
    }
}
