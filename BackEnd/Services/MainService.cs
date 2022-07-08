using BackEnd.Data;
using BackEnd.Interfaces;
using BackEnd.Repositories;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Services
{
    public class MainService : IMainService
    {
        //private readonly ModelStateDictionary _modelState;
        private readonly IUserRepository Users;
        private readonly ITitleRepository Titles;
        private readonly ITypeRepository Types;

        //private readonly LRS_DBContext _context;

        /// <summary>Initializes a new instance of the <see cref="MainService" /> class.</summary>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="titleRepository">The title repository.</param>
        /// <param name="typeRepository">The type repository.</param>
        public MainService(IUserRepository userRepository, ITitleRepository titleRepository, ITypeRepository typeRepository)
        {
            Users = userRepository;
            Titles = titleRepository;
            Types = typeRepository;
        }


        /// <summary>Validates the user.</summary>
        /// <param name="userToValidate">The user to validate.</param>
        /// <returns>True or false</returns>
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

        /// <summary>Gets the users.</summary>
        /// <returns>The users</returns>
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await Users.GetAll();
        }

        /// <summary>Gets the active users.</summary>
        /// <returns>The active users</returns>
        public async Task<IEnumerable<User>> GetActiveUsers()
        {
            return await Users.GetAllActive();
        }

        /// <summary>Gets the user by its identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The user</returns>
        public async Task<User> GetUser(int id)
        {
            User user = await Users.GetById(id);
            return user;
        }

        /// <summary>Creates the user.</summary>
        /// <param name="userToCreate">The user to create.</param>
        /// <returns>True or false</returns>
        public async Task<bool> CreateUser(User userToCreate)
        {
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

        /// <summary>Updates the user.</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="userToUpdate">The user to update.</param>
        /// <returns>True or false</returns>
        public async Task<bool> UpdateUser(int id, User userToUpdate)
        {
            if (id != userToUpdate.Id)
            {
                return false;
            }
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

        /// <summary>Deletes the user.</summary>
        /// <param name="id">The identifier of the user.</param>
        /// <returns>True or false</returns>
        public async Task<bool> DeleteUser(int id)
        {
            var userToDelete = GetUser(id).Result;

            if (userToDelete == null)
            {
                return false;
            }
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

        /// <summary>Gets the titles.</summary>
        /// <returns>The titles</returns>
        public async Task<IEnumerable<UserTitle>> GetTitles()
        {
            return await Titles.GetAll();
        }

        /// <summary>Gets the title by its identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The title</returns>
        public async Task<UserTitle> GetTitle(int id)
        {
            return await Titles.GetById(id);
        }

        /// <summary>Gets the types.</summary>
        /// <returns>The types</returns>
        public async Task<IEnumerable<UserType>> GetTypes()
        {
            return await Types.GetAll();
        }

        /// <summary>Gets the type by its identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The type</returns>
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

