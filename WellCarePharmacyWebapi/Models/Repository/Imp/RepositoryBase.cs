using Microsoft.EntityFrameworkCore;
using WellCarePharmacyWebapi.Models.Context;
using WellCarePharmacyWebapi.Models.Repository.Interfaces;

namespace WellCarePharmacyWebapi.Models.Repository.Imp
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {

        private readonly WellCareDC _context;

        private readonly DbSet<T> dbset;

        public RepositoryBase(WellCareDC context)
        {
            _context = context;
            dbset= _context.Set<T>();
        }
        public async Task<T> Create(T entity)
        {
            var entitydata= await _context.Set<T>().AddAsync(entity);
           await  _context.SaveChangesAsync();
            return  entitydata.Entity;
        }

        public async Task<T> Delete(int id)
        {
            var entity= await dbset.FindAsync(id);
            dbset.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;

        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync(); 
        }

        public async Task<T> GetById(int id)
        {
            return await dbset.FindAsync(id);

        }

        public async Task Update(T entity)
        {
            
            dbset.Update(entity);
           await _context.SaveChangesAsync();
        }
    }
}
