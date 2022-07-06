using BackEnd.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsers();
        Task<IEnumerable<User>> GetActiveUsers();
        Task<User> GetUser(int id);
        Task<bool> CreateUser(User userToCreate);
        Task<bool> UpdateUser(int id, User userToUpdate);
        Task<bool> DeleteUser(int id);
        Task<IEnumerable<UserTitle>> GetTitles();
        Task<UserTitle> GetTitle(int id);
        Task<IEnumerable<UserType>> GetTypes();
        Task<UserType> GetType(int id);
        
    }
}
