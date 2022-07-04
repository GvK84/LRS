using BackEnd.Interfaces;
using BackEnd.Models;
using BackEnd.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Services
{
    public class UserService : IUserService
    {
        private readonly UserRepository _repository;

        public UserService(UserRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<User>> GetUsers() => await _repository.GetUsers();

        public async Task<User> GetUserByID(int userId) => await _repository.GetUserByID(userId);

        public void InsertUser(User user) => _repository.InsertUser(user);

        public void UpdateUser(User user) => _repository.UpdateUser(user);

        public void DeleteUser(User user) => _repository.DeleteUser(user);

        public async Task Save() => await _repository.Save();

        public bool UserExists(User user) => _repository.UserExists(user);
        
        public void Dispose() => _repository.Dispose();
        

        

       

        

        

        

       
    }
}
