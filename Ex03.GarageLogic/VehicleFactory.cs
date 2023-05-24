using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public class VehicleFactory
    {
        public static Vehicle CreateNewVehicle(eVehiclesInSystem i_NewVehicle)
        {
            Vehicle newVehicle = null;

            if (i_NewVehicle == eVehiclesInSystem.FuelMotorcycle)
            {
                newVehicle = new Motorcycle(EnergyPower.eEnergyType.Fuel);
            }
            else if (i_NewVehicle == eVehiclesInSystem.ElectricMotorcycle)
            {
                newVehicle = new Motorcycle(EnergyPower.eEnergyType.Electric);
            }
            else if (i_NewVehicle == eVehiclesInSystem.FuelCar)
            {
                newVehicle = new Car(EnergyPower.eEnergyType.Fuel);
            }
            else if (i_NewVehicle == eVehiclesInSystem.ElectricCar)
            {
                newVehicle = new Car(EnergyPower.eEnergyType.Electric);
            }
            else if (i_NewVehicle == eVehiclesInSystem.FuelTruck)
            {
                newVehicle = new Truck(EnergyPower.eEnergyType.Fuel);
            }
            else
            {
                throw new ArgumentException("Error! vehicle not support in system");
            }

            return newVehicle;
        }

        public enum eVehiclesInSystem
        {
            FuelMotorcycle = 1,
            ElectricMotorcycle,
            FuelCar,
            ElectricCar,
            FuelTruck
        }
    }
}

