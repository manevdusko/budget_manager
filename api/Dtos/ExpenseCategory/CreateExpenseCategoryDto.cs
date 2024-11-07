using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.ExpenseCategory
{
    public class CreateExpenseCategoryDto
    {
        public string CategoryName { get; set; } = String.Empty;
    }
}