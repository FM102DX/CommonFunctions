using Primark.MpeTestingSuite.MpeMetricsCollectionApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primark.MpeTestingSuite.Service.Service.Menu
{
    public class AppMenu
    {
        //TODO сделать через интерфейс способы вывода
        //TODO сделать словарь сообщений, возможно отдельным проектом

        //класс, который делает меню и запускает цикл его исполнения

        private ConsoleHelper conHlp = new ConsoleHelper();

        private List<MenuItem> Items = new List<MenuItem>();
        private string MenuHeader = "";
        private string MenuItemSeparator = "----------------------------";
        public delegate void MenuItemDelegate();
        private string MenuExitText = "";

        public void Run()
        {
            ShowMenu();
        }
        public AppMenu(string menuHeader = "", string menuExitText = "")
        {
            MenuHeader = menuHeader;
            MenuExitText = menuExitText;
        }

        public AppMenu(string menuHeader = "", string menuExitText = "", string menuItemSeparator = "")
        {
            MenuItemSeparator = menuItemSeparator;
            MenuHeader = menuHeader;
            MenuExitText = menuExitText;
        }

        public void SetWritingDevice(ConsoleHelper consoleHelper)
        {
            conHlp = consoleHelper;
        }

        public void AddMenuItemDelegate(int userNumber, string userText, MenuItemDelegate delegate0)
        {
            MenuItem item = new MenuItem
            {
                MenuItemType = MenuItemTypeEnum.Acion,
                UserNumber = userNumber,
                UserText = userText,
                Delegate = delegate0
            };
            Items.Add(item);
        }
        public void AddMenuItemSubMenu(int userNumber, string userText, AppMenu subMenu)
        {
            MenuItem item = new MenuItem
            {
                MenuItemType = MenuItemTypeEnum.SubMenu,
                UserNumber = userNumber,
                UserText = userText,
                SubMenu = subMenu
            };
            subMenu.SetWritingDevice(conHlp);
            Items.Add(item);
        }

        public void AddSeparator()
        {
            MenuItem item = new MenuItem
            {
                MenuItemType = MenuItemTypeEnum.Separartor,
                UserNumber = -1,
                UserText = MenuItemSeparator
            };
            Items.Add(item);
        }

        private class MenuItem
        {
            public int UserNumber { get; set; } //какую цифру надо нажать пользователю, чтобы отработал этот пункт меню
            public string UserText { get; set; }
            public AppMenu SubMenu { get; set; }
            public MenuItemTypeEnum MenuItemType { get; set; }
            public MenuItemDelegate Delegate { get; set; }

            public void Show()
            {
                Console.WriteLine($"{UserNumber}.  {UserText}");
            }
            public void Run()
            {
                switch (MenuItemType)
                {
                    case MenuItemTypeEnum.Acion:
                        Delegate();
                        break;
                    case MenuItemTypeEnum.SubMenu:
                        SubMenu.Run();
                        break;
                }

            }

        }

        private void ShowMenu()
        {

            while (true)
            {
                conHlp.WriteRegular("");
                conHlp.WriteRegular($"{MenuHeader}");
                conHlp.WriteRegular("");
                conHlp.WriteRegular($"Select option below (enter number), or '/' for exit");

                Items.ForEach(x => x.Show());

                int targetNumber = -1;

                string input = Console.ReadLine();

                if (input == @"/") break;

                if (!int.TryParse(input, out targetNumber))
                {
                    conHlp.WriteRegular("Enter valid number or '/' for exit");
                    continue;
                };

                if (!hasMenuItem(targetNumber))
                {
                    conHlp.WriteRed("Enter valid number or '/' for exit");
                    continue;
                };

                //запустить пункт меню
                MenuItem menuItem = getMenuItemByMumberOrNull(targetNumber);

                if (menuItem == null)
                {
                    conHlp.WriteRed("Unknown error, try again");
                    continue;
                }

                menuItem.Run();
            }
        }
        private bool hasMenuItem(int number)
        {
            var rez = Items.Where(x => x.UserNumber == number).ToList();
            return rez.Count > 0;
        }

        private MenuItem getMenuItemByMumberOrNull(int number)
        {
            var rez = Items.Where(x => x.UserNumber == number).ToList();

            if (rez.Count == 0) return null; else return rez.FirstOrDefault();
        }


        private enum MenuItemTypeEnum
        {
            Acion = 1,
            SubMenu = 2,
            Separartor = 3
        }


    }
}
