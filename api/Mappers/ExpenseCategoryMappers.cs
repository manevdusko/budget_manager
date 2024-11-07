using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.ExpenseCategory;
using api.Models;

namespace api.Mappers
{
    public static class ExpenseCategoryMappers
    {
        public static ExpenseCategoryDto ToExpenseCategoryDto(this ExpenseCategory expenseCategoryModel)
        {
            return new ExpenseCategoryDto
            {
                Id = expenseCategoryModel.Id,
                CategoryName = expenseCategoryModel.CategoryName
            };
        }
    }
}