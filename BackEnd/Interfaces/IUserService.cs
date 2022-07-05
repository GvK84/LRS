using BackEnd.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsers();

        Task<User> GetUserByID(int userId);


        bool UserExists(User user);


        void InsertUser(User user);


        void DeleteUser(User user);


        void UpdateUser(User user);


        Task Save();

        void Dispose();
       

    }
}
