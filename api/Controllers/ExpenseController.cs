using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Extensions;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using api.Mappers;
using api.Dtos.Expense;

namespace api.Controllers
{
    [Route("api/expense")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly UserManager<AppUser> _userManager;

        public ExpenseController(UserManager<AppUser> userManager, IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetExpenses()
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return NotFound();
            }

            var expenses = await _expenseRepository.GetUserExpensesAsync(user);

            return Ok(expenses);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateExpense([FromBody] CreateExpenseDto expense)
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return NotFound();
            }

            var expenseModel = new Expense
            {
                Month = expense.Month,
                Year = expense.Year,
                Amount = expense.Amount,
                CategoryId = expense.CategoryId,
                TypeId = expense.TypeId,
                AppUserId = user.Id
            };

            var newExpense = await _expenseRepository.CreateAsync(expenseModel);

            if (newExpense == null)
            {
                return BadRequest();
            }
            
            return Ok(newExpense.ToExpenseDto());
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> updateExpense([FromRoute] int id, [FromBody] UpdateExpenseDto expense)
        {
            var updatedExpense = await _expenseRepository.UpdateAsync(id, expense);

            if (updatedExpense == null)
            {
                return BadRequest();
            }

            return Ok(updatedExpense.ToExpenseDto());
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteExpense([FromRoute] int id)
        {
            var deletedExpense = await _expenseRepository.DeleteAsync(id);

            if (deletedExpense == null)
            {
                return BadRequest();
            }

            return Ok(deletedExpense.ToExpenseDto());
        }
    }
}