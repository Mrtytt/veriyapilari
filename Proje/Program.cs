using System;
using structures;
using structs;
using CLI;

namespace App {
    public class Globals
    {
        public const string MENU_PATH = "json/Menu.json";
    }
}
class Program
{
    static void Main()
    {
        // Restoran Yönetim Sistemi

        /*
            Yiğit Özdemir 032290024

            Barış Işık 032290004

            Murat Berk Yetiştirir 032290008

            Gökay Göncagül 032111004Ç

            Mehmet Halim Baş 032290157
        */
        Interface.Init();
        Interface.Run();
    }
}