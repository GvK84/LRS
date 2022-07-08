using BackEnd.Interfaces;
using BackEnd.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BackEnd.Repositories
{
    public class TitleRepository : GenericRepository<UserTitle>, ITitleRepository
    {
        /// <summary>Initializes a new instance of the <see cref="TitleRepository" /> class.</summary>
        /// <param name="dbContext">The database context.</param>
        public TitleRepository(LRS_DBContext dbContext) : base(dbContext)
        {


        }

        /// <summary>Gets the maximum identifier of the User Titles.</summary>
        /// <returns>The maximum identifier</returns>
        public async Task<int> GetMaxId()
        {
            return await _context.UserTitles.MaxAsync(t => t.Id);
        } 
    }
}
