using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.IncomeCategory;
using api.Dtos.Type;

namespace api.Dtos.Income
{
    public class IncomeDto
    {
        public int Id { get; set; }
        public string Month { get; set; } = String.Empty;
        public string Year { get; set; } = String.Empty;
        public IncomeCategoryDto Category { get; set; }
        public double Amount { get; set; }
        public TypeDto Type { get; set; }
        public string CreatedBy { get; set; }
    }
}