using BackEnd.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Interfaces
{
    public interface ITitleService
    {
        Task<IEnumerable<UserTitle>> GetTitles();

        Task<UserTitle> GetTitleByID(int usertitleId);
    }
}
