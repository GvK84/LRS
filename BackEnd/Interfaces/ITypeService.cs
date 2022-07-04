using BackEnd.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Interfaces
{
    public interface ITypeService
    {
        Task<IEnumerable<UserType>> GetTypes();

        Task<UserType> GetTypeByID(int usertypeId);
    }
}
