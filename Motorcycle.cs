using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GarageLogic2.enums;

namespace GarageLogic2
{
    internal class Motorcycle : Vehicle
    {
        protected eMotorcicleLicenceType LicenceType { get; }
        protected int EngineVolumeInCC { get; }

        public Motorcycle(
            string i_ModelName,
            string i_LicenseNumber,
            Engine i_Engine,
            List<Wheel> i_Wheels,
            eMotorcicleLicenceType i_LicenceType,
            int i_EngineVolumeInCC)
            : base(i_ModelName, i_LicenseNumber, i_Engine, i_Wheels)
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

