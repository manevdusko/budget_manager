using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Type = api.Models.Type;

namespace api.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<IncomeCategory> IncomeCategories { get; set; }
        public DbSet<Type> Types { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>()
            .HasMany(u => u.Expenses)
            .WithOne(e => e.AppUser)
            .HasForeignKey(e => e.AppUserId);

            builder.Entity<AppUser>()
               .HasMany(u => u.Incomes)
               .WithOne(i => i.AppUser)
               .HasForeignKey(i => i.AppUserId);

            builder.Entity<IncomeCategory>()
                .HasMany(c => c.Incomes)
                .WithOne(e => e.Category)
                .HasForeignKey(e => e.CategoryId);

            builder.Entity<ExpenseCategory>()
                .HasMany(c => c.Expenses)
                .WithOne(e => e.Category)
                .HasForeignKey(e => e.CategoryId);

            builder.Entity<Type>()
                .HasMany(t => t.Expenses)
                .WithOne(e => e.Type)
                .HasForeignKey(e => e.TypeId);

            builder.Entity<Type>()
                .HasMany(t => t.Incomes)
                .WithOne(i => i.Type)
                .HasForeignKey(i => i.TypeId);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole { Name = "User", NormalizedName = "USER" },
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}