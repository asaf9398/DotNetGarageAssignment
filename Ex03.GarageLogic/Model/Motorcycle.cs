using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic.Enums;

namespace Ex03.GarageLogic.Model
{
    internal class Motorcycle : Vehicle
    {
        private eMotorcicleLicenceType m_LicenseType;
        private int m_EngineVolume;

        public eMotorcicleLicenceType LicenseType
        {
            get { return m_LicenseType; }
            set
            {
                if (!Enum.IsDefined(typeof(eMotorcicleLicenceType), value))
                {
                    throw new ArgumentException($"Invalid license type: {value}");
                }

                m_LicenseType = value;
            }
        }

        public int EngineVolume
        {
            get { return m_EngineVolume; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Engine volume must be greater than zero.");
                }

                m_EngineVolume = value;
            }
        }

        public Motorcycle(Engine i_Engine, eMotorcicleLicenceType i_LicenseType = eMotorcicleLicenceType.A1, int i_EngineVolume = 0) : base(i_Engine, i_NumberOfWheels: 2, i_MaxAirPressurePerWheel: 32.0f)
        {
            LicenseType = i_LicenseType;
            EngineVolume = i_EngineVolume;
        }

        public override string ToString()
        {
            return $"{base.ToString()}, License Type: {m_LicenseType}, Engine Volume: {m_EngineVolume}cc";
        }
    }

}

