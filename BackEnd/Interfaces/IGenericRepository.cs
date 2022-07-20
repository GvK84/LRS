using System.Collections.Generic;
using System.Threading.Tasks;

// TODO remove unused imports
namespace BackEnd.Interfaces
{
    // TODO folder hierarchy by type
    public interface IGenericRepository<T> where T : class
    {
        // TODO xml comments on interfaces
        /// <summary>Gets the Entity (User, UserTitle, UserType) by its identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The Entity</returns>
        Task<T> GetById(int id);

        // TODO single space between methods
        Task<IEnumerable<T>> GetAll();

        Task Create(T entity);

        Task Update(T entity);

        Task Delete(T entity);
    }
}