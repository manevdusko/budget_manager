using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    [Table("Expenses")]
    public class Expense
    {
        public int Id { get; set; }
        public string Month { get; set; } = String.Empty;
        public string Year { get; set; } = String.Empty;
        public int CategoryId { get; set; }
        public ExpenseCategory Category { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public double Amount { get; set; }
        public int TypeId { get; set; }
        public Type Type { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        
    }
}