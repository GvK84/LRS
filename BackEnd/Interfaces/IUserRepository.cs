using System.Collections.Generic;
using System.Threading.Tasks;
using BackEnd.Data;

namespace BackEnd.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        // TODO not used?
        bool UserExists(int id);

        Task<IEnumerable<User>> GetAllActive();
    }
}