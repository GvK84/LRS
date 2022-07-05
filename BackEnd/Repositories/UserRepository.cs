using BackEnd.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using BackEnd.Data;

namespace BackEnd.Repositories
{
    public class UserRepository : IUserRepository, IDisposable
    {
        private LRS_DBContext context;

        public UserRepository(LRS_DBContext dbContext)
        {
            context = dbContext;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await context.Users.ToListAsync();
        }

        public async Task<User> GetUserByID(int userId)
        {
            return await context.Users.FindAsync(userId);
        }

        public bool UserExists(User user)
        {
            return context.Users.Any(e => e.Id == user.Id);
        }

        public void InsertUser(User user)
        {
            context.Users.Add(user); 

        }

        public void DeleteUser(User user)
        {
            context.Users.Remove(user);         }

        public void UpdateUser(User user)
        {
            context.Entry(user).State = EntityState.Modified;
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


    }
}
