using JonkerBudget.Domain.Models.Expenses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JonkerBudget.Domain.Services.EscalationDetails
{
    public interface IExpenseService
    {
        Task<IEnumerable<ExpenseModel>> GetAllExpenses();
        Task<IEnumerable<ExpenseModel>> GetMonthExpenses(int year, string month);
        Task<ExpenseModel> AddExpense(ExpenseModel expenseModel);
        Task AddExpenses(List<ExpenseModel> expenseModels);


    }
}
