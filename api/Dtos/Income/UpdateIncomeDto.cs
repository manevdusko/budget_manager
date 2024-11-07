using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Income
{
    public class UpdateIncomeDto
    {
        public string Month { get; set; } = String.Empty;
        public string Year { get; set; } = String.Empty;
        public double Amount { get; set; }
    }
}