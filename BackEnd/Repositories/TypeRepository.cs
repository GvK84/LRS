using System.Threading.Tasks;
using BackEnd.Data;
using BackEnd.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Repositories
{
    public class TypeRepository : GenericRepository<UserType>, ITypeRepository
    {
        /// <summary>Initializes a new instance of the <see cref="TypeRepository" /> class.</summary>
        /// <param name="dbContext">The database context.</param>
        public TypeRepository(LRS_DBContext dbContext) : base(dbContext)
        {
        }

        /// <summary>Gets the maximum identifier of the User Types.</summary>
        /// <returns>The max identifier</returns>
        public async Task<int> GetMaxId()
        {
            return await _context.UserTypes.MaxAsync(t => t.Id);
        }
    }
}