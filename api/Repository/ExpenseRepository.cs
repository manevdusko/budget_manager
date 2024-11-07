using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Expense;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly ApplicationDBContext _context;
        public ExpenseRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Expense> CreateAsync(Expense expenseModel)
        {
            await _context.Expenses.AddAsync(expenseModel);
            await _context.SaveChangesAsync();

            var createdExpense = await _context.Expenses
        .Include(e => e.Category)
        .Include(e => e.Type)
        .Include(e => e.AppUser)
        .FirstOrDefaultAsync(e => e.Id == expenseModel.Id);

            if (createdExpense == null)
                return null;

            return createdExpense;

        }

        public async Task<Expense?> DeleteAsync(int id)
        {
            var expenseModel = _context.Expenses.FirstOrDefault((e) => e.Id == id);

            if (expenseModel == null)
            {
                return null;
            }

            _context.Expenses.Remove(expenseModel);

            await _context.SaveChangesAsync();

            return expenseModel;
        }

        public async Task<List<Expense>> GetAllAsync()
        {
            return await _context.Expenses.Include(e => e.AppUser).ToListAsync();
        }

        public async Task<Expense?> GetByIdAsync(int id)
        {
            return await _context.Expenses.Include(e => e.AppUser).FirstOrDefaultAsync((e) => e.Id == id);
        }

        public async Task<List<ExpenseDto>> GetUserExpensesAsync(AppUser user)
        {
            return await _context.Expenses
        .Where(e => e.AppUserId == user.Id)
        .Include(e => e.Category)
        .Include(e => e.Type)
        .Include(e => e.AppUser)
        .Select(e => e.ToExpenseDto())
        .ToListAsync();
        }

        public async Task<Expense?> UpdateAsync(int id, UpdateExpenseDto expenseModel)
        {
            var existingExpense = _context.Expenses.FirstOrDefault(x => x.Id == id);

            if (existingExpense == null)
            {
                return null;
            }

            existingExpense.Month = expenseModel.Month;
            existingExpense.Year = expenseModel.Year;
            existingExpense.Amount = expenseModel.Amount;

            await _context.SaveChangesAsync();

            return existingExpense;
        }
    }
}