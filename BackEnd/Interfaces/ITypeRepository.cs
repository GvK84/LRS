using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Interfaces
{
    public interface ITypeRepository
    {
        Task<IEnumerable<UserType>> GetTypes();
        Task<UserType> GetTypeByID(int userTypeId);
    }
}
