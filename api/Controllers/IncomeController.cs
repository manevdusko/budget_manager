using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Income;
using api.Extensions;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/income")]
    [ApiController]
    public class IncomeController : ControllerBase
    {
        private readonly IIncomeRepository _incomeRepository;
        private readonly UserManager<AppUser> _userManager;

        public IncomeController(UserManager<AppUser> userManager, IIncomeRepository incomeRepository)
        {
            _incomeRepository = incomeRepository;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetIncomes()
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return NotFound();
            }

            var incomes = await _incomeRepository.GetUserIncomesAsync(user);

            return Ok(incomes);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateIncome([FromBody] CreateIncomeDto income)
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return NotFound();
            }

            var incomeModel = new Income
            {
                Month = income.Month,
                Year = income.Year,
                Amount = income.Amount,
                CategoryId = income.CategoryId,
                TypeId = income.TypeId,
                AppUserId = user.Id
            };

            var newIncome = await _incomeRepository.CreateAsync(incomeModel);

            if (newIncome == null)
            {
                return BadRequest();
            }

            return Ok(newIncome);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateIncome([FromRoute] int id, [FromBody] UpdateIncomeDto income)
        {
            var updatedIncome = await _incomeRepository.UpdateAsync(id, income);

            if (updatedIncome == null)
            {
                return BadRequest();
            }

            return Ok(updatedIncome.ToIncomeDto());
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteIncome([FromRoute] int id)
        {
            var income = await _incomeRepository.DeleteAsync(id);
            if (income == null)
            {
                return NotFound();
            }

            return Ok(income.ToIncomeDto());
        }
    }
}