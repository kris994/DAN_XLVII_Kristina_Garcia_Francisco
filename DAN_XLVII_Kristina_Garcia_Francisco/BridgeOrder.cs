using System;
using System.Threading;

namespace DAN_XLVII_Kristina_Garcia_Francisco
{
    /// <summary>
    /// Controls the way cars can pass the bridge
    /// </summary>
    class BridgeOrder
    {
        /// <summary>
        /// This is used to control the vehicles direction in case a new vehicle with the oposing direction arrived.
        /// </summary>
        private static EventWaitHandle waitingDirection = new AutoResetEvent(true);
        /// <summary>
        /// Next vehicle can enter the bridge, used to preserve the correct data of the vehicle.
        /// </summary>
        public static EventWaitHandle nextVehicle = new AutoResetEvent(true);
        /// <summary>
        /// Saves the current bridge passing direction.
        /// </summary>
        public static string currentDirection;

        public void BridgePass(string vehicleDirection, int order)
        {
            ApplicationTime time = new ApplicationTime();
            // Only one veichle at a time can set the direction depending on their order
            waitingDirection.WaitOne();

            // Initially only the first vehicle that enters the bridge can set the starting direction
            if (order == 1)
            {
                // Set the initial direction equal to the first vehicles direction
                currentDirection = vehicleDirection;
                Console.WriteLine("Vehicle {0} is going {1}.", order, vehicleDirection);
                // Now every other vehicle that enters the bridge can adjust the direction depending on their order, one at a time .
                waitingDirection.Set();
                // Allows the next vehicle to enter the bridge
                nextVehicle.Set();
                // Time it takes for the vehicle to pass the bridge
                Thread.Sleep(500);

                time.ApplicationTotalRunningTime(order);
            }
            // Vehicle with same direction as current bridge direction and it is not the first vehicle
            else if (currentDirection == vehicleDirection)
            {
                Console.WriteLine("Vehicle {0} is going {1}.", order, vehicleDirection);
                // Next vehicle can pess and set their own bridge direction in case it changed
                waitingDirection.Set();
                nextVehicle.Set();
                // Time it takes for the vehicle to pass the bridge
                Thread.Sleep(500);

                time.ApplicationTotalRunningTime(order);
            }
            else
            {
                Console.WriteLine("Vehicle {0} is waiting to pass the bridge from {1} side.", order, vehicleDirection);
                // Change the direction of the bridge to the current vehicles direction
                currentDirection = vehicleDirection;
                // Waiting time for the vehicle in the oposite direction to cross the bridge
                Thread.Sleep(500);
                waitingDirection.Set();
                // Now only this vehicle can enter the bridge but no other since nextVehicle event handler was not Set();
                BridgePass(vehicleDirection, order);
            }
        }
    }
}
