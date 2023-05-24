using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ClientService
    {
        private readonly string r_OwnersName;
        private readonly string r_OwnersPhoneNumber;
        private eVehicleStatus m_Status;
        private Vehicle m_Vehicel;

        public ClientService(Vehicle i_Vehicel, string i_OwnersName, string i_OwnersPhoneNumber)
        {
            r_OwnersName = i_OwnersName;
            r_OwnersPhoneNumber = i_OwnersPhoneNumber;
            m_Status = eVehicleStatus.InProgress;
            m_Vehicel = i_Vehicel;
        }

        public eVehicleStatus VehicleStatus
        {
            get { return m_Status; }
            set { m_Status = value; }
        }

        public void InflateTiresToMaxPSI()
        {
            m_Vehicel.InflateTiresToMaxPSI();
        }

        public void ChargeBattery(float i_AmountOfMinutes)
        {
            ElectricPower electricEnergyPower = m_Vehicel.GetEnergyPower as ElectricPower;

            if (electricEnergyPower != null)
            {
                m_Vehicel.AddEnergy(i_AmountOfMinutes / 60);
            }
            else
            {
                throw new ArgumentException("Error! the vehicle is not electric");
            }
        }
       
        public bool IsClientVehicleRunsOnFuel()
        {
            bool isFuelVehicle = false;
            FuelPower fuelEngine = m_Vehicel.GetEnergyPower as FuelPower;

            if (fuelEngine != null)
            {
                isFuelVehicle = true;
            }

            return isFuelVehicle;
        }

        public void AddFuel(eFuelType i_FuelType, float i_AmountOfFuel)
        {
            FuelPower fuelEngine = m_Vehicel.GetEnergyPower as FuelPower;

            if (fuelEngine != null)
            {
                if (fuelEngine.FuelType == i_FuelType)
                {
                    m_Vehicel.AddEnergy(i_AmountOfFuel);
                }
                else
                {
                    throw new ArgumentException("Error! the fuel does not match the vehicle");
                }
            }
            else
            {
                throw new ArgumentException("Error! the vehicle does not run on fuel");
            }
        }
        public void CheckEnergyToAddInRange(float i_AmountOfEnergyToAdd)
        {
            FuelPower fuelEnergyPower = m_Vehicel.GetEnergyPower as FuelPower;
            ElectricPower electricEnergyPower = m_Vehicel.GetEnergyPower as ElectricPower;
            float maxEnergyToAdd = 0;

            if (fuelEnergyPower != null)
            {
                if (fuelEnergyPower.EnergyLeft + i_AmountOfEnergyToAdd > fuelEnergyPower.MaxEnergyCapacity)
                {
                    maxEnergyToAdd = fuelEnergyPower.MaxEnergyCapacity - fuelEnergyPower.EnergyLeft + 1;
                    throw new ValueOutOfRangeException(0, maxEnergyToAdd - 1, "amount of fuel you can add in liters to this vehicle");
                }
            }
            else if (electricEnergyPower != null)
            {
                if (electricEnergyPower.EnergyLeft + (i_AmountOfEnergyToAdd / 60) > electricEnergyPower.MaxEnergyCapacity)
                {
                    maxEnergyToAdd = (electricEnergyPower.MaxEnergyCapacity - electricEnergyPower.EnergyLeft) * 60;
                    throw new ValueOutOfRangeException(0, maxEnergyToAdd, "amount of minutes you can charge this vehicle");
                }
            }
        }



        public override string ToString()
        {
            string clientVehicleInformation = string.Format(
@"Vehicle's owner name: {0}
Vehicle status in the garage: {1}
{2}",
r_OwnersName,
m_Status,
m_Vehicel.ToString());

            return clientVehicleInformation;
        }
    }
}
