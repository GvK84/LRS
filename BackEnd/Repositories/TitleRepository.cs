using BackEnd.Interfaces;
using BackEnd.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace BackEnd.Repositories
{
    public class TitleRepository : ITitleRepository
    {
        private LRS_DBContext context;

        public TitleRepository(LRS_DBContext dbContext)
        {
            context = dbContext;
        }

        public async Task<IEnumerable<UserTitle>> GetTitles()
        {
            return await context.UserTitles.ToListAsync();
        }

        public async Task<UserTitle> GetTitleByID(int userTitleId)
        {
            return await context.UserTitles.FindAsync(userTitleId);
        }
    }
}
