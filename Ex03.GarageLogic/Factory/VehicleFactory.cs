using Ex03.GarageLogic.Enums;
using Ex03.GarageLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Ex03.GarageLogic.Factory
{

    internal static class VehicleFactory
    {
        public static Vehicle CreateVehicle(eVehicleType i_VehicleType)
        {
            Vehicle vehicle = null;
            switch (i_VehicleType)
            {
                case eVehicleType.Motorcycle:
                    vehicle = new Motorcycle(new FuelEngine());
                    break;
                case eVehicleType.EMotorcycle:
                    vehicle = new Motorcycle(new ElectricEngine());
                    break;
                case eVehicleType.Car:
                    vehicle = new Car(new FuelEngine());
                    break;
                case eVehicleType.ECar:
                    vehicle = new Car(new ElectricEngine());
                    break;
                case eVehicleType.Truck:
                    vehicle = new Truck(new FuelEngine());
                    break;
                default:
                    throw new ArgumentException($"Vehicle type '{i_VehicleType}' is not supported.");
            }
            return vehicle;
        }
    }
}
