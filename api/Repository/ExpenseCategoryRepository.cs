using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.ExpenseCategory;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class ExpenseCategoryRepository : IExpenseCategoryRepository
    {
        private readonly ApplicationDBContext _context;

        public ExpenseCategoryRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<ExpenseCategoryDto> CreateExpenseCategoryAsync(CreateExpenseCategoryDto expenseCategory)
        {
            var expenseCategoryModel = new ExpenseCategory
            {
                CategoryName = expenseCategory.CategoryName,
            };

            await _context.ExpenseCategories.AddAsync(expenseCategoryModel);
            await _context.SaveChangesAsync();

            var createdExpenseCategory = await _context.ExpenseCategories
        .FirstOrDefaultAsync(e => e.Id == expenseCategoryModel.Id);

            if (createdExpenseCategory == null)
                return null;

            return createdExpenseCategory.ToExpenseCategoryDto();
        }

        public async Task<ExpenseCategory> DeleteExpenseCategoryAsync(ExpenseCategory expenseCategory)
        {
            _context.ExpenseCategories.Remove(expenseCategory);
            await _context.SaveChangesAsync();
            return expenseCategory;
        }

        public async Task<List<ExpenseCategoryDto>> GetExpenseCategoriesAsync()
        {
            return await _context.ExpenseCategories.Include(e => e.Expenses).Select(e => e.ToExpenseCategoryDto()).ToListAsync();
        }

        public async Task<ExpenseCategory> GetExpenseCategoryByIdAsync(int id)
        {
            return await _context.ExpenseCategories.FindAsync(id);
        }
    }
}