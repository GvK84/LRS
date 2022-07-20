using System.Threading.Tasks;
using BackEnd.Data;

namespace BackEnd.Interfaces
{
    public interface ITypeRepository : IGenericRepository<UserType>
    {
        Task<int> GetMaxId();
    }
}