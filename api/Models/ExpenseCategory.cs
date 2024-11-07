using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    [Table("ExpenseCategories")]
    public class ExpenseCategory
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = String.Empty;
        public List<Expense> Expenses { get; set; } = new List<Expense>();
    }
}