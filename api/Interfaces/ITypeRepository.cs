using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Type;

namespace api.Interfaces
{
    public interface ITypeRepository
    {
        public Task<List<TypeDto>> GetAllTypesAsync();
        public Task<TypeDto> CreateTypeAsync(CreateTypeDto type);
        public Task<TypeDto> DeleteTypeAsync(Models.Type type);
        public Task<Models.Type> GetTypeByIdAsync(int id);
    }
}