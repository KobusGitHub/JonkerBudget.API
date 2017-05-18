﻿using JonkerBudget.Domain.Models.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JonkerBudget.Domain.Models.Expenses
{
    public class ExpenseModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }

        public CategoryModel Category { get; set; }

        public double ExpenseValue { get; set; }

        public int Year { get; set; }
        public string Month { get; set; }

        public DateTime? RecordDate { get; set; }

        public string expenseCode { get; set; }

    }
}
