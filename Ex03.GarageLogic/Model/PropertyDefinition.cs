using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic.Model
{
    public class PropertyDefinition
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public Type PropertyType { get; set; }
        public bool IsEnum { get; set; }
        public string EnumOptions { get; set; }
    }
}
