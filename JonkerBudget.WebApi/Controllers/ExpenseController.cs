using AutoMapper;
using DragonFire.Core.Request;
using JonkerBudget.Application.Services.TaskNotifications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using JonkerBudget.Application.Dto.Expenses.Dto.In;
using JonkerBudget.Application.Dto.Expenses.Dto.Out;

namespace JonkerBudget.WebApi.Controllers
{
    [RoutePrefix("api/Expenses")]
    public class ExpenseController : ControllerBase
    {
        private IMapper mapper;
        private IExpenseApplicationService expenseService;

        public ExpenseController(
            IExpenseApplicationService expenseService,
            IMapper mapper,
            IRequestInfoProvider requestInfoProvider) : base(requestInfoProvider)
        {
            this.expenseService = expenseService;
            this.mapper = mapper;
        }

        //[Authorize(Roles = "User, Administrator")]
        [HttpGet]
        [Route("GetAllExpenses")]
        public async Task<IHttpActionResult> GetAllExpenses()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IEnumerable<ExpenseDtoOut> categories = new List<ExpenseDtoOut>();
            try
            {
                categories = await expenseService.GetAllExpenses();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            if (categories == null)
            {
                return BadRequest();
            }

            return Ok(categories);
        }

        [HttpGet]
        [Route("GetMonthExpenses")]
        public async Task<IHttpActionResult> GetMonthExpenses(int year, string month)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IEnumerable<ExpenseDtoOut> categories = new List<ExpenseDtoOut>();
            try
            {
                categories = await expenseService.GetMonthExpenses(year, month);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            if (categories == null)
            {
                return BadRequest();
            }

            return Ok(categories);
        }


        [HttpPost]
        [Route("AddExpense")]
        public async Task<IHttpActionResult> AddExpense([FromBody]CreateExpenseDtoIn createExpenseDtoIn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var expense = new ExpenseDtoOut();
            //task.CreatedBy = HttpContext.Current.User.Identity.GetUserId();
            try
            {
                expense = await expenseService.AddExpense(createExpenseDtoIn);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            if (expense == null)
            {
                return BadRequest();
            }

            return Ok(expense);
        }

        [HttpPost]
        [Route("AddExpenses")]
        public async Task<IHttpActionResult> AddExpenses([FromBody]List<CreateExpenseDtoIn> createExpenseDtoInList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //task.CreatedBy = HttpContext.Current.User.Identity.GetUserId();
            try
            {
                await expenseService.AddExpenses(createExpenseDtoInList);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            

            return Ok();
        }


        [HttpPost]
        [Route("AddExpensesTest")]
        public async Task<IHttpActionResult> AddExpensesTest([FromBody]List<CreateExpenseTestDtoIn> createExpenseDtoInList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok();
        }
    }
}
