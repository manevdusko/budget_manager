using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.ExpenseCategory
{
    public class ExpenseCategoryDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = String.Empty;
    }
}