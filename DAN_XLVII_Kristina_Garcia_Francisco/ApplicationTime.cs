using System;

namespace DAN_XLVII_Kristina_Garcia_Francisco
{
    class ApplicationTime
    {
        public void ApplicationTotalRunningTime()
        {
            Program.stopWatch.Stop();
            TimeSpan ts = Program.stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0} seconds and {1} milliseconds",
                ts.Seconds, ts.Milliseconds);
            Console.WriteLine("\nApplication run time: " + elapsedTime);
        }
    }
}
