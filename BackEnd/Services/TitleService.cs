using BackEnd.Interfaces;
using BackEnd.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Services
{
    public class TitleService : ITitleService
    {
        private readonly ITitleRepository _repository;

        public TitleService(ITitleRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<UserTitle>> GetTitles() => await _repository.GetTitles();

        public async Task<UserTitle> GetTitleByID(int userTitleId) => await _repository.GetTitleByID(userTitleId);
    }
}
