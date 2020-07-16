using System;
using System.Threading;

namespace DAN_XLVII_Kristina_Garcia_Francisco
{
    class BridgeOrder
    {
        /// <summary>
        /// Letting vehicles set the direction in case there was an vehicle on the opposite side
        /// </summary>
        private static EventWaitHandle waitingDirection = new AutoResetEvent(true);
        /// <summary>
        /// Vehicle can enter the bridge
        /// </summary>
        public static EventWaitHandle nextVehicle = new AutoResetEvent(true);

        public void BridgePass(string vehicleDirection, int order)
        {
            // Only first vehicle can pass and set the initial direction
            waitingDirection.WaitOne();

            // Set the first direction
            if (order == 1)
            {
                Vehicle.currentDirection = vehicleDirection;
                Console.WriteLine("Vehicle {0} is going {1}.", order, vehicleDirection);
                waitingDirection.Set();
                nextVehicle.Set();
                Thread.Sleep(500);
            }
            else if (Vehicle.currentDirection == vehicleDirection)
            {
                Console.WriteLine("Vehicle {0} is going {1}.", order, vehicleDirection);
                waitingDirection.Set();
                nextVehicle.Set();
                Thread.Sleep(500);

                // Claculate the application end time once all vehicles pass
                if (order == Vehicle.AllVehicles.Count)
                {
                    ApplicationTime time = new ApplicationTime();
                    time.ApplicationTotalRunningTime();
                }
            }
            else
            {
                Console.WriteLine("Vehicle {0} is waiting to pass the bridge from {1} side.", order, vehicleDirection);
                Vehicle.currentDirection = vehicleDirection;
                Thread.Sleep(500);
                waitingDirection.Set();
                BridgePass(vehicleDirection, order);
            }
        }
    }
}
