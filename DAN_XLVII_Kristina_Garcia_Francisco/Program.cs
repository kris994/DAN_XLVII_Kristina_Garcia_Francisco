using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace DAN_XLVII_Kristina_Garcia_Francisco
{
    class Program
    {
        public static int vehicleAmount = 0;
        public static Stopwatch stopWatch = new Stopwatch();

        static void Main(string[] args)
        {           
            stopWatch.Start();

            Random rng = new Random();
            vehicleAmount = rng.Next(1, 16);
            
            List<Thread> allVehicleThreads = new List<Thread>();
            Vehicle vehicle = new Vehicle();

            // Create all threads
            for (int i = 1; i < vehicleAmount + 1; i++)
            {
                Thread vehicleThread = new Thread(vehicle.CreateVehicle)
                {
                    Name = "Vehicle_" + i
                };
                allVehicleThreads.Add(vehicleThread);
            }

            // Start all threads at the same time
            foreach (var item in allVehicleThreads)
            {
                item.Start();
            }

            Console.ReadKey();
        }
    }
}
