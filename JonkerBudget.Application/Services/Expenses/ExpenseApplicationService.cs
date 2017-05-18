using AutoMapper;
using DragonFire.Core.Request;
using JonkerBudget.Application.Dto.Expenses.Dto.In;
using JonkerBudget.Application.Dto.Expenses.Dto.Out;
using JonkerBudget.Application.Dto.NotificationTasks.Dto.Out;
using JonkerBudget.Application.Services.Base;
using JonkerBudget.Domain.Models.Expenses;
using JonkerBudget.Domain.Models.NotificationTasks;
using JonkerBudget.Domain.Services.EscalationDetails;
using JonkerBudget.Domain.Services.NotificationTasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JonkerBudget.Application.Services.TaskNotifications
{
    public class ExpenseApplicationService : ApplicationService, IExpenseApplicationService
    {

        private IMapper mapper;
        private IExpenseService expenseService;
        private IRequestInfoProvider requestInfoProvider;

        public ExpenseApplicationService(IMapper mapper, IExpenseService expenseService, IRequestInfoProvider requestInfoProvider) : base (requestInfoProvider)
        {
            this.requestInfoProvider = requestInfoProvider;
            this.expenseService = expenseService;
            this.mapper = mapper;
        }

        public async Task<ExpenseDtoOut> AddExpense(CreateExpenseDtoIn createExpenseDtoIn)
        {
            var model = this.mapper.Map<ExpenseModel>(createExpenseDtoIn);
            var task = await expenseService.AddExpense(model);
            var dto = this.mapper.Map<ExpenseDtoOut>(task);
            return dto;
        }

        public async Task AddExpenses(List<CreateExpenseDtoIn> createExpenseDtoInList)
        {

            var models = this.mapper.Map<List<ExpenseModel>>(createExpenseDtoInList);
            await expenseService.AddExpenses(models);
        }

        public async Task<IEnumerable<ExpenseDtoOut>> GetAllExpenses()
        {
            //var currentUserId = requestInfoProvider.Current.UserId;
            var expenseModels = await expenseService.GetAllExpenses();
            var dtos = this.mapper.Map<IEnumerable<ExpenseDtoOut>>(expenseModels);

            return dtos;
        }

    }
}
