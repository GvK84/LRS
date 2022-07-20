using System.Collections.Generic;
using System.Threading.Tasks;
using BackEnd.Data;
using BackEnd.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly LRS_DBContext _context;

        /// <summary>Initializes a new instance of the <see cref="GenericRepository{T}" /> class.</summary>
        /// <param name="context">The database context.</param>
        public GenericRepository(LRS_DBContext context)
        {
            _context = context;
        }

        public async Task<T> GetById (int id)
        {
            //T entity =  await _context.Set<T>().FindAsync(id);
            //return entity;
            return await _context.Set<T>().FindAsync(id);
        }

        /// <summary>Gets all elements of an Entity (User, UserTitle, UserType).</summary>
        /// <returns>All elements of the Entity</returns>
        public async Task<IEnumerable<T>> GetAll()
        {
            //IEnumerable<T> entities = await _context.Set<T>().ToListAsync();
            //return entities;
            return await _context.Set<T>().ToListAsync();
        }

        /// <summary>Creates the specified entity (User, UserTitle, UserType).</summary>
        /// <param name="entity">The entity.</param>
        public async Task Create(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>Updates the specified entity (User, UserTitle, UserType).</summary>
        /// <param name="entity">The entity.</param>
        public async Task Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        /// <summary>Deletes the specified entity (User, UserTitle, UserType).</summary>
        /// <param name="entity">The entity.</param>
        public async Task Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }


    }
}