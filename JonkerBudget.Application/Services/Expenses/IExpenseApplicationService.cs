using JonkerBudget.Application.Dto.Expenses.Dto.In;
using JonkerBudget.Application.Dto.Expenses.Dto.Out;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JonkerBudget.Application.Services.TaskNotifications
{
    public interface IExpenseApplicationService
    {
        Task<IEnumerable<ExpenseDtoOut>> GetAllExpenses();
        Task<IEnumerable<ExpenseDtoOut>> GetMonthExpenses(int year, string month);
        Task<ExpenseDtoOut> AddExpense(CreateExpenseDtoIn createExpenseDtoIn);
        Task AddExpenses(List<CreateExpenseDtoIn> createExpenseDtoInList);
    }
}
