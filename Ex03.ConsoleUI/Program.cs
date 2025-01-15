using System;
using System.Collections.Generic;
using System.Linq;

namespace GarageManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\nהיי! נא לבחור אפשרות רצויה:");
                Console.WriteLine("1. הכנסת רכב חדש למוסך");
                Console.WriteLine("2. הצג רכבים קיימים במוסך");
                Console.WriteLine("3. שינוי סטטוס של הרכב במוסך");
                Console.WriteLine("4. ניפוח אוויר בגלגלים למקסימום");
                Console.WriteLine("5. תדלוק רכב על ידי דלק");
                Console.WriteLine("6. הטענת רכב חשמלי");
                Console.WriteLine("7. הצגת נתונים מלאים");
                Console.WriteLine("8. יציאה");

                Console.Write("נא להזין בחירה: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddNewVehicle();
                        break;
                    case "2":
                        ShowExistingVehicles();
                        break;
                    case "3":
                        ChangeVehicleStatus();
                        break;
                    case "4":
                        InflateTiresToMax();
                        break;
                    case "5":
                        RefuelVehicle();
                        break;
                    case "6":
                        ChargeElectricVehicle();
                        break;
                    case "7":
                        ShowFullVehicleData();
                        break;
                    case "8":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("נא לבחור אופציה תקינה.");
                        break;
                }
            }
        }

        static void AddNewVehicle()
        {
            Console.WriteLine("\n[הוספת רכב חדש למוסך]");
            var vehicle = GarageLogic.VehicleFactory.CreateVehicle();

            foreach (var prop in vehicle.GetType().GetProperties())
            {
                Console.Write($"נא להזין {prop.Name}: ");
                string input = Console.ReadLine();
                prop.SetValue(vehicle, Convert.ChangeType(input, prop.PropertyType));
            }

            GarageLogic.Garage.AddVehicle(vehicle);
            Console.WriteLine("הרכב נוסף בהצלחה.");
        }

        static void ShowExistingVehicles()
        {
            Console.WriteLine("\n[הצגת רכבים קיימים במוסך]");
            Console.WriteLine("1. כל הרכבים");
            Console.WriteLine("2. סינון לפי סוג רכב");

            Console.Write("נא להזין בחירה: ");
            string choice = Console.ReadLine();

            List<GarageLogic.Vehicle> vehicles = GarageLogic.Garage.GetAllVehicles();

            if (choice == "2")
            {
                Console.Write("נא להזין סוג רכב: ");
                string selectedType = Console.ReadLine();
                vehicles = FilterVehiclesByType(vehicles, selectedType);
            }

            foreach (var vehicle in vehicles)
            {
                Console.WriteLine($"מספר רישוי: {vehicle.LicenseNumber}, סוג רכב: {vehicle.Type}");
            }
        }

        static List<GarageLogic.Vehicle> FilterVehiclesByType(List<GarageLogic.Vehicle> vehicles, string selectedType)
        {
            var filteredVehicles = new List<GarageLogic.Vehicle>();
            foreach (var v in vehicles)
            {
                if (v.Type.ToString() == selectedType)
                {
                    filteredVehicles.Add(v);
                }
            }
            return filteredVehicles;
        }

        static void ChangeVehicleStatus()
        {
            Console.WriteLine("\n[שינוי סטטוס של רכב במוסך]");
            Console.Write("נא להזין מספר רישוי: ");
            string licenseNumber = Console.ReadLine();

            var vehicle = GarageLogic.Garage.FindVehicleByLicense(licenseNumber);
            if (vehicle == null)
            {
                Console.WriteLine("הרכב לא נמצא במוסך.");
                return;
            }

            Console.WriteLine("סטטוסים אפשריים:");
            foreach (var status in Enum.GetValues(typeof(GarageLogic.VehicleStatus)))
            {
                Console.WriteLine(status);
            }

            Console.Write("נא להזין סטטוס חדש: ");
            string newStatus = Console.ReadLine();

            vehicle.Status = (GarageLogic.VehicleStatus)Enum.Parse(typeof(GarageLogic.VehicleStatus), newStatus);
            Console.WriteLine("סטטוס הרכב שונה בהצלחה.");
        }

        static void InflateTiresToMax()
        {
            Console.WriteLine("\n[ניפוח אוויר בגלגלים למקסימום]");
            Console.Write("נא להזין מספר רישוי: ");
            string licenseNumber = Console.ReadLine();

            var vehicle = GarageLogic.Garage.FindVehicleByLicense(licenseNumber);
            if (vehicle != null)
            {
                vehicle.InflateTiresToMax();
                Console.WriteLine("כל הגלגלים נופחו למקסימום.");
            }
            else
            {
                Console.WriteLine("הרכב לא נמצא במוסך.");
            }
        }

        static void RefuelVehicle()
        {
            Console.WriteLine("\n[תדלוק רכב]");
            Console.Write("נא להזין מספר רישוי: ");
            string licenseNumber = Console.ReadLine();
            Console.WriteLine("סוגי דלק אפשריים:");

            foreach (var fuelType in Enum.GetValues(typeof(GarageLogic.FuelType)))
            {
                Console.WriteLine(fuelType);
            }

            Console.Write("נא להזין סוג דלק: ");
            string fuelTypeInput = Console.ReadLine();

            Console.Write("נא להזין כמות דלק: ");
            float amount = float.Parse(Console.ReadLine());

            try
            {
                GarageLogic.Garage.Refuel(licenseNumber, (GarageLogic.FuelType)Enum.Parse(typeof(GarageLogic.FuelType), fuelTypeInput), amount);
                Console.WriteLine("הרכב תודלק בהצלחה.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"שגיאה: {ex.Message}");
            }
        }

        static void ChargeElectricVehicle()
        {
            Console.WriteLine("\n[הטענת רכב חשמלי]");
            Console.Write("נא להזין מספר רישוי: ");
            string licenseNumber = Console.ReadLine();

            Console.Write("נא להזין כמות דקות להטענה: ");
            float minutes = float.Parse(Console.ReadLine());

            try
            {
                GarageLogic.Garage.Charge(licenseNumber, minutes);
                Console.WriteLine("הרכב הוטען בהצלחה.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"שגיאה: {ex.Message}");
            }
        }

        static void ShowFullVehicleData()
        {
            Console.WriteLine("\n[הצגת נתונים מלאים]");
            Console.Write("נא להזין מספר רישוי: ");
            string licenseNumber = Console.ReadLine();

            var vehicle = GarageLogic.Garage.FindVehicleByLicense(licenseNumber);
            if (vehicle == null)
            {
                Console.WriteLine("הרכב לא נמצא במוסך.");
                return;
            }

            Console.WriteLine("נתוני הרכב:");
            ShowAllProperties(vehicle);

            if (vehicle.Wheels != null)
            {
                Console.WriteLine("נתוני הגלגלים:");
                foreach (var wheel in vehicle.Wheels)
                {
                    ShowAllProperties(wheel);
                }
            }

            if (vehicle.Engine != null)
            {
                Console.WriteLine("נתוני המנוע:");
                ShowAllProperties(vehicle.Engine);
            }
        }

        static void ShowAllProperties(object obj)
        {
            foreach (var prop in obj.GetType().GetProperties())
            {
                Console.WriteLine($"{prop.Name}: {prop.GetValue(obj)}");
            }
        }
    }
}

