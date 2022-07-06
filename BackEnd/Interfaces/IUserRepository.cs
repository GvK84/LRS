using BackEnd.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace BackEnd.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        bool EntityExists(int id);
        Task<IEnumerable<User>> GetAllActive();

    }
}
