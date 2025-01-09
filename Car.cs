using GarageLogic2.enums;

namespace GarageLogic2
{
    internal class Car : Vehicle
    {
        // Protected properties
        protected eCarColor Color { get;}
        protected eNumberOfDoors NumberOfDoors { get; }

        // Constructor
        public Car(
            string i_ModelName,
            string i_LicenseNumber,
            Engine i_Engine,
            List<Wheel> i_Wheels,
            eCarColor i_Color,
            eNumberOfDoors i_NumberOfDoors)
            : base(i_ModelName, i_LicenseNumber, i_Engine, i_Wheels)
        {
            // Validate the number of doors
            if (!Enum.IsDefined(typeof(eNumberOfDoors), i_NumberOfDoors))
            {
                // Dynamically generate the allowed values from the enum
                var allowedValues = string.Join(", ", Enum.GetValues(typeof(eNumberOfDoors)).Cast<eNumberOfDoors>());
                throw new ArgumentException($"Invalid number of doors. Allowed values are: {allowedValues}.");
            }

            Color = i_Color;
            NumberOfDoors = i_NumberOfDoors;
        }
    }
}
