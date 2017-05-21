using JonkerBudget.Domain.Models.Categories;
using JonkerBudget.Domain.Models.EscalationDetails;
using JonkerBudget.Domain.Models.Expenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JonkerBudget.Domain.Services.EscalationDetails
{
    public interface IExpenseService
    {
        Task<IEnumerable<ExpenseModel>> GetAllExpenses();
        Task<ExpenseModel> AddExpense(ExpenseModel expenseModel);
        Task AddExpenses(List<ExpenseModel> expenseModels);


    }
}
