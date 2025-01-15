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
    class Program
    {
        public static void Main(string[] args)
        {
            Garage garage = new Garage();
            MethodInfo[] methods = typeof(Garage).GetMethods();
            int counter = 1;
            Console.WriteLine($"Please enter the wanted funcionallity you want to do:");
            foreach (MethodInfo method in methods)
            {
                if (IsUserDefinedMethod(method))
                {
                    Console.WriteLine($"{counter}) {method.Name}");
                    counter++;
                }
            }
            Console.ReadLine();
        }
        public static bool IsUserDefinedMethod(MethodInfo method)
        {
            if (method.IsSpecialName)
            {
                return false;
            }

            if (method.DeclaringType.Assembly.FullName.Contains("System.Private.CoreLib") ||
                method.DeclaringType.Assembly.FullName.Contains("mscorlib"))
            {
                return false;
            }

            if (method.GetBaseDefinition().DeclaringType == typeof(object))
            {
                return false;
            }

            return true;
        }
    }
}
