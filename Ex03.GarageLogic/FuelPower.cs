using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public class FuelPower : EnergyPower
    {
        private readonly eFuelType r_FuelType;

        public FuelPower(eFuelType i_FuelType, float i_MaxFuelQuantityInLiters) : base(i_MaxFuelQuantityInLiters)
        {
            r_FuelType = i_FuelType;
        }

        public eFuelType FuelType
        {
            get { return r_FuelType; }
        }
    }
    public enum eFuelType
    {
        Soler = 1,
        Octan95,
        Octan96,
        Octan98,
    }
}
