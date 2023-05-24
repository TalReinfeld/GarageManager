using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ElectricPower : EnergyPower
    {
        public ElectricPower(float i_MaxBatteryTimeInHours) : base(i_MaxBatteryTimeInHours)
        {
        }
    }
}
