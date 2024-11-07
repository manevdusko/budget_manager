using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    [Table("Types")]
    public class Type
    {
        public int Id { get; set; }
        public string TypeName { get; set; } = String.Empty;
        public List<Expense> Expenses { get; set; }
        public List<Income> Incomes { get; set; }
    }
}