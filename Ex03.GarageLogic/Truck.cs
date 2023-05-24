using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private const float k_MaxFuelQuantityInLiters = 135;
        private const int k_MaxWheelsPSI = 26;
        private const int k_AmountOfWheels = 14;
        private const eFuelType k_TruckFuelType = eFuelType.Soler;
        private readonly EnergyPower.eEnergyType r_EnergyType;
        private bool m_TransportsHazardousMaterials;
        private float m_TrunkVolume;

        public Truck(EnergyPower.eEnergyType i_EngineType)
        {
            m_TransportsHazardousMaterials = false;
            m_TrunkVolume = 0;
            r_EnergyType = i_EngineType;
            this.m_EnergyPower = BuildEnergyPower();
        }

        public bool TransportTrunkWithHazardMaterials
        {
            get { return m_TransportsHazardousMaterials; }
            set { m_TransportsHazardousMaterials = value; }
        }

        public float TrunkVolume
        {
            get { return m_TrunkVolume; }
            set { m_TrunkVolume = value; }
        }

        public override void GetVehicleParameter(ref List<string> io_VehicleParameters)
        {
            base.GetVehicleParameter(ref io_VehicleParameters);
            io_VehicleParameters.Add("is trunk with hazard materials true/false");
            io_VehicleParameters.Add("Trunk volume");
        }

        private void checkTrunkWithHazardMaterials(string i_TrunkWithHazardMaterials)
        {
            bool isValid = bool.TryParse(i_TrunkWithHazardMaterials, out m_TransportsHazardousMaterials);

            if (isValid == false)
            {
                throw new FormatException("invalid input");
            }
        }

        private void checkTrunkVolume(string i_TrunkVolume)
        {
            bool isValid = float.TryParse(i_TrunkVolume, out m_TrunkVolume);

            if (isValid == false)
            {
                throw new FormatException("invalid input");
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
                energyPower = new FuelPower(k_TruckFuelType, k_MaxFuelQuantityInLiters);
            }

            return energyPower;
        }

        public override void CheckParameter(string i_ParameterType, string i_Parameter, Garage i_Garage)
        {
            if (i_Parameter == string.Empty)
            {
                throw new FormatException("Error! you must enter a value");
            }
            else if (i_ParameterType == "is trunk with hazard materials true/false")
            {
                checkTrunkWithHazardMaterials(i_Parameter);
            }
            else if (i_ParameterType == "Trunk volume")
            {
                checkTrunkVolume(i_Parameter);
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

        public override string CheckIfEnumParameter(string i_Parameter)
        {
            string[] names = null;

            return GetStringOfEnums(names);
        }

        public override string ToString()
        {
            string fuelTrackInformation = string.Format(
@"Truck:
Engine type: {0}
{1}
Transport trunk with hazardous materials: {2}
Trunk volume: {3}",
r_EnergyType.ToString(),
base.ToString(),
m_TransportsHazardousMaterials,
m_TrunkVolume);

            return fuelTrackInformation;
        }
    }
}
