using CommsApi.Client.EmailComms.Classes;
using JonkerBudget.WorkerAPI.Client.Escalations;
using JonkerBudget.WorkerAPI.Client.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace JonkerBudget.Workers
{
    public class Processor : IDisposable
    {
        private string commsAddress;
        private string commsApiUsername;
        private string commsApiPassword;
        private string commsFromAddress;
        private string workerApiAddress;

        public Processor(
            string commsAddress,
            string commsApiUsername,
            string commsApiPassword,
            string commsFromAddress,
            string workerApiAddress)
        {
            this.commsAddress = commsAddress;
            this.commsApiUsername = commsApiUsername;
            this.commsApiPassword = commsApiPassword;
            this.commsFromAddress = commsFromAddress;
            this.workerApiAddress = workerApiAddress;
        }

        public void Dispose()
        {
            //Cleanup
        }

        public async void DoWorkAsync()
        {
            try
            {
                var escalations = await GetEscalationsToProcess();

                if (escalations != null)
                {
                    var remindersSent = await SendEscalations(escalations);
                    UpdateRemindersAsProcessed(remindersSent);
                }
            }
            catch (Exception ex)
            {
                using (CommsApi.Client.CommsApi comms = new CommsApi.Client.CommsApi(commsAddress, commsApiUsername, commsApiPassword))
                {
                    var result = await comms.EmailApi.SendEmailBasicAsync(new SendEmailBasic
                    {
                        From = commsFromAddress,
                        To = ConfigurationManager.AppSettings["ErrorAddress"],
                        Cc = "",
                        Bcc = "",
                        Body = "Escalations Failed to process:" + ex.Message,
                        Subject = "Error sending escalations from JonkerBudget",
                        IsBodyHtml = true
                    });
                }

            }
        }

        private async void UpdateRemindersAsProcessed(List<ProcessedEscalation> processedEscalations)
        {
            await UpdateSuccessEscalations(processedEscalations.Where(r => r.Success == true).ToList());

            await UpdateErrorEscalations(processedEscalations.Where(r => r.Success == false).ToList());
        }

        private async Task UpdateSuccessEscalations(List<ProcessedEscalation> successEscalations)
        {
            List<int> successIds = new List<int>();

            try
            {
                //update success 
                foreach (var escalation in successEscalations)
                {
                    successIds.Add(escalation.Escalation.Id);
                }

                if (successIds.Count > 0)
                {
                    using (var remindersService = new EscalationService(workerApiAddress))
                    {
                        await remindersService.SetEscalationsProcessedAsSuccessAsync(successIds);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task UpdateErrorEscalations(List<ProcessedEscalation> errorEscalations)
        {
            List<int> errorIds = new List<int>();

            try
            {
                //update error 
                foreach (var escalation in errorEscalations)
                {
                    errorIds.Add(escalation.Escalation.Id);
                }

                string errorIdString = string.Empty;
                errorIds.ForEach(s => errorIdString += s.ToString() + ",");

                if (errorIds.Count > 0)
                {
                    //Send an email with the error reminders
                    using (CommsApi.Client.CommsApi comms = new CommsApi.Client.CommsApi(commsAddress, commsApiUsername, commsApiPassword))
                    {
                        var result = await comms.EmailApi.SendEmailBasicAsync(new SendEmailBasic
                        {
                            From = commsFromAddress,
                            To = ConfigurationManager.AppSettings["ErrorAddress"],
                            Cc = "",
                            Bcc = "",
                            Body = "The following Escalation Ids failed to process: " + errorIdString,
                            Subject = "Error sending reminders from JonkerBudget",
                            IsBodyHtml = true
                        });
                    }                 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task<List<ProcessedEscalation>> SendEscalations(IEnumerable<EscalationModel> escalationsToSend)
        {
            var remindersToProcess = BuildEscalationsToProcess(escalationsToSend);
            return await ProcessReminders(remindersToProcess);
        }

        private async Task<List<ProcessedEscalation>> ProcessReminders(List<ProcessedEscalation> toProcess)
        {
            //Connect to the Comms Api
            using (CommsApi.Client.CommsApi comms = new CommsApi.Client.CommsApi(commsAddress, commsApiUsername, commsApiPassword))
            {
                foreach (var reminderToProcess in toProcess)
                {
                    await ProcessEscalations(comms, reminderToProcess);
                }
            }

            return toProcess;
        }

        private async Task ProcessEscalations(CommsApi.Client.CommsApi comms, ProcessedEscalation escalationToProcess)
        {
            var message = string.Format(@"<style type=""text / css"">
                    p {{
                        font-family: ""helvetica"";
                        font-size: 15px;
	                }}
                    td {{
                        font-family: ""helvetica"";
                        font-size: 13px;
	                }}
                </style>
                <p>
                    Good Day,<br />
                    <br />
                    An escalation limit for a task has been reached on JonkerBudget.<br />
                    If you believe this is an error, please contact the administrator immediately.<br />           
                    <br />        
                    Task Description: {0} <br />
                    Task Impact: {1} <br />
                    System Info: {2} <br />
                    Assigned User: {3} <br />
                    </p>", escalationToProcess.Escalation.NotificationTask.Description,
                    escalationToProcess.Escalation.NotificationTask.ImpactDescription,
                    escalationToProcess.Escalation.NotificationTask.SystemInfo,
                    escalationToProcess.Escalation.NotificationTask.AssignedTo.FirstName + " " + escalationToProcess.Escalation.NotificationTask.AssignedTo.Surname);

            message += @"<p> Regards, <br /> 
                    JonkerBudget Administrator <br /> </p>
                    <img style = 'width:50px;height:50px;'  /> <br />
                    <p>This is an automatically generated email, please do not reply to this email address.";

            try
            {
                escalationToProcess.SubmitDateTimeUtc = DateTime.UtcNow;

                //send email through the reminder API
                var result = await comms.EmailApi.SendEmailBasicAsync(new SendEmailBasic
                {
                    From = commsFromAddress,
                    To = escalationToProcess.Escalation.User.Email,
                    Body = message,
                    Subject = "JonkerBudget Escalation",
                    IsBodyHtml = true
                });

                if (result.Success)
                {
                    escalationToProcess.Success = true;
                }
                else
                {
                    escalationToProcess.Message = result.Message;
                }
            }
            catch (Exception ex)
            {
                escalationToProcess.Success = false;
                escalationToProcess.Message = ex.Message;
            }
        }

        private List<ProcessedEscalation> BuildEscalationsToProcess(IEnumerable<EscalationModel> escalations)
        {
            List<ProcessedEscalation> toProcess = new List<ProcessedEscalation>();

            //Build processed Escalations
            foreach (var escalation in escalations)
            {
                toProcess.Add(new ProcessedEscalation()
                {
                    Message = string.Empty,
                    Escalation = escalation,
                    SubmitDateTimeUtc = DateTime.UtcNow,
                    Success = false
                });
            }

            return toProcess;
        }

        private async Task<IEnumerable<EscalationModel>> GetEscalationsToProcess()
        {
            var now = DateTime.Now;

            // good to keep so that escalations dont get sent at night
            if (now.TimeOfDay.Hours > 7 && now.TimeOfDay.Hours < 23)
            {
                using (var escalationsService = new EscalationService(workerApiAddress))
                {
                    var result = await escalationsService.GetEscalationsBreached();
                    return result;
                }
            }

            return null;
        }
    }
}


