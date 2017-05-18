using System;
using System.Configuration;
using System.ServiceProcess;
using System.Timers;

namespace JonkerBudget.Workers.EscalationWorker
{
    partial class JonkerBudgetEscalationService : ServiceBase
    {
        Timer timer = null;

        string commsAddress = string.Empty;
        string commsApiUsername = string.Empty;
        string commsApiPassword = string.Empty;
        string commsFromAddress = string.Empty;
        string workerApiAddress = string.Empty;
        int timerIntervalInMinutes = 10;

        public JonkerBudgetEscalationService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            ReadConfig();
            SetupTimer();
            DoInitialWork();
        }

        protected override void OnStop()
        {
            if (timer != null)
                timer.Dispose();
        }

        private void DoInitialWork()
        {
            //do initial work, and then start timing...
            try
            {
                using (var processor = new Processor(commsAddress, commsApiUsername, commsApiPassword, commsFromAddress, workerApiAddress))
                {
                    processor.DoWorkAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error doing initial work : ", ex.ToString());
            }

            timer.Start();
        }

        private void SetupTimer()
        {
            timer = new Timer();
            timer.Elapsed += Timer_Elapsed;
            timer.Interval = 1000 * 60 * timerIntervalInMinutes;
        }
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            timer.Stop();

            try
            {
                using (var processor = new Processor(commsAddress, commsApiUsername, commsApiPassword, commsFromAddress, workerApiAddress))
                {
                    processor.DoWorkAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error doing work : ", ex.ToString());
            }

            timer.Start();
        }

        private void ReadConfig()
        {
            timerIntervalInMinutes = int.Parse(ConfigurationManager.AppSettings["TimerIntervalInMins"].ToString());
            commsAddress = ConfigurationManager.AppSettings["CommsApiAddress"].ToString();
            commsApiUsername = ConfigurationManager.AppSettings["CommsApiUsername"].ToString();
            commsApiPassword = ConfigurationManager.AppSettings["CommsApiPassword"].ToString();
            commsFromAddress = ConfigurationManager.AppSettings["CommsApiFromAddress"].ToString();
            workerApiAddress = ConfigurationManager.AppSettings["WorkerApiAddress"].ToString();
        }
    }
}
