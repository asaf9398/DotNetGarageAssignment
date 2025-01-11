using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic.Enums;

namespace Ex03.GarageLogic.Model
{
    internal class Motorcycle : Vehicle
    {
        protected eMotorcicleLicenceType LicenceType { get; }
        protected int EngineVolumeInCC { get; }

        public Motorcycle(
            string i_ModelName,
            string i_LicenseNumber,
            List<Wheel> i_Wheels,
            eMotorcicleLicenceType i_LicenceType,
            eEnergyType i_EnergyType,
            float i_EnergyMaxCapacity,
            int i_EngineVolumeInCC, float i_CurrentEnergyAmount = 0) : base(i_ModelName, i_LicenseNumber, i_EnergyType, i_EnergyMaxCapacity, i_Wheels, i_CurrentEnergyAmount)
        {
            // Validate the licence type
            if (!Enum.IsDefined(typeof(eMotorcicleLicenceType), i_LicenceType))
            {
                var allowedValues = string.Join(", ", Enum.GetValues(typeof(eMotorcicleLicenceType)).Cast<eMotorcicleLicenceType>());
                throw new ArgumentException($"Invalid licence type. Allowed values are: {allowedValues}.");
            }

            // Validate the engine volume
            if (i_EngineVolumeInCC <= 0)
            {
                throw new ArgumentException("Engine volume must be a positive value.");
            }

            LicenceType = i_LicenceType;
            EngineVolumeInCC = i_EngineVolumeInCC;
        }
    }
}

