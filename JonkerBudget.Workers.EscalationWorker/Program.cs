using System;
using System.Reflection;
using System.ServiceProcess;

namespace JonkerBudget.Workers.EscalationWorker
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new JonkerBudgetEscalationService()
            };

            if (Environment.UserInteractive)
            {
                Type type = typeof(ServiceBase);
                BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
                MethodInfo method = type.GetMethod("OnStart", flags);

                foreach (ServiceBase service in ServicesToRun)
                {
                    method.Invoke(service, new object[] { args });
                }

                Console.WriteLine("Press any key to exit");
                Console.Read();

                foreach (ServiceBase service in ServicesToRun)
                {
                    service.Stop();
                }
            }
            else
            {
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
