using BackEnd.Data;
using BackEnd.Interfaces;
using BackEnd.Repositories;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Services
{
    public class UserService : IUserService
    {
        //private readonly ModelStateDictionary _modelState;
        private readonly IUserRepository Users;
        private readonly ITitleRepository Titles;
        private readonly ITypeRepository Types;
        
        //private readonly LRS_DBContext _context;

        public UserService(IUserRepository userRepository, ITitleRepository titleRepository, ITypeRepository typeRepository)
        {
            //_context = context;
            Users = userRepository;
            Titles = titleRepository;
            Types = typeRepository;
        }

        //public UserService()
        //{
        //    _context = new LRS_DBContext();
        //    Users = new UserRepository(_context);
        //    Titles = new TitleRepository(_context);
        //    Types = new TypeRepository(_context);
        //}

        //public IUserRepository Users { get; private set; }
        //public ITitleRepository Titles { get; private set; }
        //public ITypeRepository Types { get; private set; }

        public async Task<bool> ValidateUser(User userToValidate)
        {
            var titlemax = await Titles.GetMaxId();
            var typemax = await Types.GetMaxId();
            if (string.IsNullOrEmpty(userToValidate.Name))
                return false;
            if (userToValidate.UserTypeId > typemax || userToValidate.UserTypeId<1)
                return false;
            if (userToValidate.UserTitleId > titlemax || userToValidate.UserTitleId < 1)
                return false;
            return true;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await Users.GetAll();
        }

        public async Task<IEnumerable<User>> GetActiveUsers()
        {
            return await Users.GetAllActive();
        }

        public async Task<User> GetUser(int id)
        {
            User user = await Users.GetById(id);
            return user;
        }

        public async Task<bool> CreateUser(User userToCreate)
        {
            // Validation logic
            if (!ValidateUser(userToCreate).Result)
                return false;

            // Database logic
            try
            {
                await Users.Create(userToCreate);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public async Task<bool> UpdateUser(int id, User userToUpdate)
        {
            // Validation logic
            if (!ValidateUser(userToUpdate).Result)
                return false;

            if (id != userToUpdate.Id)
            {
                return false;
            }
            // Database logic
            try
            {
                await Users.Update(userToUpdate);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var userToDelete = GetUser(id).Result;
            // Validation logic

            if (userToDelete == null)
            {
                return false;
            }
            // Database logic
            try
            {
                await Users.Delete(userToDelete);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public async Task<IEnumerable<UserTitle>> GetTitles()
        {
            return await Titles.GetAll();
        }

        public async Task<UserTitle> GetTitle(int id)
        {
            return await Titles.GetById(id);
        }

        public async Task<IEnumerable<UserType>> GetTypes()
        {
            return await Types.GetAll();
        }

        public async Task<UserType> GetType(int id)
        {
            return await Types.GetById(id);
        }

        //public int Complete()
        //{
        //    return _context.SaveChanges();
        //}
        //public void Dispose()
        //{
        //    _context.Dispose();

        //}
    }
}

