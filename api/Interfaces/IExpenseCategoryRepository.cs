using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.ExpenseCategory;
using api.Models;

namespace api.Interfaces
{
    public interface IExpenseCategoryRepository
    {
        public Task<ExpenseCategory> GetExpenseCategoryByIdAsync(int id);
        public Task<List<ExpenseCategoryDto>> GetExpenseCategoriesAsync();
        public Task<ExpenseCategoryDto> CreateExpenseCategoryAsync(CreateExpenseCategoryDto expenseCategory);
        public Task<ExpenseCategory> DeleteExpenseCategoryAsync(ExpenseCategory expenseCategory);
    }
}