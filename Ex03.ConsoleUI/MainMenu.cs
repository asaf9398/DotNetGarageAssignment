using Ex03.ConsoleUI.Utils;
using Ex03.GarageLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Ex03.ConsoleUI
{
    internal class MainMenu
    {
        private readonly Garage m_Garage;
        private readonly MethodInfo[] m_Methods;

        public MainMenu()
        {
            m_Garage = new Garage();
            m_Methods = typeof(Garage).GetMethods(BindingFlags.Public | BindingFlags.Instance);
        }

        public void Run()
        {
            bool exitRequested = false;

            List<MethodInfo> filteredMethods = new List<MethodInfo>();
            foreach (MethodInfo method in m_Methods)
            {
                if (method.DeclaringType != typeof(object))
                {
                    filteredMethods.Add(method);
                }
            }

            while (!exitRequested)
            {
                Console.Clear();
                DisplayMenuOptions();
                Console.WriteLine("Select an option:");
                string userChoice = Console.ReadLine();

                if (int.TryParse(userChoice, out int option) && option >= 1 && option <= filteredMethods.Count)
                {
                    MethodInfo selectedMethod = filteredMethods[option - 1];
                    GarageUI.InvokeFunction(selectedMethod, m_Garage);
                }
                else if (option == filteredMethods.Count + 1)
                {
                    exitRequested = true;
                }
                else
                {
                    Console.WriteLine("Invalid option. Please try again.");
                }

                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
            }
        }


        private void DisplayMenuOptions()
        {
            Console.WriteLine();
            Console.WriteLine("Garage Management System - Main Menu:");

            int optionIndex = 1;
            foreach (MethodInfo method in m_Methods)
            {
                if (method.DeclaringType != typeof(object))
                {
                    Console.WriteLine($"{optionIndex}. {StringUtils.SplitCamelCase(method.Name)}");
                    optionIndex++;
                }
            }

            Console.WriteLine($"{optionIndex}. Exit");
        }


        private void DisplayMenuOptionsOld()
        {
            Console.WriteLine();
            Console.WriteLine("Garage Management System - Main Menu:");
            for (int i = 0; i < m_Methods.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {m_Methods[i].Name}");
            }
            Console.WriteLine($"{m_Methods.Length + 1}. Exit");
        }
    }
}
