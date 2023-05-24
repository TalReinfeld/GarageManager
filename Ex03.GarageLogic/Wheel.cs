using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private readonly string r_ManufacturerName;
        private readonly float r_MaxPSI;
        private float m_CurrentPSI;

        public Wheel(string i_ManufacturerName, float i_AirPressure, float i_MaxAirPressure)
        {
            r_ManufacturerName = i_ManufacturerName;
            m_CurrentPSI = i_AirPressure;
            r_MaxPSI = i_MaxAirPressure;
        }

        public float MaxAirPSI
        {
            get { return r_MaxPSI; }
        }

        public string ManufacturerName
        {
            get { return r_ManufacturerName; }
        }

        public float CurrentPSI
        {
            get { return m_CurrentPSI; }
        }

        public void AddPSIToWheel(int i_AmountOfPSIToAdd)
        {
            bool pressureNotInRange = m_CurrentPSI + i_AmountOfPSIToAdd > r_MaxPSI;

            if (i_AmountOfPSIToAdd < 0)
            {
                throw new ArgumentException("Error! PSI to add must be positive");
            }

            if (pressureNotInRange == false)
            {
                m_CurrentPSI += i_AmountOfPSIToAdd;
            }
            else
            {
                throw new ValueOutOfRangeException(0, r_MaxPSI, "Wheels PSI");
            }
        }

        public void InflateTiresToMaxPSI()
        {
            m_CurrentPSI = r_MaxPSI;
        }

        public override string ToString()
        {
            string wheelsFullInformation = string.Format(
@"The Wheels manufacturer: {0} 
Current PSI: {1}",
r_ManufacturerName,
m_CurrentPSI);

            return wheelsFullInformation;
        }
    }
}
