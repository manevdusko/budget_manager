using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Expense;
using api.Models;

namespace api.Interfaces
{
    public interface IExpenseRepository
    {
        Task<Expense?> GetByIdAsync(int id);
        Task<Expense> CreateAsync(Expense expenseModel);
        Task<Expense?> UpdateAsync(int id, UpdateExpenseDto expenseModel);
        Task<Expense?> DeleteAsync(int id);
        Task<List<ExpenseDto>> GetUserExpensesAsync(AppUser user);
    }
}