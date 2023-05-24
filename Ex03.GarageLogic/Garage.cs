﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private readonly Dictionary<string, ClientService> r_Clients;

        public Garage()
        {
            r_Clients = new Dictionary<string, ClientService>();
        }

        public Dictionary<string, ClientService> Clients
        {
            get { return r_Clients; }
        }

        public void EnterNewVehicle(Vehicle i_Vehicle, string i_OwnersName, string i_OwnersPhoneNumber)
        {
            bool isNewClient = !r_Clients.ContainsKey(i_Vehicle.VehicleLicenseNumber);
            ClientService newClient = null;

            if (isNewClient == false)
            {
                r_Clients[i_Vehicle.VehicleLicenseNumber].VehicleStatus = eVehicleStatus.InProgress;
                throw new ArgumentException("Note that this vehicle already in the garage");
            }
            else
            {
                newClient = new ClientService(i_Vehicle, i_OwnersName, i_OwnersPhoneNumber);
                r_Clients.Add(i_Vehicle.VehicleLicenseNumber, newClient);
            }
        }

        public void ChangeVehicleStatus(string i_VehicleNumber, eVehicleStatus i_NewStatus)
        {
            bool isVehicleExist = IsClientExist(i_VehicleNumber);

            if (isVehicleExist == true)
            {
                r_Clients[i_VehicleNumber].VehicleStatus = i_NewStatus;
            }
        }

        public void InflateWheelsToMaxPSI(string i_VehicleNumber)
        {
            bool isVehicleExist = IsClientExist(i_VehicleNumber);

            if (isVehicleExist == true)
            {
                r_Clients[i_VehicleNumber].InflateTiresToMaxPSI();
            }
        }

        public bool AddFuel(string i_VehicleNumber, eFuelType i_FuelType, float i_AmountOfFuelInLiters)
        {
            bool isVehicleExist = IsClientExist(i_VehicleNumber);

            if (isVehicleExist == true)
            {
                r_Clients[i_VehicleNumber].AddFuel(i_FuelType, i_AmountOfFuelInLiters);
            }

            return isVehicleExist;
        }

        public bool ChargeBattery(string i_VehicleNumber, float i_AmountOfMinutes)
        {
            bool isVehicleExist = IsClientExist(i_VehicleNumber);

            if (isVehicleExist == true)
            {
                r_Clients[i_VehicleNumber].ChargeBattery(i_AmountOfMinutes);
            }

            return isVehicleExist;
        }

        public void CheckifAmountOfEnergyToAddInRange(string i_VehicleNumber, float i_AmountOfEnergy)
        {
            bool isVehicleExist = IsClientExist(i_VehicleNumber);

            if (isVehicleExist == true)
            {
                r_Clients[i_VehicleNumber].CheckEnergyToAddInRange(i_AmountOfEnergy);
            }
        }

        public void CheckIfClientVehicleFitsToTypeOfEnergy(string i_VehicleNumber, bool i_IsFuel)
        {
            if (i_IsFuel == true && r_Clients[i_VehicleNumber].IsClientVehicleRunsOnFuel() == false)
            {
                throw new ArgumentException("Error! this vehicle is electric");
            }
            else if (i_IsFuel == false && r_Clients[i_VehicleNumber].IsClientVehicleRunsOnFuel() == true)
            {
                throw new ArgumentException("Error! this vehicle is run on fuel");
            }
        }

        public string GetVehicleInformationByLicenseNumber(string i_VehicleNumber)
        {
            bool isVehicleExist = IsClientExist(i_VehicleNumber);
            string vehicleInformation = string.Empty;

            if (isVehicleExist == true)
            {
                vehicleInformation = r_Clients[i_VehicleNumber].ToString();
            }

            return vehicleInformation;
        }

        public List<string> GetAllLicenseNumberWithOutFilter()
        {
            List<string> licenseNumberOfVehicleInTheGarage = new List<string>();

            foreach (string licenseNumber in r_Clients.Keys)
            {
                licenseNumberOfVehicleInTheGarage.Add(licenseNumber);
            }

            if (licenseNumberOfVehicleInTheGarage.Count == 0)
            {
                licenseNumberOfVehicleInTheGarage.Add("No vehicle in the garage");
            }

            return licenseNumberOfVehicleInTheGarage;
        }

        public List<string> GetAllLicenseNumberWithFilter(eVehicleStatus i_Status)
        {
            List<string> licenseNumberOfVehicleInTheGarage = new List<string>();

            foreach (string licenseNumber in r_Clients.Keys)
            {
                if (i_Status == r_Clients[licenseNumber].VehicleStatus)
                {
                    licenseNumberOfVehicleInTheGarage.Add(licenseNumber);
                }
            }

            if (licenseNumberOfVehicleInTheGarage.Count == 0)
            {
                licenseNumberOfVehicleInTheGarage.Add(string.Format("No vehicle of status {0} in the garage", i_Status.ToString()));
            }

            return licenseNumberOfVehicleInTheGarage;
        }

        public bool IsClientExist(string i_VehicleNumber)
        {
            bool isClientExist = r_Clients.ContainsKey(i_VehicleNumber);

            if (isClientExist == false)
            {
                throw new ArgumentException("Error, this vehicle not exist in the garage");
            }

            return isClientExist;
        }

        public bool IsVehicleInGarage(string i_VehicleNumber)
        {
            return r_Clients.ContainsKey(i_VehicleNumber);
        }

        public void IsGarageEmpty()
        {
            if (r_Clients.Count == 0)
            {
                throw new ArgumentException("Error! no vehicles in the garage");
            }
        }
    }
    public enum eVehicleStatus
    {
        InProgress,
        Fixed,
        Paid
    }
}
