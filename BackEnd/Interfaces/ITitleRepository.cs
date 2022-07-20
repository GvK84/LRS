using System.Threading.Tasks;
using BackEnd.Data;

namespace BackEnd.Interfaces
{
    public interface ITitleRepository : IGenericRepository<UserTitle>
    {
        Task<int> GetMaxId();
    }
}