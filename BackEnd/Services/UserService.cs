using BackEnd.Data;
using BackEnd.Interfaces;
using BackEnd.Repositories;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Services
{
    public class UserService : IUserService
    {
        //private readonly ModelStateDictionary _modelState;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //protected bool ValidateUser(User userToValidate)
        //{
        //    //if (userToValidate.Name.Trim().Length == 0)
        //    //    _modelState.AddModelError("Name", "Name is required.");
        //    //if (userToValidate.UserTypeId < 0)
        //    //    _modelState.AddModelError("UserType", "User Type is invalid.");
        //    //if (userToValidate.UserTitleId < 0)
        //    //    _modelState.AddModelError("UserTitle", "User Title is invalid.");
        //    return _modelState.IsValid;
        //}

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _unitOfWork.Users.GetAll();
        }

        public async Task<IEnumerable<User>> GetActiveUsers()
        {
            return await _unitOfWork.Users.GetAllActive();
        }
        public async Task<User> GetUser(int id)
        {
            User user = await _unitOfWork.Users.GetById(id);
            return user;
        }


        public async Task<bool> CreateUser(User userToCreate)
        {
            // Validation logic
            //if (!ValidateUser(userToCreate))
            //    return false;

            // Database logic
            try
            {
                await _unitOfWork.Users.Create(userToCreate);
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
            //if (!ValidateUser(userToUpdate))
            //    return false;

            if (id != userToUpdate.Id)
            {
                return false;
            }
            // Database logic
            try
            {
                await _unitOfWork.Users.Update(userToUpdate);
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
                await _unitOfWork.Users.Delete(userToDelete);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public async Task<IEnumerable<UserTitle>> GetTitles()
        {
            return await _unitOfWork.Titles.GetAll();
        }

       
        public async Task<UserTitle> GetTitle(int id)
        {
            return await _unitOfWork.Titles.GetById(id);
        }

        public async Task<IEnumerable<UserType>> GetTypes()
        {
            return await _unitOfWork.Types.GetAll();
        }

       
        public async Task<UserType> GetType(int id)
        {
            return await _unitOfWork.Types.GetById(id);
        }
    }

}

