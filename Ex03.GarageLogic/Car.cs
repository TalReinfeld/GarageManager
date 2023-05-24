using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private const float k_MaxBatteryTimeInHours = 5.2f;
        private const float k_MaxFuelQuantityInLiters = 46;
        private const eFuelType k_CarFuelType = eFuelType.Octan95;
        private const int k_MaxWheelsPSI = 33;
        private const int k_AmountOfWheels = 5;
        private readonly EnergyPower.eEnergyType r_EnergyType;
        private eColor m_Color;
        private eAmountOfDoors m_AmountOfDoors;

        public Car(EnergyPower.eEnergyType i_EnergyType)
        {
            r_EnergyType = i_EnergyType;
            m_Color = 0;
            m_AmountOfDoors = 0;
            this.m_EnergyPower = BuildEnergyPower();
        }

        private enum eAmountOfDoors
        {
            Two = 1,
            Three,
            Four,
            Five
        }

        private enum eColor
        {
            White = 1,
            Black,
            Yellow,
            Red
        }
        public override EnergyPower BuildEnergyPower()
        {
            EnergyPower energyPower = null;

            if (r_EnergyType == EnergyPower.eEnergyType.Fuel)
            {
                energyPower = new FuelPower(k_CarFuelType, k_MaxFuelQuantityInLiters);
            }
            else if (r_EnergyType == EnergyPower.eEnergyType.Electric)
            {
                energyPower = new ElectricPower(k_MaxBatteryTimeInHours);
            }

            return energyPower;
        }


        private void checkAmountOfDoors(string i_AmountOfDoors)
        {
            int counter = 0;
            bool isValid = Enum.TryParse(i_AmountOfDoors, out m_AmountOfDoors);

            if (isValid == false)
            {
                throw new FormatException("invalid input");
            }

            isValid = false;
            foreach (eAmountOfDoors num in Enum.GetValues(typeof(eAmountOfDoors)))
            {
                if (num == m_AmountOfDoors)
                {
                    isValid = true;
                }

                counter++;
            }

            if (isValid == false)
            {
                throw new ValueOutOfRangeException(1, counter, "option");
            }
        }

        public override string CheckIfEnumParameter(string i_Parameter)
        {
            string[] names = null;

            if (i_Parameter == "amount of doors")
            {
                names = Enum.GetNames(typeof(eAmountOfDoors));
            }
            else if (i_Parameter == "color")
            {
                names = Enum.GetNames(typeof(eColor));
            }

            return GetStringOfEnums(names);
        }
        public override void GetVehicleParameter(ref List<string> io_VehicleParameters)
        {
            base.GetVehicleParameter(ref io_VehicleParameters);
            io_VehicleParameters.Add("amount of doors");
            io_VehicleParameters.Add("color");
        }

        private void checkColor(string i_Color)
        {
            bool isValid = Enum.TryParse(i_Color, out m_Color);
            int counter = 0;

            if (isValid == false)
            {
                throw new FormatException("invalid input");
            }

            isValid = false;
            foreach (eColor color in Enum.GetValues(typeof(eColor)))
            {
                if (color == m_Color)
                {
                    isValid = true;
                }

                counter++;
            }

            if (isValid == false)
            {
                throw new ValueOutOfRangeException(1, counter, "option");
            }
        }

        public override void CheckParameter(string i_ParameterType, string i_Parameter, Garage i_Garage)
        {
            if (i_Parameter == string.Empty)
            {
                throw new FormatException("Error! you must enter a value ");
            }
            else if (i_ParameterType == "amount of doors")
            {
                checkAmountOfDoors(i_Parameter);
            }
            else if (i_ParameterType == "color")
            {
                checkColor(i_Parameter);
            }
            else if (i_ParameterType == "wheels PSI")
            {
                CheckVehicleWheels(i_Parameter, k_MaxWheelsPSI);
            }
            else
            {
                base.CheckParameter(i_ParameterType, i_Parameter, i_Garage);
            }
        }

        public override void CheckVehicleWheels(string i_PSI, float i_MaxWheelsPSI)
        {
            this.CheckWheelsPSIAndCreateThem(i_PSI, i_MaxWheelsPSI, k_AmountOfWheels);
        }

        public override string ToString()
        {
            string fuelCarInformation = string.Format(
@"Car:
Engine type: {0}
{1}
Car's Color: {2}
Amount of doors: {3}",
r_EnergyType.ToString(),
base.ToString(),
m_Color,
m_AmountOfDoors);

            return fuelCarInformation;
        }
    }
}
