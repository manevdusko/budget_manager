using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.IncomeCategory;
using api.Models;

namespace api.Interfaces
{
    public interface IIncomeCategoryRepository
    {
        public Task<IncomeCategory> GetIncomeCategoryByIdAsync(int id);
        public Task<List<IncomeCategoryDto>> GetIncomeCategoriesAsync();
        public Task<IncomeCategoryDto> CreateIncomeCategoryAsync(CreateIncomeCategoryDto incomeCategory);
        public Task<IncomeCategory> DeleteIncomeCategoryAsync(IncomeCategory incomeCategory);
    }
}