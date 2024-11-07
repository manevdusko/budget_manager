using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Expense;
using api.Models;

namespace api.Mappers
{
    public static class ExpenseMappers
    {
        public static ExpenseDto ToExpenseDto(this Expense expenseModel)
        {
            return new ExpenseDto
            {
                Id = expenseModel.Id,
                Month = expenseModel.Month,
                Year = expenseModel.Year,
                Category = expenseModel.Category?.ToExpenseCategoryDto(),
                Amount = expenseModel.Amount,
                Type = expenseModel.Type?.ToTypeDto(),
                CreatedBy = expenseModel?.AppUser?.UserName,
            };
        }

       
    }
}