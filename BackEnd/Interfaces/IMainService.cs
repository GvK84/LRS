using System.Collections.Generic;
using System.Threading.Tasks;
using BackEnd.Data;

namespace BackEnd.Interfaces
{
    public interface IMainService
    {
        Task<bool> ValidateUser(User user);

        // TODO it's preferable if you had a method to filter out users on backend
        Task<IEnumerable<User>> GetUsersAsync();

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