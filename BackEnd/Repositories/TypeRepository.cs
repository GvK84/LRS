using BackEnd.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BackEnd.Data;

namespace BackEnd.Repositories
{
    public class TypeRepository : GenericRepository<UserType>, ITypeRepository
    {
        public TypeRepository(LRS_DBContext dbContext) : base(dbContext)
        {


        }

    }
}
