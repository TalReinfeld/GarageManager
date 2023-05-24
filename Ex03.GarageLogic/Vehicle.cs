using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        protected string m_Model;
        protected string m_VehicleLicenseNumber;
        protected float m_WheelsPSI;
        protected string m_WheelsManufacturerName;
        protected EnergyPower m_EnergyPower;
        protected int m_AmountOfWheels;
        protected List<Wheel> m_Wheels;

        protected Vehicle()
        {
            m_Model = string.Empty;
            m_VehicleLicenseNumber = string.Empty;
            m_WheelsManufacturerName = string.Empty;
            m_Wheels = null;
            m_AmountOfWheels = 0;
            m_WheelsPSI = 0;
            m_EnergyPower = null;
        }

        public List<Wheel> Wheels
        {
            get { return m_Wheels; }
            set { m_Wheels = value; }
        }

        public string Model
        {
            get { return m_Model; }
        }

        public float WheelsPSI
        {
            get { return m_WheelsPSI; }
            set { m_WheelsPSI = value; }
        }

        public string VehicleLicenseNumber
        {
            get { return m_VehicleLicenseNumber; }
        }

        public int AmountOfWheels
        {
            get { return m_AmountOfWheels; }
        }

        public void InflateTiresToMaxPSI()
        {
            foreach (Wheel wheel in m_Wheels)
            {
                wheel.InflateTiresToMaxPSI();
            }
        }

        public EnergyPower GetEnergyPower
        {
            get { return m_EnergyPower; }
            set { m_EnergyPower = value; }
        }

        public bool CheckWheelsRange(float i_MaxPSI, float i_WheelsPSI)
        {
            bool inRange = false;

            if (i_WheelsPSI <= i_MaxPSI && i_WheelsPSI >= 0)
            {
                inRange = true;
            }
            else
            {
                throw new ValueOutOfRangeException(0, i_MaxPSI, "wheels PSI");
            }

            return inRange;
        }

        public virtual void GetVehicleParameter(ref List<string> io_VehicleParameters)
        {
            io_VehicleParameters.Add("model");
            io_VehicleParameters.Add("license number");
            io_VehicleParameters.Add("amount of energy left");
            io_VehicleParameters.Add("wheels manufacturer name");
            io_VehicleParameters.Add("wheels PSI");
        }

        public void AddEnergy(float i_AmountOfEnergy)
        {
            m_EnergyPower.AddEnergy(i_AmountOfEnergy);
        }

        public void CheckWheelsPSIAndCreateThem(string i_PSI, float i_MaxWheelsPSI, int i_AmountOfWheels)
        {
            float wheelsPSI = 0;
            bool isValid = float.TryParse(i_PSI, out wheelsPSI);

            if (isValid == false)
            {
                throw new FormatException("invalid input");
            }
            else if (CheckWheelsRange(i_MaxWheelsPSI, wheelsPSI))
            {
                m_WheelsPSI = wheelsPSI;
                createVehicleWheels(i_AmountOfWheels, i_MaxWheelsPSI);
            }
        }

        private void createVehicleWheels(int i_AmountOfWheels, float i_MaxWheelsPSI)
        {
            m_Wheels = new List<Wheel>();

            for (int i = 0; i < i_AmountOfWheels; i++)
            {
                m_Wheels.Add(new Wheel(m_WheelsManufacturerName, m_WheelsPSI, i_MaxWheelsPSI));
            }

            m_AmountOfWheels = i_AmountOfWheels;
        }

        public string GetStringOfEnums(string[] i_Enums)
        {
            string stringOfEnums = string.Empty;
            int counter = 1;

            if (i_Enums != null)
            {
                foreach (string name in i_Enums)
                {
                    stringOfEnums += counter.ToString() + ". " + separateWords(name) + Environment.NewLine;
                    counter++;
                }
            }

            return stringOfEnums;
        }

        private string separateWords(string i_Line)
        {
            StringBuilder newLine = new StringBuilder();
            int i = 0;

            while (i < i_Line.Length)
            {
                if (char.IsUpper(i_Line[i]) == true)
                {
                    newLine.Append(' ');
                }

                newLine.Append(i_Line[i]);
                i++;
            }

            return newLine.ToString();
        }

        public abstract void CheckVehicleWheels(string i_AirPressure, float i_MaxWheelsPressure);

        public abstract string CheckIfEnumParameter(string i_Parameter);

        public abstract EnergyPower BuildEnergyPower();

        private void checkModel(string i_Model)
        {
            for (int i = 0; i < i_Model.Length; i++)
            {
                if (char.IsLetterOrDigit(i_Model[i]) == false && i_Model[i] != ' ')
                {
                    throw new FormatException("Model should contain only letters and number");
                }
            }

            m_Model = i_Model;
        }

        private void checkVehicleLicenseNumber(string i_VehicleLicenseNumber, Garage i_Garage)
        {
            for (int i = 0; i < i_VehicleLicenseNumber.Length; i++)
            {
                if (char.IsLetterOrDigit(i_VehicleLicenseNumber[i]) == false)
                {
                    throw new FormatException("license number should contain only letters and number");
                }
            }

            if (i_Garage.IsVehicleInGarage(i_VehicleLicenseNumber) == true)
            {
                throw new ArgumentException("This vehicle is already exist in the garage");
            }

            m_VehicleLicenseNumber = i_VehicleLicenseNumber;
        }

        private void checkWheelsManufacturerName(string i_ManufacturerName)
        {
            m_WheelsManufacturerName = i_ManufacturerName;
        }

        public virtual void CheckParameter(string i_ParameterType, string i_Parameter, Garage i_Garage)
        {
            if (i_Parameter == string.Empty)
            {
                throw new FormatException("Error! you must enter a value");
            }
            else if (i_ParameterType == "model")
            {
                checkModel(i_Parameter);
            }
            else if (i_ParameterType == "license number")
            {
                checkVehicleLicenseNumber(i_Parameter, i_Garage);
            }
            else if (i_ParameterType == "wheels manufacturer name")
            {
                checkWheelsManufacturerName(i_Parameter);
            }
            else if (i_ParameterType == "amount of energy left")
            {
                checkEnergyTimeLeftInHours(i_Parameter);
            }
        }

        private void checkEnergyTimeLeftInHours(string i_EnergyTimeLeftInHours)
        {
            float energyLeft = 0;
            bool isValid = float.TryParse(i_EnergyTimeLeftInHours, out energyLeft);

            if (isValid == false)
            {
                throw new FormatException("input must be float type");
            }
            else if (energyLeft > m_EnergyPower.MaxEnergyCapacity || energyLeft < 0)
            {
                throw new ValueOutOfRangeException(0, m_EnergyPower.MaxEnergyCapacity, "Energy");
            }

            m_EnergyPower.EnergyLeft = energyLeft;
        }


        public override string ToString()
        {
            string fullVehicleInformation = string.Format(
@"License Vehicle number: {0} 
Model Vehicle name: {1} 
Number of wheels: {2} 
{3}
Current Energy remaining: {4}%
Max energy capacity in the vehicle: {5}",
m_VehicleLicenseNumber,
m_Model,
m_AmountOfWheels,
m_Wheels[0].ToString(),
m_EnergyPower.PercentageOfEnergyLeft,
m_EnergyPower.MaxEnergyCapacity);

            return fullVehicleInformation;
        }
    }
}
