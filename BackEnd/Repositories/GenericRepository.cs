using BackEnd.Data;
using BackEnd.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly LRS_DBContext _context;
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

        public async Task<IEnumerable<T>> GetAll()
        {
            //IEnumerable<T> entities = await _context.Set<T>().ToListAsync();
            //return entities;
            return await _context.Set<T>().ToListAsync();
        }
       
        public async Task Create(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        
    }
}
