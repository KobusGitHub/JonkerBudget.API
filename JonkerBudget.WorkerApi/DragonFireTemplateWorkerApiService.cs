using Microsoft.Owin.Hosting;
using System;
using System.Configuration;
using System.ServiceProcess;

namespace JonkerBudget.WorkerApi
{
    partial class JonkerBudgetWorkerApiService : ServiceBase
    {
        StartOptions options = new StartOptions();
        private IDisposable _server = null;
        private string baseAddressPrefix = "http://";
        string controllerAddress = string.Empty;

        public JonkerBudgetWorkerApiService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            options = UpdateOptionsFromConfig(options);

            _server = WebApp.Start<Startup>(options);
            Console.WriteLine("Running Web Api Host. Press any key to exit.");
        }

        private StartOptions UpdateOptionsFromConfig(StartOptions options)
        {            
            var port = ConfigurationManager.AppSettings["port"].ToString();
            var pubAddress = baseAddressPrefix + Environment.MachineName + ":" + port;
            var locAddress = baseAddressPrefix + "localhost" + ":" + port;

            options.Urls.Add(pubAddress);
            options.Urls.Add(locAddress);

            return options;
        }

        protected override void OnStop()
        {
            if (_server != null)
            {
                _server.Dispose();
            }
            base.OnStop();
        }
    }
}
