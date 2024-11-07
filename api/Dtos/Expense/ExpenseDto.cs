using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.ExpenseCategory;
using api.Dtos.Type;

namespace api.Dtos.Expense
{
    public class ExpenseDto
    {
        public int Id { get; set; }
        public string Month { get; set; } = String.Empty;
        public string Year { get; set; } = String.Empty;
        public ExpenseCategoryDto Category { get; set; } = new ExpenseCategoryDto();
        public double Amount { get; set; }
        public TypeDto Type { get; set; } = new TypeDto();
        public string CreatedBy { get; set; } = String.Empty;
    }
}