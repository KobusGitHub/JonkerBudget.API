using JonkerBudget.Domain.Entity;
using JonkerBudget.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JonkerBudget.Domain.Entities
{
    public class Expense : BaseEntity
    {
        public Guid CategoryGuidId { get; set; }

        public double ExpenseValue { get; set; }

        public int Year { get; set; }
        public string Month { get; set; }

        public DateTime? RecordDate { get; set; }

        public string expenseCode { get; set; }
        
    }
}
