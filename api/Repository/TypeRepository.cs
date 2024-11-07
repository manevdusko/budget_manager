using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Type;
using api.Interfaces;
using api.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace api.Repository
{
    public class TypeRepository : ITypeRepository
    {
        private readonly ApplicationDBContext _context;

        public TypeRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<TypeDto> CreateTypeAsync(CreateTypeDto type)
        {
            var typeModel = new Models.Type
            {
                TypeName = type.TypeName,
            };
            await _context.Types.AddAsync(typeModel);
            await _context.SaveChangesAsync();

            var createdType = await _context.Types
                .FirstOrDefaultAsync(e => e.Id == typeModel.Id);
            return createdType.ToTypeDto();
        }

        public async Task<TypeDto> DeleteTypeAsync(Models.Type type)
        {
            _context.Types.Remove(type);
            await _context.SaveChangesAsync();
            return type.ToTypeDto();
        }

        public async Task<List<TypeDto>> GetAllTypesAsync()
        {
            return await _context.Types.Select(e => e.ToTypeDto()).ToListAsync();
        }

        public async Task<Models.Type> GetTypeByIdAsync(int id)
        {
            return await _context.Types.FindAsync(id);
        }
    }
}