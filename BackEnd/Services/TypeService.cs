using BackEnd.Interfaces;
using BackEnd.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Services
{
    public class TypeService : ITypeService
    {
        private readonly ITypeRepository _repository;

        public TypeService(ITypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<UserType>> GetTypes() => await _repository.GetTypes();

        public async Task<UserType> GetTypeByID(int userTypeId) => await _repository.GetTypeByID(userTypeId);
    }
}
