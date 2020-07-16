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

        /// <summary>
        /// The types of directions a vehicle can take
        /// </summary>
        private string[] AllDirections = { "North", "South" };
        /// <summary>
        /// Used for generating random directions
        /// </summary>
        private Random rng = new Random();
        /// <summary>
        /// Only one vehicle can be created at a time to ensure the correct data is preserved
        /// </summary>
        private static EventWaitHandle vehicleCreating = new AutoResetEvent(true);
        /// <summary>
        /// Counts down until all vehicles were created before letting them pass the bridge
        /// </summary>
        private static CountdownEvent countdownVehiclesFinished = new CountdownEvent(Program.vehicleAmount);
        /// <summary>
        /// List of all created vehicles
        /// </summary>
        public static List<Vehicle> AllVehicles = new List<Vehicle>();
        /// <summary>
        /// Delegate used to send store notifications when triggered
        /// </summary>
        public delegate void Notification();
        /// <summary>
        /// Event that gets triggered when the total amount of vehicleAmount was created
        /// </summary>
        public event Notification OnNotification;

        #region Constructor
        public Vehicle(string name, int orderNumber, string direction)
        {
            Name = name;
            OrderNumber = orderNumber;
            Direction = direction;
        }

        public Vehicle()
        {

        }
        #endregion

        // Run all active notifications
        internal void Notify()
        {
            if (OnNotification != null)
            {
                OnNotification();
            }
        }

        // Creates a vehicle and sends it to the bridge when all are ready
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

            // Making sure threads get correctly queued (their order) in the nextVehicle wait handle
            Thread.Sleep(15);
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
