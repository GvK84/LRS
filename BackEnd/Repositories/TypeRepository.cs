using BackEnd.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BackEnd.Data;
using System.Linq;

namespace BackEnd.Repositories
{
    public class TypeRepository : GenericRepository<UserType>, ITypeRepository
    {
        public TypeRepository(LRS_DBContext dbContext) : base(dbContext)
        {


        }
        public async Task<int> GetMaxId()
        {
            return await _context.UserTypes.MaxAsync(t => t.Id);
        }
    }
}
