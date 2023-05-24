# GarageManager
This project is a small garage management system.

The system will know how to manage a garage that currently handles five types of vehicles -
• Fuel motorcycle
	(2 wheels with maximum air pressure of 31 PSI, fuel type - Octan98, 6.4 liter fuel tank)
•  electric motorcycle
	(2 wheels with maximum air pressure of 31 PSI, maximum battery time - 2.6 hours)
• Fuel car
	(5 wheels with maximum air pressure of 33 PSI, type fuel - Octan95, 46 liter fuel tank)
•  electric car
	(5 wheels with maximum air pressure of 33 PSI, maximum battery time - 5.2 hours)
• Truck
	(14 wheels with maximum air pressureof 26 PSI, type fuel - Soler, 135 liter fuel tank)

Each vehicle has the following features:
• Model name (string)
• License number (string)
• The percentage of energy remaining in its energy source (for the fuel/electricity meter) (float)
• A collection of wheels

Each wheel has the following features:
o Manufacturer name (string)
o Current air pressure (float)
o Maximum air pressure determined by the manufacturer (float)
o Inflating operation (a method that receives data about how much air to add to the wheel,
	and changes the state of the air pressure if it does not exceed the maximum).
 
A motorcycle (fuel/electric), in addition to the features of a car, also has the following features:
• Type of license from the following options: B1 AA, A2, A1
• Engine volume in cc (int)

A car (regular/electric), in addition to the features of a car, also has the following features:
• Color (the possible colors are: white, black, yellow, red)
• Number of doors (2), 3, 4 or (5).

A truck, in addition to the features of a car, also has the following features:
• Does he transport hazardous materials (bool)
• Cargo volume (float)

In vehicles that run on fuel, you can find the following information and activate the following actions:
• Fuel type (Soler, Octan95, Octan96, Octan98)
• The current amount of fuel in liters (float)
• The current amount of fuel in liters (float)
• The maximum amount of fuel in liters (float)
• Refueling operation (a method that receives an amount of liters to add and a type of fuel, and changes the state of the fuel
	If the type of fuel is suitable and there is no deviation from the size of the tank).

In electric vehicles you can find the following information and activate the following actions:
• Remaining battery time in hours (float)
• Maximum battery time in hours (float)
• Battery charging operation (a method that receives a figure that is the number of hours to add to the battery,
	and "charges" the battery respectively as long as the number of hours does not exceed the maximum).

The following data must be kept on every vehicle in the garage:
• Owner name (string)
• Owner's phone (string)
• The condition of the vehicle in the garage (possible states: under repair, repaired, paid)
o Every vehicle that enters the garage, its initial state is 'under repair.'

The system will provide the following functionality to its user:
1. "Put" a new car into the garage -
	The user enters the license number of the vehicle they wish to enter the garage.
	If it is a vehicle that is already in the garage (according to the license number),
	the system will issue an appropriate message
	and use a vehicle that is already in the garage (and move the status of the vehicle to "under repair.)"
	
	If not - the user selects the type of vehicle he wishes to "put in the garage",
	and then enters its "condition" (its fuel quantity or the state of the battery,
	the current wheels PSI, color (if it's a car),
	type of license (if it is a motorcycle),
	does it drive hazardous materials (if it is a truck), etc.

2. Show the user the list of the license numbers of the vehicles in the garage,
	with the option to filter by their condition is in the garage.
 
3. Change the status of a vehicle in the garage (the data requested from the user is the license number and the new status).

4. To inflate the car's wheels to the maximum (the user enters a license number).

5. Refuel a vehicle powered by fuel (the user enters a license number, type of fuel to fill, amount to fill).

6. Charge an electric vehicle (the user enters the data is the license number, amount of minutes to charge).

7. Display complete vehicle data by license number (license number, model name, owner's name, condition in the garage,
	the details of the wheels (air pressure and manufacturer), fuel condition + fuel type / battery condition,
	and other details relevant to the specific vehicle type).
