using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Ex03.GarageLogic;
using Ex03.GarageLogic.Model;

namespace Ex03.ConsoleUI
{
    class ProgramTest
    {
        public class Program
        {
            public static void Main(string[] args)
            {
                MainMenu menu = new MainMenu();
                menu.Run();
            }
        }
    }
}
