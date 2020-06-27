using System;
using System.Diagnostics;
using System.Reflection;
using System.ServiceProcess;
using Altn.Service.Core;

namespace Altn.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                System.IO.Directory.SetCurrentDirectory(System.AppDomain.CurrentDomain.BaseDirectory);

                var log = new Manager.Log();
                var assembly = Assembly.GetExecutingAssembly();
                var fvi = FileVersionInfo.GetVersionInfo(assembly.Location);

                log.Info("Service: v" +fvi.FileVersion);

                if (args != null && args.Length == 1)
                {
                    StartJob.Run();
                }
                else
                {
                    var service = new ServiceBase[] { new CoreService() };
                    ServiceBase.Run(service);
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }

            Console.ReadLine();
        }
    }
}