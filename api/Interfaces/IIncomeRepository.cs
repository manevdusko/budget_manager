using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Income;
using api.Models;

namespace api.Interfaces
{
    public interface IIncomeRepository
    {
        Task<IncomeDto> CreateAsync(Income incomeModel);
        Task<Income?> DeleteAsync(int id);
        Task<Income?> GetByIdAsync(int id);
        Task<List<IncomeDto>> GetUserIncomesAsync(AppUser user);
        Task<Income?> UpdateAsync(int id, UpdateIncomeDto incomeModel);
    }
}