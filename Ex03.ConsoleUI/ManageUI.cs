using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class ManageUI
    {
        private const int k_Exit = 8;

        public static void RunManageUI()
        {
            Garage garage = new Garage();
            int userInput = 0;

            while (userInput != k_Exit)
            {
                try
                {
                    userInput = printMenuAndGetUserChoice();
                    userChoiceAnalysis((eMenu)userInput, garage);
                }
                catch (Exception exception)
                {
                    showExceptionMessage(exception);
                    Console.ReadLine();
                }
            }

            Console.WriteLine(string.Format(
@"Thank you for choosing our service!
Have a nice day!"));
        }

        private static void userChoiceAnalysis(eMenu i_UserChoice, Garage i_Garage)
        {
            if(i_UserChoice == eMenu.EnterNewVehicle)
            {
                enterNewVehicleToGarage(i_Garage);
            }
            else if(i_UserChoice == eMenu.ShowListOfLicenseNumber)
            {
                showListOfLicenseNumber(i_Garage);
            }
            else if (i_UserChoice == eMenu.ChangeVehicleState)
            {
                changeVehicleState(i_Garage);
            }
            else if (i_UserChoice == eMenu.InflateWheelsToMax)
            {
                inflateWheelsToMaxPressure(i_Garage);
            }
            else if (i_UserChoice == eMenu.RefuelGasVehicle)
            {
                refuelGasVehicle(i_Garage);
            }
            else if (i_UserChoice == eMenu.ChargeElectricVehicle)
            {
                chargeElectricVehicle(i_Garage);
            }
            else if (i_UserChoice == eMenu.ShowVehicleData)
            {
                showVehicleData(i_Garage);
            }
        }

        private static void showVehicleData(Garage i_Garage)
        {
            try
            {
                string carLicenseNumberToPrint = getVehicleLicenseNumber();

                Console.Clear();
                Console.WriteLine(i_Garage.GetVehicleInformationByLicenseNumber(carLicenseNumberToPrint));
                Console.WriteLine(string.Empty);
                Console.WriteLine("Press -enter- to continue");
            }
            catch (ArgumentException argumentException)
            {
                showExceptionMessage(argumentException);
            }
            catch (Exception exception)
            {
                showExceptionMessage(exception);
            }

            Console.ReadLine();
        }

        private static void chargeElectricVehicle(Garage i_Garage)
        {
            bool v_ElectirVehicle = true;
            string vehicleNumber = string.Empty;
            float amountOfMinutes = 0;

            i_Garage.IsGarageEmpty();
            vehicleNumber = getVehicleLicenseNumber();
            if (i_Garage.IsClientExist(vehicleNumber) == true)
            {
                i_Garage.CheckIfClientVehicleFitsToTypeOfEnergy(vehicleNumber, !v_ElectirVehicle);
                amountOfMinutes = getAmountOfMinutesToCharge();
                i_Garage.CheckifAmountOfEnergyToAddInRange(vehicleNumber, amountOfMinutes);
                try
                {
                    i_Garage.ChargeBattery(vehicleNumber, amountOfMinutes);
                    Console.WriteLine(Environment.NewLine + "Vehicle number {0} have been charged, Press -enter- to continue", vehicleNumber);
                }
                catch (ArgumentException argumentException)
                {
                    showExceptionMessage(argumentException);
                }
                catch (Exception exception)
                {
                    showExceptionMessage(exception);
                }

                Console.ReadLine();
            }
        }

        private static float getAmountOfMinutesToCharge()
        {
            bool v_IntigerNumber = true;
            float amountOfMinutes = 0;
            string massage = "Please enter amount of minutes to charge";
            string inputName = "amount of minutes for charge";

            getPositiveNumber(ref amountOfMinutes, massage, inputName, !v_IntigerNumber);

            return amountOfMinutes;
        }
  
        private static void refuelGasVehicle(Garage i_Garage)
        {
            int counter = 1;
            int userChoice = 0;
            bool v_FuelVehicle = true;
            string vehicleNumber = string.Empty;
            float amountOfFuel = 0;
            StringBuilder massage = new StringBuilder("Please enter new State");

            massage.AppendLine(Environment.NewLine);
            foreach (eFuelType type in Enum.GetValues(typeof(eFuelType)))
            {
                massage.AppendLine(string.Format("{0}. {1}", counter, wordsSeparate(type.ToString())));
                counter++;
            }

            i_Garage.IsGarageEmpty();
            vehicleNumber = getVehicleLicenseNumber();
            if (i_Garage.IsClientExist(vehicleNumber) == true)
            {
                i_Garage.CheckIfClientVehicleFitsToTypeOfEnergy(vehicleNumber, v_FuelVehicle);
                amountOfFuel = getAmountOfFuelToAdd();
                i_Garage.CheckifAmountOfEnergyToAddInRange(vehicleNumber, amountOfFuel);
                userChoice = getInputByRange(massage.ToString(), 1, 4);
                try
                {
                    i_Garage.AddFuel(vehicleNumber, (eFuelType)userChoice, amountOfFuel);
                    Console.WriteLine(Environment.NewLine + "Vehicle number {0} have been refueled, Press enter to continue", vehicleNumber);
                }
                catch (ArgumentException argumentException)
                {
                    showExceptionMessage(argumentException);
                }
                catch (Exception exception)
                {
                    showExceptionMessage(exception);
                }

                Console.ReadLine();
            }
        }

        private static float getAmountOfFuelToAdd()
        {
            bool v_IntigerNumber = true;
            float amountOfFuel = 0;
            string massage = "Please enter amount of fuel to add (Liter)";
            string inputName = "amount of fuel";

            getPositiveNumber(ref amountOfFuel, massage, inputName, !v_IntigerNumber);

            return amountOfFuel;
        }

        private static void inflateWheelsToMaxPressure(Garage i_Garage)
        {
            string vehicleNumber = string.Empty;

            i_Garage.IsGarageEmpty();
            try
            {
                vehicleNumber = getVehicleLicenseNumber();
                if (i_Garage.IsClientExist(vehicleNumber) == true)
                {
                    i_Garage.InflateWheelsToMaxPSI(vehicleNumber);
                    Console.WriteLine(
                        Environment.NewLine + "Vehicle number {0} now has max wheels PSI {1} Press -enter- to continue",
                        vehicleNumber,
                        Environment.NewLine);
                }
            }
            catch (Exception ex)
            {
                showExceptionMessage(ex);
            }

            Console.ReadLine();
        }

        private static void showExceptionMessage(Exception i_Exception)
        {
            Console.Clear();
            Console.WriteLine(i_Exception.Message + Environment.NewLine + "Press -enter- to continue, and try again");
        }

        private static void changeVehicleState(Garage i_Garage)
        {
            int userChoice = 0;
            string vehicleNumber = string.Empty;
            string massage = string.Format(
@"Please enter new State
1. In progress
2. Fixed
3. Paid");

            i_Garage.IsGarageEmpty();
            vehicleNumber = getVehicleLicenseNumber();
            if (i_Garage.IsClientExist(vehicleNumber) == true)
            {
                userChoice = getInputByRange(massage, 1, 3);
                try
                {
                    i_Garage.ChangeVehicleStatus(vehicleNumber, (eVehicleStatus)userChoice);
                    Console.WriteLine(Environment.NewLine + "Press -enter- to continue");
                }
                catch (Exception ex)
                {
                    showExceptionMessage(ex);
                }

                Console.ReadLine();
            }
        }

        private static void showListOfLicenseNumber(Garage i_Garage)
        {
            List<string> licenseNumber = null;
            int userChoice = 0;
            string massage = string.Format(
@"Would you like to filter?
1. Yes
2. No");

            i_Garage.IsGarageEmpty();
            userChoice = getInputByRange(massage, 1, 2);
            if (userChoice == 1)
            {
                massage = string.Format(
@"1. In Progress
2. Fixed
3. Paid");
                userChoice = getInputByRange(massage, 1, 3);
                licenseNumber = i_Garage.GetAllLicenseNumberWithFilter((eVehicleStatus)userChoice);
            }
            else
            {
                licenseNumber = i_Garage.GetAllLicenseNumberWithOutFilter();
            }

            Console.Clear();
            foreach (string number in licenseNumber)
            {
                Console.WriteLine(number);
            }

            Console.WriteLine(Environment.NewLine + "Press -enter- to continue");
            Console.ReadLine();
        }

        private static void enterNewVehicleToGarage(Garage i_Garage)
        {
            Vehicle newVehicle = null;
            List<string> vehicleParameters = new List<string>();
            string ownersName = getOwnersName();
            string ownersPhoneNumber = getOwnersPhoneNumber();
            VehicleFactory.eVehiclesInSystem selectedVehicle = chooseVehicle();
            try
            {
                newVehicle = createNewVehicle(selectedVehicle, ref vehicleParameters, i_Garage);
                i_Garage.EnterNewVehicle(newVehicle, ownersName, ownersPhoneNumber);
            }
            catch (Exception exception)
            {
                showExceptionMessage(exception);
                Console.ReadLine();
            }
        }

        private static void getAndCheckParameter(Vehicle i_NewVehicle, ref List<string> io_VehicleParametes, Garage i_Garage)
        {
            int size = io_VehicleParametes.Count;
            string userAnswer = string.Empty;
            bool isValidInput = false;

            foreach (string key in io_VehicleParametes)
            {
                Console.Clear();
                isValidInput = false;
                while (isValidInput == false)
                {
                    try
                    {
                        Console.WriteLine("Please enter " + key);
                        Console.WriteLine(i_NewVehicle.CheckIfEnumParameter(key));
                        userAnswer = Console.ReadLine();
                        i_NewVehicle.CheckParameter(key, userAnswer, i_Garage);
                        isValidInput = true;
                    }
                    catch (Exception exception)
                    {
                        Console.Clear();
                        Console.WriteLine(exception.Message + " please try again.");
                    }
                }
            }
        }

        private static Vehicle createNewVehicle(
            VehicleFactory.eVehiclesInSystem i_SelectedVehicle,
            ref List<string> io_VehicleParametes,
            Garage i_Garage)
        {
            Vehicle newVehicle = null;
            newVehicle = VehicleFactory.CreateNewVehicle(i_SelectedVehicle);
            newVehicle.GetVehicleParameter(ref io_VehicleParametes);
            getAndCheckParameter(newVehicle, ref io_VehicleParametes, i_Garage);

            return newVehicle;
        }

        private static string wordsSeparate(string i_Line)
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

        private static string getOwnersPhoneNumber()
        {
            bool isValidInput = false;
            string phoneNumber = string.Empty;

            Console.Clear();
            while (isValidInput == false)
            {
                Console.WriteLine("Please enter owner's phone number");
                isValidInput = true;
                phoneNumber = Console.ReadLine();

                for (int i = 0; i < phoneNumber.Length; i++)
                {
                    if (char.IsDigit(phoneNumber[i]) == false)
                    {
                        isValidInput = false;
                    }
                }

                if (isValidInput == false || phoneNumber == string.Empty)
                {
                    isValidInput = false;
                    Console.Clear();
                    Console.WriteLine("invalid input, phone number should contain only digits");
                }
            }

            return phoneNumber;
        }

        private static string getOwnersName()
        {
            bool isValidInput = false;
            string ownersName = string.Empty;

            Console.Clear();
            while (isValidInput == false)
            {
                Console.WriteLine("Please enter owner's name");
                isValidInput = true;
                ownersName = Console.ReadLine();
                for (int i = 0; i < ownersName.Length; i++)
                {
                    if (char.IsLetter(ownersName[i]) == false)
                    {
                        isValidInput = false;
                    }
                }

                if (isValidInput == false || ownersName == string.Empty)
                {
                    isValidInput = false;
                    Console.Clear();
                    Console.WriteLine("invalid input, name must contain only letter");
                }
            }

            return ownersName;
        }

        private static void getPositiveNumber(ref float io_Number, string i_Massage, string i_InputName, bool i_IsIntigerNumber)
        {
            bool isValidInput = false;
            int number = 0;

            Console.Clear();
            Console.WriteLine(i_Massage);
            while (isValidInput == false)
            {
                isValidInput = true;

                if (i_IsIntigerNumber == true)
                {
                    isValidInput = int.TryParse(Console.ReadLine(), out number);
                    if (isValidInput == true)
                    {
                        io_Number = number;
                    }
                }
                else
                {
                    isValidInput = float.TryParse(Console.ReadLine(), out io_Number);
                }

                if (isValidInput == false || io_Number < 0)
                {
                    isValidInput = false;
                    Console.Clear();
                    Console.WriteLine("Error, {0} in vehicle must be positive", i_InputName);
                    Console.WriteLine(i_Massage);
                }
            }
        }

        private static string getVehicleLicenseNumber()
        {
            bool isValidInput = false;
            string licenseNumber = string.Empty;

            Console.Clear();
            while (isValidInput == false)
            {
                Console.WriteLine("Please enter your vehicle license number");
                isValidInput = true;
                licenseNumber = Console.ReadLine();

                for (int i = 0; i < licenseNumber.Length; i++)
                {
                    if (char.IsLetterOrDigit(licenseNumber[i]) == false)
                    {
                        isValidInput = false;
                    }
                }

                if (isValidInput == false || licenseNumber == string.Empty)
                {
                    isValidInput = false;
                    Console.Clear();
                    Console.WriteLine("invalid input, license number must contain only letter and digits");
                }
            }

            return licenseNumber;
        }

        private static VehicleFactory.eVehiclesInSystem chooseVehicle()
        {
            int counter = 1;
            VehicleFactory.eVehiclesInSystem userVehicle = 0;
            StringBuilder massage = new StringBuilder("Please choose your vehicle");

            massage.AppendLine(Environment.NewLine);
            foreach (VehicleFactory.eVehiclesInSystem type in Enum.GetValues(typeof(VehicleFactory.eVehiclesInSystem)))
            {
                massage.AppendLine(string.Format("{0}. {1}", counter, wordsSeparate(type.ToString())));
                counter++;
            }

            userVehicle = (VehicleFactory.eVehiclesInSystem)getInputByRange(massage.ToString(), 1, counter - 1);

            return userVehicle;
        }

        private enum eMenu
        {
            EnterNewVehicle = 1,
            ShowListOfLicenseNumber,
            ChangeVehicleState,
            InflateWheelsToMax,
            RefuelGasVehicle,
            ChargeElectricVehicle,
            ShowVehicleData
        }

        private static int printMenuAndGetUserChoice()
        {
            const int k_FirstOption = 1;
            const int k_LastOption = 8;
            string menu = string.Format(
@"Welcome to our garage!
Please choose the preferred action:
1. Enter new vehicle to the garage.
2. Present all vehicles license number.
3. Change the state of a vehicle in the garage.
4. Inflate vehicle wheels to max PSI.
5. Refuel a vehicle that run's on gas.
6. Charge vehicle that run's on battery
7. Show vehicle information.
8. Exit");

            return getInputByRange(menu, k_FirstOption, k_LastOption);
        }

        private static int getInputByRange(string i_MessageToPrint, int i_From, int i_To)
        {
            bool isValidInput = false;
            int userInput = 0;

            Console.Clear();
            Console.WriteLine(i_MessageToPrint);
            while (isValidInput == false)
            {
                isValidInput = int.TryParse(Console.ReadLine(), out userInput);
                if (checkIfInputInRange(i_From, i_To, userInput) && isValidInput)
                {
                    isValidInput = true;
                }
                else
                {
                    isValidInput = false;
                    Console.Clear();
                    Console.WriteLine(i_MessageToPrint);
                    Console.WriteLine("Out of range input! Your input should be between {0} to {1}", i_From, i_To);
                }
            }

            return userInput;
        }

        private static bool checkIfInputInRange(int i_From, int i_To, int i_Input)
        {
            return ((i_Input >= i_From) && (i_Input <= i_To));
        }
    }
}
