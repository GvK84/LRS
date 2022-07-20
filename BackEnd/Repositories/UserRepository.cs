using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Data;
using BackEnd.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        /// <summary>Initializes a new instance of the <see cref="UserRepository" /> class.</summary>
        /// <param name="dbContext">The database context.</param>
        public UserRepository(LRS_DBContext dbContext) : base(dbContext)
        {
        }

        /// <summary>Check if the User exists.</summary>
        /// <param name="id">The identifier of the User</param>
        /// <returns>True or false</returns>
        public bool UserExists(int id)
        {
            return _context.Users
                .Any(e => e.Id == id);
        }

        /// <summary>Gets all active users.</summary>
        /// <returns>The active users</returns>
        public async Task<IEnumerable<User>> GetAllActive()
        {
            return await _context.Users
                .Where(u => u.IsActive == true)
                .ToListAsync();
        }
    }
}