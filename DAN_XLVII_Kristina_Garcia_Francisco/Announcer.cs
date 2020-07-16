using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAN_XLVII_Kristina_Garcia_Francisco
{
    class Announcer
    {
        public void VehiclePassingMessage()
        {
            List<Vehicle> allVehicles = Vehicle.AllVehicles.ToList();

            Console.WriteLine("Total amount of vehicles is: {0}\n", Program.vehicleAmount);
            for (int i = 0; i < allVehicles.Count; i++)
            {
                Console.WriteLine("Vehicle order: {0}, Vehicle direction: {1}", allVehicles[i].OrderNumber, allVehicles[i].Direction);
            }
        }
    }
}
