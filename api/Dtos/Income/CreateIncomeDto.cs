using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Income
{
    public class CreateIncomeDto
    {
        public string Month { get; set; } = String.Empty;
        public string Year { get; set; } = String.Empty;
        public int CategoryId { get; set; }
        public double Amount { get; set; }
        public int TypeId { get; set; }
    }
}