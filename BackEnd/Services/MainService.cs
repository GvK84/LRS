using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BackEnd.Data;
using BackEnd.Interfaces;

namespace BackEnd.Services
{
    // TODO different service per entity
    public class MainService : IMainService
    {
        private readonly IUserRepository _users;
        private readonly ITitleRepository _titles;
        private readonly ITypeRepository _types;

        /// <summary>Initializes a new instance of the <see cref="MainService" /> class.</summary>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="titleRepository">The title repository.</param>
        /// <param name="typeRepository">The type repository.</param>
        public MainService(IUserRepository userRepository,
            ITitleRepository titleRepository,
            ITypeRepository typeRepository)
        {
            _users = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _titles = titleRepository ?? throw new ArgumentNullException(nameof(titleRepository));
            _types = typeRepository ?? throw new ArgumentNullException(nameof(typeRepository));
        }


        /// <summary>Validates the user.</summary>
        /// <param name="userToValidate">The user to validate.</param>
        /// <returns>True or false</returns>
        public async Task<bool> ValidateUser(User userToValidate)
        {
            // TODO naming conventions
            var maxTitleId = await _titles.GetMaxId();
            var maxTypeId = await _types.GetMaxId();

            // TODO always use brackets
            if (string.IsNullOrEmpty(userToValidate.Name))
            {
                return false;
            }

            if (userToValidate.UserTypeId > maxTypeId ||
                userToValidate.UserTypeId < 1)
            {
                return false;
            }

            if (userToValidate.UserTitleId > maxTitleId ||
                userToValidate.UserTitleId < 1)
            {
                return false;
            }

            return true;
        }

        /// <summary>Gets the users.</summary>
        /// <returns>The users</returns>
        /// TODO use async suffix for async methods
        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _users.GetAll();
        }

        /// <summary>Gets the active users.</summary>
        /// <returns>The active users</returns>
        public async Task<IEnumerable<User>> GetActiveUsers()
        {
            return await _users.GetAllActive();
        }

        /// <summary>Gets the user by its identifier.</summary>
        /// <param name="id">The user identifier.</param>
        /// <returns>The user</returns>
        public async Task<User> GetUser(int id)
        {
            var user = await _users.GetById(id);
            return user;
        }

        /// <summary>Creates the user.</summary>
        /// <param name="userToCreate">The user to create.</param>
        /// <returns>True or false</returns>
        /// TODO usually after creation you either return to list or view of created
        public async Task<bool> CreateUser(User userToCreate)
        {
            if (userToCreate == null)
            {
                throw new ArgumentNullException(nameof(userToCreate));
            }

            // TODO argument checks
            try
            {
                await _users.Create(userToCreate);
            }
            catch
            {
                // TODO we do not want to catch any exception at any time and handle everything the same way
                // TODO why do you do this?
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
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            if (userToUpdate == null)
            {
                throw new ArgumentNullException(nameof(userToUpdate));
            }

            if (id != userToUpdate.Id)
            {
                return false;
            }

            try
            {
                // TODO this is a major fault you need to first fetch the user from the database and apply changes there.
                await _users.Update(userToUpdate);
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
                // TODO Delete a user (by setting IsActive field to false)
                await _users.Delete(userToDelete);
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
            return await _titles.GetAll();
        }

        /// <summary>Gets the title by its identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The title</returns>
        public async Task<UserTitle> GetTitle(int id)
        {
            return await _titles.GetById(id);
        }

        /// <summary>Gets the types.</summary>
        /// <returns>The types</returns>
        public async Task<IEnumerable<UserType>> GetTypes()
        {
            return await _types.GetAll();
        }

        /// <summary>Gets the type by its identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The type</returns>
        public async Task<UserType> GetType(int id)
        {
            return await _types.GetById(id);
        }

        // TODO we don't leave comment out code unless there is an important reason
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