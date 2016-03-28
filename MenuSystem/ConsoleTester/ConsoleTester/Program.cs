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


            //manager
            //MenuMananger M;
            //menues
            Menu StartMenu;
            Menu OptionsMenu;
            Menu ControlsMenu;
            Menu CreditsMenu;
            Menu EndMenu;
            //lists for buttons and menues
            List<Button> StartMenuButtons = new List<Button>();
            List<Button> OptionsMenuButtons = new List<Button>();
            List<Button> ControlsMenuButtons = new List<Button>();
            List<Button> CreditsMenuButtons = new List<Button>();
            List<Button> EndMenuButtons = new List<Button>();
            List<Menu> MenuS = new List<Menu>();
            //start menu 
            StartMenuButtons.Add(new Button("Start", 10, 10));
            StartMenuButtons.Add(new Button("Options", 10, 15));
            StartMenuButtons.Add(new Button("Exit", 10, 20));
            StartMenu = new Menu("Welcome to RMGF", StartMenuButtons, 20, 50);
            //options menu
            OptionsMenuButtons.Add(new Button("Restart", 10, 10));
            OptionsMenuButtons.Add(new Button("TryAgain", 10, 15));
            OptionsMenuButtons.Add(new Button("Controls", 10, 20));
            OptionsMenuButtons.Add(new Button("Credits", 10, 20));
            OptionsMenuButtons.Add(new Button("Back", 10, 20));
            OptionsMenu = new Menu("OPTIONS", OptionsMenuButtons, 20, 50);
            //controls menu
            ControlsMenuButtons.Add(new Button("Back", 10, 10));
            ControlsMenu = new Menu("CONTROLS", ControlsMenuButtons, 20, 50);
            //credits menu
            CreditsMenuButtons.Add(new Button("Back", 10, 10));
            CreditsMenu = new Menu("CREDITS", CreditsMenuButtons, 20, 50);
            //End Menu
            EndMenuButtons.Add(new Button("Restart", 10, 10));
            EndMenuButtons.Add(new Button("TryAgain", 10, 15));
            EndMenuButtons.Add(new Button("Exit", 10, 20));
            EndMenu = new Menu("GAME OVER", EndMenuButtons, 20, 50);
            //MenuManager menu list
            MenuS.Add(StartMenu);
            MenuS.Add(OptionsMenu);
            MenuS.Add(ControlsMenu);
            MenuS.Add(CreditsMenu);
            MenuS.Add(EndMenu);
            // adds menus to the single MenuManager, and it is a must
            SingleMenuManager.Instance.Feed(MenuS);
            //testing begin

            
            SingleMenuManager.Instance.Begin();
            SingleMenuManager.Instance.DisplayCurrent();
            Console.WriteLine("_________");
            SingleMenuManager.Instance.SelectMenu(OptionsMenu);
            SingleMenuManager.Instance.DisplayCurrent();
            Console.WriteLine("_________");
            SingleMenuManager.Instance.SelectMenu(StartMenu);
            SingleMenuManager.Instance.DisplayCurrent();
            Console.WriteLine("_________");
            SingleMenuManager.Instance.SelectMenu(CreditsMenu);
            SingleMenuManager.Instance.DisplayCurrent();
            
            //SingleMenuManager.Instance.test();
            //StartMenu.TestButton();


            //testing end
            Console.WriteLine("###################### Program end, hit key");
            Console.ReadKey();
        }
    }
}
