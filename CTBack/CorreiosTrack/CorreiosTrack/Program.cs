using System;
using DbUp.Helpers;

namespace CorreiosTrack
{
    class Program
    {
        public static void Main(string[] args)
        {

            try
            {
                PredefinedDeploy.Deploy(args);
            }
            catch (Exception eX)
            {
                WriteException(eX);
                Console.Read();
                Environment.Exit(1);
            }

        }

        private static void WriteException(Exception ex, string tabs = "")
        {
            Console.WriteLine("{0}Exception Message: {1}", tabs, ex.Message);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("{0}Exception StackTrace: {1}", tabs, ex.StackTrace);

            if (ex.InnerException != null)
            {
                tabs += "\t";
                WriteException(ex.InnerException, tabs);
            }
            Console.WriteLine("Completed with errors!!!");
        }
    }
}

