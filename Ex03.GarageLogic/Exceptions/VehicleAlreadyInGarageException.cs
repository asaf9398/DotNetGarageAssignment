using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Ex03.GarageLogic.Exceptions
{
    public class VehicleAlreadyInGarageException : Exception
    {
        public VehicleAlreadyInGarageException(string i_Message) : base(i_Message)
        {

        }
    }
}
