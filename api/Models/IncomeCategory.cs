using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    [Table("IncomeCategories")]
    public class IncomeCategory
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = String.Empty;
        public List<Income> Incomes { get; set; } = new List<Income>();
    }
}