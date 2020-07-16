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

        private string[] AllDirections = {"North", "South"};
        /// <summary>
        /// Used for generating random directions
        /// </summary>
        private Random rng = new Random();
        /// <summary>
        /// Only one vehicle can be created at a time
        /// </summary>
        private static EventWaitHandle vehicleCreating = new AutoResetEvent(true);
        public static List<Vehicle> AllVehicles = new List<Vehicle>();

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
            BridgeOrder bridge = new BridgeOrder();

            string name = "";
            int orederNumber = 0;
            string direction = "";
            
            vehicleCreating.WaitOne();

            name = Thread.CurrentThread.Name;
            orederNumber = AllVehicles.Count + 1;
           
            int vehicleDirection = rng.Next(0, 2);
            direction = AllDirections[vehicleDirection];

            vehicle = new Vehicle(name, orederNumber, direction);
            AllVehicles.Add(vehicle);

            vehicleCreating.Set();

            // Notify the annauncer when all vehicles were created
            if(AllVehicles.Count == Program.vehicleAmount)
            {
                OnNotification = announcer.VehiclePassingMessage;
                Notify();
            }

            bridge.BridgePass();
        }
    }
}
