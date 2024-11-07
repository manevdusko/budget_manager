using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Income;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class IncomeRepository : IIncomeRepository
    {
        private readonly ApplicationDBContext _context;

        public IncomeRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IncomeDto> CreateAsync(Income incomeModel)
        {
            await _context.Incomes.AddAsync(incomeModel);
            await _context.SaveChangesAsync();

            var createdIncome = await _context.Incomes
                .Include(i => i.Category)
                .Include(i => i.Type)
                .Include(i => i.AppUser)
                .FirstOrDefaultAsync(i => i.Id == incomeModel.Id);

            if (createdIncome == null)
                return null;

            return createdIncome.ToIncomeDto();
        }

        public async Task<Income?> DeleteAsync(int id)
        {
            var incomeModel = _context.Incomes.FirstOrDefault(i => i.Id == id);

            if (incomeModel == null)
            {
                return null;
            }

            _context.Incomes.Remove(incomeModel);
            await _context.SaveChangesAsync();

            return incomeModel;
        }

        public async Task<List<Income>> GetAllAsync()
        {
            return await _context.Incomes.Include(i => i.AppUser).ToListAsync();
        }

        public async Task<Income?> GetByIdAsync(int id)
        {
            return await _context.Incomes.Include(i => i.AppUser).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<List<IncomeDto>> GetUserIncomesAsync(AppUser user)
        {
            return await _context.Incomes
                .Where(i => i.AppUserId == user.Id)
                .Include(i => i.Category)
                .Include(i => i.Type)
                .Include(i => i.AppUser)
                .Select(i => i.ToIncomeDto())
                .ToListAsync();
        }

        public async Task<Income?> UpdateAsync(int id, UpdateIncomeDto incomeModel)
        {
            var existingIncome = _context.Incomes.FirstOrDefault(i => i.Id == id);

            if (existingIncome == null)
            {
                return null;
            }

            existingIncome.Month = incomeModel.Month;
            existingIncome.Year = incomeModel.Year;
            existingIncome.Amount = incomeModel.Amount;

            await _context.SaveChangesAsync();

            return existingIncome;
        }
    }
}