using JonkerBudget.Application.Dto.Expenses.Dto.In;
using JonkerBudget.Application.Dto.Expenses.Dto.Out;
using JonkerBudget.Application.Dto.NotificationTasks.Dto.Out;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JonkerBudget.Application.Services.TaskNotifications
{
    public interface IExpenseApplicationService
    {
        Task<IEnumerable<ExpenseDtoOut>> GetAllExpenses();
        Task<ExpenseDtoOut> AddExpense(CreateExpenseDtoIn createExpenseDtoIn);
        Task AddExpenses(List<CreateExpenseDtoIn> createExpenseDtoInList);
    }
}
