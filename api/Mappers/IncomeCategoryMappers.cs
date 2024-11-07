using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.IncomeCategory;
using api.Models;

namespace api.Mappers
{
    public static class IncomeCategoryMappers
    {
        public static IncomeCategoryDto ToIncomeCategoryDto(this IncomeCategory incomeCategoryModel)
        {
            return new IncomeCategoryDto
            {
                Id = incomeCategoryModel.Id,
                CategoryName = incomeCategoryModel.CategoryName
            };
        }
    }
}