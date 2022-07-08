using BackEnd.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using BackEnd.Data;

namespace BackEnd.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(LRS_DBContext dbContext) : base(dbContext)
        {
            

        }
        public bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        public async Task<IEnumerable<User>> GetAllActive()
        {
            return await _context.Users.Where(u=>u.IsActive==true).ToListAsync();
        }

    }
}
