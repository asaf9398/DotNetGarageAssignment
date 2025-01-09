using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic2
{
    internal class Truck : Vehicle
    {
        // Protected properties
        protected bool Refrigerator { get; set; }
        protected float CargoVolume { get; }

        // Constructor
        public Truck(
            string i_ModelName,
            string i_LicenseNumber,
            Engine i_Engine,
            List<Wheel> i_Wheels,
            bool i_Refrigerator,
            float i_CargoVolume)
            : base(i_ModelName, i_LicenseNumber, i_Engine, i_Wheels)
        {
            // Validate cargo volume
            if (i_CargoVolume <= 0)
            {
                throw new ArgumentException("Cargo volume must be a positive value.");
            }

            Refrigerator = i_Refrigerator;
            CargoVolume = i_CargoVolume;
        }
    }
}

