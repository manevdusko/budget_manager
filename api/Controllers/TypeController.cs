using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Type;
using api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/type")]
    [ApiController]
    public class TypeController : ControllerBase
    {
        private readonly ITypeRepository _typeRepository;

        public TypeController(ITypeRepository typeRepository)
        {
            _typeRepository = typeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetTypes()
        {
            var types = await _typeRepository.GetAllTypesAsync();
            return Ok(types);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTypeById(int id)
        {
            var type = await _typeRepository.GetTypeByIdAsync(id);
            if (type == null)
            {
                return NotFound();
            }
            return Ok(type);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateType([FromBody] CreateTypeDto type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdType = await _typeRepository.CreateTypeAsync(type);
            return CreatedAtAction(nameof(GetTypeById), new { id = createdType.Id }, createdType);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteType(int id)
        {
            var type = await _typeRepository.GetTypeByIdAsync(id);
            if (type == null)
            {
                return NotFound();
            }

            await _typeRepository.DeleteTypeAsync(type);
            return NoContent();
        }
    }
}