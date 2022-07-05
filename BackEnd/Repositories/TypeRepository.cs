using BackEnd.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BackEnd.Data;

namespace BackEnd.Repositories
{
    public class TypeRepository: ITypeRepository
    {
        private LRS_DBContext context;

        public TypeRepository(LRS_DBContext dbContext)
        {
            context = dbContext;
        }

        public async Task<IEnumerable<UserType>> GetTypes()
        {
            return await context.UserTypes.ToListAsync();
        }

        public async Task<UserType> GetTypeByID(int userTypeId)
        {
            return await context.UserTypes.FindAsync(userTypeId);
        }
    }
}
