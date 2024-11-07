using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.IncomeCategory;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/income-category")]
    [ApiController]
    public class IncomeCategoryController : ControllerBase
    {
        private readonly IIncomeCategoryRepository _incomeCategoryRepository;

        public IncomeCategoryController(IIncomeCategoryRepository incomeCategoryRepository)
        {
            _incomeCategoryRepository = incomeCategoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetIncomeCategory()
        {
            var categories = await _incomeCategoryRepository.GetIncomeCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetIncomeCategoryById(int id)
        {
            var category = await _incomeCategoryRepository.GetIncomeCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateIncomeCategory([FromBody] CreateIncomeCategoryDto incomeCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdCategory = await _incomeCategoryRepository.CreateIncomeCategoryAsync(incomeCategory);
            return CreatedAtAction(nameof(GetIncomeCategoryById), new { id = createdCategory.Id }, createdCategory);
        }

       
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteIncomeCategory(int id)
        {
            var category = await _incomeCategoryRepository.GetIncomeCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            await _incomeCategoryRepository.DeleteIncomeCategoryAsync(category);
            return NoContent();
        }
    }
}