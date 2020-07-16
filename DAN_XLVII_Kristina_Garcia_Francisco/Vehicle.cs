using System;
using System.Collections.Generic;
using System.Threading;

namespace DAN_XLVII_Kristina_Garcia_Francisco
{
    class Vehicle
    {
        #region Property
        public string Name { get; set; }
        public int OrderNumber { get; set; }
        public string Direction { get; set; }
        #endregion

        private string[] AllDirections = { "North", "South" };
        /// <summary>
        /// Used for generating random directions
        /// </summary>
        private Random rng = new Random();
        /// <summary>
        /// Only one vehicle can be created at a time
        /// </summary>
        private static EventWaitHandle vehicleCreating = new AutoResetEvent(true);
        private static CountdownEvent countdownVehiclesFinished = new CountdownEvent(Program.vehicleAmount);
        public static List<Vehicle> AllVehicles = new List<Vehicle>();
        public static string currentDirection = "";

        public delegate void Notification();
        public event Notification OnNotification;

        public Vehicle(string name, int orderNumber, string direction)
        {
            Name = name;
            OrderNumber = orderNumber;
            Direction = direction;
        }

        public Vehicle()
        {

        }

        internal void Notify()
        {
            if (OnNotification != null)
            {
                OnNotification();
            }
        }

        public void CreateVehicle()
        {
            Vehicle vehicle = new Vehicle();
            Announcer announcer = new Announcer();

            string name = "";
            int orederNumber = 0;
            string direction = "";

            // Create each vehicle one by one
            vehicleCreating.WaitOne();

            name = Thread.CurrentThread.Name;
            orederNumber = AllVehicles.Count + 1;

            int vehicleDirection = rng.Next(0, 2);
            direction = AllDirections[vehicleDirection];

            vehicle = new Vehicle(name, orederNumber, direction);
            AllVehicles.Add(vehicle);

            // Notify the annauncer when all vehicles were created
            if (AllVehicles.Count == Program.vehicleAmount)
            {
                OnNotification = announcer.VehiclePassingMessage;
                Notify();
            }

            // Signal when the vehicle finished creating
            countdownVehiclesFinished.Signal();

            vehicleCreating.Set();
            // Let one by one vehicle enter the bridge, but the first can immediately pass
            BridgeOrder.nextVehicle.WaitOne();
            countdownVehiclesFinished.Wait();

            BridgeOrder bridge = new BridgeOrder();

            // Once all vehicles finished creating, they can pass the bridge         
            bridge.BridgePass(direction, orederNumber);
        }
    }
}
