using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.IncomeCategory
{
    public class IncomeCategoryDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = String.Empty;
    }
}