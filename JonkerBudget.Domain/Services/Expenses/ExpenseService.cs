﻿using AutoMapper;
using DragonFire.Core.EntityFramework.Uow;
using DragonFire.Core.Repository;
using DragonFire.Core.Request;
using JonkerBudget.Domain.Entities;
using JonkerBudget.Domain.Models.Categories;
using JonkerBudget.Domain.Models.EscalationDetails;
using JonkerBudget.Domain.Models.Expenses;
using JonkerBudget.Domain.Models.NotificationTasks;
using JonkerBudget.Domain.Models.NotificationTaskUpdates;
using JonkerBudget.Domain.Services.Base;
using JonkerBudget.Domain.Services.NotificationTasks;
using JonkerBudget.Domain.Services.NotificationTaskUpdates;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JonkerBudget.Domain.Services.EscalationDetails
{
    public class ExpenseService : DomainService, IExpenseService
    {
        private IMapper mapper;
        private IRepository<Expense> expenseRepository;
        private IUnitOfWork unitOfWork;
       
        public ExpenseService(IRequestInfoProvider requestInfoProvider, 
            IRepository<Expense> expenseRepository,
            IMapper mapper, IUnitOfWork unitOfWork) : base(requestInfoProvider)
        {
            this.expenseRepository = expenseRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        

        public async Task<IEnumerable<ExpenseModel>> GetAllExpenses()
        {
            var expenses = await this.expenseRepository.GetAll().ToListAsync();
            return mapper.Map<List<ExpenseModel>>(expenses); ;
        }

        public async Task<ExpenseModel> AddExpense(ExpenseModel categoryModel)
        {
            var expense = await this.expenseRepository.InsertAsync(mapper.Map<Expense>(categoryModel));
            await unitOfWork.SaveChangesAsync();

            var returnModel = mapper.Map<ExpenseModel>(expense);

            return returnModel;
        }

        public async Task AddExpenses(List<ExpenseModel> categoryModels)
        {
            foreach (var categoryModel in categoryModels)
            {
                await this.expenseRepository.InsertAsync(mapper.Map<Expense>(categoryModel));
            }
            await unitOfWork.SaveChangesAsync();
        }
    }
}