using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Income;
using api.Models;

namespace api.Mappers
{
    public static class IncomeMappers
    {
        public static IncomeDto ToIncomeDto(this Income income)
        {
            return new IncomeDto
            {
                Id = income.Id,
                Month = income.Month,
                Year = income.Year,
                Category = income.Category?.ToIncomeCategoryDto(),
                Amount = income.Amount,
                Type = income.Type?.ToTypeDto(),
                CreatedBy = income?.AppUser?.UserName,
            };
        }
    }
}