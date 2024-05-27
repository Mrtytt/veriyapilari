using structures;
using System;
using structs;

namespace CLI
{
    /// <summary>
    /// 
    /// </summary>
    public static class Interface
    {
        public static Menu menu;

        public static OrderQueue orderQueue;

        public static bool shouldClose = false;

        public static void Init()
        {
            menu = structs.Menu.FromPath();
            menu.Init();

            orderQueue = new OrderQueue(menu);
        }

        public static void Run()
        {
            while (!shouldClose)
            {
                getInput();
            }
        }

        public static void getInput()
        {
            Console.Write("> ");
            string val = Console.ReadLine();

            if (val == "")
                ;
            else if (val == "help")
                Info();
            else if (val == "menu")
                Menu();
            else if (val == "exit")
                shouldClose = true;
            else if (val == "orders")
                PrintOrders();
            else if (val == "complete order")
                CompleteOrder();
            else if (val.IndexOf("save") != -1)
                Save(val);
            else if (val.IndexOf("new order") != -1)
                NewOrder(val); 
            else if (val.IndexOf("new category") != -1)
                NewCategory(val);
            else if (val.IndexOf("new food") != -1)
                NewFood(val);
            else if (val.IndexOf("remove category") != -1)
                RemoveCategory(val);
            else if (val.IndexOf("remove food") != -1)
                RemoveFood(val);
            else
                WrongCommand();
        }

        public static void Info()
        {
            Console.WriteLine("help                 : prints this");
            Console.WriteLine("menu                 : prints menu");
            Console.WriteLine("exit                 : exitst program");
            Console.WriteLine("orders               : prints orders");
            Console.WriteLine("complete order       : completes the first order");
            Console.WriteLine("save <file name>     : saves existing menu to file name");
            Console.WriteLine("new order <order...> : add new order, order -> int,int ...");
            Console.WriteLine("new category <name>  : add new category");
            Console.WriteLine("new food <price> <id> <name> <description> <category> : add new food");
            Console.WriteLine("remove category <id>  : remove category");
            Console.WriteLine("remove food <id>      : remove food");
        }

        public static void Menu()
        {
            menu.Print();
        }

        public static void PrintOrders()
        {
            orderQueue.Print();
        }

        public static void CompleteOrder()
        {
            orderQueue.OrderComplete();
        }

        public static void Save(string val)
        {
            string command = "save";
            string input = val.Remove(0, val.IndexOf(command) + command.Length + 1);

            menu.SaveMenu(input);
        }

        public static void NewOrder(string val)
        {
            string command = "new order";
            string input = val.Remove(0,val.IndexOf(command) + command.Length + 1);

            orderQueue.AddOrder(input);
        }

    
        public static void NewCategory(string val)
        {
            string command = "new category";
            string input = val.Remove(0, val.IndexOf(command) + command.Length + 1);

            string[] parts = input.Split(' ');
            
            string name = parts[0];
            int id = Convert.ToInt32(parts[1]);

            menu.addCategory(name,id);
        }

        public static void NewFood(string val)
        {
            string command = "new food";
            string input = val.Remove(0, val.IndexOf(command) + command.Length + 1);

            // <price> <id> <name> <description> <category>
            string[] par = input.Split(' ');

            int a = Convert.ToInt32(par[0]);
            int b = Convert.ToInt32(par[1]);
            string c = par[2];
            string d = par[3];
            int e = Convert.ToInt32(par[4]);

            Category ct = menu.getCategory(e);

            Food newFood = new Food();
            newFood.name = c;
            newFood.description = d;
            newFood.price = a;
            newFood.id = b;

            ct.AddFood(newFood);
        }

        public static void RemoveCategory(string val)
        {
            string command = "remove category";
            string input = val.Remove(0, val.IndexOf(command) + command.Length + 1);

            menu.removeCategory(Convert.ToInt32(input) - 1);
        }

        public static void RemoveFood(string val)
        {
            string command = "remove food";
            string input = val.Remove(0, val.IndexOf(command) + command.Length + 1);

            string[] par = input.Split(',');

            int a = Convert.ToInt32(par[0]) - 1;
            int b = Convert.ToInt32(par[1]) - 1;

            menu.removeFood(a,b);
        }

        public static void WrongCommand()
        {
            Console.WriteLine("command not recognized");
            Console.WriteLine("write \"help\" for help");
        }
    }
}