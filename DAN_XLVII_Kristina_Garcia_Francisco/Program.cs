using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace DAN_XLVII_Kristina_Garcia_Francisco
{
    /// <summary>
    /// The main program class
    /// </summary>
    class Program
    {
        /// <summary>
        /// Creates a random vehicle amount
        /// </summary>
        public static int vehicleAmount = 0;
        /// <summary>
        /// Counts the time that passes in the application
        /// </summary>
        public static Stopwatch stopWatch = new Stopwatch();

        /// <summary>
        /// The main method that starts all threads
        /// </summary>
        /// <param name="args">main arguments</param>
        static void Main(string[] args)
        {           
            stopWatch.Start();

            Random rng = new Random();
            vehicleAmount = rng.Next(1, 16);          
            Vehicle vehicle = new Vehicle();

            // Create all threads
            for (int i = 1; i < vehicleAmount + 1; i++)
            {
                Thread vehicleThread = new Thread(vehicle.CreateVehicle)
                {
                    Name = "Vehicle_" + i
                };
                vehicleThread.Start();
            }

            Console.ReadKey();
        }
    }
}
