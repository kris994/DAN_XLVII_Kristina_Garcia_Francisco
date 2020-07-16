using System;
using System.Collections.Generic;
using System.Linq;

namespace DAN_XLVII_Kristina_Garcia_Francisco
{
    /// <summary>
    /// Notifies the user of changes in the application
    /// </summary>
    class Announcer
    {
        /// <summary>
        /// Notifies the user when all vehicles are ready to pass the bridge, their total number, position and direction
        /// </summary>
        public void VehiclePassingMessage()
        {
            List<Vehicle> allVehicles = Vehicle.AllVehicles.ToList();

            Console.WriteLine("Total amount of vehicles is: {0}\n", Program.vehicleAmount);
            for (int i = 0; i < allVehicles.Count; i++)
            {
                Console.WriteLine("Vehicle order: {0}, Vehicle direction: {1}", allVehicles[i].OrderNumber, allVehicles[i].Direction);

                if (i == allVehicles.Count - 1)
                {
                    Console.WriteLine();
                }
            }
        }
    }
}
