using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Interfaces
{
    public interface ITitleRepository
    {
        Task<IEnumerable<UserTitle>> GetTitles();
        Task<UserTitle> GetTitleByID(int userTitleId);

    }
}
