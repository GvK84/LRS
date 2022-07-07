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
        public TitleRepository(LRS_DBContext dbContext) : base(dbContext)
        {


        }

        public async Task<int> GetMaxId()
        {
            return await _context.UserTitles.MaxAsync(t => t.Id);
        } 
    }
}
