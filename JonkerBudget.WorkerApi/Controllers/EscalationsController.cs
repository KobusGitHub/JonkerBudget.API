using AutoMapper;
using DragonFire.Core.Request;
using JonkerBudget.Domain.Services.EscalationDetails;
using JonkerBudget.WorkerApi.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace JonkerBudget.WorkerApi.Controllers
{
    [RoutePrefix("api/Escalations")]

    public class EscalationController : ControllerBase
    {
        private IEscalationDetailService escalationsService;
        private IMapper mapper;

        public EscalationController(
                IEscalationDetailService escalationsService,
               IMapper mapper,
               IRequestInfoProvider requestInfoProvider) : base(requestInfoProvider)
        {
            this.mapper = mapper;
            this.escalationsService = escalationsService;
        }

        [HttpGet]
        [Route("ToProcess")]
        public async Task<IHttpActionResult> Get()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            try
            {
                var result = await escalationsService.GetEscalationsBreached();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPost]
        [Route("UpdateSuccessful")]
        public async Task<IHttpActionResult> UpdateSuccessful(int[] ids)
        {
            await escalationsService.SetEscalationsProcessedAsSuccessAsync(ids);
            return Ok();

        }
    }
}