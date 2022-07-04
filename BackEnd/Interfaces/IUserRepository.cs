using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Interfaces
{
    public interface IUserRepository : IDisposable
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUserByID(int userId);
        void InsertUser(User user);
        void DeleteUser(User user);
        void UpdateUser(User user);
        Task Save();
        bool UserExists(User user);

    }
}
