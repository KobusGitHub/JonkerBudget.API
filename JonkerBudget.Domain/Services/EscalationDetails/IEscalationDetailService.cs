using JonkerBudget.Domain.Models.EscalationDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JonkerBudget.Domain.Services.EscalationDetails
{
    public interface IEscalationDetailService
    {
        Task<IEnumerable<EscalationDetailModelOut>> GetEscalationsBreached();
        Task SetEscalationsProcessedAsSuccessAsync(int[] ids);
    }
}
