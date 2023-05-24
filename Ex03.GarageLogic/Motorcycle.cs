using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        private const float k_MaxBatteryTimeInHours = 2.6f;
        private const float k_MaxFuelQuantityInLiters = 6.4f;
        private const eFuelType k_MotorcycleFuelType = eFuelType.Octan98;
        private const int k_MaxWheelsPSI = 31;
        private const int k_AmountOfWheels = 2;
        private readonly EnergyPower.eEnergyType r_EnergyType;
        private eLicenseType m_LicenseType;
        private int m_EngineVolumeInCC;

        public Motorcycle(EnergyPower.eEnergyType i_EngineType)
        {
            m_LicenseType = 0;
            m_EngineVolumeInCC = 0;
            r_EnergyType = i_EngineType;
            this.m_EnergyPower = BuildEnergyPower();
        }

        private enum eLicenseType
        {
            A1 = 1,
            A2,
            AA,
            B1
        }

        public override void GetVehicleParameter(ref List<string> io_VehicleParameters)
        {
            base.GetVehicleParameter(ref io_VehicleParameters);
            io_VehicleParameters.Add("license type");
            io_VehicleParameters.Add("engine volume in CC");
        }

        private void checkLicenseType(string i_LicenseType)
        {
            bool isValid = Enum.TryParse(i_LicenseType, out m_LicenseType);
            int counter = 0;

            if (isValid == false)
            {
                throw new FormatException("invalid input");
            }

            isValid = false;
            foreach (eLicenseType type in Enum.GetValues(typeof(eLicenseType)))
            {
                if (type == m_LicenseType)
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

        private void checkEngineVolume(string i_EngineVolumeInCC)
        {
            bool isValid = int.TryParse(i_EngineVolumeInCC, out m_EngineVolumeInCC);

            if (isValid == false)
            {
                throw new FormatException("invalid input");
            }
        }
        public override void CheckParameter(string i_ParameterType, string i_Parameter, Garage i_Garage)
        {
            if (i_Parameter == string.Empty)
            {
                throw new FormatException("Error! you must enter a value ");
            }
            else if (i_ParameterType == "license type")
            {
                checkLicenseType(i_Parameter);
            }
            else if (i_ParameterType == "engine volume in CC")
            {
                checkEngineVolume(i_Parameter);
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

        public override EnergyPower BuildEnergyPower()
        {
            EnergyPower energyPower = null;

            if (r_EnergyType == EnergyPower.eEnergyType.Fuel)
            {
                energyPower = new FuelPower(k_MotorcycleFuelType, k_MaxFuelQuantityInLiters);
            }
            else if (r_EnergyType == EnergyPower.eEnergyType.Electric)
            {
                energyPower = new ElectricPower(k_MaxBatteryTimeInHours);
            }

            return energyPower;
        }

        public override string CheckIfEnumParameter(string i_Parameter)
        {
            string[] names = null;

            if (i_Parameter == "license type")
            {
                names = Enum.GetNames(typeof(eLicenseType));
            }

            return GetStringOfEnums(names);
        }



        public override string ToString()
        {
            string electricMotorcycleInformation = string.Format(
@"Motorcycle:
Engine type: {0}
{1}
License type: {2}
Engine volume in CC: {3}",
r_EnergyType.ToString(),
base.ToString(),
m_LicenseType,
m_EngineVolumeInCC);

            return electricMotorcycleInformation;
        }
    }
}
