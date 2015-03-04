using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using System;

namespace Parent
{
    public class Functions
    {
        // This function will be triggered based on the schedule you have set for this WebJob
        // This function will enqueue a message on an Azure Queue called queue
        public static void ManualTrigger(TextWriter log, int value, [Queue("trigger")] out string message)
        {
            Console.Write("Parent triggered.");
            message = "Success";
        }
    }
}
