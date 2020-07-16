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
            string elapsedTime = String.Format("{0:00}:{1:00}",
                ts.Seconds, ts.Milliseconds / 10);
            Console.WriteLine("\nApplication run time: " + elapsedTime);
        }
    }
}
