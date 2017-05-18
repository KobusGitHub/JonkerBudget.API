using Newtonsoft.Json;
using JonkerBudget.WorkerAPI.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JonkerBudget.WorkerAPI.Client.Escalations
{
    public class EscalationService : IEscalationService, IDisposable
    {
        private string workerApiAddress;

        public EscalationService(string workerApiAddress)
        {
            this.workerApiAddress = workerApiAddress;
        }

        public void Dispose()
        {
            // currently no cleanup
        }

        public async Task<IEnumerable<EscalationModel>> GetEscalationsBreached()
        {
            var escalations = new List<EscalationModel>();
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(workerApiAddress);

                    var response = await client.GetAsync("api/escalations/toprocess");

                    string responseContent = response.Content.ReadAsStringAsync().Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var escalationsToSend = JsonConvert.DeserializeObject<List<EscalationModel>>(responseContent);
                        return escalationsToSend;
                    }
                    else
                    {
                        var error = response.Content.ReadAsStringAsync().Result;
                        throw new Exception(error);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public async Task SetEscalationsProcessedAsSuccessAsync(List<int> successIds)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(workerApiAddress);

                    var response = await client.PostAsJsonAsync("api/escalations/updateSuccessful", successIds);

                    string responseContent = response.Content.ReadAsStringAsync().Result;

                    if (response.IsSuccessStatusCode)
                    {
                        //successfully updated
                    }
                    else
                    {
                        var error = response.Content.ReadAsStringAsync().Result;
                        throw new Exception(error);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
