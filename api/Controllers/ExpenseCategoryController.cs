using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.ExpenseCategory;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/expense-category")]
    [ApiController]
    public class ExpenseCategoryController : ControllerBase
    {
        private readonly IExpenseCategoryRepository _expenseCategoryRepository;

        public ExpenseCategoryController(IExpenseCategoryRepository expenseCategoryRepository)
        {
            _expenseCategoryRepository = expenseCategoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetExpenseCategories()
        {
            var categories = await _expenseCategoryRepository.GetExpenseCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExpenseCategoryById(int id)
        {
            var category = await _expenseCategoryRepository.GetExpenseCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateExpenseCategory([FromBody] CreateExpenseCategoryDto expenseCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdCategory = await _expenseCategoryRepository.CreateExpenseCategoryAsync(expenseCategory);
            return CreatedAtAction(nameof(GetExpenseCategoryById), new { id = createdCategory.Id }, createdCategory);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteExpenseCategory(int id)
        {
            var category = await _expenseCategoryRepository.GetExpenseCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            await _expenseCategoryRepository.DeleteExpenseCategoryAsync(category);
            return NoContent();
        }
    }
}