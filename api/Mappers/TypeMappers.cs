using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Type;
using Type = api.Models.Type;

namespace api.Mappers
{
    public static class TypeMappers
    {
        public static TypeDto ToTypeDto(this Type expenseModel)
        {
            return new TypeDto
            {
                Id = expenseModel.Id,
                TypeName = expenseModel.TypeName,
            };
        }
    }
}