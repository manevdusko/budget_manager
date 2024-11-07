using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.IncomeCategory;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class IncomeCategoryRepository : IIncomeCategoryRepository
    {
        private readonly ApplicationDBContext _context;

        public IncomeCategoryRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IncomeCategoryDto> CreateIncomeCategoryAsync(CreateIncomeCategoryDto incomeCategory)
        {
            var incomeCategoryModel = new IncomeCategory
            {
                CategoryName = incomeCategory.CategoryName,
            };
            await _context.IncomeCategories.AddAsync(incomeCategoryModel);
            await _context.SaveChangesAsync();

            var savedIncomeCategory = await _context.IncomeCategories
                .FirstOrDefaultAsync(e => e.Id == incomeCategoryModel.Id);
            return savedIncomeCategory.ToIncomeCategoryDto();
        }

        public async Task<IncomeCategory> DeleteIncomeCategoryAsync(IncomeCategory incomeCategory)
        {
            _context.IncomeCategories.Remove(incomeCategory);
            await _context.SaveChangesAsync();
            return incomeCategory;
        }

        public async Task<List<IncomeCategoryDto>> GetIncomeCategoriesAsync()
        {
            return await _context.IncomeCategories.Include(e => e.Incomes).Select(i => i.ToIncomeCategoryDto()).ToListAsync();
        }

        public async Task<IncomeCategory> GetIncomeCategoryByIdAsync(int id)
        {
            return await _context.IncomeCategories.FindAsync(id);
        }

        public async Task<IncomeCategory> UpdateIncomeCategoryAsync(IncomeCategory incomeCategory)
        {
            var existingCategory = await _context.IncomeCategories.FindAsync(incomeCategory.Id);
            if (existingCategory == null)
            {
                return null;
            }

            existingCategory.CategoryName = incomeCategory.CategoryName;
            // Update other properties as needed

            _context.IncomeCategories.Update(existingCategory);
            await _context.SaveChangesAsync();

            return existingCategory;
        }
    }
}