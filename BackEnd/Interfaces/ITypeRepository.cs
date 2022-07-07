using BackEnd.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Interfaces
{
    public interface ITypeRepository: IGenericRepository<UserType>
    {
        Task<int> GetMaxId();
    }
}
