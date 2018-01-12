using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JonkerBudget.Application.Dto.Expenses.Dto.In
{
    public class CreateExpenseTestDtoIn
    {
        public int Id { get; set; }

        public Guid CategoryGuidId { get; set; }

        public double ExpenseValue { get; set; }

        public int Year { get; set; }
        public string Month { get; set; }

        public string RecordDate { get; set; }

        public string expenseCode { get; set; }
        public string Comment { get; set; }


        public bool inSync { get; set; }
    }
}
